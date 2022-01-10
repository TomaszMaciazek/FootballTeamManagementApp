using App.DataAccess.Interfaces;
using App.Model.Dtos;
using App.Model.Entities;
using App.Repository.Repositories;
using App.ServiceLayer.Common;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.ServiceLayer.Services
{
    public interface IMatchPlayerPerformanceService
    {
        Task<bool> ActivateAsync(Guid id);
        Task AddAsync(MatchPlayerPerformance entity);
        Task<bool> DeactivateAsync(Guid id);
        Task<List<MatchPlayerPerformance>> GetAllAsync();
        Task<MatchPlayerPerformance> GetByIdAsync(Guid id);
        Task RemoveAsync(Guid id);
        Task UpdateAsync(MatchPlayerPerformance entity);
        Task<IEnumerable<SimpleMatchPlayerDto>> GetPlayersFromMatch(Guid matchId);
    }

    public class MatchPlayerPerformanceService : IService<MatchPlayerPerformance>, IMatchPlayerPerformanceService
    {
        private readonly IMatchPlayerPerformanceRepository _matchPlayerRepository;
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public MatchPlayerPerformanceService(IMatchPlayerPerformanceRepository matchPlayerRepository, IApplicationDbContext context, IMapper mapper)
        {
            _matchPlayerRepository = matchPlayerRepository;
            _context = context;
            _mapper = mapper;
        }

        public async Task<bool> ActivateAsync(Guid id)
        {
            var objectToActivate = await _matchPlayerRepository.GetById(id).SingleOrDefaultAsync();
            if (objectToActivate == null)
            {
                return false;
            }

            objectToActivate.IsActive = true;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task AddAsync(MatchPlayerPerformance entity)
        {
            _matchPlayerRepository.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeactivateAsync(Guid id)
        {
            var objectToDeactivate = await _matchPlayerRepository.GetById(id).SingleOrDefaultAsync();
            if (objectToDeactivate == null)
            {
                return false;
            }

            objectToDeactivate.IsActive = false;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<MatchPlayerPerformance>> GetAllAsync() => await _matchPlayerRepository.GetAll().ToListAsync();

        public async Task<MatchPlayerPerformance> GetByIdAsync(Guid id) => await _matchPlayerRepository.GetById(id).FirstOrDefaultAsync();

        public async Task RemoveAsync(Guid id)
        {
            _matchPlayerRepository.Remove(id);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(MatchPlayerPerformance entity)
        {
            _matchPlayerRepository.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<SimpleMatchPlayerDto>> GetPlayersFromMatch(Guid matchId) => await _matchPlayerRepository.GetAll()
                .AsNoTracking()
                .Include(x => x.Player).ThenInclude(x => x.User)
                .Include(x => x.Player).ThenInclude(x => x.Team)
                .Where(x => x.Match.Id == matchId)
                .ProjectTo<SimpleMatchPlayerDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
    }
}
