using App.DataAccess.Interfaces;
using App.Model.Entities;
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
    public interface IMatchService
    {
        Task<bool> ActivateAsync(Guid id);
        Task AddAsync(Match entity);
        Task<bool> DeactivateAsync(Guid id);
        Task<List<Match>> GetAllAsync();
        Task<IEnumerable<Match>> GetAllFromCoach(Guid coachId);
        Task<IEnumerable<Match>> GetAllFromPlayer(Guid playerId);
        Task<Match> GetByIdAsync(Guid id);
        Task<Match> GetByIdEager(Guid id);
        Task<PaginatedList<Match>> GetPaginatedMatches(MatchQuery query);
        Task<PaginatedList<Match>> GetPaginatedMatchesFromCoach(Guid coachId, MatchQuery query);
        Task<PaginatedList<Match>> GetPaginatedMatchesFromPlayer(Guid playerId, MatchQuery query);
        Task RemoveAsync(Guid id);
        Task UpdateAsync(Match entity);
    }

    public class MatchService : IService<Match>, IMatchService
    {
        private readonly IMatchRepository _matchRepository;
        private readonly IApplicationDbContext _context;

        public MatchService(IMatchRepository matchRepository, IApplicationDbContext context)
        {
            _matchRepository = matchRepository;
            _context = context;
        }

        public async Task<bool> ActivateAsync(Guid id)
        {
            var objectToActivate = await _matchRepository.GetById(id).SingleOrDefaultAsync();
            if (objectToActivate == null)
            {
                return false;
            }

            objectToActivate.IsActive = true;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task AddAsync(Match entity)
        {
            _matchRepository.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeactivateAsync(Guid id)
        {
            var objectToDeactivate = await _matchRepository.GetById(id).SingleOrDefaultAsync();
            if (objectToDeactivate == null)
            {
                return false;
            }

            objectToDeactivate.IsActive = false;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Match>> GetAllAsync() => await _matchRepository.GetAll().ToListAsync();

        public async Task<Match> GetByIdAsync(Guid id) => await _matchRepository.GetById(id).FirstOrDefaultAsync();

        public async Task<Match> GetByIdEager(Guid id) => await _matchRepository.GetAllEager().FirstOrDefaultAsync(x => x.Id == id);

        public async Task RemoveAsync(Guid id)
        {
            _matchRepository.Remove(id);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Match entity)
        {
            _matchRepository.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Match>> GetAllFromPlayer(Guid playerId) => await _matchRepository
            .GetAllEager()
            .Where(x => x.Players.Any(x => x.Player.Id == playerId) && x.IsActive)
            .ToListAsync();

        public async Task<IEnumerable<Match>> GetAllFromCoach(Guid coachId) => await _matchRepository
            .GetAllEager()
            .Where(x => x.Coaches.Any(x => x.Id == coachId && x.IsActive))
            .ToListAsync();

        public async Task<PaginatedList<Match>> GetPaginatedMatches(MatchQuery query)
        {
            var matches = _matchRepository.GetAll().Where(x => x.IsActive);

            matches = matches.WhereStringPropertyContains(x => x.OpponentsClubName, query.OpponentsClubName);

            matches = matches.WhereStringPropertyContains(x => x.Location, query.Location);

            matches = matches.WhereDatetimePropertyLessOrEqual(x => x.Date, query.MaxDate);

            matches = matches.WhereDatetimePropertyGreaterOrEqual(x => x.Date, query.MinDate);

            matches = query.PlayersGender.HasValue
                ? matches.Where(x => x.PlayersGender == query.PlayersGender.Value)
                : matches;

            matches = matches.OrderByProperty(query.OrderByColumnName, query.OrderByDirection);

            return await matches.PaginatedListAsync(query.PageNumber, query.PageSize);
        }

        public async Task<PaginatedList<Match>> GetPaginatedMatchesFromCoach(Guid coachId, MatchQuery query)
        {
            var matches = _matchRepository.GetAll()
                .Include(x => x.Coaches)
                .Where(x => x.Coaches.Any(x => x.Id == coachId) && x.IsActive);

            matches = matches.WhereStringPropertyContains(x => x.OpponentsClubName, query.OpponentsClubName);

            matches = matches.WhereStringPropertyContains(x => x.Location, query.Location);

            matches = matches.WhereDatetimePropertyLessOrEqual(x => x.Date, query.MaxDate);

            matches = matches.WhereDatetimePropertyGreaterOrEqual(x => x.Date, query.MinDate);

            matches = query.PlayersGender.HasValue
                ? matches.Where(x => x.PlayersGender == query.PlayersGender.Value)
                : matches;

            matches = matches.OrderByProperty(query.OrderByColumnName, query.OrderByDirection);

            return await matches.PaginatedListAsync(query.PageNumber, query.PageSize);
        }

        public async Task<PaginatedList<Match>> GetPaginatedMatchesFromPlayer(Guid playerId, MatchQuery query)
        {
            var matches = _matchRepository.GetAll()
                .Include(x => x.Players).ThenInclude(x => x.Player)
                .Where(x => x.Players.Any(x => x.Player.Id == playerId) && x.IsActive);

            matches = matches.WhereStringPropertyContains(x => x.OpponentsClubName, query.OpponentsClubName);

            matches = matches.WhereStringPropertyContains(x => x.Location, query.Location);

            matches = matches.WhereDatetimePropertyLessOrEqual(x => x.Date, query.MaxDate);

            matches = matches.WhereDatetimePropertyGreaterOrEqual(x => x.Date, query.MinDate);

            matches = query.PlayersGender.HasValue
                ? matches.Where(x => x.PlayersGender == query.PlayersGender.Value)
                : matches;

            matches = matches.OrderByProperty(query.OrderByColumnName, query.OrderByDirection);

            return await matches.PaginatedListAsync(query.PageNumber, query.PageSize);
        }
    }
}
