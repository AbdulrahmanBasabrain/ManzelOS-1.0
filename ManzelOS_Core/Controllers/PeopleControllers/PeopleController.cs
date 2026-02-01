using ManzelOS_business_layer;
using ManzelOS_data_access_layer.PeopleData;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ManzelOS_Core.Controllers.PeopleControllers
{

    [Route("api/PeopleController")]
    [ApiController]
    public class PeopleController : ControllerBase
    {
        
                


        [HttpGet("ListPeople", Name = "ListPeople")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public ActionResult<IEnumerable<PeopleDTO>> ListAllPeople()
        {

            List<PeopleDTO> peopleList = clsPeople.ListAllPeople();
            if(peopleList.Count == 0)
            {
                return NotFound("No People Found");
            }
            return Ok(peopleList);
        }


        [HttpGet("{id}", Name ="FindePersonById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        
        public ActionResult<PeopleDTO> FindePersonById(int personId)
        {

            if(personId < 0)
            {
                return BadRequest($"The Value You Gave: {personId} Is not valid to use as Person ID");
            }
            else
            {

                clsPeople person = clsPeople.FindPersonById(personId);

                if (person == null)
                {
                    return NotFound($"Person With ID {personId} Is not found");
                }
                else
                {

                    PeopleDTO personDTO = person.PersonDTO;

                    return Ok(personDTO);

                }
            }

        }
 

        [HttpPut("UpdatePerson", Name ="UpdateNewPerson")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        
        public ActionResult<PeopleDTO> UpdatePerson(int personId, PeopleDTO updatedPerson)
        {

            if (updatedPerson == null || personId < 0)
                {
                    return BadRequest("Invalid Person Data");
                }
            
            clsPeople personToUpdate = clsPeople.FindPersonById(personId);

            personToUpdate = new clsPeople(updatedPerson);

            if(personToUpdate == null)
            {
                    return NotFound($"Person With ID {personId} Is not found");
            }

            
            personToUpdate.FirstName = updatedPerson.FirstName;
            personToUpdate.FatherName = updatedPerson.FatherName;
            personToUpdate.GrandFatherName = updatedPerson.GrandFatherName;
            personToUpdate.LastName = updatedPerson.LastName;
            personToUpdate.NationalId = updatedPerson.NationalID;
            personToUpdate.DateOfBirth = updatedPerson.DateOfBirth;
            personToUpdate.Gender = updatedPerson.Gender;
            personToUpdate.CountryId = updatedPerson.CountryId;
            personToUpdate.CityId = updatedPerson.CityId;
            personToUpdate.AddressDistrict = updatedPerson.AddressDistrict;
            personToUpdate.PersonalEmail = updatedPerson.PersonalEmail;
            personToUpdate.Phone = updatedPerson.Phone;
            personToUpdate.PersonalImagePath = updatedPerson.PersonalImagePath;
            personToUpdate.MarriageStatusId = updatedPerson.MarriageStatusId;
            personToUpdate.CreatedAt = updatedPerson.CreatedAt;

            if(personToUpdate.Save())
            {


                return Ok(updatedPerson);

            }
            else
            {
                return BadRequest("Failed To Update Person");
            }

        }
 

        [HttpPost("AddNewPerson", Name ="AddNewPerson")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        
        public ActionResult<PeopleDTO> AddNewPerson(PeopleDTO newPerson)
        {

            if (newPerson == null)
            {
                    return BadRequest("Invalid Person Data");
            }
            
            clsPeople personToAdd = new clsPeople(newPerson);

            if(personToAdd.Save())
            {


                return CreatedAtRoute("FindePersonById", new { id = newPerson.PersonId }, newPerson);

            }
            else
            {
                return BadRequest("Failed To Save Person");
            }

        }

        [HttpDelete("{personId}", Name = "DeletePerson")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        
        public ActionResult DeletePerson(int personId)
        {
            if(personId < 0)
            {
                return BadRequest($"Not Accepted ID {personId}");
            }

            if(clsPeople.DeletePerson(personId))
            {
                return Ok($"Person With {personId} has been deleted Successfully");
            }
            else
            {
                return NotFound($"Person with {personId} has not been found ");
            }
        }


    }
}
