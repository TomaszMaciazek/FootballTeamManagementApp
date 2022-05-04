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
using System.Linq;
using System.Threading.Tasks;

namespace App.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MatchPointController : ControllerBase
    {
        private readonly IMatchPointService _matchPointService;
        private readonly IMapper _mapper;

        public MatchPointController(IMatchPointService matchPointService, IMapper mapper)
        {
            _matchPointService = matchPointService;
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = Permissions.MatchPointsPolicy)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<MatchPointDto>))]
        [Route("Match/{id}")]
        public async Task<ActionResult<IEnumerable<MatchPointDto>>> GetPointsFromMatch([FromRoute] Guid id)
        {
            return Ok(await _matchPointService.GetAllFromMatch(id));
        }

        [HttpPatch]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = Permissions.MatchPointsActivatePolicy)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [Route("Activate/{id}")]
        public async Task<IActionResult> Activate(Guid id)
        {
            await _matchPointService.ActivateAsync(id);
            return NoContent();
        }

        [HttpPatch]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = Permissions.MatchPointsDeactivatePolicy)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [Route("Deactivate/{id}")]
        public async Task<IActionResult> Deactivate(Guid id)
        {
            await _matchPointService.DeactivateAsync(id);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = Permissions.MatchPointsDeletePolicy)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status405MethodNotAllowed)]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _matchPointService.RemoveAsync(id);
            return NoContent();
        }
    }
}
