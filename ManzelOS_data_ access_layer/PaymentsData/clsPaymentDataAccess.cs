using System;
using System.ClientModel.Primitives;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Data.SqlClient;

namespace ManzelOS_data_access_layer.PaymentsData
{

    public class PaymentDTO
    {

        public int PaymentId { get; set; }
        public decimal PaymentAmount { get; set; }
        public int PaidByTenantId { get; set; }
        public int GeneratedBillId { get; set; }
        public int ReceivedByOwnerId { get; set; }
        public DateTime PaymentDate { get; set; }
        public short PaymentMethodId { get; set; }
        public short PaymentStatusId { get; set; }
        public DateTime CreatedAt { get; set; }
        
        public PaymentDTO(int paymentId, decimal paymentAmount, int paidByTenantId, int generatedBillId, int receivedByOwnerId,
            DateTime paymentDate, short paymentMethodId, short paymentStatusId, DateTime createdAt)
        {

            PaymentId = paymentId;
            PaymentAmount = paymentAmount;
            PaidByTenantId = paidByTenantId;
            GeneratedBillId = generatedBillId;
            ReceivedByOwnerId = receivedByOwnerId;
            PaymentDate = paymentDate;
            PaymentMethodId = paymentMethodId;
            PaymentStatusId = paymentStatusId;
            CreatedAt = createdAt;

        }

    }

    public static class clsPaymentDataAccess
    {


        public static PaymentDTO GetPaymentInfoById(int paymentId)
        {


            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"select * from payments where payment_id = @paymentId;";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@paymentId", paymentId);
            PaymentDTO payment = null;

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {

                    payment = new PaymentDTO
                        (
                            reader.GetInt32(reader.GetOrdinal("payment_id")),
                            reader.GetDecimal(reader.GetOrdinal("payment_amount")),
                            reader.GetInt32(reader.GetOrdinal("paid_by_tenant_id")),
                            reader.GetInt32(reader.GetOrdinal("generated_bill_id")),
                            reader.GetInt32(reader.GetOrdinal("received_by_rental_unit_owner_id")),
                            reader.GetDateTime(reader.GetOrdinal("payment_date")),
                            reader.GetInt16(reader.GetOrdinal("payment_method_id")),
                            reader.GetInt16(reader.GetOrdinal("payment_status_id")),
                            reader.GetDateTime(reader.GetOrdinal("created_at"))


                        );
                        reader.Close();
                        return payment;
                }
                else
                {
                    reader.Close();
                    return payment;
                }
            }
            catch (Exception ex)
            {
                return payment;
            }
            finally
            {
                connection.Close();

            }

        }

        public static List<PaymentDTO> ListAllPayments()
        {

            List<PaymentDTO> payments = new List<PaymentDTO>();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"select * from payments;";

            SqlCommand command = new SqlCommand(query, connection);
            PaymentDTO payment = null;

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                
                if(reader.HasRows)
                {
                    while(reader.Read())
                    {
                        payment = new PaymentDTO
                            (
                                reader.GetInt32(reader.GetOrdinal("payment_id")),
                                reader.GetDecimal(reader.GetOrdinal("payment_amount")),
                                reader.GetInt32(reader.GetOrdinal("paid_by_tenant_id")),
                                reader.GetInt32(reader.GetOrdinal("generated_bill_id")),
                                reader.GetInt32(reader.GetOrdinal("received_by_rental_unit_owner_id")),
                                reader.GetDateTime(reader.GetOrdinal("payment_date")),
                                reader.GetInt16(reader.GetOrdinal("payment_method_id")),
                                reader.GetInt16(reader.GetOrdinal("payment_status_id")),
                                reader.GetDateTime(reader.GetOrdinal("created_at"))


                            );
                        payments.Add( payment );
                    }
                    reader.Close();
                }

            }
            catch (Exception ex)
            {
                // Handle exception (log it, rethrow it, etc.)
            }
            finally
            {
                connection.Close();


            }
            return payments;
        }

        public static int AddPayment(PaymentDTO payment)
        {

            int paymentId = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            SqlCommand command = new SqlCommand("SP_ProcessAndMakePayment", connection);

            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@PaymentAmount", payment.PaymentAmount);
            command.Parameters.AddWithValue("@PaidByTenantId", payment.PaidByTenantId);
            command.Parameters.AddWithValue("@GeneratedBillId", payment.GeneratedBillId);
            command.Parameters.AddWithValue("@ReceivedByOwnerId", payment.ReceivedByOwnerId);
            command.Parameters.AddWithValue("@PaymentDate", payment.PaymentDate);
            command.Parameters.AddWithValue("@PaymentMethodId", payment.PaymentMethodId);
            command.Parameters.AddWithValue("@PaymentStatusId", payment.PaymentStatusId);
            command.Parameters.AddWithValue("@CreatedAt", payment.CreatedAt);

            SqlParameter outputIdParam = new SqlParameter("@paymentId", SqlDbType.Int)
            {
                Direction = ParameterDirection.Output
            };
            command.Parameters.Add(outputIdParam);

            try
            {
                connection.Open();
                command.ExecuteNonQuery();
                paymentId = (int)command.Parameters["@paymentId"].Value;

            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            finally
            {
                connection.Close();
            }

            return paymentId;
        }
        
        public static bool UpdatePayment(int paymentId, PaymentDTO payment)
        {
            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"
                update payments
                set payment_amount = @PaymentAmount,
                    paid_by_tenant_id = @PaidByTenantId,
                    generated_bill_id = @generatedBillId,
                    recorded_by_system_user_id = @GeneratedBillId,
                    received_by_owner_id = @ReceivedByOwnerId,
                    payment_date = @PaymentDate,
                    payment_method_id = @PaymentMethodId,
                    payment_status_id = @PaymentStatusId,
                    created_at = @CreatedAt
                where payment_id = @paymentId;";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@paymentId", paymentId);
            command.Parameters.AddWithValue("@PaymentAmount", payment.PaymentAmount);
            command.Parameters.AddWithValue("@PaidByTenantId", payment.PaidByTenantId);
            command.Parameters.AddWithValue("@GeneratedBillId", payment.GeneratedBillId);
            command.Parameters.AddWithValue("@ReceivedByOwnerId", payment.ReceivedByOwnerId);
            command.Parameters.AddWithValue("@PaymentDate", payment.PaymentDate);
            command.Parameters.AddWithValue("@PaymentMethodId", payment.PaymentMethodId);
            command.Parameters.AddWithValue("@PaymentStatusId", payment.PaymentStatusId);
            command.Parameters.AddWithValue("@CreatedAt", payment.CreatedAt);

            try
            {
                connection.Open();
                rowsAffected = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                // Handle exception (log it, rethrow it, etc.)
            }
            finally
            {
                connection.Close();
            }

            return (rowsAffected > 0);
        }

        public static bool DeletePayment(int paymentId)
        {
            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"

                DELETE FROM [dbo].[payments]

                where payment_id = @paymentId;
";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@paymentId", paymentId);

            try
            {
                connection.Open();
                rowsAffected = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                // Handle exception (log it, rethrow it, etc.)
            }
            finally
            {
                connection.Close();
            }

            return (rowsAffected > 0);
        }

        public static bool IsPaymentExist(int paymentId)
        {

            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query =

                @"


select * FROM [dbo].[payments]
                where payment_id = @paymentId;


                ";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@paymentId", paymentId);


            try
            {

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                isFound = reader.HasRows;

            }
            catch (Exception ex)
            {
            }
            finally
            {
                connection.Close();

            }

            return isFound;


        }



















    }

}
