using App.DataAccess.Interfaces;
using App.Model.Dtos;
using App.Model.Dtos.ListItemDtos;
using App.Model.Entities;
using App.Model.ViewModels.Commands;
using App.Repository.Repositories;
using App.ServiceLayer.Extenstions;
using App.ServiceLayer.Models;
using App.Model.ViewModels.Queries;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Model.Enums;
using App.DataAccess.Exceptions;

namespace App.ServiceLayer.Services
{
    public interface IPlayerService
    {
        Task<bool> ActivateAsync(Guid id);
        Task AddAsync(Player entity);
        Task<bool> DeactivateAsync(Guid id);
        Task<List<SimpleSelectPlayerDto>> GetAllAsync();
        Task<Player> GetByIdAsync(Guid id);
        Task<PlayerDto> GetByIdEagerAsync(Guid id);
        Task<Player> GetByUserIdEagerAsync(Guid id);
        Task RemoveAsync(Guid id);
        Task UpdateAsync(UpdatePlayerVM command);
        Task<PaginatedList<PlayerListItemDto>> GetPlayers(PlayerQuery query);
        Task<IEnumerable<SimpleSelectPlayerDto>> GetPlayingPlayers(DateTime? Date = null, PlayersGender? PlayersGender = null);
        Task AddPlayerToTeam(AddPlayersToTeamVM command);
        Task RemovePlayerFromTeam(Guid playerId, Guid teamId);
        Task<IEnumerable<SimpleSelectPlayerDto>> GetActivePlayersWithoutTeam();
        Task TogglePlaying(Guid id);
    }

    public class PlayerService : IPlayerService
    {
        private readonly IPlayerRepository _playerRepository;
        private readonly ICountryRepository _countryRepository;
        private readonly ITeamRepository _teamRepository;
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public PlayerService(
            IPlayerRepository playerRepository,
            ICountryRepository countryRepository,
            ITeamRepository teamRepository,
            IApplicationDbContext context,
            IMapper mapper
            )
        {
            _playerRepository = playerRepository;
            _countryRepository = countryRepository;
            _teamRepository = teamRepository;
            _context = context;
            _mapper = mapper;
        }

