using System;
using System.Data.SqlClient;
using System.Data;
using Microsoft.Data.SqlClient;

namespace ManzelOS_data_access_layer.PaymentsData
{

    public class PaymentMethodDTO
    {

        public int PaymentMethodId { get; set; }
        public string PaymentStatusName { get; set; }
        public PaymentMethodDTO(int paymentMethodId, string paymentStatusName)
        {
            PaymentMethodId = paymentMethodId;
            PaymentStatusName = paymentStatusName;
        }

    }

    public static class clsPaymentMethodDataAccess
    {

        public static List<PaymentMethodDTO> ListAllPaymentMethods()
        {

            List<PaymentMethodDTO> PaymentMethodsList = new List<PaymentMethodDTO>();

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT * FROM payment_methods";

            SqlCommand command = new SqlCommand(query, connection);
            PaymentMethodDTO paymentMethod = null;

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {

                    while (reader.Read()) 
                    {

                        paymentMethod = new PaymentMethodDTO
                            (
                            reader.GetInt16(reader.GetOrdinal("payment_method_id")),
                            reader.GetString(reader.GetOrdinal("payment_status_name"))
                            );
                        PaymentMethodsList.Add( paymentMethod );
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
            return PaymentMethodsList;

        }


    }
}
