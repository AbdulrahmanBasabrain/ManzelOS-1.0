using System;
using System.Data;
using System.Data.SqlClient;
using ManzelOS_DTOs.Owners;
using Microsoft.Data.SqlClient;

namespace ManzelOS_data_access_layer.OwnersData
{



    public static class clsOwnerDataAccess
    {

        public static OwnerDTO GetOwnerInfoById(int ownerId)
        {


            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"select * from owners where owner_id = @ownerId";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue(@"ownerId", ownerId);
            OwnerDTO owner = null;

            try
            {

                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {

                    owner = new OwnerDTO
                        (

                            reader.GetInt32(reader.GetOrdinal("owner_id")),
                            reader.GetInt32(reader.GetOrdinal("person_id")),
                            reader.GetBoolean(reader.GetOrdinal("is_business")),
                            reader.GetDateTime(reader.GetOrdinal("created_at"))


                        );
                        
                    reader.Close();
                    return owner;

                }
                else
                {
                         
                    reader.Close();
                    return owner;

   
                }
            }
            catch (Exception ex) { return owner; }
            finally { connection.Close(); }

        }

        public static List<OwnerDTO> ListAllOwners()
        {

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"Select * from owners";

            SqlCommand command = new SqlCommand(@query, connection);

            List<OwnerDTO> ownersList = new List<OwnerDTO>();
            
            OwnerDTO owner = null;

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while(reader.Read())
                    {
 
                    owner = new OwnerDTO
                        (

                            reader.GetInt32(reader.GetOrdinal("owner_id")),
                            reader.GetInt32(reader.GetOrdinal("person_id")),
                            reader.GetBoolean(reader.GetOrdinal("is_business")),
                            reader.GetDateTime(reader.GetOrdinal("created_at"))

                        );
                        ownersList.Add(owner);                        
                    }

                    reader.Close();
                }
                reader.Close();
            }
            catch (Exception ex) { }
            finally { connection.Close(); }
            return ownersList;
        }

        public static int AddNewOwner(OwnerDTO owner)
        {
            int ownerId = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"

INSERT INTO [dbo].[owners]
           ([person_id]
           ,[is_business]
           ,[created_at])
     VALUES
           (@PersonId
           ,@IsBusiness
           ,@CreatedAt)
 select SCOPE_IDENTITY();

";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue(@"PersonId", owner.PersonId);
            command.Parameters.AddWithValue(@"IsBusiness", owner.IsBusiness);
            command.Parameters.AddWithValue(@"CreatedAt", owner.CreatedAt);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out int insertedId))
                {
                    ownerId = insertedId;
                }
            }catch (Exception ex) { }
            finally { connection.Close(); }
            return ownerId;

        }

        public static bool UpdateOwner(int ownerId, OwnerDTO owner)
        {

            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"

UPDATE [dbo].[owners]
   SET [person_id] = @PersonId
      ,[is_business] = @IsBusiness
      ,[created_at] = @CreatedAt
 WHERE owner_id = @ownerId
";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue(@"ownerId", ownerId);
            command.Parameters.AddWithValue(@"PersonId", owner.PersonId);
            command.Parameters.AddWithValue(@"IsBusiness", owner.IsBusiness);
            command.Parameters.AddWithValue(@"CreatedAt", owner.CreatedAt);

            try
            {
                connection.Open();

                rowsAffected = command.ExecuteNonQuery();

            }catch (Exception ex) { }
            finally { connection.Close(); }
            return (rowsAffected > 0);

        }

        public static bool DeleteOwner(int ownerId)
        {
            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"

DELETE FROM [dbo].[owners]
      WHERE owner_id = @ownerId
";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue(@"ownerId", ownerId);

            try
            {
                connection.Open();

                rowsAffected = command.ExecuteNonQuery();

            }
            catch (Exception ex) { }
            finally { connection.Close(); }
            return (rowsAffected > 0);
        }

        public static bool IsOwnerExist(int ownerId)
        {

            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"select found = 1 from owners where owner_id = @ownerId";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue(@"ownerId", ownerId);

            try
            {
                connection.Open();
                
                SqlDataReader reader = command.ExecuteReader();

                isFound = reader.HasRows;
                reader.Close();

            }
            catch (Exception ex) { }
            finally { connection.Close(); }
            return isFound;
        }

    }
}
