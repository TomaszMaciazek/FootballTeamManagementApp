using App.Model.Dtos.StatisticsDtos;
using App.Model.Enums;
using App.Repository.Repositories;
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

        public async Task<PlayerCardStatisticsDto> GetAllPlayerCards(Guid playerId)
        {
            var cards = new PlayerCardStatisticsDto
            {
                PlayerId = playerId,
                RedCardsCount = await _playerCardRepository.GetAll().AsNoTracking().Include(x => x.Player).Include(x => x.Match).Where(x => x.IsActive && x.Match.IsActive && x.Player.Id == playerId && x.Color == Model.Enums.CardColor.Red).Select(x => x.Count).SumAsync(),
                YellowCardsCount = await _playerCardRepository.GetAll().AsNoTracking().Include(x => x.Player).Include(x => x.Match).Where(x => x.IsActive && x.Match.IsActive && x.Player.Id == playerId && x.Color == Model.Enums.CardColor.Yellow).Select(x => x.Count).SumAsync()
            };

            return cards;
        }

        public async Task<PlayerMonthlyCardStatisticsDto> GetPlayerMonthlyCards(Guid c, DateTime startDate, DateTime endDate)
        {
            var cards = new PlayerMonthlyCardStatisticsDto { PlayerId = c };

            var months = MonthsBetween(startDate, endDate);

            cards.RedCards = months
                .GroupJoin(
                     await _playerCardRepository.GetAll().AsNoTracking().Include(x => x.Match)
                     .Where(x => x.IsActive && x.Match.IsActive && x.Color == Model.Enums.CardColor.Red && x.Player.Id == c && x.Match.Date.Date >= startDate.Date && x.Match.Date.Date <= endDate.Date)
                     .Select(x => new { Year = x.Match.Date.Year, Month = x.Match.Date.Month }).ToListAsync(),
                    m => new { Month = m.Month, Year = m.Year },
                    stat => new { Month = stat.Month, Year = stat.Year },
                    (m, s) => new MonthDataCountStatisticsDto { Month = m.Month, Year = m.Year, Count = s.Count() }
                )
                .OrderBy(x => x.Year).ThenBy(x => x.Month);

            cards.YellowCards = months
                .GroupJoin(
                     await _playerCardRepository.GetAll().AsNoTracking().Include(x => x.Match)
                     .Where(x => x.IsActive && x.Match.IsActive && x.Color == Model.Enums.CardColor.Yellow && x.Player.Id == c && x.Match.Date.Date >= startDate.Date && x.Match.Date.Date <= endDate.Date)
                     .Select(x => new { Year = x.Match.Date.Year, Month = x.Match.Date.Month }).ToListAsync(),
                    m => new { Month = m.Month, Year = m.Year },
                    stat => new { Month = stat.Month, Year = stat.Year },
                    (m, s) => new MonthDataCountStatisticsDto { Month = m.Month, Year = m.Year, Count = s.Count() }
                )
                .OrderBy(x => x.Year).ThenBy(x => x.Month);

            return cards;

        }


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


        private static IEnumerable<(int Month, int Year)> MonthsBetween(
        DateTime startDate,
        DateTime endDate)
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
