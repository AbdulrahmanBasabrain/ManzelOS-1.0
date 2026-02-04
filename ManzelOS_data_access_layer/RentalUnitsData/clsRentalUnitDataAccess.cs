using ManzelOS_DTOs.RentalUnits;
using Microsoft.Data.SqlClient;

namespace ManzelOS_data_access_layer.RentalUnitsData
{


    public static class clsRentalUnitDataAccess
    {


        public static RentalUnitDTO GetRentalUnitInfoById(int rentalUnitId)
        {

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            RentalUnitDTO rentalUnit = null;

            string query = @"select * from rental_units where rental_unit_id = @rentalUnitId";
            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@RentalUnitId", rentalUnitId);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {

                    rentalUnit = new RentalUnitDTO
                        (

                           reader.GetInt32(reader.GetOrdinal("rental_unit_id")),
                           reader.GetInt32(reader.GetOrdinal("rental_unit_number_order")),
                           reader.GetString(reader.GetOrdinal("rental_unit_name")),
                           reader.GetDecimal(reader.GetOrdinal("rental_unit_space")),
                           reader.GetString(reader.GetOrdinal("rental_unit_location_name")),
                           reader.GetDecimal(reader.GetOrdinal("rental_unit_location_latitude")),
                           reader.GetDecimal(reader.GetOrdinal("rental_unit_location_longitude")),
                           reader.GetInt32(reader.GetOrdinal("rental_unit_type_id")),
                           reader.IsDBNull(reader.GetOrdinal("parent_rental_unit_id")) ? null : reader.GetInt32(reader.GetOrdinal("parent_rental_unit_id")),
                           reader.GetBoolean(reader.GetOrdinal("is_available")),
                           reader.GetInt32(reader.GetOrdinal("property_manager_id")),
                           reader.GetInt32(reader.GetOrdinal("representative_owner_id")),
                           reader.GetDateTime(reader.GetOrdinal("created_at"))

                        );

                    reader.Close();

                    return rentalUnit;

                }
                else
                {
                    reader.Close();
                    return rentalUnit;
                }


            }
            catch (Exception ex) { return rentalUnit; }
            finally { connection.Close(); }

        }

        public static List<RentalUnitDTO> ListAllRentalUnits()
        {
            List<RentalUnitDTO> rentalUnitsList = new List<RentalUnitDTO>();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"SELECT * FROM rental_units";

            SqlCommand command = new SqlCommand(query, connection);
            RentalUnitDTO rentalUnit = null;

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {

                        rentalUnit = new RentalUnitDTO
                            (

                               reader.GetInt32(reader.GetOrdinal("rental_unit_id")),
                               reader.GetInt32(reader.GetOrdinal("rental_unit_number_order")),
                               reader.GetString(reader.GetOrdinal("rental_unit_name")),
                               reader.GetDecimal(reader.GetOrdinal("rental_unit_space")),
                               reader.GetString(reader.GetOrdinal("rental_unit_location_name")),
                               reader.GetDecimal(reader.GetOrdinal("rental_unit_location_latitude")),
                               reader.GetDecimal(reader.GetOrdinal("rental_unit_location_longitude")),
                               reader.GetInt32(reader.GetOrdinal("rental_unit_type_id")),
                               reader.IsDBNull(reader.GetOrdinal("parent_rental_unit_id")) ? null : reader.GetInt32(reader.GetOrdinal("parent_rental_unit_id")),
                               reader.GetBoolean(reader.GetOrdinal("is_available")),
                               reader.GetInt32(reader.GetOrdinal("property_manager_id")),
                               reader.GetInt32(reader.GetOrdinal("representative_owner_id")),
                               reader.GetDateTime(reader.GetOrdinal("created_at"))

                            );

                        rentalUnitsList.Add(rentalUnit);

                    }

                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions as needed
            }
            finally
            {
                connection.Close();
            }
            return rentalUnitsList;
        }

        public static int AddRentalUnit(RentalUnitDTO rentalUnit)
        {

            int rentalUnitId = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"

INSERT INTO [dbo].[rental_units]
           ([rental_unit_number_order]
           ,[rental_unit_name]
           ,[rental_unit_space]
           ,[rental_unit_location_name]
           ,[rental_unit_location_latitude]
           ,[rental_unit_location_longitude]
           ,[rental_unit_type_id]
           ,[parent_rental_unit_id]
           ,[is_available]
           ,[property_manager_id]
           ,[created_at]
           ,[representative_owner_id])

     VALUES
           (@RentalUnitNumberOrder
           ,@RentalUnitName
           ,@RentalUnitSpace
           ,@RentalUnitLocationName
           ,@RentalUnitLatitude
           ,@RentalUnitLongitude
           ,@RentalUnitTypeId
           ,@ParentRentalUnitId
           ,@IsAvailable
           ,@PropertyManagerId
           ,@CreateAt
           ,@RepresentativeOwnerId)


SELECT SCOPE_IDENTITY();
";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@RentalUnitNumberOrder", rentalUnit.RentalUnitNumberOrder);
            command.Parameters.AddWithValue("@RentalUnitName", rentalUnit.RentalUnitName);
            command.Parameters.AddWithValue("@RentalUnitSpace", rentalUnit.RentalUnitSpace);
            command.Parameters.AddWithValue("@RentalUnitLocationName", rentalUnit.RentalUnitLocationName);
            command.Parameters.AddWithValue("@RentalUnitLatitude", rentalUnit.RentalUnitLatitude);
            command.Parameters.AddWithValue("@RentalUnitLongitude", rentalUnit.RentalUnitLongitude);
            command.Parameters.AddWithValue("@RentalUnitTypeId", rentalUnit.RentalUnitTypeId);
            command.Parameters.AddWithValue("@ParentRentalUnitId", (object)rentalUnit.ParentRentalUnitId ?? DBNull.Value);
            command.Parameters.AddWithValue("@IsAvailable", rentalUnit.IsAvailable);
            command.Parameters.AddWithValue("@PropertyManagerId", rentalUnit.PropertyManagerId);
            command.Parameters.AddWithValue("@RepresentativeOwnerId", rentalUnit.RepresentativeOwnerId);
            command.Parameters.AddWithValue("@CreateAt", rentalUnit.CreatedAt);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();
                if (result != null && result != DBNull.Value)
                {
                    rentalUnitId = Convert.ToInt32(result);
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions as needed
            }
            finally
            {
                connection.Close();
            }
            return rentalUnitId;
        }

        public static bool UpdateRentalUnit(int rentalUnitId, RentalUnitDTO rentalUnit)
        {
            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"


UPDATE [dbo].[rental_units]
   SET [rental_unit_number_order] = @RentalUnitNumberOrder
      ,[rental_unit_name] = @RentalUnitName
      ,[rental_unit_space] = @RentalUnitSpace
      ,[rental_unit_location_name] = @RentalUnitLocationName
      ,[rental_unit_location_latitude] = @RentalUnitLatitude
      ,[rental_unit_location_longitude] = @RentalUnitLongitude
      ,[rental_unit_type_id] = @RentalUnitTypeId
      ,[parent_rental_unit_id] = @ParentRentalUnitId
      ,[is_available] = @IsAvailable
      ,[property_manager_id] = @PropertyManagerId
      ,[created_at] = @CreatedAt
      ,[representative_owner_id] = @RepresentativeOwnerId
 WHERE rental_unit_id = @rentalUnitId


";


            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@rentalUnitId", rentalUnitId);
            command.Parameters.AddWithValue("@RentalUnitNumberOrder", rentalUnit.RentalUnitNumberOrder);
            command.Parameters.AddWithValue("@RentalUnitName", rentalUnit.RentalUnitName);
            command.Parameters.AddWithValue("@RentalUnitSpace", rentalUnit.RentalUnitSpace);
            command.Parameters.AddWithValue("@RentalUnitLocationName", rentalUnit.RentalUnitLocationName);
            command.Parameters.AddWithValue("@RentalUnitLatitude", rentalUnit.RentalUnitLatitude);
            command.Parameters.AddWithValue("@RentalUnitLongitude", rentalUnit.RentalUnitLongitude);
            command.Parameters.AddWithValue("@RentalUnitTypeId", rentalUnit.RentalUnitTypeId);
            command.Parameters.AddWithValue("@ParentRentalUnitId", (object)rentalUnit.ParentRentalUnitId ?? DBNull.Value);
            command.Parameters.AddWithValue("@IsAvailable", rentalUnit.IsAvailable);
            command.Parameters.AddWithValue("@PropertyManagerId", rentalUnit.PropertyManagerId);
            command.Parameters.AddWithValue("@RepresentativeOwnerId", rentalUnit.RepresentativeOwnerId);
            command.Parameters.AddWithValue("@CreatedAt", rentalUnit.CreatedAt);

            try
            {
                connection.Open();
                rowsAffected = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                // Handle exceptions as needed
            }
            finally
            {
                connection.Close();

            }
            return rowsAffected > 0;
        }

        public static bool DeleteRentalUnit(int rentalUnitId)
        {

            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"


DELETE FROM [dbo].[rental_units]
      WHERE rental_unit_id = @rentalUnitId


";


            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@rentalUnitId", rentalUnitId);

            try
            {
                connection.Open();
                rowsAffected = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                // Handle exceptions as needed
            }
            finally
            {
                connection.Close();

            }
            return rowsAffected > 0;
        }

        public static bool IsRentalUnitExist(int rentalUnitId)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"select found = 1 from rental_units where rental_unit_id = @rentalUnitId";


            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@rentalUnitId", rentalUnitId);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                isFound = reader.HasRows;
            }
            catch (Exception ex)
            {
                // Handle exceptions as needed
            }
            finally
            {
                connection.Close();

            }
            return isFound;
        }

        public static bool IsRentalUnitAvailable(int rentalUnitId)
        {
            bool isAvailable = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"


           select found = 1 from rental_units where rental_unit_id = @rentalUnitId and is_available = 1


";


            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@rentalUnitId", rentalUnitId);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                isAvailable = reader.HasRows;
            }
            catch (Exception ex)
            {
                // Handle exceptions as needed
            }
            finally
            {
                connection.Close();

            }
            return isAvailable;
        }

        public static List<RentalUnitDTO> ListAllChildRentalUnits(int parentRentalUnitId)
        {

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"SELECT * FROM rental_units where parent_rental_unit_id = @parentRentalUnitId";

            SqlCommand command = new SqlCommand(query, connection);

            RentalUnitDTO rentalUnit = null;
            List<RentalUnitDTO> rentalUnitsList = new List<RentalUnitDTO>();

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        rentalUnit = new RentalUnitDTO
                         (

    reader.GetInt32(reader.GetOrdinal("rental_unit_id")),
    reader.GetInt32(reader.GetOrdinal("rental_unit_number_order")),
    reader.GetString(reader.GetOrdinal("rental_unit_name")),
    reader.GetDecimal(reader.GetOrdinal("rental_unit_space")),
    reader.GetString(reader.GetOrdinal("rental_unit_location_name")),
    reader.GetDecimal(reader.GetOrdinal("rental_unit_location_latitude")),
    reader.GetDecimal(reader.GetOrdinal("rental_unit_location_longitude")),
    reader.GetInt16(reader.GetOrdinal("rental_unit_type_id")),
    reader.IsDBNull(reader.GetOrdinal("parent_rental_unit_id")) ? null : reader.GetInt32(reader.GetOrdinal("parent_rental_unit_id")),
    reader.GetBoolean(reader.GetOrdinal("is_available")),
    reader.GetInt32(reader.GetOrdinal("property_manager_id")),
    reader.GetInt32(reader.GetOrdinal("representative_owner_id")),
    reader.GetDateTime(reader.GetOrdinal("created_at"))

 );
                        rentalUnitsList.Add (rentalUnit);
                    }
                    
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions as needed
            }
            finally
            {
                connection.Close();
            }
            return rentalUnitsList;

        }

        public static List<RentalUnitDTO> ListAllParentRentalUnitsWithPropertyManager(int propertyManagerId)
        {

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"select * from rental_units where parent_rental_unit_id is null and property_manager_id = @propertyManagerId";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue(@"propertyManagerId", propertyManagerId);

            RentalUnitDTO rentalUnit = null;
            List<RentalUnitDTO> rentalUnitsList = new List<RentalUnitDTO>();

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        rentalUnit = new RentalUnitDTO
                         (

    reader.GetInt32(reader.GetOrdinal("rental_unit_id")),
    reader.GetInt32(reader.GetOrdinal("rental_unit_number_order")),
    reader.GetString(reader.GetOrdinal("rental_unit_name")),
    reader.GetDecimal(reader.GetOrdinal("rental_unit_space")),
    reader.GetString(reader.GetOrdinal("rental_unit_location_name")),
    reader.GetDecimal(reader.GetOrdinal("rental_unit_location_latitude")),
    reader.GetDecimal(reader.GetOrdinal("rental_unit_location_longitude")),
    reader.GetInt16(reader.GetOrdinal("rental_unit_type_id")),
    reader.IsDBNull(reader.GetOrdinal("parent_rental_unit_id")) ? null : reader.GetInt32(reader.GetOrdinal("parent_rental_unit_id")),
    reader.GetBoolean(reader.GetOrdinal("is_available")),
    reader.GetInt32(reader.GetOrdinal("property_manager_id")),
    reader.GetInt32(reader.GetOrdinal("representative_owner_id")),
    reader.GetDateTime(reader.GetOrdinal("created_at"))

 );
                        rentalUnitsList.Add(rentalUnit);
                    }

                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions as needed
            }
            finally
            {
                connection.Close();
            }
            return rentalUnitsList;

        }

        public static List<RentalUnitDTO> ShortListAllRentalUnitsWithPropertyManager(int propertyManagerId)
        {

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"select * from rental_units where parent_rental_unit_id is null and property_manager_id = @propertyManagerId";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue(@"propertyManagerId", propertyManagerId);
            RentalUnitDTO rentalUnit = null;
            List<RentalUnitDTO> rentalUnitsList = new List<RentalUnitDTO>();

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        rentalUnit = new RentalUnitDTO
                         (

    reader.GetInt32(reader.GetOrdinal("rental_unit_id")),
    reader.GetInt32(reader.GetOrdinal("rental_unit_number_order")),
    reader.GetString(reader.GetOrdinal("rental_unit_name")),
    reader.GetDecimal(reader.GetOrdinal("rental_unit_space")),
    reader.GetString(reader.GetOrdinal("rental_unit_location_name")),
    reader.GetDecimal(reader.GetOrdinal("rental_unit_location_latitude")),
    reader.GetDecimal(reader.GetOrdinal("rental_unit_location_longitude")),
    reader.GetInt16(reader.GetOrdinal("rental_unit_type_id")),
    reader.IsDBNull(reader.GetOrdinal("parent_rental_unit_id")) ? null : reader.GetInt32(reader.GetOrdinal("parent_rental_unit_id")),
    reader.GetBoolean(reader.GetOrdinal("is_available")),
    reader.GetInt32(reader.GetOrdinal("property_manager_id")),
    reader.GetInt32(reader.GetOrdinal("representative_owner_id")),
    reader.GetDateTime(reader.GetOrdinal("created_at"))

 );
                        rentalUnitsList.Add(rentalUnit);
                    }

                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions as needed
            }
            finally
            {
                connection.Close();
            }
            return rentalUnitsList;

        }

    }
}
