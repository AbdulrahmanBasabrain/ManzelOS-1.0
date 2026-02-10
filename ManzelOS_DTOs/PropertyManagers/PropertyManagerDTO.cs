using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManzelOS_DTOs.Employees
{

    public class PropertyManagerDTO
    {

        public int PropertyManagerId { get; set; }
        public int PersonID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public DateTime PropertyManagerCreatedAt { get; set; } = DateTime.Now;

        public PropertyManagerDTO(int propertyManagerId, int personId, string userName, string password)
        {

            PropertyManagerId = propertyManagerId;
            PersonID = personId;
            UserName = userName;
            Password = password;
            PropertyManagerCreatedAt = DateTime.Now;

        }

    }


}
