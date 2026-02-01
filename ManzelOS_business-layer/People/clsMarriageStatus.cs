using System;
using System.Data;
using ManzelOS_data_access_layer.PeopleData;

namespace ManzelOS_business_layer
{
    public class clsMarriageStatus
    {

        public short MarriageStatusId { get; set; }
        public string MarriageStatusName { get; set; }
        
        MarriageStatusDTO marriageStatusDTO { get {  return new MarriageStatusDTO(this.MarriageStatusId, this.MarriageStatusName); } }
        public clsMarriageStatus()
        {
            MarriageStatusId = -1;
            MarriageStatusName = string.Empty;

        }

        public clsMarriageStatus(MarriageStatusDTO marriageStatusDTO)
        {

            MarriageStatusId = marriageStatusDTO.MarriageStatusID;
            MarriageStatusName = marriageStatusDTO.MarriageStatusName;

        }

        public static clsMarriageStatus Find(short marriageStatusId)
        {

            MarriageStatusDTO marriageStatusDTO = clsMarriageStatusDataAccess.GetMarriageStatusInfo(marriageStatusId);
            if (marriageStatusDTO != null)
            {
                return new clsMarriageStatus(marriageStatusDTO);
            }
            else
            {
                return null;
            }
        }
        
        public static List<MarriageStatusDTO> ListMarriageStatus()
        {
            return clsMarriageStatusDataAccess.ListAllMarriageStatuses();
        }

    }
}
