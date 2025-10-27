using System;
using System.Data.SqlClient;
using System.Data;
using Microsoft.Data.SqlClient;

namespace ManzelOS_data_access_layer.PeopleData
{

    public class CountryDTO
    {


        public short CountryId { get; set; }
        public short ContinentId { get; set; }
        public string CountryName { get; set; }

        public CountryDTO(short countryId, short continentId, string countryName)
        {
            CountryId = countryId;
            ContinentId = continentId;
            CountryName = countryName;

        }





    }

    public static class clsCountryDataAccess
    {

        public static CountryDTO GetCountryInfo(short countryId)
        {

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"select * from countries where country_id = @countryId";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue(@"countryId", countryId);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {

                    CountryDTO country = new CountryDTO(
                        reader.GetInt16(reader.GetOrdinal("country_id")),
                        reader.GetInt16(reader.GetOrdinal("continent_id")),
                        reader.GetString(reader.GetOrdinal("country_name"))
                        );
                    reader.Close();
                    return country;
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

        public static List<CountryDTO> ListAllCountries()
        {

            List<CountryDTO> countriesList = new List<CountryDTO>();
            
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"select * from countries";

            SqlCommand command = new SqlCommand(query, connection);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                CountryDTO country;
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        country = new CountryDTO(
                            reader.GetInt16(reader.GetOrdinal("country_id")),
                            reader.GetInt16(reader.GetOrdinal("continent_id")),
                            reader.GetString(reader.GetOrdinal("country_name"))
                            );
                        countriesList.Add(country);
                    }
                }

                reader.Close();
            }
            catch (Exception ex) { }
            finally { connection.Close(); }
            return countriesList;

        }

    }

}
