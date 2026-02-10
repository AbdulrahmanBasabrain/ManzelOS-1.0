using System;
using System.Data;
using System.Data.SqlClient;
using ManzelOS_data_access_layer.PeopleData;
using ManzelOS_DTOs.Employees;
using ManzelOS_DTOs.People;
using ManzelOS_DTOs.PropertyManagers;
using Microsoft.Data.SqlClient;

namespace ManzelOS_data_access_layer.EmployeesData
{

    public static class clsPropertyManagerDataAccess
    {


        public static PropertyManagerPersonDTO GetFullPropertyManagerInfoById(int propertyManagerId)
        {


            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"select * from property_managers where property_manager_id = @propertyManagerId";

            SqlCommand command = new SqlCommand (query, connection);

            command.Parameters.AddWithValue(@"propertyManagerId", @propertyManagerId);
            PropertyManagerPersonDTO fullPropertyManager = null;

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    PeopleDTO peopleDTO = clsPeopleDataAccess.GetPersonInfoById(propertyManagerId);
                    fullPropertyManager  = new PropertyManagerPersonDTO
                        (
                            
                            reader.GetString(reader.GetOrdinal("first_name")),
                            reader.GetString(reader.GetOrdinal("last_name")),
                            reader.GetString(reader.GetOrdinal("national_id")),
                            reader.GetDateTime(reader.GetOrdinal("date_of_birth")),
                            reader.GetBoolean(reader.GetOrdinal("gender")),
                            reader.GetInt16(reader.GetOrdinal("country_id")),
                            reader.GetInt32(reader.GetOrdinal("city_id")),
                            reader.GetString(reader.GetOrdinal("district")),
                            reader.GetString(reader.GetOrdinal("street")),
                            reader.GetString(reader.GetOrdinal("personal_email")),
                            reader.GetString(reader.GetOrdinal("phone")),
                            reader.GetString(reader.GetOrdinal("personal_image_path")),
                            reader.GetInt16(reader.GetOrdinal("marriage_status_id")),

                            reader.GetString(reader.GetOrdinal("user_name")),
                            reader.GetString(reader.GetOrdinal("password"))

                        );
                    reader.Close();
                    return fullPropertyManager;
                }
                else
                {
                    reader.Close();
                    return fullPropertyManager;
                }

            }
            catch (Exception ex) { }
            finally { connection.Close(); }
            return fullPropertyManager;


        }

        public static PropertyManagerDTO GetPrivatePropertyManagerInfoByUserName(string userName)
          
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
                            reader.GetInt32(reader.GetOrdinal("person_id")),
                            reader.GetString(reader.GetOrdinal("user_name")),
                            reader.GetString(reader.GetOrdinal("password"))

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
                                reader.GetInt32(reader.GetOrdinal("person_id")),
                                reader.GetString(reader.GetOrdinal("user_name")),
                                reader.GetString(reader.GetOrdinal("password"))

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
           ([person_id]
            ,[username]
           ,[password]
           ,[created_at])
     VALUES
           (@personId,
            @userName,
            @password,
            @createdAt) 
            select SCOPE_IDENTITY();
";
            
            SqlCommand command = new SqlCommand(@query, connection);

            command.Parameters.AddWithValue(@"personId", propertyManager.PersonID);
            command.Parameters.AddWithValue(@"userName", propertyManager.UserName);
            command.Parameters.AddWithValue(@"password", propertyManager.Password);
            command.Parameters.AddWithValue(@"createdAt", propertyManager.PropertyManagerCreatedAt);

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
            command.Parameters.AddWithValue(@"created_at", propertyManager.PropertyManagerCreatedAt);


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
