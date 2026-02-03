using System;
using System.Data.SqlClient;
using System.Data;
using ManzelOS_DTOs.People;
using Microsoft.Data.SqlClient;

namespace ManzelOS_data_access_layer.PeopleData
{

    public class clsCityDataAccess
    {
    

        public static CityDTO GetCityInfo(int cityId)
        {

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"select * from cities where city_id = @cityId";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue(@"cityId", cityId);
            
            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {

                    CityDTO city = new CityDTO
                        (
                                reader.GetInt32(reader.GetOrdinal("city_id")),
                                reader.GetInt16(reader.GetOrdinal("country_id")),
                                reader.GetString(reader.GetOrdinal("city_name"))
                        );
                    reader.Close();
                    return city;

                }
                else
                {
                    return null;
                }

            }
            catch (Exception ex)
            {
                return null; 
            }
            finally { connection.Close(); }

        }

        public static List<CityDTO> ListAllCities()
        {

            List<CityDTO> cities = new List<CityDTO>();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"select * from cities";
            CityDTO city = null;
            SqlCommand command = new SqlCommand(query, connection);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {

                    while(reader.Read())
                    {
                        city = new CityDTO
                            (
                                reader.GetInt32(reader.GetOrdinal("city_id")),
                                reader.GetInt16(reader.GetOrdinal("country_id")),
                                reader.GetString(reader.GetOrdinal("city_name"))
                            ); 
                        cities.Add( city );
                    }
                }

                reader.Close();
            }
            catch (Exception ex) { }
            finally { connection.Close(); }
            return cities;

        }

    }
}
