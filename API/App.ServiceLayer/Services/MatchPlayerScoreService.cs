using App.DataAccess.Interfaces;
using App.Model.Dtos;
using App.Model.Entities;
using App.Model.ViewModels.Commands;
using App.Repository.Repositories;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.ServiceLayer.Services
{
    public interface IMatchPlayerScoreService
    {
        Task<bool> ActivateAsync(Guid id);
        Task AddAsync(MatchPlayerScore entity);
        Task<bool> DeactivateAsync(Guid id);
        Task<List<MatchPlayerScore>> GetAllAsync();
        Task<MatchPlayerScore> GetByIdAsync(Guid id);
        Task<IEnumerable<SimpleMatchScoreDto>> GetScoresFromMatch(Guid matchId);
        Task RemoveAsync(Guid id);
        Task UpdateAsync(UpdateMatchScoreVM command);
    }

    public class MatchPlayerScoreService : IMatchPlayerScoreService
    {
        private readonly IMatchPlayerScoreRepository _matchPlayerScoreRepository;
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public MatchPlayerScoreService(IMatchPlayerScoreRepository matchPlayerScoreRepository, IApplicationDbContext context, IMapper mapper)
        {
            _matchPlayerScoreRepository = matchPlayerScoreRepository;
            _context = context;
            _mapper = mapper;
        }

        public async Task<bool> ActivateAsync(Guid id)
        {
            var objectToActivate = await _matchPlayerScoreRepository.GetById(id).SingleOrDefaultAsync();
            if (objectToActivate == null)
            {
                return false;
            }

            objectToActivate.IsActive = true;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task AddAsync(MatchPlayerScore entity)
        {
            _matchPlayerScoreRepository.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeactivateAsync(Guid id)
        {
            var objectToDeactivate = await _matchPlayerScoreRepository.GetById(id).SingleOrDefaultAsync();
            if (objectToDeactivate == null)
            {
                return false;
            }

            objectToDeactivate.IsActive = false;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<MatchPlayerScore>> GetAllAsync() => await _matchPlayerScoreRepository.GetAll().AsNoTracking().ToListAsync();

        public async Task<MatchPlayerScore> GetByIdAsync(Guid id) => await _matchPlayerScoreRepository.GetById(id).AsNoTracking().FirstOrDefaultAsync();

        public async Task RemoveAsync(Guid id)
        {
            _matchPlayerScoreRepository.Remove(id);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(UpdateMatchScoreVM command)
        {
            var score = await _matchPlayerScoreRepository.GetById(command.Id).SingleOrDefaultAsync();

            score.Value = command.Value;

            _matchPlayerScoreRepository.Update(score);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<SimpleMatchScoreDto>> GetScoresFromMatch(Guid matchId)
            => await _matchPlayerScoreRepository.GetAll()
                .AsNoTracking()
                .Include(x => x.Match)
                .Include(x => x.Player)
                .Where(x => x.Match.Id == matchId)
                .ProjectTo<SimpleMatchScoreDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
    }
}
