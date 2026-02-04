using ManzelOS_DTOs.Payments;
using Microsoft.Data.SqlClient;

namespace ManzelOS_data_access_layer.PaymentsData
{


    public static class clsBillTypeDataAccess
    {



        public static List<BillTypeDTO> ListAllBillTypes()
        {

            List<BillTypeDTO> billTypesList = new List<BillTypeDTO>();

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT * FROM bill_types";

            SqlCommand command = new SqlCommand(query, connection);
            BillTypeDTO billType = null;

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                
                if(reader.HasRows)
                {
                    while(reader.Read())
                    {
                        billType = new BillTypeDTO
                            (

                                reader.GetInt32(reader.GetOrdinal("bill_type_id")),
                                reader.GetString(reader.GetOrdinal("billing_type_name"))

                            );
                        billTypesList.Add( billType );

                    }
                }
                reader.Close();

            }
            catch (Exception ex)
            {
                // Handle exception (log it, rethrow it, etc.)
            }
            finally
            {
                connection.Close();
            }
            return billTypesList;

        }


    }
}
