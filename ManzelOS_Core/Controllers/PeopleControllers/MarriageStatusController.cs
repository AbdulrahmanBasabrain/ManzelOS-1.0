using ManzelOS_business_layer;
using ManzelOS_DTOs.People;
using Microsoft.AspNetCore.Mvc;

namespace ManzelOS_Core.Controllers.PeopleControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MarriageStatusController : ControllerBase
    {

        [HttpGet("ListMarritialStatuses", Name = "ListMarriatialStatus")]        
        
        public ActionResult<IEnumerable<MarriageStatusDTO>> ListMarriatialStatus()
        {
            List<MarriageStatusDTO> marriageStatuses = clsMarriageStatus.ListMarriageStatus();
            if(marriageStatuses.Count == 0)
            {
                return NotFound("There is no marritial status we don't intervine with people relationship statuses");
            }
            else
            {
                return Ok(marriageStatuses);
            }
        }

    }
}
