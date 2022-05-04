using App.Model.Dtos.StatisticsDtos;
using App.Model.ViewModels.Queries;
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
        [Route("Cards/Team/All")]
        public async Task<ActionResult<TeamCardStatisticsDto>> GetTeamCardStatistics(Guid teamId)
        {
            return Ok(await _statisticsService.GetAllTeamCards(teamId));
        }

        [HttpGet]
        [Route("Cards/Team/Monthly")]
        public async Task<ActionResult<PlayerMonthlyCardStatisticsDto>> GetMonthlyTeamCardStatistics([FromQuery] Guid teamId, [FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            return Ok(await _statisticsService.GetTeamMonthlyCards(teamId, startDate, endDate));
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

        [HttpGet]
        [Route("Points/Team/All")]
        public async Task<ActionResult<TeamMatchPointStatisticsDto>> GetTeamPointsStatistics([FromQuery] Guid teamId)
        {
            return Ok(await _statisticsService.GetAllTeamMatchPoints(teamId));
        }

        [HttpGet]
        [Route("Points/Team/Monthly")]
        public async Task<ActionResult<TeamMonthlyMatchPointStatisticsDto>> GetMonthlyTeamMatchPoints([FromQuery] Guid teamId, [FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            return Ok(await _statisticsService.GetTeamMonthlyMatchPoints(teamId, startDate, endDate));
        }

        [HttpGet]
        [Route("TrainingScores/Club/All")]
        public async Task<ActionResult<IEnumerable<TrainingScoreStatisticsDto>>> GetClubTrainingScoreStatistics()
        {
            return Ok(await _statisticsService.GetClubTrainingScoreStatistics());
        }

        [HttpGet]
        [Route("TrainingScores/Club/Monthly")]
        public async Task<ActionResult<ClubMonthlyTrainingScoreStatisticsDto>> GetMonthlyClubTrainingScoreStatistics(DateTime startDate, DateTime endDate)
        {
            return Ok(await _statisticsService.GetMonthlyClubTrainingScoreStatistics(startDate, endDate));
        }

        [HttpGet]
        [Route("TrainingScores/Player/All")]
        public async Task<ActionResult<PlayerTrainingScoreStatisticsDto>> GetPlayerTrainingScoreStatistics(Guid playerId)
        {
            return Ok(await _statisticsService.GetPlayerTrainingScoreStatistics(playerId));
        }

        [HttpGet]
        [Route("TrainingScores/Player/Monthly")]
        public async Task<ActionResult<PlayerMonthlyTrainingScoreStatisticsDto>> GetMonthlyPlayerTrainingScoreStatistics(Guid playerId, DateTime startDate, DateTime endDate)
        {
            return Ok(await _statisticsService.GetMonthlyPlayerTrainingScoreStatistics(playerId, startDate, endDate));
        }

        [HttpGet]
        [Route("MatchScores/Club/All")]
        public async Task<ActionResult<IEnumerable<MatchScoreStatisticsDto>>> GetClubMatchScoreStatistics()
        {
            return Ok(await _statisticsService.GetClubMatchScoreStatistics());
        }

        [HttpGet]
        [Route("MatchScores/Club/Monthly")]
        public async Task<ActionResult<ClubMonthlyTrainingScoreStatisticsDto>> GetMonthlyClubMatchScoreStatistics(DateTime startDate, DateTime endDate)
        {
            return Ok(await _statisticsService.GetMonthlyClubMatchScoreStatistics(startDate, endDate));
        }

        [HttpGet]
        [Route("MatchScores/Player/All")]
        public async Task<ActionResult<PlayerMatchScoreStatisticsDto>> GetPlayerMatchScoreStatistics(Guid playerId)
        {
            return Ok(await _statisticsService.GetPlayerMatchScoreStatistics(playerId));
        }

        [HttpGet]
        [Route("MatchScores/Player/Monthly")]
        public async Task<ActionResult<PlayerMonthlyMatchScoreStatisticsDto>> GetMonthlyPlayerMatchScoreStatistics(Guid playerId, DateTime startDate, DateTime endDate)
        {
            return Ok(await _statisticsService.GetMonthlyPlayerMatchScoreStatistics(playerId, startDate, endDate));
        }

        [HttpGet]
        [Route("Rankings/TrainingScores")]
        public async Task<ActionResult<PlayersTrainingScoresRankingDto>> GetPlayersTrainingScoresRanking([FromQuery] TrainingScoresRankingQuery query)
        {
            return Ok(await _statisticsService.GetPlayersTrainingScoresRanking(query));
        }

        [HttpGet]
        [Route("Rankings/MatchScores")]
        public async Task<ActionResult<PlayersTrainingScoresRankingDto>> GetPlayersMatchScoresRanking([FromQuery] MatchScoresRankingQuery query)
        {
            return Ok(await _statisticsService.GetPlayersMatchScoresRanking(query));
        }
    }
}
