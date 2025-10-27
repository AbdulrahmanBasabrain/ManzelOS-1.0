using ManzelOS_business_layer;
using ManzelOS_data_access_layer.TenantsData;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ManzelOS_Core.Controllers.Tenants
{
    [Route("api/TenantController")]
    [ApiController]
    public class TenantController : ControllerBase
    {

        [HttpGet("List Tenants")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public ActionResult<IEnumerable<TenantDTO>> ListAllTenants()
        {

            List<TenantDTO> tenants = clsTenant.ListAllTenants();
            if (tenants.Count == 0)
            {
                return NotFound("There Is No Tenants");
            }
            else { return Ok(tenants); }

        }

        [HttpGet("{tenantId}", Name = "FindTenantById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public ActionResult FindTenantById(int tenantId)
        {

            if (tenantId < 0)
            {

                return BadRequest("You Entered Invalide ID Data");

            }
            else
            {
                clsTenant tenant = new clsTenant();
                tenant = clsTenant.FindTenantById(tenantId);
                if (tenant == null)
                {
                    return NotFound($"Tenant with the ID {tenantId} has not been found");
                }
                else
                {
                    TenantDTO tenantDTO = tenant.TenantDTO;

                    return Ok(tenantDTO);
                }

            }

        }


        [HttpPost("AddNewTenant", Name = "AddNewTenant")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<TenantDTO> AddNewTenant(TenantDTO tenantDTO)
        {

            if (tenantDTO == null)
            {
                return BadRequest("Invalid Data");
            }

            clsTenant newTenant = new clsTenant(tenantDTO);

            tenantDTO.TenantId = newTenant.TenantId;

            if (newTenant.Save())
            {
                return CreatedAtRoute("FindTenantById", new { id = tenantDTO.TenantId }, tenantDTO);
            }
            else
            {
                return BadRequest("Failed To Add Tenant");
            }

        }

        [HttpPost("UpdateTenant")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public ActionResult<TenantDTO>UpdateTenant(int tenantId, TenantDTO tenantDTO)
        {
            
            if(tenantDTO == null || tenantId < 0)
            {
                return BadRequest("Invalid Data");
            }
            else
            {
                clsTenant tenantToUpdate = clsTenant.FindTenantById(tenantId);
                if (tenantToUpdate == null)
                {
                    return BadRequest("There is no tenant with that ID");
                }
                else
                {
                    tenantToUpdate.PersonId = tenantDTO.PersonId;
                    tenantToUpdate.DocumentsFilePath = tenantDTO.DocumentsFilePath;
                    tenantToUpdate.CreatedAt = tenantDTO.CreatedAt;

                    if(tenantToUpdate.Save())
                    {
                        return Ok("Tenant Updated Successfully");
                    }
                    else
                    {
                        return BadRequest("Failed To Tenant");
                    }
                }

            }
        }

        [HttpDelete("{tenantId}", Name = "DeleteTenant")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public ActionResult DeleteTenant(int tenantId)
        {

            if (tenantId < 0)
            {

                return BadRequest("Invalid ID value");

            }
            
            if(clsTenant.FindTenantById(tenantId) == null)
            {

                return BadRequest($"Failed To Delete Tenant with tenant ID: {tenantId} because it is not found");

            }

            if(clsTenant.DeleteTenant(tenantId))
            {
                return Ok($"Tenant With {tenantId} has been Deleted Successfully");
            }
            else
            {
                return BadRequest($"Failed To Delete Tenant with tenant ID {tenantId}");
            }

        }

    }
}
