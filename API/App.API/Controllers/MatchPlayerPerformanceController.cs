using App.Model.Dtos;
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
    public class MatchPlayerPerformanceController : ControllerBase
    {
        private readonly IMatchPlayerPerformanceService _matchPlayerPerformanceService;
        private readonly IMapper _mapper;

        public MatchPlayerPerformanceController(IMatchPlayerPerformanceService matchPlayerPerformanceService, IMapper mapper)
        {
            _matchPlayerPerformanceService = matchPlayerPerformanceService;
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = Permissions.MatchPlayerPerformancesPolicy)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<SimpleMatchPlayerDto>))]
        [Route("Match/{id}")]
        public async Task<ActionResult<IEnumerable<SimpleMatchPlayerDto>>> GetPlayersFromMatch(Guid id)
        {
            return Ok(await _matchPlayerPerformanceService.GetPlayersFromMatch(id));
        }

        [HttpPatch]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = Permissions.MatchPlayerPerformancesActivatePolicy)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [Route("Activate/{id}")]
        public async Task<IActionResult> Activate(Guid id)
        {
            await _matchPlayerPerformanceService.ActivateAsync(id);
            return NoContent();
        }

        [HttpPatch]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = Permissions.MatchPlayerPerformancesDeactivatePolicy)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [Route("Deactivate/{id}")]
        public async Task<IActionResult> Deactivate(Guid id)
        {
            await _matchPlayerPerformanceService.DeactivateAsync(id);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = Permissions.MatchPlayerPerformancesDeletePolicy)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status405MethodNotAllowed)]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _matchPlayerPerformanceService.RemoveAsync(id);
            return NoContent();
        }
    }
}
