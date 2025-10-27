using System;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Data.SqlClient;

namespace ManzelOS_data_access_layer.PaymentsData
{

    public class GeneratedBillDTO
    {

        public int GeneratedBillId { get; set; }
        public decimal GeneratedBillFees { get; set; }
        public int BillTypeId { get; set; }
        public int RentalContractId { get; set; }
        public int RentalUnitId { get; set; }
        public DateTime GeneratedBillDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public short BillPaymentStatusId { get; set; }
        public DateTime BillExpectedPaymentDate { get; set; }
        public bool IsPaid { get; set; }

        public GeneratedBillDTO(int generatedBillId, decimal generatedBillFees, int billTypeId, int rentalContractId, int rentalUnitId, DateTime generatedBillDate, DateTime createdAt, short billPaymentStatusId,
            DateTime billExpectedPaymentDate, bool isPaid)
        {

            GeneratedBillId = generatedBillId;
            GeneratedBillFees = generatedBillFees;
            BillTypeId = billTypeId;
            RentalContractId = rentalContractId;
            RentalUnitId = rentalUnitId;
            GeneratedBillDate = generatedBillDate;
            CreatedAt = createdAt;
            BillPaymentStatusId = billPaymentStatusId;
            BillExpectedPaymentDate = billExpectedPaymentDate;
            IsPaid = isPaid;

        }

    }

    public static class clsGeneratedBillDataAccess
    {



