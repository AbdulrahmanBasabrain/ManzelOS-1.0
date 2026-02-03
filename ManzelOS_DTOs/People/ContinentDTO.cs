using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManzelOS_DTOs.People
{
    public class ContinentDTO
    {

        public short ContinentId { get; set; }

        public string ContinentName { get; set; }
        
        public ContinentDTO(short continentId, string continentName)
        {
            ContinentId = continentId;
            ContinentName = continentName;

        }


    }

}
