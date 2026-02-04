using ManzelOS_business_layer.RentalUnits;
using ManzelOS_DTOs.RentalUnits;
using Microsoft.AspNetCore.Mvc;

namespace ManzelOS_Core.Controllers.RentalUnits
{
    [Route("api/RentalUnitController")]
    [ApiController]
    public class RentalUnitController : ControllerBase
    {

        

        [HttpGet("ListRentalUnits", Name = "ListRentalUnits")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<RentalUnitDTO>>ListRentalUnits()
        {
            List<RentalUnitDTO> rentalUnitDTOs = clsRentalUnit.ListAllRentalUnits();

            if(rentalUnitDTOs.Count == 0)
            {
                return NotFound("There Is No Rental Units");
            }
            else
            {
                return Ok(rentalUnitDTOs);
            }
        }

        [HttpGet("FindRentalUnitById", Name = "FindRentalUnitById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        
        public ActionResult<RentalUnitDTO> FindRentalUnitById(int rentalUnitId)
        {

            if(rentalUnitId < 0)
            {
                return BadRequest("invalid id value");
            }
            else
            {
                clsRentalUnit rentalUnitToFind = clsRentalUnit.FindRentalUnitById(rentalUnitId);
                if(rentalUnitToFind == null)
                {
                    return NotFound("The Rental Unit has not been found");
                }
                else
                {
                    RentalUnitDTO rentalUnitDTO = rentalUnitToFind.RentalUnitDTO;
                    return Ok(rentalUnitDTO);
                }
            }

        }


        [HttpPost("AddNewRentalUnit", Name = "AddNewRentalUnit")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        
        public ActionResult<RentalUnitDTO>AddRentalUnit(RentalUnitDTO rentalUnitDTO)
        {

            if (rentalUnitDTO == null)
            {
                return BadRequest("invalid data");
            }
            else
            {
                clsRentalUnit rentalUnit = new clsRentalUnit(rentalUnitDTO);

                if(rentalUnit.Save())
                {

                    rentalUnitDTO.RentalUnitId = rentalUnitDTO.RentalUnitId;
                    return CreatedAtRoute("FindRentalUnitById", rentalUnitDTO);

                }
                else
                {
                    return BadRequest("Failed To Add Rental unit");
                }
            }
        }

        [HttpPost("UpdateRentalUnit", Name = "UpdateRentalUnit")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public ActionResult<RentalUnitDTO> UpdateRentalUnit(int rentalUnitId, RentalUnitDTO rentalUnitDTO)
        {

            if (rentalUnitDTO == null || rentalUnitId < 0)
            {
                return BadRequest("invalid data");
            }

            else
            {

                clsRentalUnit rentalUnitToUpdate = clsRentalUnit.FindRentalUnitById(rentalUnitId);

                rentalUnitToUpdate.RentalUnitId = rentalUnitDTO.RentalUnitId;
                rentalUnitToUpdate.RentalUnitNumberOrder = rentalUnitDTO.RentalUnitNumberOrder;
                rentalUnitToUpdate.RentalUnitName = rentalUnitDTO.RentalUnitName;
                rentalUnitToUpdate.RentalUnitSpace = rentalUnitDTO.RentalUnitSpace;
                rentalUnitToUpdate.RentalUnitLocationName = rentalUnitDTO.RentalUnitLocationName;
                rentalUnitToUpdate.RentalUnitLatitude = rentalUnitDTO.RentalUnitLatitude;
                rentalUnitToUpdate.RentalUnitLongitude = rentalUnitDTO.RentalUnitLongitude;
                rentalUnitToUpdate.RentalUnitTypeId = rentalUnitDTO.RentalUnitTypeId;
                rentalUnitToUpdate.ParentRentalUnitId = rentalUnitDTO.ParentRentalUnitId;
                rentalUnitToUpdate.IsAvailable = rentalUnitDTO.IsAvailable;
                rentalUnitToUpdate.PropertyManagerId = rentalUnitDTO.PropertyManagerId;
                rentalUnitToUpdate.RepresentativeOwnerId = rentalUnitDTO.RepresentativeOwnerId;
                rentalUnitToUpdate.CreatedAt = rentalUnitDTO.CreatedAt;
               

                if (rentalUnitToUpdate.Save())
                {

                    return Ok(rentalUnitDTO);

                }
                else
                {
                    return BadRequest("Failed To Update Rental unit");
                }

            }
        }

        [HttpDelete("DeleteRentalUnit", Name = "DeleteRentalUnit")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        
        public ActionResult DeleteRentalUnit(int rentalUnitId)
        {
            if (rentalUnitId < 0)
            {
                return BadRequest("invalid id");
            }
            else
            {
                if (clsRentalUnit.DeleteRentalUnit(rentalUnitId))
                {
                    return Ok("Renal Unit Has been deleted successfully");
                }
                else { return BadRequest("Failed To Delete Rental Unit"); }
            }
        }

    }
}
