using Microsoft.Data.SqlClient;

namespace ManzelOS_data_access_layer.RentalUnitsData.RentalUnitsData
{

    public class AssignEmployeeToPropertyDTO
    {

        public int AssignmentId { get; set; }
        public int EmployeeId { get; set; }
        public int RentalUnitID { get; set; }
        public DateTime AssignmentDate { get; set; }

        public AssignEmployeeToPropertyDTO(int assignmentId, int employeeId, int rentalUnitId, DateTime assignmentDate)
        {

            AssignmentId = assignmentId;
            EmployeeId = employeeId;
            RentalUnitID = rentalUnitId;
            AssignmentDate = assignmentDate;

        }

    }

    public static class clsRentalUnitAssignedToEmployeeDataAccess
    {



        public static AssignEmployeeToPropertyDTO FindEmployeeRentalUnitAssignment(int assignmentId)
        {

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"select * from property_employee_assignments where property_employee_assignment_id = @assignmentId";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue(@"assignmentId", assignmentId);

            try
            {

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                AssignEmployeeToPropertyDTO assignEmployeeToProperty = null;
                if(reader.HasRows && reader.Read())
                {


                    assignEmployeeToProperty = new AssignEmployeeToPropertyDTO
                        (

                            reader.GetInt32(reader.GetOrdinal("property_employee_assignment_id")),
                            reader.GetInt32(reader.GetOrdinal("rental_unit_id")),
                            reader.GetInt32(reader.GetOrdinal("employee_id")),
                            reader.GetDateTime(reader.GetOrdinal("date_of_assignment"))

                        );

                    return assignEmployeeToProperty;

                }
                else
                {
                    return null;
                }

            }
            catch (Exception ex) { return null; }
            finally { connection.Close(); }
        }

        public static int AssignEmployeeToProperty(AssignEmployeeToPropertyDTO assignEmployeeToProperty)
        {

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            int assignmentId = -1;

            string query = @"INSERT INTO [dbo].[property_employee_assignments]
           ([rental_unit_id]
           ,[employee_id]
           ,[date_of_assignment])
     VALUES
           (@RentalUnitId
           ,@EmployeeId
           ,@AssignedAt)
 select SCOPE_IDENTITY();
";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue(@"EmployeeId", assignEmployeeToProperty.EmployeeId);
            command.Parameters.AddWithValue(@"RentalUnitId", assignEmployeeToProperty.RentalUnitID);
            command.Parameters.AddWithValue(@"AssignedAt", assignEmployeeToProperty.AssignmentId);

            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                connection.Close();


                if (result != null && int.TryParse(result.ToString(), out int insertedId))
                {
                    assignmentId = insertedId;
                }
            }
            catch { Exception ex; }
            finally { connection.Close(); }

            return assignmentId;
        }

        public static bool ChangePropertyEmployee(int assignmentId, AssignEmployeeToPropertyDTO assignEmployeeToProperty)
        {


            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            int rowsAffected = 0;
            string query = @"

			UPDATE [dbo].[property_employee_assignments]
			   SET [rental_unit_id] = @RentalUnitID
				  ,[employee_id] = @EmployeeId
				  ,[date_of_assignment] = @AssignmentDate
			 WHERE property_employee_assignment_id = @assignmentId ";


            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue(@"assignmentId", assignmentId);
            command.Parameters.AddWithValue(@"RentalUnitID", assignEmployeeToProperty.RentalUnitID);
            command.Parameters.AddWithValue(@"EmployeeId", assignEmployeeToProperty.EmployeeId);
            command.Parameters.AddWithValue(@"AssignmentDate", assignEmployeeToProperty.AssignmentDate);

            try
            {
                connection.Open();
                rowsAffected = command.ExecuteNonQuery();
            }
            catch (Exception ex) { }
            finally { connection.Close(); }
            
            return (rowsAffected > 0);  

        }

        public static bool DeleteAssignment(int assignmentId)
        {

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            
            int rowsAffected = 0;

            string query = @" DELETE FROM [dbo].[property_employee_assignments] WHERE property_employee_assignment_id = @assignmentId";
            
            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue(@"assignmentId", assignmentId);

            try
            {
                connection.Open();
                rowsAffected = command.ExecuteNonQuery();

            }
            catch (Exception ex) { } finally { connection.Close(); }

            return (rowsAffected > 0);

        }

        
        public static List<AssignEmployeeToPropertyDTO>ListEmployeeRentalUnitRelations()
        {

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"select * from property_employee_assignments";
            List<AssignEmployeeToPropertyDTO> assignmentsList = new List<AssignEmployeeToPropertyDTO>();

            SqlCommand comand = new SqlCommand(query, connection);
            AssignEmployeeToPropertyDTO assignment = null;

            try
            {
                connection.Open();
                SqlDataReader reader = comand.ExecuteReader();
                if(reader.HasRows)
                {
                    while(reader.Read())
                    {

                        assignment = new AssignEmployeeToPropertyDTO
                            (

                                reader.GetInt32(reader.GetOrdinal("property_employee_assignment_id")),
                                reader.GetInt32(reader.GetOrdinal("rental_unit_id")),
                                reader.GetInt32(reader.GetOrdinal("employee_id")),
                                reader.GetDateTime(reader.GetOrdinal("date_of_assignment"))

                            );

                        assignmentsList.Add(assignment);                        
                    }
                }
            }catch { Exception ex; }
            finally { connection.Close(); }
            return assignmentsList;

        }


    }
}
