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
    public class PlayerCardController : ControllerBase
    {
        private readonly IPlayerCardService _playerCardService;
        private readonly IMapper _mapper;

        public PlayerCardController(IPlayerCardService playerCardService, IMapper mapper)
        {
            _playerCardService = playerCardService;
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = Permissions.CardsPolicy)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<PlayerCardDto>))]
        [Route("Match/{id}")]
        public async Task<ActionResult<IEnumerable<PlayerCardDto>>> GetCardsFromMatch([FromRoute] Guid id)
        {
            return Ok(await _playerCardService.GetCardsFromMatchAsync(id));
        }

        [HttpPatch]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = Permissions.CardsActivatePolicy)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [Route("Activate/{id}")]
        public async Task<IActionResult> Activate(Guid id)
        {
            await _playerCardService.ActivateAsync(id);
            return NoContent();
        }

        [HttpPatch]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = Permissions.CardsDeactivatePolicy)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [Route("Deactivate/{id}")]
        public async Task<IActionResult> Deactivate(Guid id)
        {
            await _playerCardService.DeactivateAsync(id);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = Permissions.CardsDeletePolicy)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status405MethodNotAllowed)]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _playerCardService.RemoveAsync(id);
            return NoContent();
        }
    }
}
