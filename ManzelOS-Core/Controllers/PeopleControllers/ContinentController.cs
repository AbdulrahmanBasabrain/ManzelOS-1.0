using ManzelOS_business_layer;
using ManzelOS_data_access_layer.PeopleData;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ManzelOS_Core.Controllers.PeopleControllers
{
    [Route("api/ContinentController")]
    [ApiController]
    public class ContinentController : ControllerBase
    {



        [HttpGet("ListContinent", Name = "ListAllContinent")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public ActionResult<IEnumerable<PeopleDTO>> ListAllContinent()
        {

            List<ContinentDTO> continent = clsContinent.ListAllContinents();
            if (continent.Count == 0)
            {
                return NotFound("No Continent Found");
            }
            return Ok(continent);
        }


        [HttpGet("FindeContinentId", Name = "FindeContinentId")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public ActionResult<ContinentDTO> FindeContinentId(short continentId)
        {

            if (continentId < 0)
            {
                return BadRequest($"The Value You Gave: {continentId} Is not valid to use ");
            }
            else
            {

                clsContinent continent = clsContinent.Find(continentId);
                if (continent == null)
                {
                    return NotFound($"Continent With ID {continentId} Is not found");
                }
                else
                {

                    ContinentDTO continentDTO = continent.ContinentDTO; 
                    return Ok(continentDTO);

                }
            }

        }




    }
}
