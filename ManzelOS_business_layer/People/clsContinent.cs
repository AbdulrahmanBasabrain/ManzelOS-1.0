using ManzelOS_data_access_layer.PeopleData;
using ManzelOS_DTOs.People;

namespace ManzelOS_business_layer
{
    public class clsContinent
    {
        public short ContinentId { get; set; }

        public string ContinentName { get; set; }

        public ContinentDTO ContinentDTO { get { return new ContinentDTO(this.ContinentId, this.ContinentName); } }

        public clsContinent()
        {
            ContinentId = 0;
            ContinentName = string.Empty;

        }

        public clsContinent(ContinentDTO continentDTO)
        {
            ContinentId = continentDTO.ContinentId;
            ContinentName = continentDTO.ContinentName;
        }

        public static clsContinent Find(short continentId)
        {

            ContinentDTO continent = clsContinentDataAccess.GetContinentInfo(continentId);
            if (continent != null)
            {
                return new clsContinent(continent);
            }
            else
            {
                return null;
            }


        }

        public static List<ContinentDTO> ListAllContinents()
        {
            return clsContinentDataAccess.ListAllContinents();
        }


    }
}
