using ManzelOS_business_layer.Payments;
using ManzelOS_DTOs.Payments;
using Microsoft.AspNetCore.Mvc;

namespace ManzelOS_Core.Controllers.PaymentAndBills.PaymentAndBillsLookUps
{
    [Route("api/BillTypeController")]
    [ApiController]
    public class BillTypeController : ControllerBase
    {


        [HttpGet("ListBillTypes", Name = "ListBillTypes")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public ActionResult<IEnumerable<BillTypeDTO>> ListBillTypes()
        {

            List<BillTypeDTO> billTypeDTOs = clsBillType.ListAllBillTypes();
            if (billTypeDTOs.Count == 0)
            {
                return NotFound("Nothing Found");
            }
            return Ok(billTypeDTOs);
        }
    }
}
