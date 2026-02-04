using ManzelOS_business_layer.Payments;
using ManzelOS_DTOs.Payments;
using Microsoft.AspNetCore.Mvc;

namespace ManzelOS_Core.Controllers.PaymentAndBills.PaymentAndBillsLookUps
{
    [Route("api/BillingPeriodTypeController")]
    [ApiController]
    public class BillingPeriodTypeController : ControllerBase
    {

        [HttpGet("ListBillingPeriodType", Name = "ListBillingPeriodType")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public ActionResult<IEnumerable<BillingPeriodTypeDTO>> ListBillingPeriodType()
        {

            List<BillingPeriodTypeDTO> billingPeriodTypeDTOs = clsBillingPeriodType.ListAllBillingPeriods();
            if (billingPeriodTypeDTOs.Count == 0)
            {
                return NotFound("Nothing Found");
            }
            return Ok(billingPeriodTypeDTOs);

        }
    }
}
