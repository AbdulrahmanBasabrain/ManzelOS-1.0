using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManzelOS_DTOs.RentalUnits
{
    public class RentalUnitContractDTO
    {

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

        public RentalUnitContractDTO(int rentalUnitContractId, DateTime startDate, DateTime endDate, int representativeOwnerId, int tenantId, int billingPeriodTypeId,
            int rentalUnitId, bool isActive, decimal rentalAmount, int billTypeId, DateTime createdAt) 
        {

            RentalUnitContractId = rentalUnitContractId;
            StartDate = startDate;
            EndDate = endDate;
            RepresentativeOwnerId = representativeOwnerId;
            TenantId = tenantId;
            this.billingPeriodTypeId = billingPeriodTypeId;
            RentalUnitId = rentalUnitId;
            IsActive = isActive;
            RentalAmount = rentalAmount;
            BillTypeId = billTypeId;
            CreatedAt = createdAt;

        }

    }
}
