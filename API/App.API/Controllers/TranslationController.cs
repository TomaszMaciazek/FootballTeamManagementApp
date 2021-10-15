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
    public class TranslationController : ControllerBase
    {
        private readonly ITranslationService _translationService;
        private readonly IMapper _mapper;

        public TranslationController(ITranslationService translationService, IMapper mapper)
        {
            _translationService = translationService;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<TranslationDto>))]
        public async Task<ActionResult<IEnumerable<TranslationDto>>> GetTranslations()
        {
            var languages = _mapper.Map<IEnumerable<TranslationDto>>(await _translationService.GetAllAsync());
            return Ok(languages);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<TranslationDto>))]
        [Route("/Language/{id}")]
        public async Task<ActionResult<IEnumerable<TranslationDto>>> GetTranslationsFromLanguage(Guid id)
        {
            var languages = _mapper.Map<IEnumerable<TranslationDto>>(await _translationService.GetAllFromLanguageAsync(id));
            return Ok(languages);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TranslationDto))]
        public async Task<ActionResult<IEnumerable<TranslationDto>>> GetTranslationById(Guid id)
        {
            var language = _mapper.Map<TranslationDto>(await _translationService.GetByIdAsync(id));
            return Ok(language);
        }
    }
}
