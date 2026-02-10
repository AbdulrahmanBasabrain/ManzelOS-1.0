using ManzelOS_DTOs.People;
using ManzelOS_data_access_layer.PeopleData;

namespace ManzelOS_business_layer
{
    public class clsCountry
    {

        public short CountryId { get; set; }
        public short ContinentId { get; set; }
        public string CountryName { get; set; }
        
        public CountryDTO CountryDTO { get { return new CountryDTO(this.CountryId, this.ContinentId, this.CountryName); } }
        
        public clsCountry()
        {
            CountryId = -1;
            ContinentId = -1;
            CountryName = string.Empty;


        }

        public clsCountry(CountryDTO country)
        {
            CountryId = country.CountryId;
            ContinentId = country.ContinentId;
            CountryName = country.CountryName;

        }

        public static CountryDTO Find(short countryId)
        {

            CountryDTO country = clsCountryDataAccess.GetCountryInfo(countryId);
            if(country != null)
            {
                return country;
            }
            else
            {
                return null;
            }
        }

        public static List<CountryDTO> ListAllCountries()
        {
            return clsCountryDataAccess.ListAllCountries();
        }


    }
}
