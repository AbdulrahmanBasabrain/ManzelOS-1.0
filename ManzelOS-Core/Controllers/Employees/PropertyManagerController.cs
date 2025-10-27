using ManzelOS_business_layer;
using ManzelOS_data_access_layer.EmployeesData;
using Microsoft.AspNetCore.Http;
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

        public ActionResult<PropertyManagerDTO>FindPropertyManager(int propertyManagerId)
        {
            
            if(propertyManagerId < 0)
            {
                return BadRequest("Invalid Property Manager Id");
            }

            clsPropertyManager propertyManagerToFind = clsPropertyManager.FindPropertyManagerById(propertyManagerId) ;
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
   
        public ActionResult<PropertyManagerDTO>AddNewPropertyManager(PropertyManagerDTO propertyManagerDTO)
        {

            if(propertyManagerDTO == null)
            {
                return BadRequest("Sorry Invalid Data");
            }

            clsPropertyManager propertyManagerToAdd = new clsPropertyManager(propertyManagerDTO);
            
            propertyManagerDTO.PropertyManagerId = propertyManagerToAdd.PropertyManagerId;

            if(propertyManagerToAdd.Save())
            {
                return CreatedAtRoute("FindPropertyManager", propertyManagerDTO);
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

        public ActionResult<PropertyManagerDTO> UpdatePropertyManager(int propertyManagerId, PropertyManagerDTO propertyManagerDTO)
        {

            if (propertyManagerDTO == null || propertyManagerId < 0) 
            {
                return BadRequest("Sorry Invalid Data");
            }

            clsPropertyManager propertyManagerToUpdate = clsPropertyManager.FindPropertyManagerById(propertyManagerId);

            propertyManagerToUpdate.EmployeeId = propertyManagerDTO.EmployeeId;
            propertyManagerToUpdate.UserName = propertyManagerDTO.UserName;
            propertyManagerToUpdate.Password = propertyManagerDTO.Password;
            propertyManagerToUpdate.IsActive = propertyManagerDTO.IsActive;
            propertyManagerToUpdate.Permession = propertyManagerDTO.Permession;
            propertyManagerToUpdate.AssignedAt = propertyManagerDTO.AssignedAt;

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
