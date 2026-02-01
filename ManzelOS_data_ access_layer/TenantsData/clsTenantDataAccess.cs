using System.Data;
using Microsoft.Data.SqlClient;

namespace ManzelOS_data_access_layer.TenantsData
{

    public class TenantDTO
    {

        public int TenantId { get; set; }
        public int PersonId { get; set; }
        public string DocumentsFilePath { get; set; }
        public DateTime CreatedAt { get; set; }

        public TenantDTO(int tenantId, int personId, string documentsFilePath, DateTime createdAt)
        {
            this.TenantId = tenantId;
            this.PersonId = personId;
            this.DocumentsFilePath = documentsFilePath;
            this.CreatedAt = createdAt;
        }


    }

    public static class clsTenantDataAccess
    {

        public static TenantDTO GetTenatnInfoById(int tenantId)
        {


            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);


            SqlCommand command = new SqlCommand("SP_GetTenantById", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue(@"tenantId", tenantId);

            TenantDTO tenant = null;

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    tenant = new TenantDTO(reader.GetInt32(reader.GetOrdinal("tenant_id")),
                        reader.GetInt32(reader.GetOrdinal("person_id")),
                        reader.GetString(reader.GetOrdinal("documents_file_path")),
                        reader.GetDateTime(reader.GetOrdinal("created_at")));
                    reader.Close();

                    return tenant;
                }
                else
                {
                    return tenant;
                }
            }
            catch (Exception ex) { return tenant; }
            finally { connection.Close(); }
        }

        public static int AddNewTenant(TenantDTO newTenant)
        {

            int tenantId = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand("SP_AddNewTenant", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue(@"PersonId", newTenant.PersonId);
            command.Parameters.AddWithValue(@"DocumentsFilePath", newTenant.DocumentsFilePath);
            command.Parameters.AddWithValue(@"CreatedAt", newTenant.CreatedAt);


                    SqlParameter outputIdParam = new SqlParameter("@TenantId", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(outputIdParam);

            try
            {
                connection.Open();
                command.ExecuteNonQuery();
                tenantId = (int)command.Parameters["@TenantId"].Value;

            }

            catch (Exception ex) { Console.Write(ex.Message); }
            finally { connection.Close(); }
            return tenantId;
        }

        public static bool UpdateTenant(int tenantId, TenantDTO tenant)
        {

            int rowsAffected = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"UPDATE [dbo].[tenants]
   SET [person_id] = @PersonId
      ,[documents_file_path] = @DocumentsFilePath
     ,[created_at] = @CreatedAt
 WHERE tenant_id = @tenantId";

            SqlCommand command = new SqlCommand(query, connection);
            

            command.Parameters.AddWithValue(@"tenantId", tenantId);
            command.Parameters.AddWithValue(@"PersonId", tenant.PersonId);
            command.Parameters.AddWithValue(@"DocumentsFilePath", tenant.DocumentsFilePath);
            command.Parameters.AddWithValue(@"CreatedAt", tenant.CreatedAt);

            try
            {
                connection.Open();

                rowsAffected = command.ExecuteNonQuery();
            }
            catch (Exception ex) { }
            finally { connection.Close(); }

            return (rowsAffected > 0);
        }

        public static List<TenantDTO> ListAllTenants()
        {
            List<TenantDTO> tenantsList = new List<TenantDTO>();

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"select * from tenants";

            SqlCommand command = new SqlCommand(@query, connection);
            TenantDTO tenant = null;
            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        tenant = new TenantDTO
                            (
                                reader.GetInt32(reader.GetOrdinal("tenant_id")),
                                reader.GetInt32(reader.GetOrdinal("person_id")),
                                reader.GetString(reader.GetOrdinal("documents_file_path")),
                                reader.GetDateTime(reader.GetOrdinal("created_at"))
                            );
                        tenantsList.Add(tenant);

                    }
                }

                reader.Close();
            }
            catch (Exception ex) { }
            finally { connection.Close(); }
            return tenantsList;
        }

        public static bool IsTenantExist(int tenantId)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"select found = 1 from tenants where tenant_id = @tenantId";

            bool isFound = false;

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue(@"tenantId", tenantId);
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
        
        public static bool DeleteTenant(int tenantId)
        {
            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"delete from tenants where tenant_id = @tenantId";

            SqlCommand command = new SqlCommand(@query, connection);

            command.Parameters.AddWithValue(@"tenantId", tenantId);

            try
            {
                connection.Open();
                rowsAffected = command.ExecuteNonQuery();
            }
            catch (Exception ex) { }
            finally { connection.Close(); }
            return (rowsAffected > 0);

        }
    
    }
}
