using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ManzelOS_DTOs.People;

namespace ManzelOS_DTOs.Tenants
{
    public class TenantPersonDTO
    {
        public TenantDTO tenantDTO {get; set;}
        public PeopleDTO peopleDTO {get; set;}

        public TenantPersonDTO(TenantDTO tenantDTO, PeopleDTO peopleDTO) 
        {

           this.tenantDTO = tenantDTO;
           this.peopleDTO = peopleDTO;

        }
    }
}
