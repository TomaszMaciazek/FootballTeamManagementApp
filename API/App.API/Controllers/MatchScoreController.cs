using App.Model.Dtos;
using App.Model.Dtos.ListItemDtos;
using App.Model.Entities;
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
    public class MatchScoreController : ControllerBase
    {
        private readonly IMatchPlayerScoreService _matchScoreService;
        private readonly IMapper _mapper;

        public MatchScoreController(IMatchPlayerScoreService matchScoreService, IMapper mapper)
        {
            _matchScoreService = matchScoreService;
            _mapper = mapper;
        }

        //[HttpGet]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = Permissions.TrainingScoresPolicy)]
        //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PaginatedList<MatchScoreListItemDto>))]
        //public async Task<ActionResult<PaginatedList<MatchScoreListItemDto>>> GetScores([FromQuery] MatchScoreQuery query)
        //{
        //    var result = await _matchScoreService.GetScores(query);
        //    return Ok(result);
        //}

        [HttpGet("Match/{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = Permissions.MatchScoresPolicy)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<SimpleMatchScoreDto>))]
        public async Task<ActionResult<IEnumerable<SimpleMatchScoreDto>>> GetScoresFromMatch(Guid id)
        {
            return Ok(await _matchScoreService.GetScoresFromMatch(id));
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = Permissions.MatchScoresAddPolicy)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> CreateScore([FromBody] CreateMatchScoreVM model)
        {
            var trainingScore = _mapper.Map<MatchPlayerScore>(model);
            await _matchScoreService.AddAsync(trainingScore);
            return NoContent();
        }

        [HttpPut]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = Permissions.MatchScoresEditPolicy)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateScore([FromBody] UpdateMatchScoreVM model)
        {
            await _matchScoreService.UpdateAsync(model);
            return NoContent();
        }

        [HttpPatch]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = Permissions.MatchScoresActivatePolicy)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [Route("Activate/{id}")]
        public async Task<IActionResult> ActivateScore(Guid id)
        {
            await _matchScoreService.ActivateAsync(id);
            return NoContent();
        }

        [HttpPatch]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = Permissions.MatchScoresDeactivatePolicy)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [Route("Deactivate/{id}")]
        public async Task<IActionResult> DeactivateScore(Guid id)
        {
            await _matchScoreService.DeactivateAsync(id);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = Permissions.MatchScoresDeletePolicy)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _matchScoreService.RemoveAsync(id);
            return NoContent();
        }
    }
}
