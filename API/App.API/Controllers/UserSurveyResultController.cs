using App.Model.Dtos;
using App.Model.Dtos.ListItemDtos;
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
using System.Linq;
using System.Threading.Tasks;

namespace App.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserSurveyResultController : ControllerBase
    {
        private readonly IUserSurveyResultService _userSurveyResultService;
        private readonly IMapper _mapper;

        public UserSurveyResultController(IUserSurveyResultService userSurveyResultService, IMapper mapper)
        {
            _userSurveyResultService = userSurveyResultService;
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = Permissions.SurveysAssignedToMePolicy)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PaginatedList<UserSurveyResultListItemDto>))]
        public async Task<ActionResult<PaginatedList<UserSurveyResultListItemDto>>> GetUserResults([FromQuery] UserSurveyResultQuery query)
        {
            return Ok(await _userSurveyResultService.GetUserResults(query));
        }

        [HttpGet("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = Permissions.SurveysPolicy)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserSurveyResultDto))]
        public async Task<ActionResult<UserSurveyResultDto>> GetSurveyResult([FromRoute] Guid id)
        {
            return Ok(await _userSurveyResultService.GetByIdAsync(id));
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = Permissions.SurveysRespondentsPolicy)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<SimpleUserDto>))]
        [Route("Respondents/{id}")]
        public async Task<ActionResult<IEnumerable<SimpleUserDto>>> GetSurveyRespondents([FromRoute] Guid id)
        {
            return Ok(await _userSurveyResultService.GetSurveyRespondents(id));
        }

        [HttpPut]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = Permissions.SurveysPolicy)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Update([FromBody] FillSurveyVM model)
        {
            await _userSurveyResultService.UpdateAsync(model);
            return NoContent();
        }
    }
}
