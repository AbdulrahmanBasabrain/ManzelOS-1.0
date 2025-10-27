using ManzelOS_business_layer.Payments;
using ManzelOS_data_access_layer.PaymentsData;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace ManzelOS_Core.Controllers.PaymentAndBills.PaymentAndBills
{
    [Route("api/PaymentController")]
    [ApiController]
    public class PaymentController : ControllerBase
    {

        [HttpGet("ListPayments", Name = "ListPayments")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public ActionResult <IEnumerable<PaymentDTO>>ListPayments()
        {
            List<PaymentDTO>paymentDTOs = clsPayment.ListAllPayments();

            if(paymentDTOs.IsNullOrEmpty())
            {
                return NotFound("there are no payment whatsoever");
            }
            else
            {
                return Ok(paymentDTOs);
            }
        }

        [HttpGet("FindPaymentById", Name = "FindPaymentById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public ActionResult <PaymentDTO>FindPaymentById(int paymentId)
        {
            
            if(paymentId < 0)
            {
                return BadRequest("bad data");
            }

            clsPayment paymentToFind = clsPayment.FindPaymentById(paymentId); 
            if(paymentToFind == null)
            {
                return NotFound("There are no payment with that id");
            }
            else
            {

                PaymentDTO paymentDTO = paymentToFind.PaymentDTO;
                return Ok(paymentDTO);

            }


        }

        [HttpPost("AddNewPayment", Name = "AddNewPayment")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public ActionResult<PaymentDTO> AddNewPayment(PaymentDTO paymentDTO)
        {

            if (paymentDTO == null)
            {
                return BadRequest("bad data");
            }

            clsPayment paymentToAdd = new clsPayment(paymentDTO);

            if (paymentToAdd.Save())
            {

                paymentDTO.PaymentId = paymentToAdd.PaymentId;
                return CreatedAtRoute("FindPaymentById", paymentToAdd);

            }
            else
            {

                return BadRequest("Failed To add Payment");

            }


        }


    }
}
