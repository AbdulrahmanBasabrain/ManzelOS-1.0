using System;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Data.SqlClient;

namespace ManzelOS_data_access_layer.EmployeesData
{

    public class PropertyManagerDTO
    {

        public int PropertyManagerId { get; set; }
        public int EmployeeId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }
        public int Permession { get; set; }
        public DateTime AssignedAt { get; set; }

        public PropertyManagerDTO(int propertyManagerId, int employeeId, string userName, string password,
            bool isActive, int permession, DateTime assignedAt)
        {

            PropertyManagerId = propertyManagerId;
            EmployeeId = employeeId;
            UserName = userName;
            Password = password;
            IsActive = isActive;
            Permession = permession;
            AssignedAt = assignedAt;

        }

    }

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
                            reader.GetInt32(reader.GetOrdinal("employee_id")),
                            reader.GetString(reader.GetOrdinal("user_name")),
                            reader.GetString(reader.GetOrdinal("password")),
                            reader.GetBoolean(reader.GetOrdinal("is_active")),
                            reader.GetInt32(reader.GetOrdinal("permession")),
                            reader.GetDateTime(reader.GetOrdinal("assigned_at"))

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
                            reader.GetInt32(reader.GetOrdinal("employee_id")),
                            reader.GetString(reader.GetOrdinal("user_name")),
                            reader.GetString(reader.GetOrdinal("password")),
                            reader.GetBoolean(reader.GetOrdinal("is_active")),
                            reader.GetInt32(reader.GetOrdinal("permession")),
                            reader.GetDateTime(reader.GetOrdinal("assigned_at"))

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
                                reader.GetInt32(reader.GetOrdinal("employee_id")),
                                reader.GetString(reader.GetOrdinal("user_name")),
                                reader.GetString(reader.GetOrdinal("password")),
                                reader.GetBoolean(reader.GetOrdinal("is_active")),
                                reader.GetInt32(reader.GetOrdinal("permession")),
                                reader.GetDateTime(reader.GetOrdinal("assigned_at"))

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
           ([employee_id]
           ,[user_name]
           ,[password]
           ,[is_active]
           ,[permession]
           ,[assigned_at])
     VALUES
           (@EmployeeId,
            @UserName,
            @Password,
            @IsActive,
            @Permession,
            @AssignedAt) 
            select SCOPE_IDENTITY();
";
            
            SqlCommand command = new SqlCommand(@query, connection);

            command.Parameters.AddWithValue(@"EmployeeId", propertyManager.EmployeeId);
            command.Parameters.AddWithValue(@"UserName", propertyManager.UserName);
            command.Parameters.AddWithValue(@"Password", propertyManager.Password);
            command.Parameters.AddWithValue(@"IsActive", propertyManager.IsActive);
            command.Parameters.AddWithValue(@"Permession", propertyManager.Permession);
            command.Parameters.AddWithValue(@"AssignedAt", propertyManager.AssignedAt);

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
   SET [employee_id] = @EmployeeId
      ,[user_name] = @UserName
      ,[password] = @Password
      ,[is_active] = @IsActive 
      ,[permession] = @Permession
      ,[assigned_at] = @AssignedAt
where property_manager_id = @propertyManagerId
";


            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue(@"propertyManagerId", propertyManagerId);
            command.Parameters.AddWithValue(@"EmployeeId", propertyManager.EmployeeId);
            command.Parameters.AddWithValue(@"UserName", propertyManager.UserName);
            command.Parameters.AddWithValue(@"Password", propertyManager.Password);
            command.Parameters.AddWithValue(@"IsActive", propertyManager.IsActive);
            command.Parameters.AddWithValue(@"Permession", propertyManager.Permession);
            command.Parameters.AddWithValue(@"AssignedAt", propertyManager.AssignedAt);


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
