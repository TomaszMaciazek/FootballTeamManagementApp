using App.Model.Dtos;
using App.ServiceLayer.Services;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace App.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LanguageController : ControllerBase
    {
        private readonly ILanguageService _languageService;
        private readonly IMapper _mapper;

        public LanguageController(ILanguageService languageService, IMapper mapper)
        {
            _languageService = languageService;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<LanguageDto>))]
        public async Task<ActionResult<IEnumerable<LanguageDto>>> GetLanguages()
        {
            var languages = _mapper.Map<IEnumerable<LanguageDto>>(await _languageService.GetAllAsync());
            return Ok(languages);

        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(LanguageDto))]
        public async Task<ActionResult<IEnumerable<LanguageDto>>> GetLanguageById(Guid id)
        {
            var language = _mapper.Map<LanguageDto>(await _languageService.GetByIdAsync(id));
            return Ok(language);
        }
    }
}
