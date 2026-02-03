using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManzelOS_DTOs.Employees
{

    public class EmployeeDTO
    {


        public int EmployeeId { get; set; }
        public int PersonId { get; set; }
        public decimal Salary { get; set; }
        public string Job { get; set; }
        public string OrganizationEmail { get; set; }
        public DateTime CreatedAt { get; set; }


        public EmployeeDTO(int employeeId, int personId, decimal salary, string job, string organizationEmail, DateTime createdAt)
        {

            EmployeeId = employeeId;
            PersonId = personId;
            Salary = salary;
            Job = job;
            OrganizationEmail = organizationEmail;
            CreatedAt = createdAt;

        }

    }

}
