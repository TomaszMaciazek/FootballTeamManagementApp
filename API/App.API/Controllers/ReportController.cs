using App.Model.Dtos.StatisticsDtos;
using App.ServiceLayer.Services;
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
    public class ReportController : ControllerBase
    {
        private readonly IStatisticsService _statisticsService;

        public ReportController(IStatisticsService statisticsService)
        {
            _statisticsService = statisticsService;
        }

        [HttpGet]
        [Route("PlayersCount")]
        public async Task<ActionResult<IEnumerable<MonthDataCountStatisticsDto>>> GetPlayerCountStatistics([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            return Ok(await _statisticsService.GetPlayersCountStatistics(startDate, endDate));
        }

        [HttpGet]
        [Route("CoachesCount")]
        public async Task<ActionResult<IEnumerable<MonthDataCountStatisticsDto>>> GetCoachesCountStatistics([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            return Ok(await _statisticsService.GetCoachesCountStatistics(startDate, endDate));
        }

        [HttpGet]
        [Route("Cards/Club/All")]
        public async Task<ActionResult<ClubCardsStatisticsDto>> GetClubCardStatistics()
        {
            return Ok(await _statisticsService.GetAllClubCards());
        }

        [HttpGet]
        [Route("Cards/Club/Monthly")]
        public async Task<ActionResult<ClubMonthlyCardsCountStatisticsDto>> GetMonthlyClubCardStatistics([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            return Ok(await _statisticsService.GetClubMonthlyCards(startDate, endDate));
        }

        [HttpGet]
        [Route("Cards/Player/All")]
        public async Task<ActionResult<PlayerCardStatisticsDto>> GetPlayerCardStatistics(Guid playerId)
        {
            return Ok(await _statisticsService.GetAllPlayerCards(playerId));
        }

        [HttpGet]
        [Route("Cards/Player/Monthly")]
        public async Task<ActionResult<PlayerMonthlyCardStatisticsDto>> GetMonthlyPlayerCardStatistics([FromQuery] Guid playerId ,[FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            return Ok(await _statisticsService.GetPlayerMonthlyCards(playerId,startDate, endDate));
        }

        [HttpGet]
        [Route("Cards/Coach/All")]
        public async Task<ActionResult<CoachCardsStatisticsDto>> GetCoachCardStatistics(Guid coachId)
        {
            return Ok(await _statisticsService.GetAllCoachCards(coachId));
        }

        [HttpGet]
        [Route("Cards/Coach/Monthly")]
        public async Task<ActionResult<CoachMonthlyCardStatisticsDto>> GetMonthlyCoachCardStatistics([FromQuery] Guid coachId, [FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            return Ok(await _statisticsService.GetCoachMonthlyCards(coachId, startDate, endDate));
        }


        [HttpGet]
        [Route("Points/Club/All")]
        public async Task<ActionResult<ClubMatchPointStatisticsDto>> GetClubPointsStatistics()
        {
            return Ok(await _statisticsService.GetClubMatchPoints());
        }

        [HttpGet]
        [Route("Points/Club/Monthly")]
        public async Task<ActionResult<ClubMonthlyMatchPointStatisticsDto>> GetMonthlyClubMatchPoints([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            return Ok(await _statisticsService.GetMonthlyClubMatchPoints(startDate, endDate));
        }

        [HttpGet]
        [Route("Points/Player/All")]
        public async Task<ActionResult<PlayerMatchPointStatisticsDto>> GetPlayerPointsStatistics([FromQuery] Guid playerId)
        {
            return Ok(await _statisticsService.GetAllPlayerMatchPoints(playerId));
        }

        [HttpGet]
        [Route("Points/Player/Monthly")]
        public async Task<ActionResult<PlayerMonthlyMatchPointStatisticsDto>> GetMonthlyPlayerMatchPoints([FromQuery] Guid playerId, [FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            return Ok(await _statisticsService.GetPlayerMonthlyMatchPoints(playerId, startDate, endDate));
        }
    }
}
