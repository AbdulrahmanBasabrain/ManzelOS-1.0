using ManzelOS_business_layer;
using ManzelOS_DTOs.People;
using Microsoft.AspNetCore.Mvc;
using ManzelOS_DTOs.People;

namespace ManzelOS_Core.Controllers.PeopleControllers
{
    [Route("api/CountriesController")]
    [ApiController]
    public class CountriesController : ControllerBase
    {




        [HttpGet("FindCountryById", Name ="FindCountryById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        
        public ActionResult<CountryDTO> FindecountryById(short countryId)
        {

            if(countryId < 0)
            {
                return BadRequest($"The Value You Gave: {countryId} Is not valid to use as Person ID");
            }
            else
            {

                clsCountry country = clsCountry.Find(countryId);

                if (country == null)
                {
                    return NotFound($"Country With ID {countryId} Is not found");
                }
                else
                {

                   CountryDTO countryDTO = country.CountryDTO;

                    return Ok(countryDTO);

                }
            }

        }
 

        [HttpGet("ListCountries", Name = "ListCountries")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public ActionResult<IEnumerable<CountryDTO>> ListCountries()
        {

            List<CountryDTO> countryList = new List<CountryDTO>();
            countryList = clsCountry.ListAllCountries();
            if(countryList.Count == 0)
            {
                return NotFound("No Countries Found");
            }
            return Ok(countryList);
        }




    }
}
