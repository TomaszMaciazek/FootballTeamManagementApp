﻿using App.Model.Dtos;
using App.Model.Dtos.ListItemDtos;
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
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoachController : ControllerBase
    {
        private readonly ICoachService _coachService;
        private readonly IMapper _mapper;

        public CoachController(ICoachService coachService, IMapper mapper)
        {
            _coachService = coachService;
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = Permissions.CoachesPolicy)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PaginatedList<CoachListItemDto>))]
        public async Task<ActionResult<PaginatedList<CoachListItemDto>>> GetCoaches([FromQuery] CoachQuery query)
        {
            return Ok(await _coachService.GetCoaches(query));
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = Permissions.CoachesPolicy)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<SimpleCoachDto>))]
        [Route("All")]
        public async Task<ActionResult<IEnumerable<SimpleCoachDto>>> GetAllPlayers()
        {
            return Ok(await _coachService.GetAllAsync());
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = Permissions.CoachesPolicy)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<SimpleCoachDto>))]
        [Route("Working")]
        public async Task<ActionResult<IEnumerable<SimpleCoachDto>>> GetPlayingPlayers([FromQuery] DateTime? date = null)
        {
            return Ok(await _coachService.GetWorkingCoaches(date));
        }
    }
}
