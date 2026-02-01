using System;
using ManzelOS_data_access_layer.RentalUnitsData.RentalUnitsData;
using System.Data;

namespace ManzelOS_business_layer.RentalUnits.RentalUnit
{
    public class clsRentalUnitType
    {

        public int RentalUnitTypeId { get; set; }
        public string RentalUnitTypeName { get; set; }
        
        public enum enMode { enAddNew = 1, enUpdateNew = 2 }
        private enMode _Mode = enMode.enAddNew;


        public RentalUnitTypeDTO rentalUnitTypeDTO { get { return new RentalUnitTypeDTO(this.RentalUnitTypeId, this.RentalUnitTypeName); } }

        public clsRentalUnitType()
        {
            RentalUnitTypeId = -1;
            RentalUnitTypeName = string.Empty;
        }

        public clsRentalUnitType(RentalUnitTypeDTO rentalUnitTypeDTO, enMode mode = enMode.enAddNew)
        {
            RentalUnitTypeId = rentalUnitTypeDTO.RentalUnitTypeId;
            RentalUnitTypeName = rentalUnitTypeDTO.RentalUnitTypeName;

            _Mode = mode;

        }
        
        public static clsRentalUnitType GetRentalUnitTypeById(int rentalUnitTypeId)
        {

            RentalUnitTypeDTO rentalUnitTypeDTO = clsRentalUnitTypeDataAccess.GetRentalUnitTypeInfoById((short)rentalUnitTypeId);

            if (rentalUnitTypeDTO != null)
            {
                return new clsRentalUnitType(rentalUnitTypeDTO, enMode.enUpdateNew);
            }
            else
            {
                return null; // or throw an exception if preferred
            }
        }

        public static List<RentalUnitTypeDTO> ListAllRentalUnitTypes()
        {
            return clsRentalUnitTypeDataAccess.ListAllRentalUnitTypes();
        }


    }
}
