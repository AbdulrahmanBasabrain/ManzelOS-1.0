using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ManzelOS_DTOs.Employees;
using ManzelOS_DTOs.People;

namespace ManzelOS_DTOs.PropertyManagers
{

    public class PropertyManagerPersonDTO
    {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string NationalId { get; set; }
            
            public DateTime DateOfBirth { get; set; }

            public bool Gender { get; set; }

            public short Country { get; set; }

            public int City { get; set; }

            public string AddressDistrict { get; set; }
            public string Street { get; set; }

            public string PersonalEmail { get; set; }

            public string Phone { get; set; }

            public string PersonalImagePath { get; set; }

            public short MarriageStatusId { get; set; }
            public DateTime PersonCreatedAt { get; set; } = DateTime.Now;
            
            public string UserName { get; set; }
            public string Password { get; set; }

 

        public PropertyManagerPersonDTO(string firstName, string lastName, string nationalId, DateTime dateOfBirth, bool gender, short country, int city, string AddressDistrict, string street,string personalEmail,
            string phone, string personalImagePath, short marriageStatusId, string userName, string password)
        {

            this.FirstName = firstName;
            this.LastName = lastName;
            this.NationalId = nationalId;
            this.DateOfBirth = dateOfBirth;
            this.Gender = gender;
            this.Country = country;
            this.City = city;
            this.AddressDistrict = AddressDistrict;
            this.Street = street;
            this.PersonalEmail = personalEmail;
            this.Phone = phone;
            this.PersonalImagePath = personalImagePath;
            this.MarriageStatusId = marriageStatusId;

            this.UserName = userName;
            this.Password = password;

        }
    }

}
