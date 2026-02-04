using ManzelOS_business_layer.Payments;
using ManzelOS_DTOs.Payments;
using Microsoft.AspNetCore.Mvc;

namespace ManzelOS_Core.Controllers.PaymentAndBills.PaymentAndBillsLookUps
{
    [Route("api/PaymentStatusController")]
    [ApiController]
    public class PaymentStatusController : ControllerBase
    {

        [HttpGet("ListPaymentStatuses", Name = "ListPaymentStatuses")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public ActionResult<IEnumerable<PaymentStatusDTO>> ListPaymentStatuses()
        {

            List<PaymentStatusDTO> paymentStatusDTOs = clsPaymentStatus.ListAllPaymentStatuses ();
            if (paymentStatusDTOs.Count == 0)
            {
                return NotFound("Nothing Found");
            }
            return Ok(paymentStatusDTOs);
        }

    }
}
