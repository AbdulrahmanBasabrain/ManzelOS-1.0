using System.Security.Cryptography;
using System.Text;
using ManzelOS_DTOs.Employees;
using ManzelOS_business_layer.People;
using ManzelOS_data_access_layer.EmployeesData;
using ManzelOS_DTOs.PropertyManagers;
using ManzelOS_DTOs.People;



namespace ManzelOS_business_layer
{
    public class clsPropertyManager : clsPeople
    {

        public enum enMode { enAddNewPropertyManager = 1, enUpdateNewPropertyManager = 2 }
        private enMode _Mode;
        public int PropertyManagerId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public DateTime PropertyMangerCreatedAt { get; set; }

        PropertyManagerDTO propertyManagerDTO { get { return new PropertyManagerDTO(this.PropertyManagerId, this.PersonId, this.UserName, this.Password); } }
        PropertyManagerPersonDTO propertyManagerPersonDTO
        {
            get
            {
                return new
                    PropertyManagerPersonDTO
                    (
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
                    this.MarriageStatusId,
                    this.UserName,
                    this.Password
                    );
            }
        }

        public clsPropertyManager()
        {

            PropertyManagerId = -1;
            UserName = string.Empty;
            Password = string.Empty;
            PropertyMangerCreatedAt = DateTime.Now;

            PersonId = -1;
            FirstName = string.Empty;
            LastName = string.Empty;
            NationalId = string.Empty;
            DateOfBirth = DateTime.Now;
            Gender = false;
            Country = -1;
            City = -1;
            AddressDistrict = string.Empty;
            PersonalEmail = string.Empty;
            Phone = string.Empty;
            PersonalImagePath = string.Empty;
            MarriageStatusId = -1;
            PersonCreatedAt = DateTime.Now;


            _Mode = enMode.enAddNewPropertyManager;

        }

        public clsPropertyManager(PropertyManagerPersonDTO propertyManagerDTO, enMode mode = enMode.enAddNewPropertyManager)
        {

            FirstName = propertyManagerDTO.FirstName;
            this.PersonDTO.FirstName = propertyManagerDTO.FirstName;
            LastName = propertyManagerDTO.LastName;
            this.PersonDTO.LastName = propertyManagerDTO.LastName;
            NationalId = propertyManagerDTO.NationalId;
            this.PersonDTO.NationalId = propertyManagerDTO.NationalId;
            DateOfBirth = propertyManagerDTO.DateOfBirth;
            this.PersonDTO.DateOfBirth = propertyManagerDTO.DateOfBirth;
            Gender = propertyManagerDTO.Gender;
            this.PersonDTO.Gender = propertyManagerDTO.Gender;
            Country = propertyManagerDTO.Country;
            this.PersonDTO.Country = propertyManagerDTO.Country;
            City = propertyManagerDTO.City;
            this.PersonDTO.City = propertyManagerDTO.City;
            AddressDistrict = propertyManagerDTO.AddressDistrict;
            this.PersonDTO.AddressDistrict = propertyManagerDTO.AddressDistrict;
            Street = propertyManagerDTO.Street;
            this.PersonDTO.Street = propertyManagerDTO.Street;
            PersonalEmail = propertyManagerDTO.PersonalEmail;
            this.PersonalEmail = propertyManagerDTO.PersonalEmail;
            Phone = propertyManagerDTO.Phone;
            this.PersonDTO.Phone = propertyManagerDTO.Phone;
            PersonalImagePath = propertyManagerDTO.PersonalImagePath;
            this.PersonDTO.PersonalImagePath = propertyManagerDTO.PersonalImagePath;
            MarriageStatusId = propertyManagerDTO.MarriageStatusId;
            this.PersonDTO.MarriageStatusId = propertyManagerDTO.MarriageStatusId;
            PersonCreatedAt = DateTime.Now;

            UserName = propertyManagerDTO.UserName;
            this.propertyManagerDTO.UserName = propertyManagerDTO.UserName;
            Password = propertyManagerDTO.Password;
            this.propertyManagerDTO.Password = propertyManagerDTO.Password;
            PropertyMangerCreatedAt = DateTime.Now;


            _Mode = mode;

        }

        public static PropertyManagerPersonDTO FindFullPropertyManagerById(int propertyManagerId)
        {

            PropertyManagerPersonDTO propertyManagerPersonDTO = clsPropertyManagerDataAccess.GetFullPropertyManagerInfoById(propertyManagerId);

            if (propertyManagerPersonDTO != null)
            {

                return propertyManagerPersonDTO;
            }
            else
            {
                return null;
            }
        }

        public static PropertyManagerDTO FindPropertyManagerByUserName(string userName)
        {

            PropertyManagerDTO propertyManagerDTO = clsPropertyManagerDataAccess.GetPrivatePropertyManagerInfoByUserName(userName);

            if (propertyManagerDTO != null)
            {

                return propertyManagerDTO;
            }
            else
            {
                return null;
            }

        }

        public static List<PropertyManagerDTO> ListAllPropertyManagers()
        {
            return clsPropertyManagerDataAccess.ListAllPropertyManagers();
        }

        public bool _AddNewPropertyManager()
        {
            if (base.Save())
            {
                this.propertyManagerDTO.PersonID = this.PersonId;
                this.PropertyManagerId = clsPropertyManagerDataAccess.AddNewPropertyManager(this.propertyManagerDTO);
                if (this.PropertyManagerId != -1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            { return false; }

        }

        private bool _UpdatePropertyManager()
        {

            base.Save();
            return clsPropertyManagerDataAccess.UpdatePropertyManager(this.PropertyManagerId, this.propertyManagerDTO);

        }

        public bool Save()
        {

            switch (_Mode)
            {

                case enMode.enAddNewPropertyManager:

                    if (_AddNewPropertyManager())
                    {
                        _Mode = enMode.enUpdateNewPropertyManager;
                        return true;

                    }
                    else
                    { return false; }

                case enMode.enUpdateNewPropertyManager:
                    return _UpdatePropertyManager();
            }
            return false;

        }

        public static bool DeletePropertyManager(int propertyManagerID)
        {
            return clsPropertyManagerDataAccess.DeletePropertyManager(propertyManagerID);
        }

        public static bool IsPropertyManagerExist(int propertyManagerId)
        {
            return clsPropertyManagerDataAccess.IsPropertyManagerExist(propertyManagerId);
        }

        private static bool _CheckCredentials(string userName, string password)
        {
            return clsPropertyManagerDataAccess.IsUserNameAndPasswordCorrect(userName, _HashPassword(password));
        }


        public bool ValidateCredentials()
        {
            return _CheckCredentials(this.UserName, this.Password);
        }

        private static string _HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashByte = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));

                StringBuilder sb = new StringBuilder(hashByte.Length * 2); // Pre-allocate capacity
                for (int i = 0; i < hashByte.Length; i++)
                {
                    sb.AppendFormat("{0:x2}", hashByte[i]);
                }
                return sb.ToString();
            }
        }


    }
}
