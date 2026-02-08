using System;
using System.Data;
using System.Data.SqlClient;
using ManzelOS_DTOs.Employees;
using Microsoft.Data.SqlClient;

namespace ManzelOS_data_access_layer.EmployeesData
{

    public static class clsPropertyManagerDataAccess
    {


        public static PropertyManagerDTO GetPropertyManagerInfoById(int propertyManagerId)
        {


            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"select * from property_managers where property_manager_id = @propertyManagerId";

            SqlCommand command = new SqlCommand (query, connection);

            command.Parameters.AddWithValue(@"propertyManagerId", @propertyManagerId);
            PropertyManagerDTO propertyManager = null;

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    propertyManager = new PropertyManagerDTO
                        (
                            reader.GetInt32(reader.GetOrdinal("property_manager_id")),
                            reader.GetString(reader.GetOrdinal("user_name")),
                            reader.GetString(reader.GetOrdinal("password")),
                            reader.GetDateTime(reader.GetOrdinal("created_at"))

                        );
                    reader.Close();
                    return propertyManager;
                }
                else
                {
                    reader.Close();
                    return propertyManager;
                }

            }
            catch (Exception ex) { }
            finally { connection.Close(); }
            return propertyManager;


        }

        public static PropertyManagerDTO GetPropertyManagerInfoByUserName(string userName)
          
        {


            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"select * from property_managers where user_name = @userName";

            SqlCommand command = new SqlCommand (query, connection);

            command.Parameters.AddWithValue(@"userName", userName);

            PropertyManagerDTO propertyManager = null;

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    propertyManager = new PropertyManagerDTO
                        (
                            reader.GetInt32(reader.GetOrdinal("property_manager_id")),
                            reader.GetString(reader.GetOrdinal("user_name")),
                            reader.GetString(reader.GetOrdinal("password")),
                            reader.GetDateTime(reader.GetOrdinal("created_at"))

                        );
                    reader.Close();
                    return propertyManager;
                }
                else
                {
                    reader.Close();
                    return propertyManager;
                }

            }
            catch (Exception ex) { }
            finally { connection.Close(); }
            return propertyManager;


        }


        public static List<PropertyManagerDTO> ListAllPropertyManagers()
        {

            List<PropertyManagerDTO> PropertyManagersList = new List<PropertyManagerDTO>();

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"select * from property_managers";

            SqlCommand command = new SqlCommand(query, connection);

            PropertyManagerDTO propertyManager = null;

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if(reader.HasRows)
                {

                    while (reader.Read())
                    {
                        propertyManager = new PropertyManagerDTO
                            (
                                reader.GetInt32(reader.GetOrdinal("property_manager_id")),
                                reader.GetString(reader.GetOrdinal("user_name")),
                                reader.GetString(reader.GetOrdinal("password")),
                                reader.GetDateTime(reader.GetOrdinal("created_at"))

                            );
                        PropertyManagersList.Add (propertyManager);
                    }

                    reader.Close();
                }


            }catch (Exception ex) { }
            finally { connection.Close(); }

            return PropertyManagersList;
        }

        public static int AddNewPropertyManager(PropertyManagerDTO propertyManager)
        {
            int propertyManagerId = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"


INSERT INTO [dbo].[property_managers]
           ,[user_name]
           ,[password]
           ,[created_at])
     VALUES
           (
            @userName,
            @password,
            @createdAt) 
            select SCOPE_IDENTITY();
";
            
            SqlCommand command = new SqlCommand(@query, connection);

            command.Parameters.AddWithValue(@"userName", propertyManager.UserName);
            command.Parameters.AddWithValue(@"password", propertyManager.Password);
            command.Parameters.AddWithValue(@"created_at", propertyManager.CreatedAt);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if(result != null && int.TryParse(result.ToString(), out int insertedId))
                {
                    propertyManagerId = insertedId;
                }
            }catch (Exception ex) { }
            finally { connection.Close(); }
            return propertyManagerId;
        }

        public static bool UpdatePropertyManager(int propertyManagerId, PropertyManagerDTO propertyManager)
        {

            int rowsAffected = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"
UPDATE [dbo].[property_managers]
   SET [user_name] = @userName
      ,[password] = @password
      ,[created_at] = @createdAt
where property_manager_id = @propertyManagerId
";


            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue(@"propertyManagerId", propertyManagerId);
            command.Parameters.AddWithValue(@"userName", propertyManager.UserName);
            command.Parameters.AddWithValue(@"password", propertyManager.Password);
            command.Parameters.AddWithValue(@"created_at", propertyManager.CreatedAt);


            try
            {
                connection.Open();

                rowsAffected = command.ExecuteNonQuery();
            }
            catch (Exception ex) { }
            finally { connection.Close(); }
            return (rowsAffected > 0);
            



        }

        public static bool DeletePropertyManager(int propertyManagerId)
        {
 
            int rowsAffected = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"

DELETE FROM [dbo].[property_managers]
      WHERE property_manager_id = @propertyManagerId
";


            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue(@"propertyManagerId", propertyManagerId);
            
            try
            {
                connection.Open();

                rowsAffected = command.ExecuteNonQuery();
            }
            catch (Exception ex) { }
            finally { connection.Close(); }
            return (rowsAffected > 0);
            
        }


        public static bool IsPropertyManagerExist(int propertyManagerId)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"select found = 1 from property_managers where property_manager_id = @propertyManagerId";

            SqlCommand command = new SqlCommand(query,connection);

            command.Parameters.AddWithValue(@"propertyManagerId", propertyManagerId);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                isFound = reader.HasRows;
                reader.Close();
            }catch (Exception ex) { }
            finally { connection.Close(); }
            return isFound;
        }

        public static bool IsUserNameAndPasswordCorrect(string userName, string password)
        {

            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"select found = 1 from property_managers where user_name = @userName and password = @password";

            SqlCommand command = new SqlCommand(query,connection);

            command.Parameters.AddWithValue(@"userName", userName);
            command.Parameters.AddWithValue(@"password", password);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                isFound = reader.HasRows;
                reader.Close();
            }catch (Exception ex) { }
            finally { connection.Close(); }
            return isFound;

        }


    }
}
