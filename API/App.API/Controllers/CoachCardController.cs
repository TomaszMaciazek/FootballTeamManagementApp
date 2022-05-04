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
    public class CoachCardController : ControllerBase
    {
        private readonly ICoachCardService _coachCardService;
        private readonly IMapper _mapper;

        public CoachCardController(ICoachCardService coachCardService, IMapper mapper)
        {
            _coachCardService = coachCardService;
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = Permissions.CardsPolicy)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<CoachCardDto>))]
        [Route("Match/{id}")]
        public async Task<ActionResult<IEnumerable<CoachCardDto>>> GetCardsFromMatch([FromRoute] Guid id)
        {
            return Ok(await _coachCardService.GetCardsFromMatchAsync(id));
        }

        [HttpPatch]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = Permissions.CardsActivatePolicy)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [Route("Activate/{id}")]
        public async Task<IActionResult> Activate(Guid id)
        {
            await _coachCardService.ActivateAsync(id);
            return NoContent();
        }

        [HttpPatch]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = Permissions.CardsDeactivatePolicy)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [Route("Deactivate/{id}")]
        public async Task<IActionResult> Deactivate(Guid id)
        {
            await _coachCardService.DeactivateAsync(id);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = Permissions.CardsDeletePolicy)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status405MethodNotAllowed)]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _coachCardService.RemoveAsync(id);
            return NoContent();
        }
    }
}
