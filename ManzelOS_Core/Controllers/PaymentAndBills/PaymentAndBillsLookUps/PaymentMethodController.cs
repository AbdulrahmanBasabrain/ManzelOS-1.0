using ManzelOS_business_layer.Payments;
using ManzelOS_DTOs.Payments;
using Microsoft.AspNetCore.Mvc;

namespace ManzelOS_Core.Controllers.PaymentAndBills.PaymentAndBillsLookUps
{
    [Route("api/PaymentMethodController")]
    [ApiController]
    public class PaymentMethodController : ControllerBase
    {

        [HttpGet("ListPaymentMethods", Name = "ListPaymentMethods")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public ActionResult<IEnumerable<PaymentMethodDTO>> ListPaymentMethods()
        {

            List<PaymentMethodDTO> paymentMethodDTOs = clsPaymentMethod.ListAllPaymentMethods();
            if (paymentMethodDTOs.Count == 0)
            {
                return NotFound("Nothing Found");
            }
            return Ok(paymentMethodDTOs);
        }

    }
}
