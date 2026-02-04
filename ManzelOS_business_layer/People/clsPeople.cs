using System;
using System.Data;
using ManzelOS_DTOs.People;
using ManzelOS_data_access_layer.PeopleData;

namespace ManzelOS_business_layer
{
    public class clsPeople
    {

        public enum enMode { _addNewPerson = 1, _updatePerson = 2 };
        private enMode _Mode = enMode._addNewPerson; /*Default Value*/

        public int PersonId { get; set; }

        public string FirstName { get; set; }

        public string FatherName { get; set; }

        public string GrandFatherName { get; set; }

        public string LastName { get; set; }

        public int NationalId { get; set; }

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
        
        public PeopleDTO PersonDTO
        {
            get 
            {
                return new PeopleDTO
            (
                    this.PersonId, 
                    this.FirstName, 
                    this.FatherName, 
                    this.GrandFatherName, 
                    this.LastName, 
                    this.NationalId,
                    this.DateOfBirth, 
                    this.Gender, 
                    this.CountryId, 
                    this.CityId, 
                    this.AddressDistrict, 
                    this.PersonalEmail, 
                    this.Phone, 
                    this.PersonalImagePath,
                    this.MarriageStatusId,
                    this.CreatedAt
            ); 
            } 
        }

        public clsPeople()
        {

            PersonId = -1;
            FirstName = string.Empty;
            FatherName = string.Empty;
            GrandFatherName = string.Empty;
            LastName = string.Empty;
            NationalId = -1;
            DateOfBirth = DateTime.Now;
            Gender = false;
            CountryId = -1;
            CityId = -1;
            AddressDistrict = string.Empty;
            PersonalEmail = string.Empty;
            Phone = string.Empty;
            PersonalImagePath = string.Empty;
            MarriageStatusId = -1;
            CreatedAt = DateTime.Now;

            _Mode = enMode._addNewPerson;
        }

        public clsPeople(PeopleDTO personDTO, enMode personMode = enMode._addNewPerson )
        {

            PersonId = personDTO.PersonId;
            FirstName = personDTO.FirstName;
            FatherName = personDTO.FatherName;
            GrandFatherName = personDTO.GrandFatherName;
            LastName = personDTO.LastName;
            NationalId = personDTO.NationalID;
            DateOfBirth = personDTO.DateOfBirth;
            Gender = personDTO.Gender;
            CountryId = personDTO.CountryId;
            CityId = personDTO.CityId;
            AddressDistrict = personDTO.AddressDistrict;
            PersonalEmail = personDTO.PersonalEmail;
            Phone = personDTO.Phone;
            PersonalImagePath = personDTO.PersonalImagePath;
            MarriageStatusId = personDTO.MarriageStatusId;
            CreatedAt = personDTO.CreatedAt;

            _Mode = personMode;

        }

        public static clsPeople FindPersonById(int personId)
        {

            PeopleDTO person = clsPeopleDataAccess.GetPersonInfoById(personId);

            if (person != null)
            {
                return new clsPeople(person, enMode._updatePerson);
            }

            else
            {
                return null;
            }

        }

        public bool _AddNewPerson()
        {
            this.PersonId = clsPeopleDataAccess.AddNewPerson(PersonDTO);

            if (this.PersonId != -1)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public bool _UpdatePerson()
        {
            return clsPeopleDataAccess.UpdatePerson(PersonDTO);
        }

        public static List<PeopleDTO> ListAllPeople()
        {
            return clsPeopleDataAccess.ListAllPeople();
        }

        public bool Save()
        {
            switch (_Mode)
            {
                case enMode._addNewPerson:
                    if (_AddNewPerson())
                    {
                        _Mode = enMode._updatePerson;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode._updatePerson:
                    return _UpdatePerson();

            }

            return false;
        }

        public static bool IsPersonExist(int personId)
        {
            return clsPeopleDataAccess.IsPersonExist(personId);
        }
        
        public static bool IsPersonExistByEmail(string email)
        {
            return clsPeopleDataAccess.IsPersonExistByEmail(email);
        }
        
        public static bool DeletePerson(int personId)
        {
            return clsPeopleDataAccess.DeletePerson(personId);
        }


    }

}
