using System;
using System.Data;
using System.Data.SqlClient;
using ManzelOS_DTOs.People;
using Microsoft.Data.SqlClient;

namespace ManzelOS_data_access_layer.PeopleData
{


    public static class clsContinentDataAccess
    {

        public static ContinentDTO GetContinentInfo(int continentId)
        {

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"select * from continents where continent_id = @continentId";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue(@"continentId", continentId);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {

                    ContinentDTO continent = new ContinentDTO
                        (
                            reader.GetInt16(reader.GetOrdinal("continent_id")),
                            reader.GetString(reader.GetOrdinal("continent_name"))
                        );
                    
                    reader.Close();
                    return continent;

                }
                else
                {
                    reader.Close();
                    return null;
                }

            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex.Message);
                return null;
            }
            finally { connection.Close(); }

        }

        public static List<ContinentDTO> ListAllContinents()
        {


            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            List<ContinentDTO> contienentList = new List<ContinentDTO>();
            string query = @"select * from continents";

            SqlCommand command = new SqlCommand(query, connection);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while(reader.Read())
                    {
                        ContinentDTO continent =  new ContinentDTO
                        (
                            reader.GetInt16(reader.GetOrdinal("continent_id")),
                            reader.GetString(reader.GetOrdinal("continent_name"))
                        );

                        contienentList.Add(continent); 

                    }
                }

                reader.Close();
            }
            catch (Exception ex) { }
            finally { connection.Close(); }
            return contienentList;

        }

    }
}
