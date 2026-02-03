using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManzelOS_DTOs.People
{
    public class PeopleDTO
        {
            public int PersonId { get; set; }
            public string FirstName { get; set; }
            public string FatherName { get; set; }
            public string GrandFatherName { get; set; }
            public string LastName { get; set; }
            public int NationalID { get; set; }
            
            public DateTime DateOfBirth { get; set; }

            public bool Gender { get; set; }

            public short CountryId { get; set; }

            public int CityId { get; set; }

            public string AddressDistrict { get; set; }

            public string PersonalEmail { get; set; }

            public string Phone { get; set; }

            public string PersonalImagePath { get; set; }

            public short MarriageStatusId { get; set; }

            public DateTime CreatedAt { get; set; }

            public PeopleDTO(int personId, string firstName, string fatherName, string grandFatherName, string lastName, int nationalId, DateTime dateOfBirth, bool gender, short countryId, int cityId, string addressDistrict,
            string personalEmail, string phone, string personalImagePath, short marriageStatusId, DateTime createdAt)
            {

                PersonId = personId;
                FirstName = firstName;
                FatherName = fatherName;
                GrandFatherName = grandFatherName;
                LastName = lastName;
                NationalID = nationalId;
                DateOfBirth = dateOfBirth;
                Gender = gender;
                CountryId = countryId;
                CityId = cityId;
                AddressDistrict = addressDistrict;
                PersonalEmail = personalEmail;
                Phone = phone;
                PersonalImagePath = personalImagePath;
                MarriageStatusId = marriageStatusId;
                CreatedAt = createdAt;

            }



        }
}
