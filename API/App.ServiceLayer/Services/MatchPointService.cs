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
using System.Text;
using System.Threading.Tasks;

namespace App.ServiceLayer.Services
{
    public interface IMatchPointService
    {
        Task<bool> ActivateAsync(Guid id);
        Task AddAsync(MatchPoint entity);
        Task<bool> DeactivateAsync(Guid id);
        Task<List<MatchPoint>> GetAllAsync();
        Task<IEnumerable<MatchPointDto>> GetAllFromMatch(Guid matchId);
        Task<MatchPoint> GetByIdAsync(Guid id);
        Task RemoveAsync(Guid id);
        Task UpdateAsync(MatchPoint entity);
    }

    public class MatchPointService : IMatchPointService
    {
        private readonly IMatchPointRepository _matchPointRepository;
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public MatchPointService(IMatchPointRepository matchPointRepository, IApplicationDbContext context, IMapper mapper)
        {
            _matchPointRepository = matchPointRepository;
            _context = context;
            _mapper = mapper;
        }

        public async Task<bool> ActivateAsync(Guid id)
        {
            var objectToActivate = await _matchPointRepository.GetById(id).SingleOrDefaultAsync();
            if (objectToActivate == null)
            {
                return false;
            }

            objectToActivate.IsActive = true;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task AddAsync(MatchPoint entity)
        {
            _matchPointRepository.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeactivateAsync(Guid id)
        {
            var objectToDeactivate = await _matchPointRepository.GetById(id).SingleOrDefaultAsync();
            if (objectToDeactivate == null)
            {
                return false;
            }

            objectToDeactivate.IsActive = false;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<MatchPoint>> GetAllAsync() => await _matchPointRepository.GetAll().ToListAsync();

        public async Task<MatchPoint> GetByIdAsync(Guid id) => await _matchPointRepository.GetById(id).FirstOrDefaultAsync();

        public async Task RemoveAsync(Guid id)
        {
            _matchPointRepository.Remove(id);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(MatchPoint entity)
        {
            _matchPointRepository.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<MatchPointDto>> GetAllFromMatch(Guid matchId) => await _matchPointRepository.GetAll()
                .AsNoTracking()
                .Include(x => x.Match)
                .Include(x => x.GoalScorer)
                .Where(x => x.Match.Id == matchId)
                .ProjectTo<MatchPointDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
    }
}
