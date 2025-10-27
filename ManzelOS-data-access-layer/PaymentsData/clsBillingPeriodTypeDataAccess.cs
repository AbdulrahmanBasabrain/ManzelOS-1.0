using Microsoft.Data.SqlClient;

namespace ManzelOS_data_access_layer.PaymentsData
{

    public class BillingPeriodTypeDTO
    {

        public int BillingPeriodID { get; set; }
        public string BillingPeriodName { get; set; }

        public BillingPeriodTypeDTO(int billingPeriodTypeId, string billingPeriodTypeName) { this.BillingPeriodID = billingPeriodTypeId; this.BillingPeriodName = billingPeriodTypeName; }

    }

    public static class clsBillingPeriodTypeDataAccess
    {


        public static List<BillingPeriodTypeDTO> ListAllBillingPeriodTypes()
        {

            List<BillingPeriodTypeDTO> billingPeriodTypesList = new List<BillingPeriodTypeDTO>();

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT * FROM billing_periods";

            SqlCommand command = new SqlCommand(query, connection);
            BillingPeriodTypeDTO billingPeriodType = null;

            try
            {

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows) 
                {

                    while (reader.Read())
                    {

                        billingPeriodType = new BillingPeriodTypeDTO(reader.GetInt32(reader.GetOrdinal("billing_period_type_id")), reader.GetString(reader.GetOrdinal("billing_period")));
                        billingPeriodTypesList.Add(billingPeriodType);
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
            return billingPeriodTypesList;

        }

    }

}
