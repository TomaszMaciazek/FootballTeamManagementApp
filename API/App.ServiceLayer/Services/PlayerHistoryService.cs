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
    public interface IPlayerHistoryService
    {
        Task<PlayerHistoryDto> GetPlayerHistoryAsync(Guid playerId);
    }

    public class PlayerHistoryService : IPlayerHistoryService
    {
        private readonly IPlayerHistoryRepository _playerHistoryRepository;
        private readonly IPlayerRepository _playerRepository;
        private readonly IMapper _mapper;

        public PlayerHistoryService(IPlayerHistoryRepository playerHistoryRepository, IPlayerRepository playerRepository, IMapper mapper)
        {
            _playerHistoryRepository = playerHistoryRepository;
            _playerRepository = playerRepository;
            _mapper = mapper;
        }

        public async Task<PlayerHistoryDto> GetPlayerHistoryAsync(Guid playerId)
        {
            var player = await _playerRepository.GetAll().AsNoTracking().SingleOrDefaultAsync(x => x.Id == playerId);

            var history = new PlayerHistoryDto
            {
                Id = player.Id,
                Created = player.CreatedDate,
                MatchEvents = await _playerHistoryRepository.GetMatchPlayerPerformances()
                    .Where(x => x.PlayerHistory.PlayerId == playerId)
                    .ProjectTo<PlayerHistoryMatchDto>(_mapper.ConfigurationProvider)
                    .ToListAsync(),
                PlayerJoinedTeamEvents = await _playerHistoryRepository.GetPlayerJoinedTeamEvents()
                    .Where(x => x.PlayerHistory.PlayerId == playerId)
                    .ProjectTo<PlayerHistoryPlayerJoinedTeamEventDto>(_mapper.ConfigurationProvider)
                    .ToListAsync(),
                PlayerLeftTeamEvents = await _playerHistoryRepository.GetPlayerLeftTeamEvents()
                    .Where(x => x.PlayerHistory.PlayerId == playerId)
                    .ProjectTo<PlayerHistoryPlayerLeftTeamEventDto>(_mapper.ConfigurationProvider)
                    .ToListAsync(),
            };

            return history;
        }
    }
}
