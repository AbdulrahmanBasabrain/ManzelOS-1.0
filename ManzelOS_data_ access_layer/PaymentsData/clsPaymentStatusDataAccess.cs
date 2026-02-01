using System;
using System.Data.SqlClient;
using System.Data;
using Microsoft.Data.SqlClient;

namespace ManzelOS_data_access_layer.PaymentsData
{

    public class PaymentStatusDTO
    {

        public short PaymentStatusId { get; set; }
        public string PaymentStatusName { get; set; }

        public PaymentStatusDTO(short paymentStatusId, string paymentStatusName)
        {
            PaymentStatusId = paymentStatusId;
            PaymentStatusName = paymentStatusName;
        }

        
    }

    public static class clsPaymentStatusDataAccess
    {








        public static List<PaymentStatusDTO> ListAllPaymentStatuses()
        {

            List<PaymentStatusDTO> PaymentStatusesList = new List<PaymentStatusDTO>();

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT * FROM payment_statuses";

            SqlCommand command = new SqlCommand(query, connection);
            PaymentStatusDTO paymentStatus = null;

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                
                if(reader.HasRows)
                {
                    while(reader.Read())
                    {
                        paymentStatus = new PaymentStatusDTO
                            (
                                reader.GetInt16(reader.GetOrdinal("payment_status_id")),
                                reader.GetString(reader.GetOrdinal("payment_status_name"))
                            );
                        PaymentStatusesList.Add(paymentStatus);

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
            return PaymentStatusesList;

        }
















    }
}
