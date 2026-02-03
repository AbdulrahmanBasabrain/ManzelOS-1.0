using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManzelOS_DTOs.RentalUnits
{
    public class RentalUnitDTO
    {

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


        public RentalUnitDTO(int rentalUnitId, int rentalUnitNumberOrder, string rentalUnitName, decimal rentalUnitSpace, string rentalUnitLocationName, decimal rentalUnitLatitude, decimal rentalUnitLongitude,
            int rentalUnitTypeId, int? parentRentalUnitId, bool isAvailable, int propertyManagerId, int representativeOwnerId, DateTime createdAt)
        {

            RentalUnitId = rentalUnitId;
            RentalUnitNumberOrder = rentalUnitNumberOrder;
            RentalUnitName = rentalUnitName;
            RentalUnitSpace = rentalUnitSpace;
            RentalUnitLocationName = rentalUnitLocationName;
            RentalUnitLatitude = rentalUnitLatitude;
            RentalUnitLongitude = rentalUnitLongitude;
            RentalUnitTypeId = rentalUnitTypeId;
            ParentRentalUnitId = parentRentalUnitId;
            IsAvailable = isAvailable;
            PropertyManagerId = propertyManagerId;
            RepresentativeOwnerId = representativeOwnerId;
            CreatedAt = createdAt;

        }

    }
}
