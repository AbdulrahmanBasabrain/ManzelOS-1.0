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
        public string UserName { get; set; }
        public string Password { get; set; }
        public DateTime CreatedAt { get; set; }

        public PropertyManagerDTO(int propertyManagerId, string userName, string password, DateTime createdAt)
        {

            PropertyManagerId = propertyManagerId;
            UserName = userName;
            Password = password;
            CreatedAt = createdAt;
        }

    }


}
