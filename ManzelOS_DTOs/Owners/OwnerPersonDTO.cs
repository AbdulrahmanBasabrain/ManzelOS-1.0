using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ManzelOS_DTOs.People;

namespace ManzelOS_DTOs.Owners
{
    public class OwnerPersonDTO
    {

        public OwnerDTO ownerDTO {get; set;}
        public PeopleDTO personDTO {get; set;}

        public OwnerPersonDTO(OwnerDTO ownerDTO, PeopleDTO personDTO) 
        {
            
            this.ownerDTO = ownerDTO;
            this.personDTO = personDTO;

        }

    }
}
