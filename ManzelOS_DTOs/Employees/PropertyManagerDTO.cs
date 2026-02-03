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
        public int EmployeeId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }
        public int Permession { get; set; }
        public DateTime AssignedAt { get; set; }

        public PropertyManagerDTO(int propertyManagerId, int employeeId, string userName, string password,
            bool isActive, int permession, DateTime assignedAt)
        {

            PropertyManagerId = propertyManagerId;
            EmployeeId = employeeId;
            UserName = userName;
            Password = password;
            IsActive = isActive;
            Permession = permession;
            AssignedAt = assignedAt;

        }

    }


}
