using ManzelOS_DTOs.Payments;
using Microsoft.Data.SqlClient;


namespace ManzelOS_data_access_layer.PaymentsData
{


    public static class clsBillPaymentsStatusDataAccess
    {


        public static List<BillPaymentStatusDTO> ListAllBillPaymentStatuses()
        {

            List<BillPaymentStatusDTO> BillPaymentStatuses = new List<BillPaymentStatusDTO>();

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT * FROM bill_payment_statuses";

            SqlCommand command = new SqlCommand(query, connection);

            BillPaymentStatusDTO billPaymentStatus = null;

            try
            {

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if(reader.HasRows)
                {
                    while(reader.Read())
                    {

                        billPaymentStatus = new BillPaymentStatusDTO
                        (reader.GetInt16(reader.GetOrdinal("bill_payment_status_id")), reader.GetString(reader.GetOrdinal("bill_payment_status_name")));
                        BillPaymentStatuses.Add(billPaymentStatus);

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
            return BillPaymentStatuses;

        }



























    }
}
