using App.DataAccess.Interfaces;
using App.Model.Entities;
using App.Repository.Repositories;
using App.ServiceLayer.Common;
using App.ServiceLayer.Extenstions;
using App.ServiceLayer.Models;
using App.Model.ViewModels.Queries;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using App.Model.Dtos.ListItemDtos;
using AutoMapper.QueryableExtensions;

namespace App.ServiceLayer.Services
{
    public interface IMatchService
    {
        Task<bool> ActivateAsync(Guid id);
        Task AddAsync(Match entity);
        Task<bool> DeactivateAsync(Guid id);
        Task<List<Match>> GetAllAsync();
        Task<IEnumerable<Match>> GetAllFromPlayer(Guid playerId);
        Task<Match> GetByIdAsync(Guid id);
        Task<Match> GetByIdEager(Guid id);
        Task<PaginatedList<MatchListItemDto>> GetMatches(MatchQuery query);
        Task RemoveAsync(Guid id);
        Task UpdateAsync(Match entity);
    }

    public class MatchService : IService<Match>, IMatchService
    {
        private readonly IMatchRepository _matchRepository;
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public MatchService(IMatchRepository matchRepository, IApplicationDbContext context, IMapper mapper)
        {
            _matchRepository = matchRepository;
            _context = context;
            _mapper = mapper;
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

        public async Task<PaginatedList<MatchListItemDto>> GetMatches(MatchQuery query)
        {
            var matches = _matchRepository.GetAll()
                .Include(x => x.Players).ThenInclude(x => x.Player).ThenInclude(x => x.Team)
                .AsNoTracking();

            if (query.MinDate.HasValue)
            {
                matches = matches.Where(x => x.Date.Date >= query.MinDate.Value.Date);
            }

            if (query.MaxDate.HasValue)
            {
                matches = matches.Where(x => x.Date.Date <= query.MaxDate.Value.Date);
            }

            if (query.PlayersGenders != null && query.PlayersGenders.Any())
            {
                matches = matches.Where(x => query.PlayersGenders.Contains(x.PlayersGender));
            }

            if (query.MatchTypes != null && query.MatchTypes.Any())
            {
                matches = matches.Where(x => query.MatchTypes.Contains(x.MatchType));
            }

            if (query.TeamsIds != null && query.TeamsIds.Any())
            {
                matches = matches.Where(x => query.TeamsIds.Any(y => x.Players.Any(z => z.Player.Team.Id == y)));
            }

            matches = query.IsActive.HasValue ? matches.Where(x => x.IsActive == query.IsActive.Value) : matches;

            matches = matches.OrderByProperty(query.OrderByColumnName, query.OrderByDirection);

            return await matches
                .ProjectTo<MatchListItemDto>(_mapper.ConfigurationProvider)
                .PaginatedListAsync(query.PageNumber, query.PageSize);
        }
    }
}
