using App.API.Models;
using App.DataAccess.Configurations.Interfaces;
using App.Model.Dtos;
using App.Model.Dtos.ListItemDtos;
using App.Model.Entities;
using App.Model.Enums;
using App.Model.ViewModels;
using App.Model.ViewModels.Commands;
using App.ServiceLayer.Models;
using App.ServiceLayer.Queries;
using App.ServiceLayer.Services;
using App.UserMiddleware;
using App.UserMiddleware.Helpers;
using App.UserMiddleware.Services;
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
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IPlayerService _playerService;
        private readonly ICoachService _coachService;
        private readonly IJwtService _jwtService;
        private readonly IMapper _mapper;
        private readonly IUserSettings _userSettings;

        public UserController(
            IUserService userService, 
            IPlayerService playerService, 
            ICoachService coachService, 
            IJwtService jwtService, 
            IMapper mapper, 
            IUserSettings userSettings
            )
        {
            _userService = userService;
            _playerService = playerService;
            _coachService = coachService;
            _jwtService = jwtService;
            _mapper = mapper;
            _userSettings = userSettings;
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = Permissions.UsersPolicy)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PaginatedList<UserListItemDto>))]
        public async Task<ActionResult<PaginatedList<UserListItemDto>>> GetUsers([FromQuery] UserQuery query)
        {
            return Ok(await _userService.GetUsers(query));
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = Permissions.UsersPolicy)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<UserDto>))]
        [Route("All")]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetAllUsers()
        {
            return Ok(await _userService.GetAll());
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = Permissions.UsersPolicy)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PaginatedList<PlayerDto>))]
        [Route("Players")]
        public async Task<ActionResult<PaginatedList<PlayerDto>>> GetPlayers([FromQuery] PlayerQuery query)
        {
            return Ok(_mapper.Map<PaginatedList<PlayerDto>>(await _playerService.GetPlayers(query)));
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = Permissions.UsersPolicy)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PaginatedList<CoachDto>))]
        [Route("Coaches")]
        public async Task<ActionResult<PaginatedList<CoachDto>>> GetCoaches([FromQuery] CoachQuery query)
        {
            return Ok(_mapper.Map<PaginatedList<CoachDto>>(await _coachService.GetCoaches(query)));
        }

        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AuthenticationResult))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status409Conflict, Type = typeof(double))]
        [Route("Account/SignIn")]
        public async Task<IActionResult> SignIn(SignInVM model)
        {
            var user = await _userService.GetByLogin(model.Login);
            if(user != null)
            {
                if (PasswordHashHelper.Verify(model.Password, user.PasswordHash))
                {
                    if(user.AccountLockoutTime.HasValue && user.AccountLockoutTime.Value > DateTime.Now)
                    {
                        return Conflict(TimeSpan.FromTicks(user.AccountLockoutTime.Value.Ticks - DateTime.Now.Ticks).TotalMinutes);
                    }
                    var command = new UpdateUserCommandVM { Id = user.Id, LastLogon = DateTime.Now };
                    if (user.BadLogonCount > 0)
                    {
                        command.BadLogonCount = 0;
                    }
                    await _userService.Update(command);
                    var token = _jwtService.CreateJwtToken(user);
                    return Ok(new AuthenticationResult { UserId = user.Id, Token = token });
                }
                else
                {
                    var command = new UpdateUserCommandVM { Id = user.Id, LastBadPasswordAttempt = DateTime.Now };
                    if(user.BadLogonCount == _userSettings.MaxBadLogonCout - 1)
                    {
                        command.BadLogonCount = 0;
                        command.AccountLockoutTime = DateTime.Now.AddMinutes(_userSettings.AccountLockoutTimeInMinutes);
                    }
                    else
                    {
                        command.BadLogonCount = user.BadLogonCount + 1;
                    }
                    await _userService.Update(command);
                    return Conflict(_userSettings.AccountLockoutTimeInMinutes);
                }
            }
            return Unauthorized();
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = Permissions.UsersPolicy)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [Route("Players")]
        public async Task<IActionResult> CreatePlayer([FromBody] CreatePlayerVM model)
        {
            var player = _mapper.Map<Player>(model);
            await _playerService.AddAsync(player);
            return NoContent();
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = Permissions.UsersPolicy)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [Route("Coaches")]
        public async Task<IActionResult> CreateCoach([FromBody] CreateCoachVM model)
        {
            var coach = _mapper.Map<Coach>(model);
            await _coachService.AddAsync(coach);
            return NoContent();
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = Permissions.UsersPolicy)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [Route("Administrators")]
        public async Task<IActionResult> CreateAdministrator([FromBody] CreateAdminVM model)
        {
            var user = _mapper.Map<User>(model);
            await _userService.Add(user);
            return NoContent();
        }

        [HttpPut]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = Permissions.UsersPolicy)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [Route("Players")]
        public async Task<IActionResult> UpdateCoach([FromBody] UpdatePlayerVM model)
        {
            await _playerService.UpdateAsync(model);
            return NoContent();
        }

        [HttpPut]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = Permissions.UsersPolicy)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [Route("Coaches")]
        public async Task<IActionResult> UpdateCoach([FromBody] UpdateCoachVM model)
        {
            await _coachService.UpdateAsync(model);
            return NoContent();
        }
    }
}
