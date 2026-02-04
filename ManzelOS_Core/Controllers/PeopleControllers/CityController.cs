using ManzelOS_business_layer;
using ManzelOS_DTOs.People;
using Microsoft.AspNetCore.Mvc;

namespace ManzelOS_Core.Controllers.PeopleControllers
{
    [Route("api/CityController")]
    [ApiController]
    public class CityController : ControllerBase
    {



        [HttpGet("FindCityById", Name ="FindCityById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        
        public ActionResult<CityDTO> FindeCityById(int cityId)
        {

            if(cityId < 0)
            {
                return BadRequest($"The Value You Gave: {cityId} Is not valid to use as ID");
            }
            else
            {

                clsCity city = clsCity.Find(cityId);

                if (city == null)
                {
                    return NotFound($"Country With ID {cityId} Is not found");
                }
                else
                {


                    CityDTO cityDTO = city.CityDTO;
                    return Ok(cityDTO);

                }
            }

        }
 

        [HttpGet("ListCities", Name = "ListCities")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public ActionResult<IEnumerable<CityDTO>> ListCities()
        {

            List<CityDTO> citiesList = new List<CityDTO>();
            citiesList = clsCity.ListAllCities();
            if(citiesList.Count == 0)
            {
                return NotFound("No Countries Found");
            }
            return Ok(citiesList);
        }


    }
}
