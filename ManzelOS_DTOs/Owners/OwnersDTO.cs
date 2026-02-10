using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManzelOS_DTOs.Owners
{
    public class OwnerDTO
    {
        
        public int OwnerId { get; set; }
        public int PersonId { get; set; }
        public bool IsBusiness { get; set; }
        public DateTime CreatedAt { get; set; }

        public OwnerDTO(int ownerId, int personId, bool isBusiness, DateTime createdAt)
        {

            OwnerId = ownerId;
            PersonId = personId;
            IsBusiness = isBusiness;
            CreatedAt = createdAt;

 
        }

    }
}
