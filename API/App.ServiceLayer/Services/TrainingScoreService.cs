﻿using App.DataAccess.Interfaces;
using App.Model.Dtos;
using App.Model.Dtos.ListItemDtos;
using App.Model.Entities;
using App.Model.ViewModels.Commands;
using App.Repository.Repositories;
using App.ServiceLayer.Extenstions;
using App.ServiceLayer.Models;
using App.Model.ViewModels.Queries;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.ServiceLayer.Services
{
    public interface ITrainingScoreService
    {
        Task<bool> ActivateAsync(Guid id);
        Task AddAsync(TrainingScore entity);
        Task<bool> DeactivateAsync(Guid id);
        Task<List<TrainingScore>> GetAllAsync();
        Task<TrainingScore> GetByIdAsync(Guid id);
        Task RemoveAsync(Guid id);
        Task UpdateAsync(UpdateTrainingScoreVM command);
        Task<PaginatedList<TrainingScoreListItemDto>> GetScores(TrainingScoreQuery query);
    }

    public class TrainingScoreService : ITrainingScoreService
    {
        private readonly ITrainingScoreRepository _trainingScoreRepository;
        private readonly IApplicationDbContext _context;

        public TrainingScoreService(ITrainingScoreRepository trainingScoreRepository, IApplicationDbContext context)
        {
            _trainingScoreRepository = trainingScoreRepository;
            _context = context;
        }

        public async Task<bool> ActivateAsync(Guid id)
        {
            var objectToActivate = await _trainingScoreRepository.GetById(id).SingleOrDefaultAsync();
            if (objectToActivate == null)
            {
                return false;
            }

            objectToActivate.IsActive = true;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task AddAsync(TrainingScore entity)
        {
            _trainingScoreRepository.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeactivateAsync(Guid id)
        {
            var objectToDeactivate = await _trainingScoreRepository.GetById(id).SingleOrDefaultAsync();
            if (objectToDeactivate == null)
            {
                return false;
            }

            objectToDeactivate.IsActive = false;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<TrainingScore>> GetAllAsync() => await _trainingScoreRepository.GetAll().ToListAsync();

        public async Task<TrainingScore> GetByIdAsync(Guid id) => await _trainingScoreRepository.GetById(id).FirstOrDefaultAsync();

        public async Task RemoveAsync(Guid id)
        {
            _trainingScoreRepository.Remove(id);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(UpdateTrainingScoreVM command)
        {
            var score = await _trainingScoreRepository.GetById(command.Id).SingleOrDefaultAsync();
            score.Value = command.Value;
            _trainingScoreRepository.Update(score);
            await _context.SaveChangesAsync();
        }

        public async Task<PaginatedList<TrainingScoreListItemDto>> GetScores(TrainingScoreQuery query)
        {
            var scores = _trainingScoreRepository.GetAll()
                .Include(x => x.Training)
                .Include(x => x.Player).ThenInclude(x => x.User)
                .AsNoTracking()
                .Where(x => x.Training.Id == query.TrainingId);

            if(query.ScoreTypes != null && query.ScoreTypes.Any())
            {
                scores.Where(x => query.ScoreTypes.Contains(x.ScoreType));
            }

            scores = scores.OrderByProperty(query.OrderByColumnName, query.OrderByDirection);

            return await scores.Select(x => new TrainingScoreListItemDto { 
                Id = x.Id,
                LastModyfication = x.UpdatedDate,
                LastModifier = string.IsNullOrEmpty(x.Modifier.Surname) ? x.Modifier.Username : x.Modifier.Surname,
                PlayerName = $"{x.Player.User.Surname} {x.Player.User.Name} {x.Player.User.MiddleName}"
            }).PaginatedListAsync(query.PageNumber, query.PageSize);
        }
    }
}