        public static GeneratedBillDTO GetGeneratedBillInfoById(int generatedBillId)
        {

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"select * from generated_bills where generated_bill_id = @generatedBillId;";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@generatedBillId", generatedBillId);

            GeneratedBillDTO generatedBill = null;

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    generatedBill = new GeneratedBillDTO
                        (

                            reader.GetInt32(reader.GetOrdinal("generated_bill_id")),
                            reader.GetDecimal(reader.GetOrdinal("generated_bill_fees")),
                            reader.GetInt32(reader.GetOrdinal("bill_type_id")),
                            reader.GetInt32(reader.GetOrdinal("rental_contract_id")),
                            reader.GetInt32(reader.GetOrdinal("rental_unit_id")),
                            reader.GetDateTime(reader.GetOrdinal("generated_date")),
                            reader.GetDateTime(reader.GetOrdinal("created_at")),
                            reader.GetInt16(reader.GetOrdinal("bill_payment_status_id")),
                            reader.GetDateTime(reader.GetOrdinal("expected_payment_date")),
                            reader.GetBoolean(reader.GetOrdinal("is_paid"))

                        );
                        
                    reader.Close();
                    return generatedBill;

                }
                else
                {
                    reader.Close();
                    return generatedBill;
                }
            }
            catch (Exception ex)
            {
                return generatedBill;
            }
            finally
            {
                connection.Close();
            }

        }

        public static List<GeneratedBillDTO> ListAllGeneratedBills()
        {

            List<GeneratedBillDTO> generatedBillsList = new List<GeneratedBillDTO>();

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"select * from generated_bills;";
            SqlCommand command = new SqlCommand(query, connection);
            GeneratedBillDTO generatedBill = null;

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if(reader.HasRows)
                {
                    while(reader.Read())
                    {
                        generatedBill = new GeneratedBillDTO
                            (

                                reader.GetInt32(reader.GetOrdinal("generated_bill_id")),
                                reader.GetDecimal(reader.GetOrdinal("generated_bill_fees")),
                                reader.GetInt32(reader.GetOrdinal("bill_type_id")),
                                reader.GetInt32(reader.GetOrdinal("rental_contract_id")),
                                reader.GetInt32(reader.GetOrdinal("rental_unit_id")),
                                reader.GetDateTime(reader.GetOrdinal("generated_date")),
                                reader.GetDateTime(reader.GetOrdinal("created_at")),
                                reader.GetInt16(reader.GetOrdinal("bill_payment_status_id")),
                                reader.GetDateTime(reader.GetOrdinal("expected_payment_date")),
                                reader.GetBoolean(reader.GetOrdinal("is_paid"))

                            );
                        generatedBillsList.Add(generatedBill);
                    }
                    reader.Close();
                }
            }
            catch (Exception ex) { }
            finally
            {
                connection.Close();
            }

            return generatedBillsList;
        }

        public static List<GeneratedBillDTO> ListAllGeneratedBillsWithContract(int contractId)
        {

            List<GeneratedBillDTO> generatedBillsList = new List<GeneratedBillDTO>();

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"select * from generated_bills where rental_contract_id = @contractId";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue(@"contractId", contractId);
            GeneratedBillDTO generatedBill = null;

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        generatedBill = new GeneratedBillDTO
                            (

                                reader.GetInt32(reader.GetOrdinal("generated_bill_id")),
                                reader.GetDecimal(reader.GetOrdinal("generated_bill_fees")),
                                reader.GetInt32(reader.GetOrdinal("bill_type_id")),
                                reader.GetInt32(reader.GetOrdinal("rental_contract_id")),
                                reader.GetInt32(reader.GetOrdinal("rental_unit_id")),
                                reader.GetDateTime(reader.GetOrdinal("generated_date")),
                                reader.GetDateTime(reader.GetOrdinal("created_at")),
                                reader.GetInt16(reader.GetOrdinal("bill_payment_status_id")),
                                reader.GetDateTime(reader.GetOrdinal("expected_payment_date")),
                                reader.GetBoolean(reader.GetOrdinal("is_paid"))

                            );
                        generatedBillsList.Add(generatedBill);
                    }
                    reader.Close();
                }
            }
            catch (Exception ex) { }
            finally
            {
                connection.Close();
            }

            return generatedBillsList;

        }

        public static List<GeneratedBillDTO> ListAllUnpaidBillsWithContract(int contractId)
        {

            List<GeneratedBillDTO> generatedBillsList = new List<GeneratedBillDTO>();

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"select * from generated_bills where rental_contract_id = @contractId and bill_payment_status_id = 1
                                and bill_payment_status_id = 2";
            SqlCommand command = new SqlCommand(query, connection);

            GeneratedBillDTO generatedBill = null;

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        generatedBill = new GeneratedBillDTO
                            (

                                reader.GetInt32(reader.GetOrdinal("generated_bill_id")),
                                reader.GetDecimal(reader.GetOrdinal("generated_bill_fees")),
                                reader.GetInt32(reader.GetOrdinal("bill_type_id")),
                                reader.GetInt32(reader.GetOrdinal("rental_contract_id")),
                                reader.GetInt32(reader.GetOrdinal("rental_unit_id")),
                                reader.GetDateTime(reader.GetOrdinal("generated_date")),
                                reader.GetDateTime(reader.GetOrdinal("created_at")),
                                reader.GetInt16(reader.GetOrdinal("bill_payment_status_id")),
                                reader.GetDateTime(reader.GetOrdinal("expected_payment_date")),
                                reader.GetBoolean(reader.GetOrdinal("is_paid"))

                            );
                        generatedBillsList.Add(generatedBill);
                    }
                    reader.Close();
                }
            }
            catch (Exception ex) { }
            finally
            {
                connection.Close();
            }

            return generatedBillsList;

        }

        public static int AddNewGeneratedBill(GeneratedBillDTO generatedBill)
        {

            int generatedBillId = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query =

                @"
                            insert into generated_bills ([generated_bill_fees]
           ,[bill_type_id]
           ,[rental_contract_id]
           ,[rental_unit_id]
           ,[generated_date]
           ,[created_at]
           ,[bill_payment_status_id]
           ,[expected_payment_date]
           ,[is_paid])
                            values (@GeneratedBillFees, @BillTypeId, @RentalContractId, @RentalUnitId, 
                            @GeneratedBillDate, @CreatedAt, @BillPaymentStatusId, @BillExpectedPaymentDate, @IsPaid);
                select SCOPE_IDENTITY();

                ";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@GeneratedBillFees", generatedBill.GeneratedBillFees);
            command.Parameters.AddWithValue("@BillTypeId", generatedBill.BillTypeId);
            command.Parameters.AddWithValue("@RentalContractId", generatedBill.RentalContractId);
            command.Parameters.AddWithValue("@RentalUnitId", generatedBill.RentalUnitId);
            command.Parameters.AddWithValue("@GeneratedBillDate", generatedBill.GeneratedBillDate);
            command.Parameters.AddWithValue("@CreatedAt", generatedBill.CreatedAt);
            command.Parameters.AddWithValue("@BillPaymentStatusId", generatedBill.BillPaymentStatusId);
            command.Parameters.AddWithValue("@BillExpectedPaymentDate", generatedBill.BillExpectedPaymentDate);
            command.Parameters.AddWithValue("@IsPaid", generatedBill.IsPaid);

            try
            {

                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int insertedId))
                {
                    generatedBillId = insertedId;
                }
                else
                {
                    generatedBillId = -1; // In case of failure to insert
                }
            }
            catch (Exception ex)
            {
            }
            finally
            {
                connection.Close();

            }
            return generatedBillId;

        }

        public static bool UpdateGeneratedBill(int generatedBillId, GeneratedBillDTO generatedBill)
        {
            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query =

                @"


UPDATE [dbo].[generated_bills]
   SET [generated_bill_fees] = @GeneratedBillFees
      ,[bill_type_id] = @BillTypeId
      ,[rental_contract_id] = @RentalContractId
      ,[rental_unit_id] = @RentalUnitId
      ,[generated_date] = @GeneratedBillDate
      ,[created_at] = @CreatedAt
      ,[bill_payment_status_id] = @BillPaymentStatusId
      ,[expected_payment_date] = @BillExpectedPaymentDate
      ,[is_paid] = @IsPaid
 WHERE generated_bill_id = @generatedBillId



                ";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@generatedBillId", generatedBillId);
            command.Parameters.AddWithValue("@GeneratedBillFees", generatedBill.GeneratedBillFees);
            command.Parameters.AddWithValue("@BillTypeId", generatedBill.BillTypeId);
            command.Parameters.AddWithValue("@RentalContractId", generatedBill.RentalContractId);
            command.Parameters.AddWithValue("@RentalUnitId", generatedBill.RentalUnitId);
            command.Parameters.AddWithValue("@GeneratedBillDate", generatedBill.GeneratedBillDate);
            command.Parameters.AddWithValue("@CreatedAt", generatedBill.CreatedAt);
            command.Parameters.AddWithValue("@BillPaymentStatusId", generatedBill.BillPaymentStatusId);
            command.Parameters.AddWithValue("@BillExpectedPaymentDate", generatedBill.BillExpectedPaymentDate);
            command.Parameters.AddWithValue("@IsPaid", generatedBill.IsPaid);

            try
            {

                connection.Open();
                rowsAffected = command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
            }
            finally
            {
                connection.Close();

            }
            return (rowsAffected > 0);

        }

        public static bool DeleteGeneratedBill(int generatedBillId)
        {
            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query =

                @"


DELETE FROM [dbo].[generated_bills]
 WHERE generated_bill_id = @generatedBillId


                ";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@generatedBillId", generatedBillId);


            try
            {

                connection.Open();
                rowsAffected = command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
            }
            finally
            {
                connection.Close();

            }
            return (rowsAffected > 0);


        }

        public static bool IsGeneratedBillExist(int generatedBillId)
        {

            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query =

                @"


select * FROM [dbo].[generated_bills]
 WHERE generated_bill_id = @generatedBillId


                ";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@generatedBillId", generatedBillId);


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
