using ManzelOS_business_layer.Payments;
using ManzelOS_data_access_layer.PaymentsData;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ManzelOS_Core.Controllers.PaymentAndBills.PaymentAndBills
{
    [Route("api/GeneratedBillController")]
    [ApiController]
    public class GeneratedBillController : ControllerBase
    {

        [HttpGet("ListGeneratedBills", Name = "ListGeneratedBills")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public ActionResult<IEnumerable<GeneratedBillDTO>>ListGeneratedBills()
        {

            List<GeneratedBillDTO> generatedBillDTOs = clsGeneratedBill.ListAllGeneratedBills();

            if (generatedBillDTOs.Count <= 0)
            {
                return NotFound("There Are no generated bills");
            }
            else
            {
                return Ok(generatedBillDTOs);
            }

        }

        [HttpGet("FindGeneratedBillById", Name = "FindGeneratedBillById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public ActionResult<GeneratedBillDTO> FindGeneratedBillById(int generatedBillId)
        {
            
            if(generatedBillId < 0)
            {
                return BadRequest("bad id value");
            }
            else
            {
                
                clsGeneratedBill generatedBillToFind = clsGeneratedBill.FindGeneratedBillById(generatedBillId);
                if (generatedBillToFind == null)
                {
                    return NotFound("There is no generated bill with that id");
                }
                else
                {
                    GeneratedBillDTO generatedBillDTO = generatedBillToFind.generatedBillDTO;
                    return Ok(generatedBillDTO);
                }

            }
            

        }

        [HttpPost("AddNewGeneratedBill", Name = "AddNewGeneratedBill")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        
        public ActionResult<GeneratedBillDTO> AddNewGeneratedBill(GeneratedBillDTO generatedBillDTO)
        {

           if(generatedBillDTO == null)
           {
                return BadRequest("bad data");
           }
           else
           {
                clsGeneratedBill generatedBillToAdd = new clsGeneratedBill(generatedBillDTO);
                if(generatedBillToAdd.Save())
                {
                    generatedBillDTO.GeneratedBillId = generatedBillToAdd.GeneratedBillId;
                    return CreatedAtRoute("FindGeneratedBillById", generatedBillDTO);
                }
                else
                {
                    return BadRequest("Failed To add a generated bill");
                }
           }

        }

        [HttpPost("UpdateGeneratedBill", Name = "UpdateGeneratedBill")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public ActionResult<GeneratedBillDTO> UpdateGeneratedBill(int generatedBillId, GeneratedBillDTO generatedBillDTO)
        {

            if (generatedBillDTO == null || generatedBillId < 0)
            {
                return BadRequest("bad data");
            }
            else
            {

                clsGeneratedBill generatedBillToUpdate = clsGeneratedBill.FindGeneratedBillById(generatedBillId);
                
                generatedBillToUpdate.GeneratedBillId = generatedBillDTO.GeneratedBillId;
                generatedBillToUpdate.GeneratedBillFees = generatedBillDTO.GeneratedBillFees;
                generatedBillToUpdate.BillTypeId = generatedBillDTO.BillTypeId;
                generatedBillToUpdate.RentalContractId = generatedBillDTO.RentalContractId;
                generatedBillToUpdate.RentalUnitId = generatedBillDTO.RentalUnitId;
                generatedBillToUpdate.GeneratedBillId = generatedBillDTO.GeneratedBillId;
                generatedBillToUpdate.CreatedAt = generatedBillDTO.CreatedAt;
                generatedBillToUpdate.BillPaymentStatusId = generatedBillDTO.BillPaymentStatusId;
                generatedBillToUpdate.BillExpectedPaymentDate = generatedBillDTO.BillExpectedPaymentDate;
                generatedBillToUpdate.IsPaid = generatedBillDTO.IsPaid;

                if (generatedBillToUpdate.Save())
                {
                    return Ok(generatedBillDTO);
                }
                else
                {
                    return BadRequest("Failed To Update a generated bill");
                }
            }

        }

        [HttpDelete("DeleteGeneratedBill", Name = "DeleteGeneratedBill")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        
        public ActionResult DeleteGeneratedBill(int generatedBillId)
        {
           if(generatedBillId < 0)
            {
                return BadRequest("Bad data");
            }
            else
            {
                if(clsGeneratedBill.Delete(generatedBillId))
                {

                return Ok("Generated Bill Has been deleted successfully");
                }
                else
                {
                    return BadRequest("Failed To Delete generated bill");
                }

            }
        }

    }
}
