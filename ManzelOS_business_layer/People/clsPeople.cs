using ManzelOS_data_access_layer.PeopleData;
using ManzelOS_DTOs.People;

namespace ManzelOS_business_layer.People
{
    public abstract class clsPeople
    {

        public enum enMode { _addNewPerson = 1, _updatePerson = 2 };
        private enMode _Mode = enMode._addNewPerson; /*Default Value*/

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

        public DateTime PersonCreatedAt { get; set; }

        public PeopleDTO PersonDTO
        {
            get
            {
                return new PeopleDTO
            (
                    this.PersonId,
                    this.FirstName,
                    this.LastName,
                    this.NationalId,
                    this.DateOfBirth,
                    this.Gender,
                    this.Country,
                    this.City,
                    this.AddressDistrict,
                    this.Street,
                    this.PersonalEmail,
                    this.Phone,
                    this.PersonalImagePath,
                    this.MarriageStatusId
            );
            }
        }


        public static PeopleDTO FindPersonById(int personId)
        {

            PeopleDTO person = clsPeopleDataAccess.GetPersonInfoById(personId);

            if (person != null)
            {
                return person;
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
