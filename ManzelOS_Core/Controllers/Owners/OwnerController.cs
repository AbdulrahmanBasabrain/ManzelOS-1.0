using ManzelOS_business_layer;
using ManzelOS_DTOs.Owners;
using Microsoft.AspNetCore.Mvc;

namespace ManzelOS_Core.Controllers.Owners
{
    [Route("api/OwnerController")]
    [ApiController]
    public class OwnerController : ControllerBase
    {



        [HttpGet("ListOwners", Name = "ListOwners")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<OwnerDTO>> ListOwners()
        {
            List<OwnerDTO> owners = clsOwner.ListAllOwners();

            if (owners.Count < 0)
            {
                return NotFound("There Is No Owners");
            }
            else
            {
                return Ok(owners);
            }
        }

        [HttpGet("FindOwnerById", Name = "FindOwnerById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]           
        [ProducesResponseType(StatusCodes.Status400BadRequest)]           

        public ActionResult<OwnerDTO>FindOwnerById(int ownerId)
        {
            
            if(ownerId < 0)
            {
                return BadRequest("Invalid id value");
            }
            clsOwner ownerToFind = clsOwner.FindOwnerById(ownerId);

            if (ownerToFind == null)
            {
                return NotFound("There Is No Owner with that id");
            }
            else
            {

                OwnerDTO ownerDTO = null;
                ownerDTO = ownerToFind.ownerDTO;
                return Ok(ownerDTO);
            }

        }

        [HttpPost("AddNewOwner", Name ="AddNewOwner")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        
        public ActionResult<OwnerDTO> AddOwner(OwnerDTO ownerDTO)
        {
            if(ownerDTO == null)
            {
                return BadRequest("invalid data");
            }
            else
            {
                clsOwner ownerToAdd = new clsOwner(ownerDTO);

                if(ownerToAdd.Save())
                {
                    ownerDTO.OwnerId = ownerToAdd.OwnerId;
                    return CreatedAtRoute("FindOwnerById", ownerDTO);
                }
                else
                {
                    return BadRequest("Failed To add Owner");
                }
            }
        }
        
        
        [HttpDelete("{ownerId}", Name = "DeleteOwner")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        
        public ActionResult DeleteOwner(int ownerId)
        {
            if(ownerId < 0)
            {
                return BadRequest("invalid id value");
            }
            else
            {
                if(clsOwner.DeleteOwner(ownerId))
                {

                    return Ok($"owner with the ID {ownerId} Has been deleted successfully");
                }
                else
                {
                    return BadRequest("Failed To delete owner");
                }
            }
        }

    }
}
