using App.DataAccess.Interfaces;
using App.Model.Entities;
using App.Model.ViewModels.Commands;
using App.Repository.Repositories;
using App.ServiceLayer.Common;
using App.ServiceLayer.Extenstions;
using App.ServiceLayer.Models;
using App.ServiceLayer.Queries;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.ServiceLayer.Services
{
    public interface IPlayerService
    {
        Task<bool> ActivateAsync(Guid id);
        Task AddAsync(Player entity);
        Task<bool> DeactivateAsync(Guid id);
        Task<List<Player>> GetAllAsync();
        Task<Player> GetByIdAsync(Guid id);
        Task<Player> GetByIdEagerAsync(Guid id);
        Task<Player> GetByUserIdEagerAsync(Guid id);
        Task RemoveAsync(Guid id);
        Task UpdateAsync(UpdatePlayerVM command);
        Task<PaginatedList<Player>> GetPlayers(PlayerQuery query);
    }

    public class PlayerService : IPlayerService
    {
        private readonly IPlayerRepository _playerRepository;
        private readonly ICountryRepository _countryRepository;
        private readonly ITeamRepository _teamRepository;
        private readonly IApplicationDbContext _context;

        public PlayerService(
            IPlayerRepository playerRepository,
            ICountryRepository countryRepository, 
            ITeamRepository teamRepository, 
            IApplicationDbContext context
            )
        {
            _playerRepository = playerRepository;
            _countryRepository = countryRepository;
            _teamRepository = teamRepository;
            _context = context;
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

        public async Task<List<Player>> GetAllAsync() => await _playerRepository.GetAll().ToListAsync();

        public async Task<Player> GetByIdAsync(Guid id) => await _playerRepository.GetById(id).FirstOrDefaultAsync();

        public async Task<Player> GetByIdEagerAsync(Guid id) => await _playerRepository.GetByIdEager(id).FirstOrDefaultAsync();

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

        public async Task<PaginatedList<Player>> GetPlayers(PlayerQuery query)
        {
            var players = _playerRepository.GetAll()
                .Include(x => x.User)
                .Include(x => x.Country)
                .Where(x => x.IsActive);

            players = players.WhereStringPropertyContains(x => x.User.Email, query.Email);

            players = players.WhereStringPropertyContains(x => x.User.Name, query.Firstname);

            players = players.WhereStringPropertyContains(x => x.User.Surname, query.Surname);

            players = players.WhereIntPropertyEquals(x => x.Country.Id, query.CountryId);

            players = query.Gender.HasValue ? players.Where(x => x.Gender == query.Gender.Value) : players;

            players = players.OrderByProperty(query.OrderByColumnName, query.OrderByDirection);

            return await players.PaginatedListAsync(query.PageNumber, query.PageSize);
        }
    }
}
