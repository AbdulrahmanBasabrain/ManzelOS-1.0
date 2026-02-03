using ManzelOS_DTOs.RentalUnits;
using Microsoft.Data.SqlClient;

namespace ManzelOS_data_access_layer.RentalUnitsData
{


    public static class clsRentalUnitTypeDataAccess
    {

        public static RentalUnitTypeDTO GetRentalUnitTypeInfoById(short rentalUnitTypeId)
        {

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"select * from rental_unit_types where rental_unit_type_id = @rentalUnitTypeId";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@rentalUnitTypeId", rentalUnitTypeId);
            RentalUnitTypeDTO rentalUnitType = null;
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    rentalUnitType = new RentalUnitTypeDTO
                        (
                        reader.GetInt32(reader.GetOrdinal("rental_unit_type_id")),
                        reader.GetString(reader.GetOrdinal("rental_unit_name"))
                        );
                    reader.Close();
                    return rentalUnitType;


                }
                else
                {
                    return rentalUnitType;
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                // Handle exception (log it, rethrow it, etc.)
                
                return rentalUnitType;
            }
            finally
            {
                connection.Close();

            }
        }

        public static List<RentalUnitTypeDTO> ListAllRentalUnitTypes()
        {

            List<RentalUnitTypeDTO> rentalUnitTypesList = new List<RentalUnitTypeDTO>();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"select * from rental_unit_types";
            SqlCommand command = new SqlCommand(query, connection);
            RentalUnitTypeDTO rentalUnitType = null;

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if(reader.HasRows)
                {
                    while(reader.Read())
                    {
                        rentalUnitType = new RentalUnitTypeDTO
                            (

                                reader.GetInt32(reader.GetOrdinal("rental_unit_type_id")),
                                reader.GetString(reader.GetOrdinal("rental_unit_name"))

                            );
                        rentalUnitTypesList.Add( rentalUnitType );
                    }
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
            return rentalUnitTypesList;
        }

        
    }
}
