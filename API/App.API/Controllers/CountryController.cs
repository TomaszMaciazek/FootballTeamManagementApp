using App.Model.Dtos;
using App.ServiceLayer.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace App.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly ICountryService _countryService;

        public CountryController(ICountryService countryService)
        {
            _countryService = countryService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<CountryDto>))]
        public async Task<ActionResult<IEnumerable<CountryDto>>> GetCountries()
        {
            return Ok(await _countryService.GetCountriesAsync());
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CountryDto))]
        public async Task<ActionResult<CountryDto>> GetCountryById(int id)
        {
            return Ok(await _countryService.GetCountryByIdAsync(id));
        }
    }
}
