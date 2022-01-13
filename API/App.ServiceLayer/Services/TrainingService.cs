using App.DataAccess.Interfaces;
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

namespace App.ServiceLayer.Services
{
    public interface ITrainingService
    {
        Task<bool> ActivateAsync(Guid id);
        Task AddAsync(Training entity);
        Task<bool> DeactivateAsync(Guid id);
        Task<List<Training>> GetAllAsync();
        Task<Training> GetByIdAsync(Guid id);
        Task<PaginatedList<TrainingListItem>> GetTrainings(TrainingQuery query);
        Task RemoveAsync(Guid id);
        Task UpdateAsync(UpdateTrainingVM command);
    }

    public class TrainingService : ITrainingService
    {
        private readonly ITrainingRepository _trainingRepository;
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public TrainingService(ITrainingRepository trainingRepository, IApplicationDbContext context, IMapper mapper)
        {
            _trainingRepository = trainingRepository;
            _context = context;
            _mapper = mapper;
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
            .GetByIdEager(id).FirstOrDefaultAsync();

        public async Task RemoveAsync(Guid id)
        {
            _trainingRepository.Remove(id);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(UpdateTrainingVM command)
        {
            var entity = await _trainingRepository.GetById(command.Id).SingleOrDefaultAsync();
            entity.Title = !string.IsNullOrEmpty(command.Title) ? command.Title : entity.Title;
            entity.Description = !string.IsNullOrEmpty(command.Description) ? command.Title : entity.Description;
            entity.Date = command.Date ?? entity.Date;
            _trainingRepository.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<PaginatedList<TrainingListItem>> GetTrainings(TrainingQuery query)
        {
            var trainings = _trainingRepository.GetAll();

            trainings = query.IsActive.HasValue ? trainings.Where(x => x.IsActive == query.IsActive) : trainings;

            trainings = trainings.WhereStringPropertyContains(x => x.Title, query.Title);

            trainings = trainings.WhereDatetimePropertyGreaterOrEqual(x => x.Date, query.MinDate);

            trainings = trainings.WhereDatetimePropertyLessOrEqual(x => x.Date, query.MaxDate);

            if (!string.IsNullOrEmpty(query.OrderByColumnName) && query.OrderByColumnName.ToLower() == "date")
            {
                trainings = query.OrderByDirection == "asc" ? trainings.OrderBy(x => x.Date) : trainings.OrderByDescending(x => x.Date);
            }
            else
            {
                trainings = trainings.OrderByProperty(query.OrderByColumnName, query.OrderByDirection);
            }

            return await trainings.ProjectTo<TrainingListItem>(_mapper.ConfigurationProvider).PaginatedListAsync(query.PageNumber, query.PageSize);
        }
    }
}
