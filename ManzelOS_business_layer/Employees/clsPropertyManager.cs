using System.Security.Cryptography;
using System.Text;
using ManzelOS_DTOs.Employees;
using ManzelOS_data_access_layer.EmployeesData;

// i would like to use inhertance in this case but i will not use it now because we are very early 
// and don't need any of the employee class functionality yet and we will use it in the future when we need it 

namespace ManzelOS_business_layer
{
    public class clsPropertyManager : clsEmployee
    {

        public enum enMode { enAddNewPropertyManager = 1, enUpdateNewPropertyManager = 2 }

        private enMode _Mode;
        public int PropertyManagerId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }
        public int Permession { get; set; }
        public DateTime AssignedAt { get; set; }
        
        PropertyManagerDTO propertyManagerDTO { get { return new PropertyManagerDTO(this.PropertyManagerId, this.EmployeeId, this.UserName, this.Password, this.IsActive, this.Permession, this.AssignedAt); } }

        public clsPropertyManager()
        {

            PropertyManagerId = -1;
            EmployeeId = -1;
            UserName = string.Empty;
            Password = string.Empty;
            IsActive = false;
            Permession = -1;
            AssignedAt = DateTime.Now;

            _Mode = enMode.enAddNewPropertyManager;

        }

        public clsPropertyManager(PropertyManagerDTO propertyManagerDTO, enMode mode = enMode.enAddNewPropertyManager)
        {

            PropertyManagerId = propertyManagerDTO.PropertyManagerId;
            EmployeeId = propertyManagerDTO.EmployeeId;
            UserName = propertyManagerDTO.UserName;
            Password = propertyManagerDTO.Password;
            IsActive = propertyManagerDTO.IsActive;
            Permession = propertyManagerDTO.Permession;
            AssignedAt = propertyManagerDTO.AssignedAt;

           
            _Mode = mode;

        }

        public static clsPropertyManager FindPropertyManagerById(int propertyManagerId)
        {

            PropertyManagerDTO propertyManagerDTO = clsPropertyManagerDataAccess.GetPropertyManagerInfoById(propertyManagerId);

            if (propertyManagerDTO != null)
            {

                return new clsPropertyManager(propertyManagerDTO, enMode.enUpdateNewPropertyManager);
            }
            else
            {
                return null;
            }
        }

        public static clsPropertyManager FindPropertyManagerByUserName(string userName)
        {

            PropertyManagerDTO propertyManagerDTO = clsPropertyManagerDataAccess.GetPropertyManagerInfoByUserName(userName);

            if (propertyManagerDTO != null)
            {

                return new clsPropertyManager(propertyManagerDTO, enMode.enUpdateNewPropertyManager);
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

        private bool _UpdatePropertyManager()
        {
            
            return clsPropertyManagerDataAccess.UpdatePropertyManager(this.PropertyManagerId ,this.propertyManagerDTO);

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
            return clsPropertyManagerDataAccess.IsUserNameAndPasswordCorrect(userName,  _HashPassword(password));
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
