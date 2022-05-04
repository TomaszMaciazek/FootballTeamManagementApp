using App.Model.Dtos.History;
using App.Repository.Repositories;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace App.ServiceLayer.Services
{
    public interface ITeamHistoryService
    {
        Task<TeamHistoryDto> GetTeamHistory(Guid teamId, DateTime? minDate = null, DateTime? maxDate = null);
    }

    public class TeamHistoryService : ITeamHistoryService
    {
        private readonly ITeamHistoryEventsRepository _teamHistoryEventsRepository;
        private readonly ITeamRepository _teamRepository;
        private readonly IMapper _mapper;

        public TeamHistoryService(ITeamHistoryEventsRepository teamHistoryEventsRepository, ITeamRepository teamRepository, IMapper mapper)
        {
            _teamHistoryEventsRepository = teamHistoryEventsRepository;
            _teamRepository = teamRepository;
            _mapper = mapper;
        }

        public async Task<TeamHistoryDto> GetTeamHistory(Guid teamId, DateTime? minDate = null, DateTime? maxDate = null)
        {
            var team = await _teamRepository.GetAll().AsNoTracking().SingleOrDefaultAsync(x => x.Id == teamId);

            var history = new TeamHistoryDto
            {
                Id = teamId,
                Created = team.CreatedDate
            };

            var coachEvents = _teamHistoryEventsRepository.GetCoachAssignments()
                .Where(x => x.TeamHistory.TeamId == team.Id);

            coachEvents = minDate.HasValue ? coachEvents.Where(x => x.Date.Date >= minDate.Value.Date) : coachEvents;
            coachEvents = maxDate.HasValue ? coachEvents.Where(x => x.Date.Date <= maxDate.Value.Date) : coachEvents;

            history.CoachAssignmentEvents = await coachEvents.ProjectTo<TeamHistoryCoachAssignmentEventDto>(_mapper.ConfigurationProvider).ToListAsync();

            var matchEvents = _teamHistoryEventsRepository.GetMatchesPlayed().Where(x => x.TeamHistory.TeamId == teamId);

            matchEvents = minDate.HasValue ? matchEvents.Where(x => x.Match.Date.Date >= minDate.Value.Date) : matchEvents;
            matchEvents = maxDate.HasValue ? matchEvents.Where(x => x.Match.Date.Date <= maxDate.Value.Date) : matchEvents;

            history.MatchEvents = await matchEvents.ProjectTo<TeamHistoryMatchEventDto>(_mapper.ConfigurationProvider).ToListAsync();

            var playerJoined = _teamHistoryEventsRepository.GetPlayerJoinedEvents()
                .Where(x => x.TeamHistory.TeamId == team.Id);

            playerJoined = minDate.HasValue ? playerJoined.Where(x => x.Date.Date >= minDate.Value.Date) : playerJoined;
            playerJoined = maxDate.HasValue ? playerJoined.Where(x => x.Date.Date <= maxDate.Value.Date) : playerJoined;

            history.PlayerJoinedTeamEvents = await playerJoined.ProjectTo<TeamHistoryPlayerJoinedTeamEventDto>(_mapper.ConfigurationProvider).ToListAsync();

            var playerLeft = _teamHistoryEventsRepository.GetPlayerLeftEvents()
                .Where(x => x.TeamHistory.TeamId == team.Id);

            playerLeft = minDate.HasValue ? playerLeft.Where(x => x.Date.Date >= minDate.Value.Date) : playerLeft;
            playerLeft = maxDate.HasValue ? playerLeft.Where(x => x.Date.Date <= maxDate.Value.Date) : playerLeft;

            history.PlayerLeftTeamEvents = await playerLeft.ProjectTo<TeamHistoryPlayerLeftTeamEventDto>(_mapper.ConfigurationProvider).ToListAsync();

            return history;
        }
    }
}
