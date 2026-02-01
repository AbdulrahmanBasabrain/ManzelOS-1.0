using System;
using System.Data.SqlClient;
using System.Data;
using Microsoft.Data.SqlClient;

namespace ManzelOS_data_access_layer.PeopleData
{

    public class MarriageStatusDTO
    {

        public short MarriageStatusID { get; set; }
        public string MarriageStatusName { get; set; }

        public MarriageStatusDTO(short marriageStatusId, string marriageStatusName)
        {
            this.MarriageStatusID = marriageStatusId;
            this.MarriageStatusName = marriageStatusName;
        }

    }

    public static class clsMarriageStatusDataAccess
    {

        public static MarriageStatusDTO GetMarriageStatusInfo(short MarriageStatusId)
        {

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"select * from marriage_statuses where marriage_status_id = @MarriageStatusId";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue(@"MarriageStatusId", MarriageStatusId);
            MarriageStatusDTO marriageStatus = null;
            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {

                    marriageStatus = new MarriageStatusDTO(reader.GetInt16(reader.GetOrdinal("marriage_status_id")), 
                        reader.GetString(reader.GetOrdinal("marriage_status_name")));

                    reader.Close();
                    return marriageStatus;
                    
                }
                else
                {
                    reader.Close();
                    return null;
                }

            }
            catch (Exception ex) { return null; }
            finally { connection.Close(); }

        }

        public static List<MarriageStatusDTO> ListAllMarriageStatuses()
        {

            List<MarriageStatusDTO> MarriageStatusList = new List<MarriageStatusDTO>();

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"select * from marriage_statuses";

            SqlCommand command = new SqlCommand(query, connection);
            MarriageStatusDTO marriageStatus = null;
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {

                    while(reader.Read())
                    {
                        marriageStatus = new MarriageStatusDTO(reader.GetInt16(reader.GetOrdinal("marriage_status_id")), 
                        reader.GetString(reader.GetOrdinal("marriage_status_name")));
                        MarriageStatusList.Add(marriageStatus);

                    }



                }

                reader.Close();
            }
            catch (Exception ex) { }
            finally { connection.Close(); }
            return MarriageStatusList;

        }

    }
}
