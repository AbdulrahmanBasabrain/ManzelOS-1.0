using ManzelOS_business_layer;
using ManzelOS_DTOs.Employees;
using ManzelOS_DTOs.People;
using ManzelOS_DTOs.PropertyManagers;
using Microsoft.AspNetCore.Mvc;

namespace ManzelOS_Core.Controllers.Employees
{
    [Route("api/PropertyManagerController")]
    [ApiController]
    public class PropertyManagerController : ControllerBase
    {

        [HttpGet("ListAllPropertyManagers", Name = "ListAllPropertyManagers")]
        [ProducesResponseType(StatusCodes.Status200OK)]        
        [ProducesResponseType(StatusCodes.Status404NotFound)]        

        public ActionResult<IEnumerable<PropertyManagerDTO>>ListPropertyManager()
        {
            List<PropertyManagerDTO> propertyManagerDTOs = clsPropertyManager.ListAllPropertyManagers();

            if (propertyManagerDTOs.Count == 0)
            {
                return NotFound("There Is No Property Managers");
            }
            else
            {
                return Ok(propertyManagerDTOs); 
            }
        }
 
        [HttpGet("FindPropertyManager", Name = "FindPropertyManager")]
        [ProducesResponseType(StatusCodes.Status200OK)]        
        [ProducesResponseType(StatusCodes.Status404NotFound)]        
        [ProducesResponseType(StatusCodes.Status400BadRequest)]        

        public ActionResult<PropertyManagerPersonDTO>FindPropertyManager(int propertyManagerId)
        {
            
            if(propertyManagerId < 0)
            {
                return BadRequest("Invalid Property Manager Id");
            }

            PropertyManagerPersonDTO propertyManagerToFind = clsPropertyManager.FindFullPropertyManagerById(propertyManagerId) ;
            if (propertyManagerToFind == null)
            {
                return NotFound("Property Manager Not Found");
            }
            else
            {
                return Ok(propertyManagerToFind); 
            }
        }
  
        [HttpPost("AddPropertyManager", Name = "AddPropertyManager")]
        [ProducesResponseType(StatusCodes.Status201Created)]        
        [ProducesResponseType(StatusCodes.Status404NotFound)]        
        [ProducesResponseType(StatusCodes.Status400BadRequest)]        
   
        public ActionResult<PropertyManagerDTO>AddNewPropertyManager(PropertyManagerPersonDTO propertyManagerInfo)
        {

            if(propertyManagerInfo == null)
            {
                return BadRequest("Sorry Invalid Data");
            }

            clsPropertyManager propertyManagerToAdd = new clsPropertyManager(propertyManagerInfo);
            

            if(propertyManagerToAdd.Save())
            {
                return CreatedAtRoute("FindPropertyManager", propertyManagerInfo);
            }
            else
            {
                return BadRequest("Failed To Create A Property Manager");
            }

            
        }

        [HttpPost("UpdatePropertyManager", Name = "UpdatePropertyManager")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public ActionResult<PropertyManagerDTO> UpdatePropertyManager(int propertyManagerId, PropertyManagerPersonDTO propertyManagerDTO)
        {

            if (propertyManagerDTO == null || propertyManagerId < 0) 
            {
                return BadRequest("Sorry Invalid Data");
            }

            clsPropertyManager propertyManagerToUpdate = new clsPropertyManager( clsPropertyManager.FindFullPropertyManagerById(propertyManagerId));


            if (propertyManagerToUpdate.Save())
            {
                return Ok(propertyManagerDTO);
            }
            else
            {
                return BadRequest("Failed To Update A Property Manager");
            }


        }

        [HttpDelete("{propertyManagerId}", Name = "DeletePropertyManager")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public ActionResult DeletePropertyManager(int propertyManagerId)
        {
            if (propertyManagerId < 0)
            {
                return BadRequest("Invalid ID value");
            }
            else
            {
                if (clsPropertyManager.DeletePropertyManager(propertyManagerId))
                {
                    return Ok("Property Manager Has been Deleted");
                }
                else { return BadRequest("Failed To Delete Property Manager"); }
            }
        }

    }

}
