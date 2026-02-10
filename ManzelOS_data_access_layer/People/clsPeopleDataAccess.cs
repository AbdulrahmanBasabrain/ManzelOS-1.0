using System;
using System.Data;
using ManzelOS_DTOs.People;
using Microsoft.Data.SqlClient;

namespace ManzelOS_data_access_layer.PeopleData
{


    public class clsPeopleDataAccess
    {
        
        public static PeopleDTO GetPersonInfoById(int personId)
        {

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"select * from people where person_id = @personId";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@personId", personId);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    
                    PeopleDTO foundPerson = 
                    new PeopleDTO
                        (

                            reader.GetInt32(reader.GetOrdinal("person_id")),
                            reader.GetString(reader.GetOrdinal("first_name")),
                            reader.GetString(reader.GetOrdinal("father_name")),
                            reader.GetString(reader.GetOrdinal("national_id")),
                            reader.GetDateTime(reader.GetOrdinal("date_of_birth")),
                            reader.GetBoolean(reader.GetOrdinal("gender")),
                            reader.GetInt16(reader.GetOrdinal("country_id")),
                            reader.GetInt32(reader.GetOrdinal("city_id")),
                            reader.GetString(reader.GetOrdinal("address_district")),
                            reader.GetString(reader.GetOrdinal("street")),
                            reader.GetString(reader.GetOrdinal("personal_email")),
                            reader.GetString(reader.GetOrdinal("phone")),
                            reader.IsDBNull(reader.GetOrdinal("personal_image_path")) ? null : reader.GetString(reader.GetOrdinal("personal_image_path")),
                            reader.GetInt16(reader.GetOrdinal("marriage_status_id"))

                        );

                    reader.Close();

                    return foundPerson;

                }

