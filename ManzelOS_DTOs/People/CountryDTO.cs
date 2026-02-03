using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManzelOS_DTOs.People
{
    public class CountryDTO
    {


        public short CountryId { get; set; }
        public short ContinentId { get; set; }
        public string CountryName { get; set; }

        public CountryDTO(short countryId, short continentId, string countryName)
        {
            CountryId = countryId;
            ContinentId = continentId;
            CountryName = countryName;

        }





    }
}
