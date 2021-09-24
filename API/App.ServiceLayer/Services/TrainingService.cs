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
    public interface ITrainingService
    {
        Task<bool> ActivateAsync(Guid id);
        Task AddAsync(Training entity);
        Task<bool> DeactivateAsync(Guid id);
        Task<List<Training>> GetAllAsync();
        Task<Training> GetByIdAsync(Guid id);
        Task<PaginatedList<Training>> GetTrainings(TrainingQuery query);
        Task RemoveAsync(Guid id);
        Task UpdateAsync(Training entity);
    }

    public class TrainingService : IService<Training>, ITrainingService
    {
        private readonly ITrainingRepository _trainingRepository;
        private readonly IApplicationDbContext _context;

        public TrainingService(ITrainingRepository trainingRepository, IApplicationDbContext context)
        {
            _trainingRepository = trainingRepository;
            _context = context;
        }

        public async Task<bool> ActivateAsync(Guid id)
        {
            var objectToActivate = await _trainingRepository.GetById(id).SingleOrDefaultAsync();
            if (objectToActivate == null)
            {
                return false;
            }

            objectToActivate.IsActive = true;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task AddAsync(Training entity)
        {
            _trainingRepository.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeactivateAsync(Guid id)
        {
            var objectToDeactivate = await _trainingRepository.GetById(id).SingleOrDefaultAsync();
            if (objectToDeactivate == null)
            {
                return false;
            }

            objectToDeactivate.IsActive = false;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Training>> GetAllAsync() => await _trainingRepository.GetAll().ToListAsync();

        public async Task<Training> GetByIdAsync(Guid id) => await _trainingRepository
            .GetAllEager().FirstOrDefaultAsync(x => x.Id == id);

        public async Task RemoveAsync(Guid id)
        {
            _trainingRepository.Remove(id);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Training entity)
        {
            _trainingRepository.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<PaginatedList<Training>> GetTrainings(TrainingQuery query)
        {
            var trainings = _trainingRepository.GetAllEager().Where(x => x.IsActive);

            trainings = trainings.WhereStringPropertyContains(x => x.Localization, query.Location);

            trainings = trainings.WhereStringPropertyContains(x => x.Title, query.Title);

            trainings = trainings.WhereDatetimePropertyGreaterOrEqual(x => x.Date, query.MinDate);

            trainings = trainings.WhereDatetimePropertyLessOrEqual(x => x.Date, query.MaxDate);

            trainings = trainings.OrderByProperty(query.OrderByColumnName, query.OrderByDirection);

            return await trainings.PaginatedListAsync(query.PageNumber, query.PageSize);
        }
    }
}
