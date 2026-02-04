using ManzelOS_business_layer.RentalUnits;
using ManzelOS_DTOs.RentalUnits;
using Microsoft.AspNetCore.Mvc;

namespace ManzelOS_Core.Controllers.RentalUnits
{
    [Route("api/RentalUnitTypeController")]
    [ApiController]
    public class RentalUnitTypeController : ControllerBase
    {

        [HttpGet("ListRentalUnitTypes", Name = "ListRentalUnitTypes")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public ActionResult<IEnumerable<RentalUnitTypeDTO>> ListRentalUnitTypes()
        {

            List<RentalUnitTypeDTO> rentalUnitTypeDTOs = clsRentalUnitType.ListAllRentalUnitTypes();
            if (rentalUnitTypeDTOs.Count == 0)
            {
                return NotFound("No People Found");
            }
            return Ok(rentalUnitTypeDTOs);
        }

    }
}