                else
                {
                    reader.Close();
                    return null;
                }

            }

            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                return null;
            }

            finally
            {
                connection.Close();
            }


        }

        public static int AddNewPerson(PeopleDTO newPerson)
        {
            int personId = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"INSERT INTO [dbo].[people]
           ([first_name]
           ,[last_name]
           ,[national_id]
           ,[date_of_birth]
           ,[gender]
           ,[country_id]
           ,[city_id]
           ,[district]
           ,[street]
           ,[personal_email]
           ,[phone]
           ,[personal_image_path]
           ,[marriage_status_id]
           ,[created_at])

values

(@firstName, @lastName, @nationalId, @dateOfBirth, @gender, @countryId, @cityId, @addressDistrict, @street, @personalEmail, @phone, @personalImagePath, @marriageStatusId, @createdAt)
                select SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue(@"firstName", newPerson.FirstName);
            command.Parameters.AddWithValue(@"lastName", newPerson.LastName);
            command.Parameters.AddWithValue(@"nationalID", newPerson.NationalId);
            command.Parameters.AddWithValue(@"dateOfBirth", newPerson.DateOfBirth);
            command.Parameters.AddWithValue(@"gender", newPerson.Gender);
            command.Parameters.AddWithValue(@"countryId", newPerson.Country);
            command.Parameters.AddWithValue(@"cityId", newPerson.City);
            command.Parameters.AddWithValue(@"addressDistrict", newPerson.AddressDistrict);
            command.Parameters.AddWithValue(@"street", newPerson.Street);
            command.Parameters.AddWithValue(@"personalEmail", newPerson.PersonalEmail);
            command.Parameters.AddWithValue(@"phone", newPerson.Phone);
            command.Parameters.AddWithValue(@"marriageStatusId", newPerson.MarriageStatusId);
            command.Parameters.AddWithValue(@"createdAt", newPerson.PersonCreatedAt);

            if (newPerson.PersonalImagePath != "")
            {
                command.Parameters.AddWithValue("@personalImagePath", newPerson.PersonalImagePath);
            }
            else
            {
                command.Parameters.AddWithValue("@personalImagePath", System.DBNull.Value);
            }

            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                connection.Close();


                if (result != null && int.TryParse(result.ToString(), out int insertedId))
                {
                    personId = insertedId;
                }


            }
            catch (Exception ex) { }
            finally { connection.Close(); }

            return personId;
        }

        public static bool UpdatePerson(PeopleDTO personToUpdate)
        {
            int rowsAffected = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"UPDATE [dbo].[people]
   SET [first_name] = @firstName
      ,[last_name] = @lastName
      ,[national_id] = @nationalId
      ,[date_of_birth] = @dateOfBirth
      ,[gender] = @gender
      ,[country_id] = @countryId
      ,[city_id] = @cityId
      ,[address_district] = @addressDistrict
      ,[street]
      ,[personal_email] = @personalEmail
      ,[phone] = @phone
      ,[personal_image_path] = @personalImagePath
      ,[marriage_status_id] = @marriageStatusId
      ,[created_at] = @createdAt
 WHERE person_id = @personId";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue(@"personId", personToUpdate.PersonId);
            command.Parameters.AddWithValue(@"firstName", personToUpdate.FirstName);
            command.Parameters.AddWithValue(@"lastName", personToUpdate.LastName);
            command.Parameters.AddWithValue(@"nationalID", personToUpdate.NationalId);
            command.Parameters.AddWithValue(@"dateOfBirth", personToUpdate.DateOfBirth);
            command.Parameters.AddWithValue(@"gender", personToUpdate.Gender);
            command.Parameters.AddWithValue(@"countryId", personToUpdate.Country);
            command.Parameters.AddWithValue(@"cityId", personToUpdate.City);
            command.Parameters.AddWithValue(@"addressDistrict", personToUpdate.AddressDistrict);
            command.Parameters.AddWithValue(@"street", personToUpdate.Street);
            command.Parameters.AddWithValue(@"email", personToUpdate.PersonalEmail);
            command.Parameters.AddWithValue(@"phone", personToUpdate.Phone);
            command.Parameters.AddWithValue(@"marriageStatusId", personToUpdate.MarriageStatusId);
            command.Parameters.AddWithValue(@"createdAt", personToUpdate.PersonCreatedAt);

            if (personToUpdate.PersonalImagePath != "")
            {
                command.Parameters.AddWithValue("@personalImagePath", personToUpdate.PersonalImagePath);
            }
            else
            {
                command.Parameters.AddWithValue("@personalImagePath", System.DBNull.Value);
            }

            try
            {
                connection.Open();
                rowsAffected = command.ExecuteNonQuery();


            }
            catch (Exception ex)
            {
            }
            finally { connection.Close(); }

            return (rowsAffected > 0);
        }

        public static List<PeopleDTO> ListAllPeople()
        {
            
            List<PeopleDTO> peopleList = new List<PeopleDTO>();

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"select * from people;";

            SqlCommand command = new SqlCommand(query, connection);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while(reader.Read())
                    {
                        peopleList.Add(new PeopleDTO 
                        (

                            reader.GetInt32(reader.GetOrdinal("person_id")),
                            reader.GetString(reader.GetOrdinal("first_name")),
                            reader.GetString(reader.GetOrdinal("last_name")),
                            reader.GetString(reader.GetOrdinal("national_id")),
                            reader.GetDateTime(reader.GetOrdinal("date_of_birth")),
                            reader.GetBoolean(reader.GetOrdinal("gender")),
                            reader.GetInt16(reader.GetOrdinal("country_id")),
                            reader.GetInt32(reader.GetOrdinal("city_id")),
                            reader.GetString(reader.GetOrdinal("address_district")),
                            reader.GetString(reader.GetOrdinal("street")),
                            reader.GetString(reader.GetOrdinal("email")),
                            reader.GetString(reader.GetOrdinal("phone")),
                            reader.IsDBNull(reader.GetOrdinal("personal_image_path")) ? null : reader.GetString(reader.GetOrdinal("personal_image_path")),

                            reader.GetInt16(reader.GetOrdinal("marriage_status_id"))

                        ));
                    }
                }

                reader.Close();
            }

            catch (Exception ex) { }
            finally { connection.Close(); }
            return peopleList;

        }

        public static bool DeletePerson(int personID)
        {
            int rowsAffected = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "delete from people where person_id = @personID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@personID", personID);

            try
            {

                connection.Open();
                rowsAffected = command.ExecuteNonQuery();
            }
            catch (Exception ex) { }
            finally { connection.Close(); }

            return (rowsAffected > 0);
        }

        public static bool IsPersonExist(int personID)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "select found = 1 from people where person_id = @personID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@personID", personID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                isFound = reader.HasRows;
                reader.Close();
            }
            catch (Exception ex) { isFound = false; }
            finally { connection.Close(); }

            return isFound;
        }
        
        public static bool IsPersonExistByEmail(string email)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "select found = 1 from people where email = @email";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@email", email);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                isFound = reader.HasRows;
                reader.Close();
            }
            catch (Exception ex) { isFound = false; }
            finally { connection.Close(); }

            return isFound;
        }

        public static bool IsPersonExistByNationalId(int nationalId)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "select found = 1 from people where national_id = @nationalId";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@nationalId", nationalId);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                isFound = reader.HasRows;
                reader.Close();
            }
            catch (Exception ex) { isFound = false; }
            finally { connection.Close(); }

            return isFound;
        }

    }

}
