using App.Model.Dtos.History;
using App.ServiceLayer.Services;
using App.UserMiddleware;
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
    public class PlayerHistoryController : ControllerBase
    {
        private readonly IPlayerHistoryService _playerHistoryService;

        public PlayerHistoryController(IPlayerHistoryService playerHistoryService)
        {
            _playerHistoryService = playerHistoryService;
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = Permissions.PlayersHistoriesPolicy)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PlayerHistoryDto))]
        public async Task<ActionResult<PlayerHistoryDto>> GetPlayerHistory([FromQuery] Guid playerId)
        {
            return Ok(await _playerHistoryService.GetPlayerHistoryAsync(playerId));
        }
    }
}
