using App.Model.Dtos.History;
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
    public class TeamHistoryController : ControllerBase
    {
        private readonly ITeamHistoryService teamHistoryService;

        public TeamHistoryController(ITeamHistoryService teamHistoryService)
        {
            this.teamHistoryService = teamHistoryService;
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = Permissions.TeamsHistoriesPolicy)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TeamHistoryDto))]
        public async Task<ActionResult<TeamHistoryDto>> GetTeamHistory([FromQuery] TeamHistoryQuery query)
        {
            return Ok(await teamHistoryService.GetTeamHistory(query.TeamId,query.MinDate,query.MaxDate));
        }
    }
}
