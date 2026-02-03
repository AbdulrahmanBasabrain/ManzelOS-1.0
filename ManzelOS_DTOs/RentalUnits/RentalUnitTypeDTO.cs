using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManzelOS_DTOs.RentalUnits
{

    public class RentalUnitTypeDTO
    {
        public int RentalUnitTypeId { get; set; }
        public string RentalUnitTypeName { get; set; }

        public RentalUnitTypeDTO(int rentalUnitType, string rentalUnitName) 
        {
            RentalUnitTypeId = rentalUnitType;
            RentalUnitTypeName = rentalUnitName;
        }

    }

}
