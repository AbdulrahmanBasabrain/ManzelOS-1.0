using ManzelOS_business_layer.Payments;
using ManzelOS_DTOs.Payments;
using Microsoft.AspNetCore.Mvc;

namespace ManzelOS_Core.Controllers.PaymentAndBills.PaymentAndBillsLookUps
{
    [Route("api/ListBillPaymentStatus")]
    [ApiController]
    public class BillPaymentStatus : ControllerBase
    {


        [HttpGet("ListBillPaymentStatus", Name = "ListBillPaymentStatus")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public ActionResult<IEnumerable<BillPaymentStatusDTO>> ListBillPaymentStatus()
        {

            List<BillPaymentStatusDTO> billPaymentStatusDTOs = clsBillPaymentStatus.ListAllBillPaymentStatuses();
            if (billPaymentStatusDTOs.Count == 0)
            {
                return NotFound("Nothing Found");
            }
            return Ok(billPaymentStatusDTOs);

        }

    }
}
