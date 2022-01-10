using App.Model.Dtos;
using App.Model.Dtos.StatisticsDtos;
using App.Model.Enums;
using App.Model.ViewModels.Queries;
using App.Repository.Repositories;
using App.ServiceLayer.Extenstions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.ServiceLayer.Services
{
    public interface IStatisticsService
    {
        Task<ClubCardsStatisticsDto> GetAllClubCards();
        Task<ClubMonthlyCardsCountStatisticsDto> GetClubMonthlyCards(DateTime startDate, DateTime endDate);
        Task<IEnumerable<MonthDataCountStatisticsDto>> GetCoachesCountStatistics(DateTime startDate, DateTime endDate);
        Task<IEnumerable<MonthDataCountStatisticsDto>> GetPlayersCountStatistics(DateTime startDate, DateTime endDate);
        Task<PlayerCardStatisticsDto> GetAllPlayerCards(Guid playerId);
        Task<PlayerMonthlyCardStatisticsDto> GetPlayerMonthlyCards(Guid playerId, DateTime startDate, DateTime endDate);
        Task<CoachCardsStatisticsDto> GetAllCoachCards(Guid coachId);
        Task<CoachMonthlyCardStatisticsDto> GetCoachMonthlyCards(Guid coachId, DateTime startDate, DateTime endDate);
        Task<ClubMatchPointStatisticsDto> GetClubMatchPoints();
        Task<ClubMonthlyMatchPointStatisticsDto> GetMonthlyClubMatchPoints(DateTime startDate, DateTime endDate);
        Task<PlayerMatchPointStatisticsDto> GetAllPlayerMatchPoints(Guid playerId);
        Task<PlayerMonthlyMatchPointStatisticsDto> GetPlayerMonthlyMatchPoints(Guid playerId, DateTime startDate, DateTime endDate);
        Task<TeamCardStatisticsDto> GetAllTeamCards(Guid teamId);
        Task<TeamMonthlyCardStatisticsDto> GetTeamMonthlyCards(Guid teamId, DateTime startDate, DateTime endDate);
        Task<TeamMatchPointStatisticsDto> GetAllTeamMatchPoints(Guid teamId);
        Task<TeamMonthlyMatchPointStatisticsDto> GetTeamMonthlyMatchPoints(Guid teamId, DateTime startDate, DateTime endDate);
        Task<IEnumerable<TrainingScoreStatisticsDto>> GetClubTrainingScoreStatistics();
        Task<ClubMonthlyTrainingScoreStatisticsDto> GetMonthlyClubTrainingScoreStatistics(DateTime startDate, DateTime endDate);
        Task<PlayerTrainingScoreStatisticsDto> GetPlayerTrainingScoreStatistics(Guid playerId);
        Task<PlayerMonthlyTrainingScoreStatisticsDto> GetMonthlyPlayerTrainingScoreStatistics(Guid playerId, DateTime startDate, DateTime endDate);
        Task<IEnumerable<MatchScoreStatisticsDto>> GetClubMatchScoreStatistics();
        Task<ClubMonthlyMatchScoreStatisticsDto> GetMonthlyClubMatchScoreStatistics(DateTime startDate, DateTime endDate);
        Task<PlayerMatchScoreStatisticsDto> GetPlayerMatchScoreStatistics(Guid playerId);
        Task<PlayerMonthlyMatchScoreStatisticsDto> GetMonthlyPlayerMatchScoreStatistics(Guid playerId, DateTime startDate, DateTime endDate);
        Task<PlayersTrainingScoresRankingDto> GetPlayersTrainingScoresRanking(TrainingScoresRankingQuery query);
        Task<PlayersMatchScoresRankingDto> GetPlayersMatchScoresRanking(MatchScoresRankingQuery query);
    }

    public class StatisticsService : IStatisticsService
    {
        private readonly ITrainingScoreRepository _trainingScoreRepository;
        private readonly IMatchPlayerScoreRepository _matchPlayerScoreRepository;
        private readonly IPlayerRepository _playerRepository;
        private readonly ICoachRepository _coachRepository;
        private readonly ICoachCardRepository _coachCardRepository;
        private readonly IPlayerCardRepository _playerCardRepository;
        private readonly IMatchPlayerPerformanceRepository _playerPerformanceRepository;
        private readonly IPlayerHistoryRepository _playerHistoryRepository;
        private readonly ITeamHistoryEventsRepository _teamHistoryEventsRepository;
        private readonly IMatchPointRepository _matchPointRepository;

        public StatisticsService(
            ITrainingScoreRepository trainingScoreRepository,
            IMatchPlayerScoreRepository matchPlayerScoreRepository,
            IPlayerRepository playerRepository,
            ICoachRepository coachRepository,
            ICoachCardRepository coachCardRepository,
            IPlayerCardRepository playerCardRepository,
            IMatchPlayerPerformanceRepository playerPerformanceRepository,
            IPlayerHistoryRepository playerHistoryRepository,
            ITeamHistoryEventsRepository teamHistoryEventsRepository,
            IMatchPointRepository matchPointRepository
        )
        {
            _trainingScoreRepository = trainingScoreRepository;
            _matchPlayerScoreRepository = matchPlayerScoreRepository;
            _playerRepository = playerRepository;
            _coachRepository = coachRepository;
            _coachCardRepository = coachCardRepository;
            _playerCardRepository = playerCardRepository;
            _playerPerformanceRepository = playerPerformanceRepository;
            _playerHistoryRepository = playerHistoryRepository;
            _teamHistoryEventsRepository = teamHistoryEventsRepository;
            _matchPointRepository = matchPointRepository;
        }

        #region Club Members count

        public async Task<IEnumerable<MonthDataCountStatisticsDto>> GetPlayersCountStatistics(DateTime startDate, DateTime endDate)
        {
            return MonthsBetween(startDate, endDate)
                .GroupJoin(
                    await _playerRepository.GetAll()
                        .AsNoTracking()
                        .Where(x => x.CreatedDate.Date >= startDate.Date && x.CreatedDate.Date <= endDate.Date)
                        .Select(x => new { Year = x.CreatedDate.Year, Month = x.CreatedDate.Month })
                        .ToListAsync(),
                    m => new { Month = m.Month, Year = m.Year },
                    stat => new { Month = stat.Month, Year = stat.Year },
                    (m, s) => new MonthDataCountStatisticsDto { Month = m.Month, Year = m.Year, Count = s.Count() }
                )
                .OrderBy(x => x.Year).ThenBy(x => x.Month);

        }

        public async Task<IEnumerable<MonthDataCountStatisticsDto>> GetCoachesCountStatistics(DateTime startDate, DateTime endDate)
        {
            return MonthsBetween(startDate, endDate)
                .GroupJoin(
                    await _coachRepository.GetAll()
                        .AsNoTracking()
                        .Where(x => x.CreatedDate.Date >= startDate.Date && x.CreatedDate.Date <= endDate.Date)
                        .Select(x => new { Year = x.CreatedDate.Year, Month = x.CreatedDate.Month })
                        .ToListAsync(),
                    m => new { Month = m.Month, Year = m.Year },
                    stat => new { Month = stat.Month, Year = stat.Year },
                    (m, s) => new MonthDataCountStatisticsDto { Month = m.Month, Year = m.Year, Count = s.Count() }
                )
                .OrderBy(x => x.Year).ThenBy(x => x.Month);

        }

        #endregion

        #region Club Cards
        public async Task<ClubCardsStatisticsDto> GetAllClubCards()
        {
            var cards = new ClubCardsStatisticsDto
            {
                PlayersYellowCards = await _playerCardRepository.GetAll().AsNoTracking().Include(x => x.Match)
                    .Where(x => x.IsActive && x.Color == Model.Enums.CardColor.Yellow).Select(x => x.Count).SumAsync(),
                PlayersRedCards = await _playerCardRepository.GetAll().AsNoTracking().Include(x => x.Match)
                    .Where(x => x.IsActive && x.Match.IsActive && x.Color == Model.Enums.CardColor.Red).Select(x => x.Count).SumAsync(),
                CoachesRedCards = await _coachCardRepository.GetAll().AsNoTracking().Include(x => x.Match)
                    .Where(x => x.IsActive && x.Match.IsActive && x.Color == Model.Enums.CardColor.Red).Select(x => x.Count).SumAsync(),
                CoachesYellowCards = await _coachCardRepository.GetAll().AsNoTracking().Include(x => x.Match)
                    .Where(x => x.IsActive && x.Match.IsActive && x.Color == Model.Enums.CardColor.Yellow).Select(x => x.Count).SumAsync()
            };

            return cards;
        }

        public async Task<ClubMonthlyCardsCountStatisticsDto> GetClubMonthlyCards(DateTime startDate, DateTime endDate)
        {
            var cards = new ClubMonthlyCardsCountStatisticsDto();

            var months = MonthsBetween(startDate, endDate);

            cards.CoachesRedCards = months
                .GroupJoin(
                     await _coachCardRepository.GetAll().AsNoTracking().Include(x => x.Match)
                     .Where(x => x.IsActive && x.Match.IsActive && x.Color == Model.Enums.CardColor.Red && x.Match.Date.Date >= startDate.Date && x.Match.Date.Date <= endDate.Date)
                     .Select(x => new { Year = x.Match.Date.Year, Month = x.Match.Date.Month }).ToListAsync(),
                    m => new { Month = m.Month, Year = m.Year },
                    stat => new { Month = stat.Month, Year = stat.Year },
                    (m, s) => new MonthDataCountStatisticsDto { Month = m.Month, Year = m.Year, Count = s.Count() }
                )
                .OrderBy(x => x.Year).ThenBy(x => x.Month);

            cards.CoachesYellowCards = months
                .GroupJoin(
                     await _coachCardRepository.GetAll().AsNoTracking().Include(x => x.Match)
                     .Where(x => x.IsActive && x.Match.IsActive && x.Color == Model.Enums.CardColor.Yellow && x.Match.Date.Date >= startDate.Date && x.Match.Date.Date <= endDate.Date)
                     .Select(x => new { Year = x.Match.Date.Year, Month = x.Match.Date.Month }).ToListAsync(),
                    m => new { Month = m.Month, Year = m.Year },
                    stat => new { Month = stat.Month, Year = stat.Year },
                    (m, s) => new MonthDataCountStatisticsDto { Month = m.Month, Year = m.Year, Count = s.Count() }
                )
                .OrderBy(x => x.Year).ThenBy(x => x.Month);

            cards.PlayersRedCards = months
                .GroupJoin(
                     await _playerCardRepository.GetAll().AsNoTracking().Include(x => x.Match)
                     .Where(x => x.IsActive && x.Match.IsActive && x.Color == Model.Enums.CardColor.Red && x.Match.Date.Date >= startDate.Date && x.Match.Date.Date <= endDate.Date)
                     .Select(x => new { Year = x.Match.Date.Year, Month = x.Match.Date.Month }).ToListAsync(),
                    m => new { Month = m.Month, Year = m.Year },
                    stat => new { Month = stat.Month, Year = stat.Year },
                    (m, s) => new MonthDataCountStatisticsDto { Month = m.Month, Year = m.Year, Count = s.Count() }
                )
                .OrderBy(x => x.Year).ThenBy(x => x.Month);

            cards.PlayersYellowCards = months
                .GroupJoin(
                     await _playerCardRepository.GetAll().AsNoTracking().Include(x => x.Match)
                     .Where(x => x.IsActive && x.Match.IsActive && x.Color == Model.Enums.CardColor.Yellow && x.Match.Date.Date >= startDate.Date && x.Match.Date.Date <= endDate.Date)
                     .Select(x => new { Year = x.Match.Date.Year, Month = x.Match.Date.Month }).ToListAsync(),
                    m => new { Month = m.Month, Year = m.Year },
                    stat => new { Month = stat.Month, Year = stat.Year },
                    (m, s) => new MonthDataCountStatisticsDto { Month = m.Month, Year = m.Year, Count = s.Count() }
                )
                .OrderBy(x => x.Year).ThenBy(x => x.Month);

            return cards;

        }

        #endregion

        #region Player Cards
        public async Task<PlayerCardStatisticsDto> GetAllPlayerCards(Guid playerId)
        {
            var redCardsCount = await _playerCardRepository.GetAll().AsNoTracking().Include(x => x.Player).Include(x => x.Match).Where(x => x.IsActive && x.Match.IsActive && x.Player.Id == playerId && x.Color == Model.Enums.CardColor.Red).Select(x => x.Count).SumAsync();
            var yellowCardsCount = await _playerCardRepository.GetAll().AsNoTracking().Include(x => x.Player).Include(x => x.Match).Where(x => x.IsActive && x.Match.IsActive && x.Player.Id == playerId && x.Color == Model.Enums.CardColor.Yellow).Select(x => x.Count).SumAsync();

            var playerPerformancesCount = await _playerPerformanceRepository.GetAll().AsNoTracking().Include(x => x.Player).Include(x => x.Match).Where(x => x.IsActive && x.Match.IsActive && x.Player.Id == playerId).CountAsync();

            var cards = new PlayerCardStatisticsDto
            {
                PlayerId = playerId,
                RedCardsCount = redCardsCount,
                YellowCardsCount = yellowCardsCount,
                CardsAvg = (double)(redCardsCount + yellowCardsCount) / playerPerformancesCount
            };

            return cards;
        }

        public async Task<PlayerMonthlyCardStatisticsDto> GetPlayerMonthlyCards(Guid playerId, DateTime startDate, DateTime endDate)
        {
            var cards = new PlayerMonthlyCardStatisticsDto { PlayerId = playerId };

            var months = MonthsBetween(startDate, endDate);

            cards.RedCards = months
                .GroupJoin(
                     await _playerCardRepository.GetAll().AsNoTracking().Include(x => x.Match)
                     .Where(x => x.IsActive && x.Match.IsActive && x.Color == Model.Enums.CardColor.Red && x.Player.Id == playerId && x.Match.Date.Date >= startDate.Date && x.Match.Date.Date <= endDate.Date)
                     .Select(x => new { Year = x.Match.Date.Year, Month = x.Match.Date.Month }).ToListAsync(),
                    m => new { Month = m.Month, Year = m.Year },
                    stat => new { Month = stat.Month, Year = stat.Year },
                    (m, s) => new MonthDataCountStatisticsDto { Month = m.Month, Year = m.Year, Count = s.Count() }
                )
                .OrderBy(x => x.Year).ThenBy(x => x.Month);

            cards.YellowCards = months
                .GroupJoin(
                     await _playerCardRepository.GetAll().AsNoTracking().Include(x => x.Match)
                     .Where(x => x.IsActive && x.Match.IsActive && x.Color == Model.Enums.CardColor.Yellow && x.Player.Id == playerId && x.Match.Date.Date >= startDate.Date && x.Match.Date.Date <= endDate.Date)
                     .Select(x => new { Year = x.Match.Date.Year, Month = x.Match.Date.Month }).ToListAsync(),
                    m => new { Month = m.Month, Year = m.Year },
                    stat => new { Month = stat.Month, Year = stat.Year },
                    (m, s) => new MonthDataCountStatisticsDto { Month = m.Month, Year = m.Year, Count = s.Count() }
                )
                .OrderBy(x => x.Year).ThenBy(x => x.Month);

            return cards;

        }

        #endregion

        #region Coach Cards
        public async Task<CoachCardsStatisticsDto> GetAllCoachCards(Guid coachId)
        {
            var cards = new CoachCardsStatisticsDto
            {
                CoachId = coachId,
                RedCardsCount = await _coachCardRepository.GetAll().AsNoTracking().Include(x => x.Coach).Include(x => x.Match)
                    .Where(x => x.IsActive && x.Match.IsActive && x.Coach.Id == coachId && x.Color == Model.Enums.CardColor.Red).Select(x => x.Count).SumAsync(),
                YellowCardsCount = await _coachCardRepository.GetAll().AsNoTracking().Include(x => x.Coach).Include(x => x.Match)
                    .Where(x => x.IsActive && x.Match.IsActive && x.Coach.Id == coachId && x.Color == Model.Enums.CardColor.Yellow).Select(x => x.Count).SumAsync()
            };

            return cards;
        }

        public async Task<CoachMonthlyCardStatisticsDto> GetCoachMonthlyCards(Guid coachId, DateTime startDate, DateTime endDate)
        {
            var cards = new CoachMonthlyCardStatisticsDto { CoachId = coachId };

            var months = MonthsBetween(startDate, endDate);

            cards.RedCards = months
                .GroupJoin(
                     await _coachCardRepository.GetAll().AsNoTracking().Include(x => x.Match)
                     .Where(x => x.IsActive && x.Match.IsActive && x.Color == Model.Enums.CardColor.Red && x.Coach.Id == coachId && x.Match.Date.Date >= startDate.Date && x.Match.Date.Date <= endDate.Date)
                     .Select(x => new { Year = x.Match.Date.Year, Month = x.Match.Date.Month }).ToListAsync(),
                    m => new { Month = m.Month, Year = m.Year },
                    stat => new { Month = stat.Month, Year = stat.Year },
                    (m, s) => new MonthDataCountStatisticsDto { Month = m.Month, Year = m.Year, Count = s.Count() }
                )
                .OrderBy(x => x.Year).ThenBy(x => x.Month);

            cards.YellowCards = months
                .GroupJoin(
                     await _coachCardRepository.GetAll().AsNoTracking().Include(x => x.Match)
                     .Where(x => x.IsActive && x.Match.IsActive && x.Color == Model.Enums.CardColor.Yellow && x.Coach.Id == coachId && x.Match.Date.Date >= startDate.Date && x.Match.Date.Date <= endDate.Date)
                     .Select(x => new { Year = x.Match.Date.Year, Month = x.Match.Date.Month }).ToListAsync(),
                    m => new { Month = m.Month, Year = m.Year },
                    stat => new { Month = stat.Month, Year = stat.Year },
                    (m, s) => new MonthDataCountStatisticsDto { Month = m.Month, Year = m.Year, Count = s.Count() }
                )
                .OrderBy(x => x.Year).ThenBy(x => x.Month);

            return cards;

        }

        #endregion

        #region Team Cards
        public async Task<TeamCardStatisticsDto> GetAllTeamCards(Guid teamId)
        {
            var cards = new TeamCardStatisticsDto
            {
                TeamId = teamId,
                RedCardsCount = await _playerCardRepository.GetAll().AsNoTracking().Include(x => x.Team).Include(x => x.Match).Where(x => x.IsActive && x.Match.IsActive && x.Team.Id == teamId && x.Color == Model.Enums.CardColor.Red).Select(x => x.Count).SumAsync(),
                YellowCardsCount = await _playerCardRepository.GetAll().AsNoTracking().Include(x => x.Team).Include(x => x.Match).Where(x => x.IsActive && x.Match.IsActive && x.Team.Id == teamId && x.Color == Model.Enums.CardColor.Yellow).Select(x => x.Count).SumAsync()
            };

            return cards;
        }

        public async Task<TeamMonthlyCardStatisticsDto> GetTeamMonthlyCards(Guid teamId, DateTime startDate, DateTime endDate)
        {
            var cards = new TeamMonthlyCardStatisticsDto { TeamId = teamId };

            var months = MonthsBetween(startDate, endDate);

            cards.RedCards = months
                .GroupJoin(
                     await _playerCardRepository.GetAll().AsNoTracking().Include(x => x.Match)
                     .Where(x => x.IsActive && x.Match.IsActive && x.Color == Model.Enums.CardColor.Red && x.Team.Id == teamId && x.Match.Date.Date >= startDate.Date && x.Match.Date.Date <= endDate.Date)
                     .Select(x => new { Year = x.Match.Date.Year, Month = x.Match.Date.Month }).ToListAsync(),
                    m => new { Month = m.Month, Year = m.Year },
                    stat => new { Month = stat.Month, Year = stat.Year },
                    (m, s) => new MonthDataCountStatisticsDto { Month = m.Month, Year = m.Year, Count = s.Count() }
                )
                .OrderBy(x => x.Year).ThenBy(x => x.Month);

            cards.YellowCards = months
                .GroupJoin(
                     await _playerCardRepository.GetAll().AsNoTracking().Include(x => x.Match)
                     .Where(x => x.IsActive && x.Match.IsActive && x.Color == Model.Enums.CardColor.Yellow && x.Team.Id == teamId && x.Match.Date.Date >= startDate.Date && x.Match.Date.Date <= endDate.Date)
                     .Select(x => new { Year = x.Match.Date.Year, Month = x.Match.Date.Month }).ToListAsync(),
                    m => new { Month = m.Month, Year = m.Year },
                    stat => new { Month = stat.Month, Year = stat.Year },
                    (m, s) => new MonthDataCountStatisticsDto { Month = m.Month, Year = m.Year, Count = s.Count() }
                )
                .OrderBy(x => x.Year).ThenBy(x => x.Month);

            return cards;

        }
        #endregion

        #region Club Match points
        public async Task<ClubMatchPointStatisticsDto> GetClubMatchPoints()
        {
            var pointTypes = Enum.GetValues(typeof(MatchPointType)).Cast<MatchPointType>();

            var points = new ClubMatchPointStatisticsDto
            {
                CupMatchesPoints = pointTypes.GroupJoin(await _matchPointRepository.GetAll().AsNoTracking().Include(x => x.Match)
                    .Where(x => x.IsActive && x.Match.IsActive && x.Match.MatchType == MatchType.Cup).Select(x => new { Type = x.Point })
                    .ToListAsync(),
                    t => new { Type = t },
                    stat => new { Type = stat.Type },
                    (t, s) => new MatchPointCountDto { Type = t, Count = s.Count() }
                    ).OrderBy(x => x.Type),
                LeagueMatchesPoints = pointTypes.GroupJoin(await _matchPointRepository.GetAll().AsNoTracking().Include(x => x.Match)
                    .Where(x => x.IsActive && x.Match.IsActive && x.Match.MatchType == MatchType.League).Select(x => new { Type = x.Point })
                    .ToListAsync(),
                    t => new { Type = t },
                    stat => new { Type = stat.Type },
                    (t, s) => new MatchPointCountDto { Type = t, Count = s.Count() }
                    ).OrderBy(x => x.Type),
                FriendlyMatchPoints = pointTypes.GroupJoin(await _matchPointRepository.GetAll().AsNoTracking().Include(x => x.Match)
                    .Where(x => x.IsActive && x.Match.IsActive && x.Match.MatchType == MatchType.Friendly).Select(x => new { Type = x.Point })
                    .ToListAsync(),
                    t => new { Type = t },
                    stat => new { Type = stat.Type },
                    (t, s) => new MatchPointCountDto { Type = t, Count = s.Count() }
                    ).OrderBy(x => x.Type)
            };

            return points;

        }

        public async Task<ClubMonthlyMatchPointStatisticsDto> GetMonthlyClubMatchPoints(DateTime startDate, DateTime endDate)
        {
            var pointTypes = Enum.GetValues(typeof(MatchPointType)).Cast<MatchPointType>();

            var months = MonthsBetween(startDate, endDate);

            var points = await _matchPointRepository.GetAll().AsNoTracking().Include(x => x.Match)
                        .Where(x => x.IsActive && x.Match.IsActive && x.Match.Date >= startDate.Date && x.Match.Date <= endDate.Date).ToListAsync();

            var cupMatchesPoints = points.Where(x => x.Match.MatchType == MatchType.Cup);

            var legueMatchesPoints = points.Where(x => x.Match.MatchType == MatchType.League);

            var frendlyMatchesPoints = points.Where(x => x.Match.MatchType == MatchType.Friendly);


            var cupMatchesPointsGroupByMonth = months.GroupJoin(
                cupMatchesPoints,
                m => new { Month = m.Month, Year = m.Year },
                stat => new { Month = stat.Match.Date.Month, Year = stat.Match.Date.Year },
                (m, s) => new MonthlyMatchPointCountDto { 
                    Month = m.Month,
                    Year = m.Year,
                    PointsByType = pointTypes.GroupJoin(
                        s,
                        t => new { Type = t },
                        stat => new { Type = stat.Point },
                        (t, s) => new MatchPointCountDto { Type = t, Count = s.Count() }
                        )
                });

            var leagueMatchesPointsGroupByMonth = months.GroupJoin(
                legueMatchesPoints,
                m => new { Month = m.Month, Year = m.Year },
                stat => new { Month = stat.Match.Date.Month, Year = stat.Match.Date.Year },
                (m, s) => new MonthlyMatchPointCountDto
                {
                    Month = m.Month,
                    Year = m.Year,
                    PointsByType = pointTypes.GroupJoin(
                        s,
                        t => new { Type = t },
                        stat => new { Type = stat.Point },
                        (t, s) => new MatchPointCountDto { Type = t, Count = s.Count() }
                        )
                });

            var friendlyMatchesPointsGroupByMonth = months.GroupJoin(
                frendlyMatchesPoints,
                m => new { Month = m.Month, Year = m.Year },
                stat => new { Month = stat.Match.Date.Month, Year = stat.Match.Date.Year },
                (m, s) => new MonthlyMatchPointCountDto
                {
                    Month = m.Month,
                    Year = m.Year,
                    PointsByType = pointTypes.GroupJoin(
                        s,
                        t => new { Type = t },
                        stat => new { Type = stat.Point },
                        (t, s) => new MatchPointCountDto { Type = t, Count = s.Count() }
                        )
                });

            return new ClubMonthlyMatchPointStatisticsDto
            {
                CupMatchesPoints = cupMatchesPointsGroupByMonth,
                LeagueMatchesPoints = leagueMatchesPointsGroupByMonth,
                FriendlyMatchesPoints = friendlyMatchesPointsGroupByMonth
            };

        }

        #endregion

        #region Player Match Points
        public async Task<PlayerMatchPointStatisticsDto> GetAllPlayerMatchPoints(Guid playerId)
        {
            var pointTypes = Enum.GetValues(typeof(MatchPointType)).Cast<MatchPointType>();

            var points = new PlayerMatchPointStatisticsDto
            {
                PlayerId = playerId,
                CupMatchesPoints = pointTypes.GroupJoin(await _matchPointRepository.GetAll().AsNoTracking()
                    .Include(x => x.Match).Include(x => x.GoalScorer)
                    .Where(x => x.IsActive && x.Match.IsActive && x.Match.MatchType == MatchType.Cup && x.GoalScorer.Id == playerId)
                    .Select(x => new { Type = x.Point })
                    .ToListAsync(),
                    t => new { Type = t },
                    stat => new { Type = stat.Type },
                    (t, s) => new MatchPointCountDto { Type = t, Count = s.Count() }
                    ).OrderBy(x => x.Type),
                LeagueMatchesPoints = pointTypes.GroupJoin(await _matchPointRepository.GetAll().AsNoTracking()
                    .Include(x => x.Match).Include(x => x.GoalScorer)
                    .Where(x => x.IsActive && x.Match.IsActive && x.Match.MatchType == MatchType.League && x.GoalScorer.Id == playerId)
                    .Select(x => new { Type = x.Point })
                    .ToListAsync(),
                    t => new { Type = t },
                    stat => new { Type = stat.Type },
                    (t, s) => new MatchPointCountDto { Type = t, Count = s.Count() }
                    ).OrderBy(x => x.Type),
                FriendlyMatchPoints = pointTypes.GroupJoin(await _matchPointRepository.GetAll().AsNoTracking()
                    .Include(x => x.Match).Include(x => x.GoalScorer)
                    .Where(x => x.IsActive && x.Match.IsActive && x.Match.MatchType == MatchType.Friendly && x.GoalScorer.Id == playerId)
                    .Select(x => new { Type = x.Point })
                    .ToListAsync(),
                    t => new { Type = t },
                    stat => new { Type = stat.Type },
                    (t, s) => new MatchPointCountDto { Type = t, Count = s.Count() }
                    ).OrderBy(x => x.Type)
            };

            return points;
        }

        public async Task<PlayerMonthlyMatchPointStatisticsDto> GetPlayerMonthlyMatchPoints(Guid playerId, DateTime startDate, DateTime endDate)
        {
            var pointTypes = Enum.GetValues(typeof(MatchPointType)).Cast<MatchPointType>();

            var months = MonthsBetween(startDate, endDate);

            var points = await _matchPointRepository.GetAll().AsNoTracking().Include(x => x.Match).Include(x => x.GoalScorer)
                        .Where(x => x.IsActive && x.Match.IsActive && x.GoalScorer.Id == playerId && x.Match.Date >= startDate.Date && x.Match.Date <= endDate.Date).ToListAsync();

            var cupMatchesPoints = points.Where(x => x.Match.MatchType == MatchType.Cup);

            var legueMatchesPoints = points.Where(x => x.Match.MatchType == MatchType.League);

            var frendlyMatchesPoints = points.Where(x => x.Match.MatchType == MatchType.Friendly);


            var cupMatchesPointsGroupByMonth = months.GroupJoin(
                cupMatchesPoints,
                m => new { Month = m.Month, Year = m.Year },
                stat => new { Month = stat.Match.Date.Month, Year = stat.Match.Date.Year },
                (m, s) => new MonthlyMatchPointCountDto
                {
                    Month = m.Month,
                    Year = m.Year,
                    PointsByType = pointTypes.GroupJoin(
                        s,
                        t => new { Type = t },
                        stat => new { Type = stat.Point },
                        (t, s) => new MatchPointCountDto { Type = t, Count = s.Count() }
                        )
                });

            var leagueMatchesPointsGroupByMonth = months.GroupJoin(
                legueMatchesPoints,
                m => new { Month = m.Month, Year = m.Year },
                stat => new { Month = stat.Match.Date.Month, Year = stat.Match.Date.Year },
                (m, s) => new MonthlyMatchPointCountDto
                {
                    Month = m.Month,
                    Year = m.Year,
                    PointsByType = pointTypes.GroupJoin(
                        s,
                        t => new { Type = t },
                        stat => new { Type = stat.Point },
                        (t, s) => new MatchPointCountDto { Type = t, Count = s.Count() }
                        )
                });

            var friendlyMatchesPointsGroupByMonth = months.GroupJoin(
                frendlyMatchesPoints,
                m => new { Month = m.Month, Year = m.Year },
                stat => new { Month = stat.Match.Date.Month, Year = stat.Match.Date.Year },
                (m, s) => new MonthlyMatchPointCountDto
                {
                    Month = m.Month,
                    Year = m.Year,
                    PointsByType = pointTypes.GroupJoin(
                        s,
                        t => new { Type = t },
                        stat => new { Type = stat.Point },
                        (t, s) => new MatchPointCountDto { Type = t, Count = s.Count() }
                        )
                });

            return new PlayerMonthlyMatchPointStatisticsDto
            {
                PlayerId = playerId,
                CupMatchesPoints = cupMatchesPointsGroupByMonth,
                LeagueMatchesPoints = leagueMatchesPointsGroupByMonth,
                FriendlyMatchesPoints = friendlyMatchesPointsGroupByMonth
            };
        }
        #endregion

        #region Team Match Points
        public async Task<TeamMatchPointStatisticsDto> GetAllTeamMatchPoints(Guid teamId)
        {
            var pointTypes = Enum.GetValues(typeof(MatchPointType)).Cast<MatchPointType>();

            var points = new TeamMatchPointStatisticsDto
            {
                TeamId = teamId,
                CupMatchesPoints = pointTypes.GroupJoin(await _matchPointRepository.GetAll().AsNoTracking()
                    .Include(x => x.Match).Include(x => x.Team)
                    .Where(x => x.IsActive && x.Match.IsActive && x.Match.MatchType == MatchType.Cup && x.Team.Id == teamId)
                    .Select(x => new { Type = x.Point })
                    .ToListAsync(),
                    t => new { Type = t },
                    stat => new { Type = stat.Type },
                    (t, s) => new MatchPointCountDto { Type = t, Count = s.Count() }
                    ).OrderBy(x => x.Type),
                LeagueMatchesPoints = pointTypes.GroupJoin(await _matchPointRepository.GetAll().AsNoTracking()
                    .Include(x => x.Match).Include(x => x.Team)
                    .Where(x => x.IsActive && x.Match.IsActive && x.Match.MatchType == MatchType.League && x.Team.Id == teamId)
                    .Select(x => new { Type = x.Point })
                    .ToListAsync(),
                    t => new { Type = t },
                    stat => new { Type = stat.Type },
                    (t, s) => new MatchPointCountDto { Type = t, Count = s.Count() }
                    ).OrderBy(x => x.Type),
                FriendlyMatchPoints = pointTypes.GroupJoin(await _matchPointRepository.GetAll().AsNoTracking()
                    .Include(x => x.Match).Include(x => x.Team)
                    .Where(x => x.IsActive && x.Match.IsActive && x.Match.MatchType == MatchType.Friendly && x.Team.Id == teamId)
                    .Select(x => new { Type = x.Point })
                    .ToListAsync(),
                    t => new { Type = t },
                    stat => new { Type = stat.Type },
                    (t, s) => new MatchPointCountDto { Type = t, Count = s.Count() }
                    ).OrderBy(x => x.Type)
            };

            return points;
        }

        public async Task<TeamMonthlyMatchPointStatisticsDto> GetTeamMonthlyMatchPoints(Guid teamId, DateTime startDate, DateTime endDate)
        {
            var pointTypes = Enum.GetValues(typeof(MatchPointType)).Cast<MatchPointType>();

            var months = MonthsBetween(startDate, endDate);

            var points = await _matchPointRepository.GetAll().AsNoTracking().Include(x => x.Match).Include(x => x.Team)
                        .Where(x => x.IsActive && x.Match.IsActive && x.Team.Id == teamId && x.Match.Date >= startDate.Date && x.Match.Date <= endDate.Date).ToListAsync();

            var cupMatchesPoints = points.Where(x => x.Match.MatchType == MatchType.Cup);

            var legueMatchesPoints = points.Where(x => x.Match.MatchType == MatchType.League);

            var frendlyMatchesPoints = points.Where(x => x.Match.MatchType == MatchType.Friendly);


            var cupMatchesPointsGroupByMonth = months.GroupJoin(
                cupMatchesPoints,
                m => new { Month = m.Month, Year = m.Year },
                stat => new { Month = stat.Match.Date.Month, Year = stat.Match.Date.Year },
                (m, s) => new MonthlyMatchPointCountDto
                {
                    Month = m.Month,
                    Year = m.Year,
                    PointsByType = pointTypes.GroupJoin(
                        s,
                        t => new { Type = t },
                        stat => new { Type = stat.Point },
                        (t, s) => new MatchPointCountDto { Type = t, Count = s.Count() }
                        )
                });

            var leagueMatchesPointsGroupByMonth = months.GroupJoin(
                legueMatchesPoints,
                m => new { Month = m.Month, Year = m.Year },
                stat => new { Month = stat.Match.Date.Month, Year = stat.Match.Date.Year },
                (m, s) => new MonthlyMatchPointCountDto
                {
                    Month = m.Month,
                    Year = m.Year,
                    PointsByType = pointTypes.GroupJoin(
                        s,
                        t => new { Type = t },
                        stat => new { Type = stat.Point },
                        (t, s) => new MatchPointCountDto { Type = t, Count = s.Count() }
                        )
                });

            var friendlyMatchesPointsGroupByMonth = months.GroupJoin(
                frendlyMatchesPoints,
                m => new { Month = m.Month, Year = m.Year },
                stat => new { Month = stat.Match.Date.Month, Year = stat.Match.Date.Year },
                (m, s) => new MonthlyMatchPointCountDto
                {
                    Month = m.Month,
                    Year = m.Year,
                    PointsByType = pointTypes.GroupJoin(
                        s,
                        t => new { Type = t },
                        stat => new { Type = stat.Point },
                        (t, s) => new MatchPointCountDto { Type = t, Count = s.Count() }
                        )
                });

            return new TeamMonthlyMatchPointStatisticsDto
            {
                TeamId = teamId,
                CupMatchesPoints = cupMatchesPointsGroupByMonth,
                LeagueMatchesPoints = leagueMatchesPointsGroupByMonth,
                FriendlyMatchesPoints = friendlyMatchesPointsGroupByMonth
            };
        }


        #endregion

        #region Club Training Scores
        public async Task<IEnumerable<TrainingScoreStatisticsDto>> GetClubTrainingScoreStatistics()
        {
            var scoreTypes = Enum.GetValues(typeof(TrainingScoreType)).Cast<TrainingScoreType>();

            var trainingScores = await _trainingScoreRepository.GetAll().AsNoTracking()
                .Include(x => x.Training)
                .Where(x => x.IsActive && x.Training.IsActive)
                .ToListAsync();

            return scoreTypes.GroupJoin(
                trainingScores,
                t => new { Type = t },
                stat => new { Type = stat.ScoreType },
                (t, s) => new TrainingScoreStatisticsDto
                {
                    ScoreType = t,
                    Min = s.Count() > 0 ? s.Select(x => x.Value).Min() : 0,
                    Max = s.Count() > 0 ? s.Select(x => x.Value).Max() : 0,
                    Avg = s.Count() > 0 ? s.Select(x => x.Value).Average() : 0,
                    Median = s.Count() > 0 ? s.Select(x => x.Value).Median() : 0
                });
        }

        public async Task<ClubMonthlyTrainingScoreStatisticsDto> GetMonthlyClubTrainingScoreStatistics(DateTime startDate, DateTime endDate)
        {
            var scoreTypes = Enum.GetValues(typeof(TrainingScoreType)).Cast<TrainingScoreType>();

            var months = MonthsBetween(startDate, endDate);

            var scores = await _trainingScoreRepository.GetAll().AsNoTracking().Include(x => x.Training)
                        .Where(x => x.IsActive && x.Training.IsActive && x.Training.Date >= startDate.Date && x.Training.Date < endDate.Date.AddDays(1)).ToListAsync();


            var trainingScoreStatisticsGroupedByMonth = months.GroupJoin(
                scores,
                m => new { Month = m.Month, Year = m.Year },
                stat => new { Month = stat.Training.Date.Month, Year = stat.Training.Date.Year },
                (m, s) => new MonthlyTrainingScoreStatisticsDto
                {
                    Month = m.Month,
                    Year = m.Year,
                    Scores = scoreTypes.GroupJoin(
                        s,
                        t => new { Type = t },
                        stat => new { Type = stat.ScoreType },
                        (t, s) => new TrainingScoreStatisticsDto
                        {
                            ScoreType = t,
                            Min = s.Count() > 0 ? s.Select(x => x.Value).Min() : 0,
                            Max = s.Count() > 0 ? s.Select(x => x.Value).Max() : 0,
                            Avg = s.Count() > 0 ? s.Select(x => x.Value).Average() : 0,
                            Median = s.Count() > 0 ? s.Select(x => x.Value).Median() : 0
                        })
                });

            return new ClubMonthlyTrainingScoreStatisticsDto
            {
                ScoreStatistics = trainingScoreStatisticsGroupedByMonth
            };

        }
        #endregion
        public async Task<PlayerTrainingScoreStatisticsDto> GetPlayerTrainingScoreStatistics(Guid playerId)
        {
            var scoreTypes = Enum.GetValues(typeof(TrainingScoreType)).Cast<TrainingScoreType>();

            var trainingScores = await _trainingScoreRepository.GetAll().AsNoTracking()
                .Include(x => x.Training).Include(x => x.Player)
                .Where(x => x.IsActive && x.Training.IsActive && x.Player.Id == playerId)
                .ToListAsync();

            var scores = scoreTypes.GroupJoin(
                trainingScores,
                t => new { Type = t },
                stat => new { Type = stat.ScoreType },
                (t, s) => new TrainingScoreStatisticsDto
                {
                    ScoreType = t,
                    Min = s.Count() > 0 ? s.Select(x => x.Value).Min() : 0,
                    Max = s.Count() > 0 ? s.Select(x => x.Value).Max() : 0,
                    Avg = s.Count() > 0 ? s.Select(x => x.Value).Average() : 0,
                    Median = s.Count() > 0 ? s.Select(x => x.Value).Median() : 0
                });

            return new PlayerTrainingScoreStatisticsDto { PlayerId = playerId, Scores = scores };
        }

        public async Task<PlayerMonthlyTrainingScoreStatisticsDto> GetMonthlyPlayerTrainingScoreStatistics(Guid playerId, DateTime startDate, DateTime endDate)
        {
            var scoreTypes = Enum.GetValues(typeof(TrainingScoreType)).Cast<TrainingScoreType>();

            var months = MonthsBetween(startDate, endDate);

            var scores = await _trainingScoreRepository.GetAll().AsNoTracking().Include(x => x.Training).Include(x => x.Player)
                        .Where(x => x.IsActive && x.Training.IsActive && x.Player.Id == playerId && x.Training.Date >= startDate.Date && x.Training.Date < endDate.Date.AddDays(1)).ToListAsync();


            var trainingScoreStatisticsGroupedByMonth = months.GroupJoin(
                scores,
                m => new { Month = m.Month, Year = m.Year },
                stat => new { Month = stat.Training.Date.Month, Year = stat.Training.Date.Year },
                (m, s) => new MonthlyTrainingScoreStatisticsDto
                {
                    Month = m.Month,
                    Year = m.Year,
                    Scores = scoreTypes.GroupJoin(
                        s,
                        t => new { Type = t },
                        stat => new { Type = stat.ScoreType },
                        (t, s) => new TrainingScoreStatisticsDto
                        {
                            ScoreType = t,
                            Min = s.Count() > 0 ? s.Select(x => x.Value).Min() : 0,
                            Max = s.Count() > 0 ? s.Select(x => x.Value).Max() : 0,
                            Avg = s.Count() > 0 ? s.Select(x => x.Value).Average() : 0,
                            Median = s.Count() > 0 ? s.Select(x => x.Value).Median() : 0
                        })
                });

            return new PlayerMonthlyTrainingScoreStatisticsDto
            {
                PlayerId = playerId,
                ScoreStatistics = trainingScoreStatisticsGroupedByMonth
            };

        }

        public async Task<IEnumerable<MatchScoreStatisticsDto>> GetClubMatchScoreStatistics()
        {
            var scoreTypes = Enum.GetValues(typeof(MatchScoreType)).Cast<MatchScoreType>();

            var trainingScores = await _matchPlayerScoreRepository.GetAll().AsNoTracking()
                .Include(x => x.Match)
                .Where(x => x.IsActive && x.Match.IsActive)
                .ToListAsync();

            return scoreTypes.GroupJoin(
                trainingScores,
                t => new { Type = t },
                stat => new { Type = stat.ScoreType },
                (t, s) => new MatchScoreStatisticsDto
                {
                    ScoreType = t,
                    Min = s.Count() > 0 ? s.Select(x => x.Value).Min() : 0,
                    Max = s.Count() > 0 ? s.Select(x => x.Value).Max() : 0,
                    Avg = s.Count() > 0 ? s.Select(x => x.Value).Average() : 0,
                    Median = s.Count() > 0 ? s.Select(x => x.Value).Median() : 0
                });
        }

        public async Task<ClubMonthlyMatchScoreStatisticsDto> GetMonthlyClubMatchScoreStatistics(DateTime startDate, DateTime endDate)
        {
            var scoreTypes = Enum.GetValues(typeof(MatchScoreType)).Cast<MatchScoreType>();

            var months = MonthsBetween(startDate, endDate);

            var scores = await _matchPlayerScoreRepository.GetAll().AsNoTracking().Include(x => x.Match)
                        .Where(x => x.IsActive && x.Match.IsActive && x.Match.Date >= startDate.Date && x.Match.Date < endDate.Date.AddDays(1)).ToListAsync();


            var trainingScoreStatisticsGroupedByMonth = months.GroupJoin(
                scores,
                m => new { Month = m.Month, Year = m.Year },
                stat => new { Month = stat.Match.Date.Month, Year = stat.Match.Date.Year },
                (m, s) => new MonthlyMatchScoreStatisticsDto
                {
                    Month = m.Month,
                    Year = m.Year,
                    Scores = scoreTypes.GroupJoin(
                        s,
                        t => new { Type = t },
                        stat => new { Type = stat.ScoreType },
                        (t, s) => new MatchScoreStatisticsDto
                        {
                            ScoreType = t,
                            Min = s.Count() > 0 ? s.Select(x => x.Value).Min() : 0,
                            Max = s.Count() > 0 ? s.Select(x => x.Value).Max() : 0,
                            Avg = s.Count() > 0 ? s.Select(x => x.Value).Average() : 0,
                            Median = s.Count() > 0 ? s.Select(x => x.Value).Median() : 0
                        })
                });

            return new ClubMonthlyMatchScoreStatisticsDto
            {
                ScoreStatistics = trainingScoreStatisticsGroupedByMonth
            };

        }

        public async Task<PlayerMatchScoreStatisticsDto> GetPlayerMatchScoreStatistics(Guid playerId)
        {
            var scoreTypes = Enum.GetValues(typeof(MatchScoreType)).Cast<MatchScoreType>();

            var matchScores = await _matchPlayerScoreRepository.GetAll().AsNoTracking()
                .Include(x => x.Match).Include(x => x.Player)
                .Where(x => x.IsActive && x.Match.IsActive && x.Player.Id == playerId)
                .ToListAsync();

            var scores = scoreTypes.GroupJoin(
                matchScores,
                t => new { Type = t },
                stat => new { Type = stat.ScoreType },
                (t, s) => new MatchScoreStatisticsDto
                {
                    ScoreType = t,
                    Min = s.Count() > 0 ? s.Select(x => x.Value).Min() : 0,
                    Max = s.Count() > 0 ? s.Select(x => x.Value).Max() : 0,
                    Avg = s.Count() > 0 ? s.Select(x => x.Value).Average() : 0,
                    Median = s.Count() > 0 ? s.Select(x => x.Value).Median() : 0
                });

            return new PlayerMatchScoreStatisticsDto { PlayerId = playerId, Scores = scores };
        }

        public async Task<PlayerMonthlyMatchScoreStatisticsDto> GetMonthlyPlayerMatchScoreStatistics(Guid playerId, DateTime startDate, DateTime endDate)
        {
            var scoreTypes = Enum.GetValues(typeof(MatchScoreType)).Cast<MatchScoreType>();

            var months = MonthsBetween(startDate, endDate);

            var scores = await _matchPlayerScoreRepository.GetAll().AsNoTracking().Include(x => x.Match).Include(x => x.Player)
                        .Where(x => x.IsActive && x.Match.IsActive && x.Player.Id == playerId && x.Match.Date >= startDate.Date && x.Match.Date < endDate.Date.AddDays(1)).ToListAsync();


            var matchScoreStatisticsGroupedByMonth = months.GroupJoin(
                scores,
                m => new { Month = m.Month, Year = m.Year },
                stat => new { Month = stat.Match.Date.Month, Year = stat.Match.Date.Year },
                (m, s) => new MonthlyMatchScoreStatisticsDto
                {
                    Month = m.Month,
                    Year = m.Year,
                    Scores = scoreTypes.GroupJoin(
                        s,
                        t => new { Type = t },
                        stat => new { Type = stat.ScoreType },
                        (t, s) => new MatchScoreStatisticsDto
                        {
                            ScoreType = t,
                            Min = s.Count() > 0 ? s.Select(x => x.Value).Min() : 0,
                            Max = s.Count() > 0 ? s.Select(x => x.Value).Max() : 0,
                            Avg = s.Count() > 0 ? s.Select(x => x.Value).Average() : 0,
                            Median = s.Count() > 0 ? s.Select(x => x.Value).Median() : 0
                        })
                });

            return new PlayerMonthlyMatchScoreStatisticsDto
            {
                PlayerId = playerId,
                ScoreStatistics = matchScoreStatisticsGroupedByMonth
            };

        }

        public async Task<PlayersTrainingScoresRankingDto> GetPlayersTrainingScoresRanking(TrainingScoresRankingQuery query)
        {
            var trainingScores = _trainingScoreRepository.GetAll().AsNoTracking()
               .Include(x => x.Training)
               .Include(x => x.Player).ThenInclude(x => x.User)
               .Include(x => x.Player).ThenInclude(x => x.Team)
               .Where(x => x.IsActive && x.Training.IsActive && x.ScoreType == query.ScoreType && x.Training.Date >= query.StartDate.Date && x.Training.Date < query.EndDate.Date.AddDays(1));

            if (query.OnlyStillPlaying.HasValue)
            {
                if(query.OnlyStillPlaying == true)
                {
                    trainingScores = trainingScores.Where(x => x.Player.FinishedPlaying == null);
                }
                else
                {
                    trainingScores = trainingScores.Where(x => x.Player.FinishedPlaying.HasValue);
                }
            }

            if (query.TeamsIds != null && query.TeamsIds.Any())
            {
                trainingScores = query.GetPlayersWithoutTeam 
                    ? trainingScores.Where(x => query.TeamsIds.Contains(x.Player.Team.Id) || x.Player.Team == null)
                    : trainingScores.Where(x => query.TeamsIds.Contains(x.Player.Team.Id));
            }
            else if (query.GetPlayersWithoutTeam)
            {
                trainingScores = trainingScores.Where(x => x.Player.Team == null);
            }

            var scoresList = await trainingScores.ToListAsync();

            var ranking = scoresList.GroupBy(x => x.Player.Id).OrderByDescending(x => x.Average(x => x.Value))
                .Take(query.Count).Select(x => new PlayerRankingScoreValueDto
            {
                Avg = x.Average(x => x.Value),
                Player = new SimplePlayerNameDto
                {
                    Id = x.FirstOrDefault().Player.Id,
                    Name = x.FirstOrDefault().Player.User.Name,
                    MiddleName = x.FirstOrDefault().Player.User.MiddleName,
                    Surname = x.FirstOrDefault().Player.User.Surname,
                }
            });

            return new PlayersTrainingScoresRankingDto { 
                ScoreType = query.ScoreType,
                PlayerScores = ranking
            };
        }

        public async Task<PlayersMatchScoresRankingDto> GetPlayersMatchScoresRanking(MatchScoresRankingQuery query)
        {
            var matchScores = _matchPlayerScoreRepository.GetAll().AsNoTracking()
               .Include(x => x.Match)
               .Include(x => x.Player).ThenInclude(x => x.User)
               .Include(x => x.Player).ThenInclude(x => x.Team)
               .Where(x => x.IsActive && x.Match.IsActive && x.ScoreType == query.ScoreType && x.Match.Date >= query.StartDate.Date && x.Match.Date < query.EndDate.Date.AddDays(1));

            if (query.OnlyStillPlaying.HasValue)
            {
                if (query.OnlyStillPlaying == true)
                {
                    matchScores = matchScores.Where(x => x.Player.FinishedPlaying == null);
                }
                else
                {
                    matchScores = matchScores.Where(x => x.Player.FinishedPlaying.HasValue);
                }
            }

            if (query.TeamsIds != null && query.TeamsIds.Any())
            {
                matchScores = query.GetPlayersWithoutTeam
                    ? matchScores.Where(x => query.TeamsIds.Contains(x.Player.Team.Id) || x.Player.Team == null)
                    : matchScores.Where(x => query.TeamsIds.Contains(x.Player.Team.Id));
            }
            else if (query.GetPlayersWithoutTeam)
            {
                matchScores = matchScores.Where(x => x.Player.Team == null);
            }

            var scoresList = await matchScores.ToListAsync();

            var ranking = scoresList.GroupBy(x => x.Player.Id).OrderByDescending(x => x.Average(x => x.Value))
                .Take(query.Count).Select(x => new PlayerRankingScoreValueDto
                {
                    Avg = x.Average(x => x.Value),
                    Player = new SimplePlayerNameDto
                    {
                        Id = x.FirstOrDefault().Player.Id,
                        Name = x.FirstOrDefault().Player.User.Name,
                        MiddleName = x.FirstOrDefault().Player.User.MiddleName,
                        Surname = x.FirstOrDefault().Player.User.Surname,
                    }
                });

            return new PlayersMatchScoresRankingDto
            {
                ScoreType = query.ScoreType,
                PlayerScores = ranking
            };
        }


        private static IEnumerable<(int Month, int Year)> MonthsBetween(
            DateTime startDate,
            DateTime endDate
        )
        {
            DateTime iterator;
            DateTime limit;

            if (endDate > startDate)
            {
                iterator = new DateTime(startDate.Year, startDate.Month, 1);
                limit = endDate;
            }
            else
            {
                iterator = new DateTime(endDate.Year, endDate.Month, 1);
                limit = startDate;
            }
            while (iterator <= limit)
            {
                yield return (
                    iterator.Month,
                    iterator.Year
                );

                iterator = iterator.AddMonths(1);
            }
        }
    }
}
