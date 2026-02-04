using ManzelOS_business_layer.RentalUnits;
using ManzelOS_DTOs.RentalUnits;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace ManzelOS_Core.Controllers.RentalUnits
{
    [Route("api/RentalUnitContractController")]
    [ApiController]
    public class RentalUnitContractController : ControllerBase
    {

        [HttpGet("ListRentalUnitContract", Name = "ListRentalUnitContract")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public ActionResult<RentalUnitContractDTO> ListRentalUnitContract()
        {
            List<RentalUnitContractDTO> rentalUnitContractDTOs = clsRentalUnitContract.ListAllRentalContracts();

            if(rentalUnitContractDTOs.IsNullOrEmpty())
            {
                return NotFound("There Is no rental contract");
            }
            else
            {
                return Ok(rentalUnitContractDTOs);
            }

        }
        
        [HttpGet("FindRentalUnitContractById", Name = "FindRentalUnitContractById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public ActionResult<RentalUnitContractDTO> FindRentalUnitContractById(int rentalContractId)
        {
            
            if(rentalContractId< 0)
            {
                return BadRequest("bad id value");
            }

            clsRentalUnitContract rentalUnitContractToFind = clsRentalUnitContract.FindContractById(rentalContractId);

            if(rentalUnitContractToFind == null)
            {
                return NotFound("There is not rental unit with that id");
            }
            else
            {
                RentalUnitContractDTO rentalUnitContractDTO = rentalUnitContractToFind.rentalUnitContractDTO;
                return Ok(rentalUnitContractDTO);
            }
        }

        [HttpPost("AddRentacUnitContract", Name = "AddRentacUnitContract")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        
        public ActionResult<RentalUnitContractDTO> AddNewRentalContract(RentalUnitContractDTO rentalUnitContractDTO)
        {
            if(rentalUnitContractDTO == null)
            {
                return BadRequest("invalid data");
            }
            else
            {
                clsRentalUnitContract rentalUnitContractToAdd = new clsRentalUnitContract(rentalUnitContractDTO);

                if(rentalUnitContractToAdd.Save())
                {

                    rentalUnitContractDTO.RentalUnitContractId = rentalUnitContractToAdd.RentalUnitContractId;
                    return CreatedAtRoute("FindRentalUnitContractById", rentalUnitContractDTO);
                }
                else
                {
                    return BadRequest("Failed To save rental unit contract");
                }

            }
        }


        [HttpPost("UpdateRentalContract", Name = "UpdateRentalContract")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public ActionResult<RentalUnitContractDTO> UpdateRentalContract(int rentalUnitContractId, RentalUnitContractDTO rentalUnitContractDTO)
        {

            if (rentalUnitContractDTO == null || rentalUnitContractId < 0)
            {
                return BadRequest("invalid data");
            }
            else
            {

                clsRentalUnitContract rentalUnitContractToUpdate = clsRentalUnitContract.FindContractById(rentalUnitContractId);
                
                if(rentalUnitContractToUpdate == null)
                {
                    return NotFound("there is no rental unit contract with that id");
                }
                else
                {

                    rentalUnitContractToUpdate.RentalUnitContractId = rentalUnitContractDTO.RentalUnitContractId;
                    rentalUnitContractToUpdate.StartDate = rentalUnitContractDTO.StartDate;
                    rentalUnitContractToUpdate.EndDate = rentalUnitContractDTO.EndDate;
                    rentalUnitContractToUpdate.RepresentativeOwnerId = rentalUnitContractDTO.RepresentativeOwnerId;
                    rentalUnitContractToUpdate.TenantId = rentalUnitContractDTO.TenantId;
                    rentalUnitContractToUpdate.billingPeriodTypeId = rentalUnitContractDTO.billingPeriodTypeId;
                    rentalUnitContractToUpdate.RentalUnitId = rentalUnitContractDTO.RentalUnitId;
                    rentalUnitContractToUpdate.IsActive = rentalUnitContractDTO.IsActive;
                    rentalUnitContractToUpdate.RentalAmount = rentalUnitContractDTO.RentalAmount;
                    rentalUnitContractToUpdate.BillTypeId = rentalUnitContractDTO.BillTypeId;
                    rentalUnitContractToUpdate.CreatedAt = rentalUnitContractDTO.CreatedAt;
                    
                }
                if (rentalUnitContractToUpdate.Save())
                {

                    return Ok(rentalUnitContractDTO);
                }
                else
                {
                    return BadRequest("Failed To Update rental unit contract");
                }

            }
        }


        [HttpDelete("DeleteRentalContract", Name = "DeleteRentalContract")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        
        public ActionResult DeleteRentalUnitContract(int rentalUnitContractId)
        {
            if (rentalUnitContractId < 0) { return BadRequest("bad data"); }
            else
            {
                if(clsRentalUnitContract.DeleteRentalUnitContract(rentalUnitContractId))
                {
                    return Ok("The Contract Has been deleted successfully");
                }
                else { return BadRequest("Failed To Delete Rental unit contract"); }
            }
        }

    }
}
