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
    public class UserTestResultController : ControllerBase
    {
        private readonly IUserTestResultService _userTestResultService;

        public UserTestResultController(IUserTestResultService userTestResultService)
        {
            _userTestResultService = userTestResultService;
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = Permissions.TestsAssignedToMePolicy)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PaginatedList<UserTestResultListItemDto>))]
        public async Task<ActionResult<PaginatedList<UserTestResultListItemDto>>> GetUserResults([FromQuery] UserTestResultQuery query)
        {
            return Ok(await _userTestResultService.GetUserResults(query));
        }

        [HttpGet("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = Permissions.TestsPolicy)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserTestResultDto))]
        public async Task<ActionResult<UserTestResultDto>> GetTestResult([FromRoute] Guid id)
        {
            return Ok(await _userTestResultService.GetByIdAsync(id));
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = Permissions.RespondentsPolicy)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<SimpleUserDto>))]
        [Route("Respondents/{id}")]
        public async Task<ActionResult<IEnumerable<SimpleUserDto>>> GetTestRespondents([FromRoute] Guid id)
        {
            return Ok(await _userTestResultService.GetTestRespondents(id));
        }

        [HttpPut]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = Permissions.TestsPolicy)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        public async Task<IActionResult> Update([FromBody] FillTestVM model)
        {
            return Ok(await _userTestResultService.UpdateAsync(model));
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = Permissions.TestsPolicy)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PaginatedList<SimpleUserTestResultListItemDto>))]
        [Route("Test")]
        public async Task<ActionResult<PaginatedList<SimpleUserTestResultListItemDto>>> GetUserResults([FromQuery] TestResultsQuery query)
        {
            return Ok(await _userTestResultService.GetUserResults(query));
        }
    }
}
