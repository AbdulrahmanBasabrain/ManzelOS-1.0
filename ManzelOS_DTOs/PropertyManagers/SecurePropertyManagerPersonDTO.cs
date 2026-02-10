using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ManzelOS_DTOs.Employees;
using ManzelOS_DTOs.People;

namespace ManzelOS_DTOs.PropertyManagers
{
    public class SecurePropertyManagerPersonDTO
    {

        public PeopleDTO peopleDTO { get; }
        public string UserName { get; }
        
        SecurePropertyManagerPersonDTO() { }


    }
}