        public async Task<bool> ActivateAsync(Guid id)
        {
            var objectToActivate = await _playerRepository.GetById(id).SingleOrDefaultAsync();
            if (objectToActivate == null)
            {
                return false;
            }

            objectToActivate.IsActive = true;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task AddAsync(Player entity)
        {
            _playerRepository.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeactivateAsync(Guid id)
        {
            var objectToDeactivate = await _playerRepository.GetById(id).SingleOrDefaultAsync();
            if (objectToDeactivate == null)
            {
                return false;
            }

            objectToDeactivate.IsActive = false;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<SimpleSelectPlayerDto>> GetAllAsync() => await _playerRepository.GetAll()
            .AsNoTracking()
            .Include(x => x.User)
            .Include(x => x.Team)
            .ProjectTo<SimpleSelectPlayerDto>(_mapper.ConfigurationProvider)
            .ToListAsync();

        public async Task<Player> GetByIdAsync(Guid id) => await _playerRepository.GetById(id).FirstOrDefaultAsync();

        public async Task<PlayerDto> GetByIdEagerAsync(Guid id) 
            => await _playerRepository.GetByIdEager(id)
            .ProjectTo<PlayerDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync();

        public async Task<Player> GetByUserIdEagerAsync(Guid id) => await _playerRepository.GetByUserIdEager(id).FirstOrDefaultAsync();

        public async Task RemoveAsync(Guid id)
        {
            _playerRepository.Remove(id);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(UpdatePlayerVM command)
        {
            var player = await _playerRepository.GetAll()
                .Include(x => x.User)
                .Include(x => x.Country)
                .Include(x => x.Team)
                .SingleOrDefaultAsync(x => x.Id == command.Id);
            player.BirthDate = command.BirthDate ?? player.BirthDate;
            player.User.Email = !string.IsNullOrEmpty(command.Email) ? command.Email : player.User.Email;
            player.User.Username = !string.IsNullOrEmpty(command.Email) ? command.Email.ToLower() : player.User.Username;
            player.User.Name = !string.IsNullOrEmpty(command.Name) ? command.Name : player.User.Name;
            player.User.MiddleName = !string.IsNullOrEmpty(command.MiddleName) ? command.Name : player.User.MiddleName;
            player.User.Surname = !string.IsNullOrEmpty(command.Surname) ? command.Name : player.User.Surname;
            player.Country = command.CountryId.HasValue ? _countryRepository.GetById(command.CountryId.Value).FirstOrDefault() : player.Country;
            player.StartedPlaying = command.StartedPlaying ?? player.StartedPlaying;
            player.FinishedPlaying = command.FinishedPlaying ?? player.FinishedPlaying;
            player.Team = command.TeamId.HasValue ? _teamRepository.GetById(command.TeamId.Value).FirstOrDefault() : player.Team;
            _playerRepository.Update(player);
            await _context.SaveChangesAsync();
        }

        public async Task<PaginatedList<PlayerListItemDto>> GetPlayers(PlayerQuery query)
        {
            var players = _playerRepository.GetAll()
                .Include(x => x.User)
                .Include(x => x.Country)
                .Include(x => x.Team)
                .AsNoTracking();

            players = query.IsActive.HasValue ? players.Where(x => x.IsActive == query.IsActive) : players;

            if (!string.IsNullOrEmpty(query.QueryString))
            {
                players = players.Where(x =>
                x.User.Name.ToLower().Contains(query.QueryString.ToLower()) ||
                x.User.MiddleName.ToLower().Contains(query.QueryString.ToLower()) ||
                x.User.Surname.ToLower().Contains(query.QueryString.ToLower()) ||
                $"{x.User.Surname} {x.User.Name} {x.User.MiddleName}".ToLower().Contains(query.QueryString.ToLower()) ||
                $"{x.User.Surname} {x.User.MiddleName} {x.User.Name}".ToLower().Contains(query.QueryString.ToLower()) ||
                $"{x.User.Name} {x.User.MiddleName} {x.User.Surname}".ToLower().Contains(query.QueryString.ToLower()) ||
                $"{x.User.Name} {x.User.Surname}".ToLower().Contains(query.QueryString.ToLower()) ||
                $"{x.User.Name} {x.User.Surname} {x.User.MiddleName}".ToLower().Contains(query.QueryString.ToLower())
                );
            }

            players = query.TeamId.HasValue ? players.Where(x => x.Team.Id == query.TeamId.Value) : players;

            players = query.CountryId.HasValue ? players.Where(x => x.Country.Id == query.CountryId.Value) : players;

            players = query.Gender.HasValue ? players.Where(x => x.Gender == query.Gender.Value) : players;

            if (query.IsStillPlaying.HasValue)
            {
                players = query.IsStillPlaying.Value == true
                    ? players.Where(x => !x.FinishedPlaying.HasValue)
                    : players.Where(x => x.FinishedPlaying.HasValue);
            }

            if (query.OrderByColumnName.ToLower() == "playername")
            {
                
                players = query.OrderByDirection == "asc"
                    ? players.OrderBy(x => x.User.Surname).ThenBy(x => x.User.Name).ThenBy(x => x.User.MiddleName)
                    : players.OrderByDescending(x => x.User.Surname).ThenByDescending(x => x.User.Name).ThenByDescending(x => x.User.MiddleName);
            }
            else
            {
                players = players.OrderByProperty(query.OrderByColumnName, query.OrderByDirection);
            }

            return await players
                .ProjectTo<PlayerListItemDto>(_mapper.ConfigurationProvider)
                .PaginatedListAsync(query.PageNumber, query.PageSize);
        }

        public async Task<IEnumerable<SimpleSelectPlayerDto>> GetPlayingPlayers(DateTime? date = null, PlayersGender? playersGender = null)
        {
            var players = _playerRepository.GetAll()
                .Include(x => x.User)
                .AsNoTracking();


            players = date.HasValue
                ? players.Where(x => !x.FinishedPlaying.HasValue || (x.StartedPlaying.HasValue && x.StartedPlaying.Value.Date <= date.Value.Date && x.FinishedPlaying.HasValue && x.FinishedPlaying.Value.Date >= date.Value.Date))
                : players.Where(x => !x.FinishedPlaying.HasValue);

            if (playersGender.HasValue && playersGender.Value != PlayersGender.Both)
            {
                if(playersGender == PlayersGender.Females) {
                    players = players.Where(x => x.Gender == Gender.Female);
                }
                else if(playersGender == PlayersGender.Males)
                {
                    players = players.Where(x => x.Gender == Gender.Male);
                }
            }

            return await players.ProjectTo<SimpleSelectPlayerDto>(_mapper.ConfigurationProvider).ToListAsync();
        }

        public async Task AddPlayerToTeam(AddPlayersToTeamVM command)
        {
            var updateDate = DateTime.Now;
            var team = await _teamRepository.GetById(command.TeamId).Include(x => x.History).SingleOrDefaultAsync();
            if (team != null)
            {
                foreach (var playerId in command.PlayersIds)
                {
                    var player = await _playerRepository.GetById(playerId)
                    .Include(x => x.Team)
                    .Include(x => x.History).ThenInclude(x => x.PlayerJoinedTeamEvents)
                    .SingleOrDefaultAsync();
                    if (player != null)
                    {
                        player.History.PlayerJoinedTeamEvents.Add(new PlayerJoinedTeamEvent
                        {
                            PlayerHistory = player.History,
                            Date = updateDate,
                            TeamHistory = team.History
                        });
                        player.Team = team;

                        _playerRepository.Update(player);
                    }
                }
                await _context.SaveChangesAsync();
            }
        }

        public async Task RemovePlayerFromTeam(Guid playerId, Guid teamId)
        {
            var player = await _playerRepository.GetById(playerId)
                .Include(x => x.Team).ThenInclude(team => team.History)
                .Include(x => x.History).ThenInclude(x => x.PlayerLeftTeamEvents)
                .SingleOrDefaultAsync();
            if (player != null)
            {
                if(player.Team != null && player.Team.Id == teamId)
                {
                    player.History.PlayerLeftTeamEvents.Add(new PlayerLeftTeamEvent
                    {
                        Date = DateTime.Now,
                        TeamHistory = player.Team.History,
                        PlayerHistory = player.History
                    });
                    player.Team = null;
                    _playerRepository.Update(player);
                    await _context.SaveChangesAsync();
                }
            }
        }

        public async Task<IEnumerable<SimpleSelectPlayerDto>> GetActivePlayersWithoutTeam()
            => await _playerRepository.GetAll()
                .AsNoTracking()
                .Include(x => x.Team)
                .Include(x => x.User)
                .Where(x => x.IsActive && x.Team == null)
                .ProjectTo<SimpleSelectPlayerDto>(_mapper.ConfigurationProvider)
                .ToListAsync();

        public async Task TogglePlaying(Guid id)
        {
            var player = await _playerRepository.GetById(id).SingleOrDefaultAsync();

            if(player != null)
            {
                player.FinishedPlaying = player.FinishedPlaying.HasValue ? null : DateTime.Now;
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new NotFoundException();
            }
        }
    }
}
