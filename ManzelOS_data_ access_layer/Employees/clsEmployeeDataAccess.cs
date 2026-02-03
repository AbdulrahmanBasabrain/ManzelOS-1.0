using System;
using System.Data.SqlClient;
using System.Data;
using Microsoft.Data.SqlClient;
using ManzelOS_DTOs.Employees;

namespace ManzelOS_data_access_layer.EmployeesData
{


    public static class clsEmployeeDataAccess
    {




        public static EmployeeDTO GetEmployeeInfoById(int employeeId)
        {


            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "select * from Employees where employee_id = @employeeId";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue(@"employeeId", employeeId);

            EmployeeDTO employee = null;

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {

                    employee = new EmployeeDTO
                        (

                            reader.GetInt32(reader.GetOrdinal("employee_id")),
                            reader.GetInt32(reader.GetOrdinal("person_id")),
                            reader.GetDecimal(reader.GetOrdinal("salary")),
                            reader.GetString(reader.GetOrdinal("job")),
                            reader.GetString(reader.GetOrdinal("organization_email")),
                            reader.GetDateTime(reader.GetOrdinal("created_at"))

                        );
                        
                    reader.Close();
                    return employee;
                }
                else
                {
                    reader.Close();
                    return employee;
                }
            }
            catch (Exception ex)
            {
                return employee;
            }
            finally { connection.Close(); }
        }

        public static int AddNewEmployee(EmployeeDTO employee)
        {
            int employeeId = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"insert into employees (person_id, salary, job, organization_email, created_at) values (@personId, @salary, @job,  @organizationEmail, @createdAt) select SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue(@"personId", employee.PersonId);
            command.Parameters.AddWithValue(@"salary", employee.Salary);
            command.Parameters.AddWithValue(@"job", employee.Job);
            command.Parameters.AddWithValue(@"organizationEmail", employee.OrganizationEmail);
            command.Parameters.AddWithValue(@"createdAt", employee.CreatedAt);


            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                connection.Close();

                if (result != null && int.TryParse(result.ToString(), out int insertedId))
                {
                    employeeId = insertedId;
                }
            }

            catch (Exception ex) { }
            finally { connection.Close(); }
            return employeeId;
        }

        public static bool UpdateEmployee(int employeeId, EmployeeDTO employee)
        {

            int rowsAffected = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"UPDATE [dbo].[employees]
   SET [person_id] = @personId
      ,[salary] = @salary
      ,[job] = @job
     , [organization_email] = @organizationEmail
     ,[created_at] = @createdAt

 WHERE employee_id = @employeeId";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue(@"employeeId", employeeId);
            command.Parameters.AddWithValue(@"personId", employee.PersonId);
            command.Parameters.AddWithValue(@"salary", employee.Salary);
            command.Parameters.AddWithValue(@"job", employee.Job);
            command.Parameters.AddWithValue(@"organizationEmail", employee.OrganizationEmail);
            command.Parameters.AddWithValue(@"createdAt", employee.CreatedAt);

            try
            {
                connection.Open();

                rowsAffected = command.ExecuteNonQuery();
            }
            catch (Exception ex) { }
            finally { connection.Close(); }

            return (rowsAffected > 0);
        }

        public static List<EmployeeDTO> ListAllEmployees()
        {

            List<EmployeeDTO> employees = new List<EmployeeDTO>();

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"select * from employees";

            SqlCommand command = new SqlCommand(@query, connection);

            EmployeeDTO employee = null;

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    
                    while(reader.Read())
                    {
                    employee = new EmployeeDTO
                        (

                            reader.GetInt32(reader.GetOrdinal("employee_id")),
                            reader.GetInt32(reader.GetOrdinal("person_id")),
                            reader.GetDecimal(reader.GetOrdinal("salary")),
                            reader.GetString(reader.GetOrdinal("job")),
                            reader.GetString(reader.GetOrdinal("organization_email")),
                            reader.GetDateTime(reader.GetOrdinal("created_at"))

                        );

                        employees.Add( employee );

                    }
                }

                reader.Close();
            }
            catch (Exception ex) { }
            finally { connection.Close(); }
            return employees;
        }

        public static bool IsEmployeeExist(int employeeId)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"select found = 1 from employees where employee_id = @employeeId";

            bool isFound = false;

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@employeeId", employeeId);

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

        public static bool DeleteEmployee(int employeeId)
        {

            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"delete from employees where employee_id = @employeeId";

            SqlCommand command = new SqlCommand(@query, connection);

            command.Parameters.AddWithValue(@"employeeId", employeeId);

            try
            {
                connection.Open();
                rowsAffected = command.ExecuteNonQuery();
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
            finally { connection.Close(); }
            return (rowsAffected > 0);

        }


    }
}
