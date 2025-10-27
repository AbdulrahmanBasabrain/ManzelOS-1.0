using System;
using System.Data.SqlClient;
using System.Data;
using Microsoft.Data.SqlClient;

namespace ManzelOS_data_access_layer.EmployeesData
{

    public class EmployeeDTO
    {


        public int EmployeeId { get; set; }
        public int PersonId { get; set; }
        public decimal Salary { get; set; }
        public string Job { get; set; }
        public DateTime CreatedAt { get; set; }


        public EmployeeDTO(int employeeId, int personId, decimal salary, string job, DateTime createdAt)
        {

            EmployeeId = employeeId;
            PersonId = personId;
            Salary = salary;
            Job = job;
            CreatedAt = createdAt;

        }

    }

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
                reader.Close();
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
            string query = @"insert into employees (person_id, salary, job, created_at) values (@PersonId, @Salary, @Job, @CreatedAt) select SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue(@"PersonId", employee.PersonId);
            command.Parameters.AddWithValue(@"Salary", employee.Salary);
            command.Parameters.AddWithValue(@"Job", employee.Job);
            command.Parameters.AddWithValue(@"CreatedAt", employee.CreatedAt);


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
   SET [person_id] = @PersonId
      ,[salary] = @Salary
      ,[job] = @Job
     ,[created_at] = @CreatedAt
 WHERE employee_id = @employeeId";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue(@"employeeId", employeeId);
            command.Parameters.AddWithValue(@"PersonId", employee.PersonId);
            command.Parameters.AddWithValue(@"Salary", employee.Salary);
            command.Parameters.AddWithValue(@"Job", employee.Job);
            command.Parameters.AddWithValue(@"CreatedAt", employee.CreatedAt);

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
