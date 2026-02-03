using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManzelOS_DTOs.People
{

    public class CityDTO
    {

        public int CityId { get; set; }
        public short CountryId { get; set; }
        public string CityName { get; set; }
        
        public CityDTO(int cityId, short countryId, string cityName)
        {

            this.CityId = cityId;
            this.CountryId = countryId;
            this.CityName = cityName;

        }

    }
}
