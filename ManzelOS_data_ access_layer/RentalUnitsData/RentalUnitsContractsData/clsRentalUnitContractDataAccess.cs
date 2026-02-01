using System;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Data.SqlClient;

namespace ManzelOS_data_access_layer.RentalUnitsData.RentalUnitsContractsData
{


    public class RentalUnitContractDTO
    {

        public int RentalUnitContractId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int RepresentativeOwnerId { get; set; }
        public int TenantId { get; set; }
        public int billingPeriodTypeId { get; set; }
        public int RentalUnitId { get; set; }
        public bool IsActive { get; set; }
        public decimal RentalAmount { get; set; }
        public int BillTypeId { get; set; }
        public DateTime CreatedAt { get; set; }

        public RentalUnitContractDTO(int rentalUnitContractId, DateTime startDate, DateTime endDate, int representativeOwnerId, int tenantId, int billingPeriodTypeId,
            int rentalUnitId, bool isActive, decimal rentalAmount, int billTypeId, DateTime createdAt) 
        {

            RentalUnitContractId = rentalUnitContractId;
            StartDate = startDate;
            EndDate = endDate;
            RepresentativeOwnerId = representativeOwnerId;
            TenantId = tenantId;
            this.billingPeriodTypeId = billingPeriodTypeId;
            RentalUnitId = rentalUnitId;
            IsActive = isActive;
            RentalAmount = rentalAmount;
            BillTypeId = billTypeId;
            CreatedAt = createdAt;

        }

    }

    public static class clsRentalUnitContractDataAccess
    {

        public static RentalUnitContractDTO GetRentalUnitContractInfoById(int rentalUnitContractId)
        {

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"select * from rental_contracts where rental_contract_id = @RentalUnitContractId";
            SqlCommand command = new SqlCommand(query, connection);
            RentalUnitContractDTO rentalUnitContract = null;

            command.Parameters.AddWithValue("@rentalUnitContractId", rentalUnitContractId);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    rentalUnitContract = new RentalUnitContractDTO
                    (

                        reader.GetInt32(reader.GetOrdinal("rental_contract_id")),
                        reader.GetDateTime(reader.GetOrdinal("rent_start_date")),
                        reader.GetDateTime(reader.GetOrdinal("rent_end_date")),
                        reader.GetInt32(reader.GetOrdinal("representative_owner_id_at_the_time")),
                        reader.GetInt32(reader.GetOrdinal("tenant_id")),
                        reader.GetInt32(reader.GetOrdinal("billing_period_type_id")),
                        reader.GetInt32(reader.GetOrdinal("rental_unit_id")),
                        reader.GetBoolean(reader.GetOrdinal("is_active")),
                        reader.GetDecimal(reader.GetOrdinal("rent_amount")),
                        reader.GetInt32(reader.GetOrdinal("bill_type_id")),
                        reader.GetDateTime(reader.GetOrdinal("created_at"))

                    );
                    
                    reader.Close();
                    return rentalUnitContract;
                }
                else
                {
                    reader.Close();
                    return rentalUnitContract;
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                // Handle exception (log it, rethrow it, etc.)
                return rentalUnitContract;
            }
            finally
            {
                connection.Close();
            }
        }

