using System;
using ManzelOS_data_access_layer.RentalUnitsData.RentalUnitsData;
using System.Data;

namespace ManzelOS_business_layer.RentalUnits.RentalUnit
{
    public class clsRentalUnit
    {
        
        public enum enMode { enAddUnit = 1, enUpdateUnit = 2 }
        private enMode _Mode;
        public int RentalUnitId { get; set; }
        public int RentalUnitNumberOrder { get; set; }
        public string RentalUnitName { get; set; }
        public decimal RentalUnitSpace { get; set; }
        public string RentalUnitLocationName { get; set; }
        public decimal RentalUnitLatitude { get; set; }
        public decimal RentalUnitLongitude { get; set; }
        public int RentalUnitTypeId { get; set; }
        public int? ParentRentalUnitId { get; set; }
        public bool IsAvailable { get; set; }
        public int PropertyManagerId { get; set; }
        public int RepresentativeOwnerId { get; set; }
        public DateTime CreatedAt { get; set; }
        
        public RentalUnitDTO RentalUnitDTO 
        { 
            get 
            {
                return new RentalUnitDTO(this.RentalUnitId, this.RentalUnitNumberOrder, this.RentalUnitName, this.RentalUnitSpace, this.RentalUnitLocationName, this.RentalUnitLatitude,
                this.RentalUnitLongitude, this.RentalUnitTypeId, this.ParentRentalUnitId, this.IsAvailable, this.PropertyManagerId, this.RepresentativeOwnerId, this.CreatedAt);
            } 
        }

        public clsRentalUnit()
        {

            RentalUnitId = -1;
            RentalUnitNumberOrder = 0;
            RentalUnitName = string.Empty;
            RentalUnitSpace = 0.0m;
            RentalUnitLocationName = string.Empty;
            RentalUnitLatitude = 0.0m;
            RentalUnitLongitude = 0.0m;
            RentalUnitTypeId = -1;
            ParentRentalUnitId = null;
            IsAvailable = false;
            PropertyManagerId = -1;
            RepresentativeOwnerId = -1;
            CreatedAt = DateTime.Now;

            _Mode = enMode.enAddUnit;

        }

        public clsRentalUnit(RentalUnitDTO rentalUnitDTO, enMode mode = enMode.enAddUnit )

        {

            RentalUnitId = rentalUnitDTO.RentalUnitId;
            RentalUnitNumberOrder = rentalUnitDTO.RentalUnitNumberOrder;
            RentalUnitName = rentalUnitDTO.RentalUnitName;
            RentalUnitSpace = rentalUnitDTO.RentalUnitSpace;
            RentalUnitLocationName = rentalUnitDTO.RentalUnitLocationName;
            RentalUnitLatitude = rentalUnitDTO.RentalUnitLatitude;
            RentalUnitLongitude = rentalUnitDTO.RentalUnitLongitude;
            RentalUnitTypeId = rentalUnitDTO.RentalUnitTypeId;
            ParentRentalUnitId = rentalUnitDTO.ParentRentalUnitId;
            IsAvailable = rentalUnitDTO.IsAvailable;
            PropertyManagerId = rentalUnitDTO.PropertyManagerId;
            RepresentativeOwnerId = rentalUnitDTO.RepresentativeOwnerId;
            CreatedAt = rentalUnitDTO.CreatedAt;

            _Mode = mode;

        }

        public static clsRentalUnit FindRentalUnitById(int rentalUnitId)
        {

            RentalUnitDTO rentalUnitDTO = clsRentalUnitDataAccess.GetRentalUnitInfoById(rentalUnitId);

            if (rentalUnitDTO != null)
            {
                return new clsRentalUnit(rentalUnitDTO, enMode.enUpdateUnit);
            }
            else
            {
                return null;

            }
        }

        public static List<RentalUnitDTO> ListAllRentalUnits()
        {
            return clsRentalUnitDataAccess.ListAllRentalUnits();
        }

        private bool _AddRentalUnit()
        {

            this.RentalUnitId = clsRentalUnitDataAccess.AddRentalUnit(this.RentalUnitDTO);

            return (this.RentalUnitId != -1);

        }

        private bool _UpdateRentalUnit()
        {
            return clsRentalUnitDataAccess.UpdateRentalUnit(this.RentalUnitId, this.RentalUnitDTO);
        }

        public bool Save()
        {
            switch (_Mode)
            {
                case enMode.enAddUnit:
                    if (_AddRentalUnit())
                    {
                        _Mode = enMode.enUpdateUnit; // Switch to update mode after adding
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case enMode.enUpdateUnit:
                    return _UpdateRentalUnit();
            }
            return false;
        }

        public static bool DeleteRentalUnit(int rentalUnitId)
        {
            return clsRentalUnitDataAccess.DeleteRentalUnit(rentalUnitId);
        }

        public static bool IsRentalUnitExist(int rentalUnitId)
        {
            return clsRentalUnitDataAccess.IsRentalUnitExist(rentalUnitId);
        }

        public static bool IsRentalUnitAvailable(int rentalUnitId)
        {
            return clsRentalUnitDataAccess.IsRentalUnitAvailable(rentalUnitId);
        }
        
        public static List<RentalUnitDTO> GetAllChildRentalUnits(int parentRentalUnitId)
        {
            return clsRentalUnitDataAccess.ListAllChildRentalUnits(parentRentalUnitId);
        }

        public static List<RentalUnitDTO> GetAllParentRentalUnitsWithProeprtyManager(int propertyManagerId)
        {
            return clsRentalUnitDataAccess.ListAllParentRentalUnitsWithPropertyManager(propertyManagerId);
        }

        public static List<RentalUnitDTO> ShortListRentalUnitsWithProeprtyManager(int propertyManagerId)
        {
            return clsRentalUnitDataAccess.ShortListAllRentalUnitsWithPropertyManager(propertyManagerId);
        }





    }
}
