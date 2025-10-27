using System;
using ManzelOS_data_access_layer.RentalUnitsData.RentalUnitsContractsData;
using System.Data;

namespace ManzelOS_business_layer.RentalUnits.RentalUnitsContracts
{
    public class clsRentalUnitContract
    {

        public enum enMode { enAddRentalUnitContract = 1, enUpdateRentalUnitContract = 2 }
        private enMode _Mode;

        public int RentalUnitContractId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int RepresentativeOwnerId { get; set; }
        public int TenantId { get; set; }
        public int billingPeriodTypeId { get; set; }
        public int RentalUnitId { get; set; }
        public bool IsActive { get; set; }
        public decimal RentalAmount { get; set; }
        public int BillTypeId { get; set; }
        public DateTime CreatedAt { get; set; }
        
        public RentalUnitContractDTO rentalUnitContractDTO 
        { 
            get 
            {
                return new RentalUnitContractDTO(this.RentalUnitContractId, this.StartDate, this.EndDate, this.RepresentativeOwnerId, this.TenantId, this.billingPeriodTypeId,
                    this.RentalUnitId, this.IsActive, this.RentalAmount, this.BillTypeId, this.CreatedAt);
            }
        }

        public clsRentalUnitContract()
        {
            RentalUnitContractId = -1;
            StartDate = DateTime.Now;
            EndDate = DateTime.Now.AddMonths(1);
            RepresentativeOwnerId = -1;
            TenantId = -1;
            billingPeriodTypeId = -1;
            RentalUnitId = -1;
            IsActive = false;
            RentalAmount = 0.0m;
            BillTypeId = -1;
            CreatedAt = DateTime.Now;

            _Mode = enMode.enAddRentalUnitContract;
        }

        public clsRentalUnitContract(RentalUnitContractDTO rentalUnitContractDTO, enMode mode = enMode.enAddRentalUnitContract)
        {

            RentalUnitContractId = rentalUnitContractDTO.RentalUnitContractId;
            StartDate = rentalUnitContractDTO.StartDate;
            EndDate = rentalUnitContractDTO.EndDate;
            RepresentativeOwnerId = rentalUnitContractDTO.RepresentativeOwnerId;
            TenantId = rentalUnitContractDTO.TenantId;
            billingPeriodTypeId = rentalUnitContractDTO.billingPeriodTypeId;
            RentalUnitId = rentalUnitContractDTO.RentalUnitId;
            IsActive = rentalUnitContractDTO.IsActive;
            RentalAmount = rentalUnitContractDTO.RentalAmount;
            BillTypeId = rentalUnitContractDTO.BillTypeId;
            CreatedAt = rentalUnitContractDTO.CreatedAt;

            _Mode = mode;

        }

        public static clsRentalUnitContract FindContractById(int rentalUnitContractId)
        {

            RentalUnitContractDTO rentalUnitContractDTO = clsRentalUnitContractDataAccess.GetRentalUnitContractInfoById(rentalUnitContractId);

            if (rentalUnitContractDTO != null)
            {
                return new clsRentalUnitContract(rentalUnitContractDTO, enMode.enUpdateRentalUnitContract);
            }
            else
            {
                return null;
            }

        }

        public static clsRentalUnitContract FindContractByRentalId(int rentalUnitId)
        {

            RentalUnitContractDTO rentalUnitContractDTO = clsRentalUnitContractDataAccess.GetRentalUnitContractInfoByRentalId(rentalUnitId);

            if (rentalUnitContractDTO != null)
            {
                return new clsRentalUnitContract(rentalUnitContractDTO, enMode.enUpdateRentalUnitContract);
            }
            else
            {
                return null;
            }


        }

        public static List<RentalUnitContractDTO> ListAllRentalContracts()
        {
            return clsRentalUnitContractDataAccess.ListAllRentalUnitContracts();
        }

        private bool _AddNewRentalUnitContract()
        {
            this.RentalUnitContractId = clsRentalUnitContractDataAccess.AddNewRentalUnitContract(this.rentalUnitContractDTO);

            return (this.RentalUnitContractId != -1);
        }

        private bool _UpdateRentalUnitContract()
        {
            return clsRentalUnitContractDataAccess.UpdateRentalUnitContract(this.RentalUnitContractId, this.rentalUnitContractDTO);
        }

        public bool Save()
        {
            switch (_Mode)
            {
                case enMode.enAddRentalUnitContract:
                    if (_AddNewRentalUnitContract())
                    {
                        _Mode = enMode.enUpdateRentalUnitContract; // Switch to update mode after adding
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case enMode.enUpdateRentalUnitContract:
                    return _UpdateRentalUnitContract();
                default:
                    throw new InvalidOperationException("Invalid mode for saving rental unit contract.");
            }
        }

        public static bool DeleteRentalUnitContract(int rentalUnitContractId)
        {
            return clsRentalUnitContractDataAccess.DeleteRentalUnitContract(rentalUnitContractId);
        }

        public static bool IsRentalUnitContractExist(int rentalUnitContractId)
        {
            return clsRentalUnitContractDataAccess.IsRentalUnitContractExists(rentalUnitContractId);
        }

        public static bool IsRentalUnitContractActive(int rentalUnitContractId)
        {
            return clsRentalUnitContractDataAccess.IsRentalUnitContractActive(rentalUnitContractId);
        }
 
        public static bool IsRentalUnitContractExistByRentalUnitId(int rentalUnitId)
        {
            return clsRentalUnitContractDataAccess.IsRentalUnitContractExistsByRentalUnitId(rentalUnitId);
        }

       
    }
}
