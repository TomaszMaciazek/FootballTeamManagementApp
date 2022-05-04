using App.Model.Dtos;
using App.ServiceLayer.Models;
using App.Model.ViewModels.Queries;
using App.ServiceLayer.Services;
using App.UserMiddleware;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using App.Model.Dtos.ListItemDtos;
using App.Model.ViewModels.Commands;
using App.Model.Entities;

namespace App.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MatchController : ControllerBase
    {
        private readonly IMatchService _matchService;
        private readonly IMapper _mapper;

        public MatchController(IMatchService matchService, IMapper mapper)
        {
            _matchService = matchService;
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = Permissions.MatchesPolicy)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PaginatedList<MatchListItemDto>))]
        public async Task<ActionResult<PaginatedList<MatchListItemDto>>> GetMatches([FromQuery] MatchQuery query)
        {
            return Ok(await _matchService.GetMatches(query));
        }

        [HttpGet("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = Permissions.MatchesPolicy)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(NewsDto))]
        public async Task<ActionResult<MatchDto>> GetMatchById(Guid id)
        {
            var match = await _matchService.GetByIdAsync(id);
            return Ok(match);
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = Permissions.MatchesAddPolicy)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> CreateMatch([FromBody] CreateMatchVM model)
        {
            var match = _mapper.Map<Match>(model);
            await _matchService.AddAsync(match);
            return NoContent();
        }

        [HttpPatch]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = Permissions.MatchesActivatePolicy)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [Route("Activate/{id}")]
        public async Task<IActionResult> ActivateMatch(Guid id)
        {
            await _matchService.ActivateAsync(id);
            return NoContent();
        }

        [HttpPatch]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = Permissions.MatchesDeactivatePolicy)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [Route("Deactivate/{id}")]
        public async Task<IActionResult> DeactivateMatch(Guid id)
        {
            await _matchService.DeactivateAsync(id);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = Permissions.MatchesDeletePolicy)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _matchService.RemoveAsync(id);
            return NoContent();
        }
    }
}