        public static RentalUnitContractDTO GetRentalUnitContractInfoByRentalId(int rentalUnitId)
        {

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"select * from rental_contracts where rental_unit_id = @rentalUnitId";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@rentalUnitId", rentalUnitId);
            RentalUnitContractDTO rentalUnitContract = null;


            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    rentalUnitContract = new RentalUnitContractDTO
                    (

                        reader.GetInt32(reader.GetOrdinal("rental_unit_contract")),
                        reader.GetDateTime(reader.GetOrdinal("rent_start_date")),
                        reader.GetDateTime(reader.GetOrdinal("rent_end_date")),
                        reader.GetInt32(reader.GetOrdinal("representative_owner_id_at_the_time")),
                        reader.GetInt32(reader.GetOrdinal("tenant_id")),
                        reader.GetInt32(reader.GetOrdinal("billing_period_type_id")),
                        reader.GetInt32(reader.GetOrdinal("rental_unit_id")),
                        reader.GetBoolean(reader.GetOrdinal("is_active")),
                        reader.GetDecimal(reader.GetOrdinal("rent_amount")),
                        reader.GetInt32(reader.GetOrdinal("bill_type_id")),
                        reader.GetDateTime(reader.GetOrdinal("created_at"))

                    );

                    reader.Close();
                    return rentalUnitContract;
                }
                else
                {
                    reader.Close();
                    return rentalUnitContract;
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                // Handle exception (log it, rethrow it, etc.)
                return rentalUnitContract;
            }
            finally
            {
                connection.Close();
            }
        }

        public static List<RentalUnitContractDTO>  ListAllRentalUnitContracts()
        {

            List<RentalUnitContractDTO> rentalUnitsContractsDataList = new List<RentalUnitContractDTO>();

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"select * from rental_contracts";

            SqlCommand command = new SqlCommand(query, connection);
            RentalUnitContractDTO rentalUnitContract = null;

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {

                    while (reader.Read())
                    {
                        rentalUnitContract = new RentalUnitContractDTO
                                (

                                    reader.GetInt32(reader.GetOrdinal("rental_contract_id")),
                                    reader.GetDateTime(reader.GetOrdinal("rent_start_date")),
                                    reader.GetDateTime(reader.GetOrdinal("rent_end_date")),
                                    reader.GetInt32(reader.GetOrdinal("representative_owner_id_at_the_time")),
                                    reader.GetInt32(reader.GetOrdinal("tenant_id")),
                                    reader.GetInt32(reader.GetOrdinal("billing_period_type_id")),
                                    reader.GetInt32(reader.GetOrdinal("rental_unit_id")),
                                    reader.GetBoolean(reader.GetOrdinal("is_active")),
                                    reader.GetDecimal(reader.GetOrdinal("rent_amount")),
                                    reader.GetInt32(reader.GetOrdinal("bill_type_id")),
                                    reader.GetDateTime(reader.GetOrdinal("created_at"))

                                );
                        rentalUnitsContractsDataList.Add (rentalUnitContract);  
                    }
                    reader.Close();
                }
                    
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return rentalUnitsContractsDataList; // Placeholder return value

        }

        public static int AddNewRentalUnitContract(RentalUnitContractDTO rentalUnitContract)
        {

            int rentalUnitContractId = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"

INSERT INTO [dbo].[rental_contracts]
           ([rent_start_date]
           ,[rent_end_date]
           ,[representative_owner_id_at_the_time]
           ,[tenant_id]
           ,[billing_period_type_id]
           ,[rental_unit_id]
           ,[is_active]
           ,[rent_amount]
           ,[bill_type_id]
           ,[created_at])
     VALUES
           (@StartDate
           ,@EndDate
           ,@RepresentativeOwnerId
           ,@TenantId
           ,@BillingPeriodTypeId
           ,@RentalUnitId
           ,@IsActive
           ,@RentalAmount
           ,@BillTypeId
           ,@CreatedAt);

                select SCOPE_IDENTITY();

";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@StartDate", rentalUnitContract.StartDate);
            command.Parameters.AddWithValue("@EndDate", rentalUnitContract.EndDate);
            command.Parameters.AddWithValue("@RepresentativeOwnerId", rentalUnitContract.RepresentativeOwnerId);
            command.Parameters.AddWithValue("@TenantId", rentalUnitContract.TenantId);
            command.Parameters.AddWithValue("@BillingPeriodTypeId", rentalUnitContract.billingPeriodTypeId);
            command.Parameters.AddWithValue("@RentalUnitId", rentalUnitContract.RentalUnitId);
            command.Parameters.AddWithValue("@IsActive", rentalUnitContract.IsActive);
            command.Parameters.AddWithValue("@RentalAmount", rentalUnitContract.RentalAmount);
            command.Parameters.AddWithValue("@BillTypeId", rentalUnitContract.BillTypeId);
            command.Parameters.AddWithValue("@CreatedAt", rentalUnitContract.CreatedAt);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out int insertedId))
                {
                    rentalUnitContractId = insertedId;
                }
            }
            catch (Exception ex) { }
            finally { connection.Close(); }



            return rentalUnitContractId;

        }

        public static bool UpdateRentalUnitContract(int rentalUnitContractId, RentalUnitContractDTO rentalUnitContract)
        {


            int rowsAffected = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"




UPDATE [dbo].[rental_contracts]
   SET [rent_start_date] = @StartDate
      ,[rent_end_date] = @EndDate
      ,[representative_owner_id_at_the_time] = @RepresentativeOwnerId
      ,[tenant_id] = @TenantId
      ,[billing_period_type_id] = @BillingPeriodTypeId
      ,[rental_unit_id] = @RentalUnitId
      ,[is_active] = @IsActive
      ,[rent_amount] = @RentalAmount
      ,[bill_type_id] = @BillTypeId
      ,[created_at] = @CreatedAt
 WHERE [rental_contract_id] = @rentalUnitContractId;


";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@rentalUnitContractId", rentalUnitContractId);
            command.Parameters.AddWithValue("@StartDate", rentalUnitContract.StartDate);
            command.Parameters.AddWithValue("@EndDate", rentalUnitContract.EndDate);
            command.Parameters.AddWithValue("@RepresentativeOwnerId", rentalUnitContract.RepresentativeOwnerId);
            command.Parameters.AddWithValue("@TenantId", rentalUnitContract.TenantId);
            command.Parameters.AddWithValue("@BillingPeriodTypeId", rentalUnitContract.billingPeriodTypeId);
            command.Parameters.AddWithValue("@RentalUnitId", rentalUnitContract.RentalUnitId);
            command.Parameters.AddWithValue("@IsActive", rentalUnitContract.IsActive);
            command.Parameters.AddWithValue("@RentalAmount", rentalUnitContract.RentalAmount);
            command.Parameters.AddWithValue("@BillTypeId", rentalUnitContract.BillTypeId);
            command.Parameters.AddWithValue("@CreatedAt", rentalUnitContract.CreatedAt);


            try
            {
                connection.Open();
                
                rowsAffected = command.ExecuteNonQuery();

            }
            catch (Exception ex) { }
            finally { connection.Close(); }


            return (rowsAffected > 0);

        }

        public static bool DeleteRentalUnitContract(int rentalUnitContractId)
        {

            int rowsAffected = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"

        delete from [dbo].[rental_contracts] 
        where [rental_contract_id] = @rentalUnitContractId;


";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@rentalUnitContractId", rentalUnitContractId);

            try
            {
                connection.Open();

                rowsAffected = command.ExecuteNonQuery();

            }
            catch (Exception ex) { }
            finally { connection.Close(); }



            return (rowsAffected > 0);
        }

        public static bool IsRentalUnitContractExists(int rentalUnitContractId)
        {
            bool isExists = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"select  found = 1 from rental_contracts where rental_contract_id = @rentalUnitContractId";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@rentalUnitContractId", rentalUnitContractId);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                isExists = reader.HasRows;
            }catch (Exception ex) { }
            finally
            {
                connection.Close();
            }
            return isExists;
        }

        public static bool IsRentalUnitContractExistsByRentalUnitId(int rentalUnitId)
        {
            bool isExists = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"select  found = 1 from rental_contracts where rental_unit_id = @rentalUnitId";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@rentalUnitId", rentalUnitId);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                isExists = reader.HasRows;
            }catch (Exception ex) { }
            finally
            {
                connection.Close();
            }
            return isExists;
        }

        public static bool IsRentalUnitContractActive(int rentalUnitContractId)
        {
            bool isActive = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"select  active = 1 from rental_contracts where rental_contract_id = @rentalUnitId";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@rentalUnitId", rentalUnitContractId);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                isActive = reader.HasRows;
            }catch (Exception ex) { }
            finally
            {
                connection.Close();
            }
            return isActive;
        }

    }
}
