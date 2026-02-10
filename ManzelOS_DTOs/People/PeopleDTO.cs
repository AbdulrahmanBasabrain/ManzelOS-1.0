namespace ManzelOS_DTOs.People
{
    public class PeopleDTO
        {
            public int PersonId { get; set; }
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

            public PeopleDTO(int personId, string firstName, string lastName, string nationalId, DateTime dateOfBirth, bool gender, short country, int city, string addressDistrict,
            string street, string personalEmail, string phone, string personalImagePath, short marriageStatusId)
            {

                PersonId = personId;                
                FirstName = firstName;
                LastName = lastName;
                NationalId = nationalId;
                DateOfBirth = dateOfBirth;
                Gender = gender;
                Country = country;
                City = city;
                AddressDistrict = addressDistrict;
                Street = street;
                PersonalEmail = personalEmail;
                Phone = phone;
                PersonalImagePath = personalImagePath;
                MarriageStatusId = marriageStatusId;
                PersonCreatedAt = DateTime.Now;

            }



        }
}
