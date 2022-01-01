using App.Model.Dtos;
using App.Model.Dtos.ListItemDtos;
using App.Model.ViewModels.Commands;
using App.ServiceLayer.Models;
using App.Model.ViewModels.Queries;
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
    public class PlayerController : ControllerBase
    {
        private readonly IPlayerService _playerService;

        public PlayerController(IPlayerService playerService)
        {
            _playerService = playerService;
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = Permissions.PlayersPolicy)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PaginatedList<PlayerListItemDto>))]
        public async Task<ActionResult<PaginatedList<PlayerListItemDto>>> GetPlayers([FromQuery] PlayerQuery query)
        {
            return Ok(await _playerService.GetPlayers(query));
        }

        [HttpGet("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = Permissions.PlayersPolicy)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PlayerDto))]
        public async Task<ActionResult<PlayerDto>> GetPlayerById(Guid id)
        {
            return Ok(await _playerService.GetByIdEagerAsync(id));
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = Permissions.PlayersPolicy)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<SimpleSelectPlayerDto>))]
        [Route("All")]
        public async Task<ActionResult<IEnumerable<SimpleSelectPlayerDto>>> GetAllPlayers()
        {
            return Ok(await _playerService.GetAllAsync());
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = Permissions.PlayersPolicy)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<SimpleSelectPlayerDto>))]
        [Route("Playing")]
        public async Task<ActionResult<IEnumerable<SimpleSelectPlayerDto>>> GetPlayingPlayers([FromQuery] PlayingPlayerQuery query)
        {
            return Ok(await _playerService.GetPlayingPlayers(query.Date, query.PlayersGender));
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = Permissions.PlayersTeamsEditPolicy)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<SimpleSelectPlayerDto>))]
        [Route("WithoutTeam")]
        public async Task<ActionResult<IEnumerable<SimpleSelectPlayerDto>>> GetActivePlayersWithoutTeam()
        {
            var players = await _playerService.GetActivePlayersWithoutTeam();
            return Ok(players);
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = Permissions.PlayersTeamsEditPolicy)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [Route("AddToTeam")]
        public async Task<IActionResult> AddPlayerToTeam([FromBody] AddPlayersToTeamVM command)
        {
            await _playerService.AddPlayerToTeam(command);
            return NoContent();
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = Permissions.PlayersTeamsEditPolicy)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [Route("RemoveFromTeam")]
        public async Task<IActionResult> RemovePlayerFromTeam([FromBody] RemovePlayerFromTeam command)
        {
            await _playerService.RemovePlayerFromTeam(command.PlayerId, command.TeamId);
            return NoContent();
        }
    }
}
