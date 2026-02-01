using ManzelOS_business_layer.RentalUnits.RentalUnit;
using ManzelOS_data_access_layer.RentalUnitsData.RentalUnitsData;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ManzelOS_Core.Controllers.Employees
{
    [Route("api/EmployeeAssignedToPropertyController")]
    [ApiController]
    public class EmployeeAssignedToPropertyController : ControllerBase
    {

        [HttpGet("ListEmployeePropertyAssignments", Name = "ListEmployeePropertyAssignments")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public ActionResult<IEnumerable<AssignEmployeeToPropertyDTO>>ListEmployeePropertyAssignments()
        {
            List<AssignEmployeeToPropertyDTO> assignEmployeeToPropertyList = clsRentalUnitAssignedToEmployeeDataAccess.ListEmployeeRentalUnitRelations();

            if(assignEmployeeToPropertyList.Count < 0)
            {
                return NotFound("There Is No Assignments");
            }
            else
            {
                return Ok(assignEmployeeToPropertyList);
            }
        }

        [HttpGet("AssignmentId", Name = "FindEmployeePropertyAssignmentById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        
        public ActionResult<AssignEmployeeToPropertyDTO>FindEmployeePropertyAssignmentById(int assignmentId)
        {

            if (assignmentId < 0)
            {
                return BadRequest("Invalide Id value");
            }
            else
            {

                clsRentalUnitAssignedToEmployee rentalUnitAssignedToEmployeeToFind = clsRentalUnitAssignedToEmployee.FindPropertyEmployeeAssignmentById(assignmentId);
                if (rentalUnitAssignedToEmployeeToFind == null)
                {

                    return NotFound("Assignment Has not been found");

                }
                else
                {

                    AssignEmployeeToPropertyDTO assignEmployeeToPropertyDTO = rentalUnitAssignedToEmployeeToFind.AssignendEmployeeToPropertyDTO;
                    return Ok(assignEmployeeToPropertyDTO);

                }
            }

        }

        [HttpPost("AssignPropertyToEmployee", Name = "AssignPropertyToEmployee")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public ActionResult<AssignEmployeeToPropertyDTO>AssignPropertyToEmployee(AssignEmployeeToPropertyDTO assignEmployeeToPropertyDTO)
        {
            
            if(assignEmployeeToPropertyDTO == null)
            {

                return BadRequest("Invalide Assignment Data");

            }
            else
            {

                clsRentalUnitAssignedToEmployee rentalUnitAssignedToEmployee = new clsRentalUnitAssignedToEmployee(assignEmployeeToPropertyDTO);
                
                assignEmployeeToPropertyDTO.AssignmentId = rentalUnitAssignedToEmployee.AssignmentId;

                if(rentalUnitAssignedToEmployee.Save())
                {
                    return CreatedAtRoute("FindEmployeePropertyAssignmentById", assignEmployeeToPropertyDTO);
                }
                else
                {
                    return NotFound("Failed To create Assinment");
                }
            }
            
        }

        [HttpPost("ChangePropertyToEmployeeAssignemnt", Name = "ChangePropertyToEmployeeAssignemnt")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public ActionResult<AssignEmployeeToPropertyDTO> ChangePropertyToEmployeeAssignemnt(int assignmentId, AssignEmployeeToPropertyDTO assignEmployeeToPropertyDTO)
        {

            if (assignEmployeeToPropertyDTO == null || assignmentId < 0)
            {

                return BadRequest("Invalide Assignment Data");

            }
            else
            {

                clsRentalUnitAssignedToEmployee rentalUnitAssignedToEmployee = clsRentalUnitAssignedToEmployee.FindPropertyEmployeeAssignmentById(assignmentId);
                
                if(rentalUnitAssignedToEmployee == null)
                {
                    return NotFound("There Is No Assignment With that Id");
                }

                else
                {
                    rentalUnitAssignedToEmployee.RentalUnitID = assignEmployeeToPropertyDTO.RentalUnitID;
                    rentalUnitAssignedToEmployee.EmployeeId = assignEmployeeToPropertyDTO.EmployeeId;
                    rentalUnitAssignedToEmployee.AssignmentDate = assignEmployeeToPropertyDTO.AssignmentDate;
                if (rentalUnitAssignedToEmployee.Save())
                {
                    return Ok(assignEmployeeToPropertyDTO);
                }
                else
                {
                    return NotFound("Failed To Update Assinment");
                }

                }
            }

        }

        [HttpDelete("DeletePropertyToEmployeeAssignemnt", Name = "DeletePropertyToEmployeeAssignemnt")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public ActionResult DeletePropertyToEmployeeAssignemnt(int assignmentId)
        {

            if(assignmentId < 0)
            {
                return BadRequest("Invalid Data");
            }
            if(clsRentalUnitAssignedToEmployee.FindPropertyEmployeeAssignmentById(assignmentId) == null)
            {
                return NotFound("There Is No Assignent With That ID");
            }
            else
            {
                if (clsRentalUnitAssignedToEmployee.DeleteAssignment(assignmentId))
                {
                    return Ok("Assignment Has been Deleted Successfully");
                }
                else
                {
                    return BadRequest("Failed To Delete Assignment");
                }
            }

        }
    
    }
}
