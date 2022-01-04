using App.Model.Dtos;
using App.Model.Dtos.ListItemDtos;
using App.Model.Entities.SurveyEntities;
using App.Model.ViewModels.Commands;
using App.Model.ViewModels.Queries;
using App.ServiceLayer.Models;
using App.ServiceLayer.Services;
using App.UserMiddleware;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace App.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SurveyController : ControllerBase
    {
        private readonly ISurveyTemplateService _surveyTemplateService;
        private readonly IMapper _mapper;

        public SurveyController(ISurveyTemplateService surveyTemplateService, IMapper mapper)
        {
            _surveyTemplateService = surveyTemplateService;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = Permissions.SurveysPolicy)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SurveyWithResultsDto))]
        public async Task<ActionResult<SurveyWithResultsDto>> GetSurveys([FromRoute] Guid id)
        {
            return Ok(await _surveyTemplateService.GetByIdAsync(id));
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = Permissions.SurveysPolicy)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<SurveyQuestionDto>))]
        [Route("Questions/{id}")]
        public async Task<ActionResult<IEnumerable<SurveyQuestionDto>>> GetQuestionsFromSurvey([FromRoute] Guid id)
        {
            return Ok(await _surveyTemplateService.GetQuestionsFromSurvey(id));
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = Permissions.SurveysPolicy)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(FillSurveyDto))]
        [Route("FillTemplate/{id}")]
        public async Task<ActionResult<FillSurveyDto>> GetSurveyTemplateToFill([FromRoute] Guid id)
        {
            return Ok(await _surveyTemplateService.GetSurveyTemplateToFill(id));
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = Permissions.SurveysAllPolicy)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PaginatedList<SurveyListItemDto>))]
        [Route("All")]
        public async Task<ActionResult<PaginatedList<SurveyListItemDto>>> GetSurveys([FromQuery] SurveyTemplateQuery query)
        {
            return Ok(await _surveyTemplateService.GetSurveys(query));
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = Permissions.SurveysCreatedByMePolicy)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PaginatedList<MySurveyListItemDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Route("CreatedByMe")]
        public async Task<ActionResult<PaginatedList<MySurveyListItemDto>>> GetSurveysFromAuthor([FromQuery] SurveyTemplateQuery query)
        {
            if (!query.AuthorId.HasValue)
            {
                return BadRequest();
            }
            return Ok(await _surveyTemplateService.GetSurveysFromAuthor(query));
        }


        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = Permissions.SurveysAddPolicy)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> CreateSurvey([FromBody] CreateSurveyVM model)
        {
            var survey = _mapper.Map<SurveyTemplate>(model);
            await _surveyTemplateService.AddAsync(survey);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = Permissions.SurveysDeletePolicy)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _surveyTemplateService.RemoveAsync(id);
            return NoContent();
        }
    }
}
