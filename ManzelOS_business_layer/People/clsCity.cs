using ManzelOS_data_access_layer.PeopleData;
using ManzelOS_DTOs.People;

namespace ManzelOS_business_layer
{
    public class clsCity
    {

        public int CityId { get; set; }
        public short CountryId { get; set; }
        public string CityName { get; set; }
        
        public CityDTO CityDTO { get { return new CityDTO(this.CityId, this.CountryId, this.CityName); } }
        public clsCity()
        {

            CityId = -1;
            CountryId = -1;
            CityName = string.Empty;


        }

        public clsCity(CityDTO city)
        {

            CityId = city.CityId;
            CountryId = city.CountryId;
            CityName = city.CityName;

        }

        public static CityDTO Find(int cityId)
        {

            CityDTO city = clsCityDataAccess.GetCityInfo(cityId);

            if (city != null)
            {
                return city;
            }
            else { return null; }
        }

        public static List<CityDTO> ListAllCities()
        {
            return clsCityDataAccess.ListAllCities();
        }

    }
}
