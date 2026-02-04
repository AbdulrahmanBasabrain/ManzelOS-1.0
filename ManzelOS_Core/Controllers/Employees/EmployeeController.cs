using ManzelOS_business_layer;
using ManzelOS_DTOs.Employees;
using Microsoft.AspNetCore.Mvc;

namespace ManzelOS_Core.Controllers.Employees
{
    [Route("api/EmployeeController")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {


        [HttpGet("ListEmployees", Name = "ListEmployees")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public ActionResult<IEnumerable<EmployeeDTO>> ListEmployees()
        {

            List<EmployeeDTO> employees = clsEmployee.ListAllEmployees();

            if(employees.Count <= 0)
            {
                return NotFound("There Is No Employees");
            }
            else
            {
                return Ok(employees);
            }
            
        }


        [HttpGet("employeeId", Name = "FindEmployeeById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public ActionResult<EmployeeDTO> FindEmployeeById(int employeeId)
        {

            clsEmployee employeeToFind = clsEmployee.FindEmployeeById(employeeId);
            if (employeeToFind == null)
            {
                return NotFound($"Employee With The ID: {employeeId} has not been found");

            }
            else
            {

                EmployeeDTO employeeDTO = employeeToFind.employeeDTO;
                return Ok(employeeDTO);
            }

        }

        [HttpPost("AddNewEmployee", Name = "AddNewEmployee")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        
        public ActionResult<EmployeeDTO> AddEmployee(EmployeeDTO employeeDTO)
        {
            
            if(employeeDTO == null)
            {
                return BadRequest("invalid data");
            }

            clsEmployee employeeToAdd = new clsEmployee(employeeDTO);
            
            employeeDTO.EmployeeId = employeeToAdd.EmployeeId;

            if(employeeToAdd.Save())
            {
                return CreatedAtRoute("FindEmployeeById", employeeDTO);
            }
            else
            {
                return BadRequest("Failed To Add Employee");
            }
        }
        [HttpPost("UpdateEmployee", Name = "UpdateEmployee")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public ActionResult<EmployeeDTO> UpdateEmployee(int employeeId, EmployeeDTO employeeDTO)
        {

            if (employeeDTO == null || employeeId < 0)
            {
                return BadRequest("invalid data");
            }

            clsEmployee employeeToUpdate = clsEmployee.FindEmployeeById(employeeId);

            if(employeeToUpdate == null)
            {
                return NotFound("No Employee Found");
            }

            employeeToUpdate.EmployeeId = employeeDTO.EmployeeId;
            employeeToUpdate.PersonId = employeeDTO.PersonId;
            employeeToUpdate.Salary = employeeDTO.Salary;
            employeeToUpdate.Job = employeeDTO.Job;
            employeeDTO.CreatedAt = employeeDTO.CreatedAt;

            if (employeeToUpdate.Save())
            {
                return Ok(employeeDTO);
            }
            else
            {
                return BadRequest("Failed To Update Employee");
            }
        }

        [HttpDelete("{employeeId}", Name = "DeleteEmployee")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult DeleteEmployee(int employeeId)
        {
            if (employeeId < 0)
            {
                return BadRequest("Invalid Id Value");
            }
            else
            {
                if(clsEmployee.DeleteEmployee(employeeId))
                {
                    return Ok("Employee Has Been Deleted Successfully");
                }
                else
                {
                    return BadRequest("Failed To Delete Employee ");
                }
            }
        }

    }


}
