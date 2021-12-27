using App.DataAccess.Interfaces;
using App.Model.Dtos;
using App.Model.Dtos.ListItemDtos;
using App.Model.Entities;
using App.Model.ViewModels.Commands;
using App.Model.ViewModels.Queries;
using App.Repository.Repositories;
using App.ServiceLayer.Extenstions;
using App.ServiceLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace App.ServiceLayer.Services
{
    public interface ITeamService
    {
        Task<bool> ActivateAsync(Guid id);
        Task AddAsync(Team entity);
        Task<bool> DeactivateAsync(Guid id);
        Task<List<Team>> GetAllAsync();
        Task<Team> GetByIdAsync(Guid id);
        Task<PaginatedList<Team>> GetTeams(TeamQuery query);
        Task RemoveAsync(Guid id);
        Task UpdateAsync(Team entity);
    }

    public class TeamService : IService<Team>, ITeamService
    {
        private readonly ITeamRepository _teamRepository;
        private readonly IApplicationDbContext _context;

        public TeamService(ITeamRepository teamRepository, IApplicationDbContext context)
        {
            _teamRepository = teamRepository;
            _context = context;
        }

        public async Task<bool> ActivateAsync(Guid id)
        {
            var objectToActivate = await _teamRepository.GetById(id).SingleOrDefaultAsync();
            if (objectToActivate == null)
            {
                return false;
            }

            objectToActivate.IsActive = true;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task AddAsync(Team entity)
        {
            _teamRepository.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeactivateAsync(Guid id)
        {
            var objectToDeactivate = await _teamRepository.GetById(id).SingleOrDefaultAsync();
            if (objectToDeactivate == null)
            {
                return false;
            }

            objectToDeactivate.IsActive = false;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Team>> GetAllAsync() => await _teamRepository.GetAll().ToListAsync();

        public async Task<Team> GetByIdAsync(Guid id) => await _teamRepository.GetById(id).FirstOrDefaultAsync();

        public async Task RemoveAsync(Guid id)
        {
            _teamRepository.Remove(id);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Team entity)
        {
            _teamRepository.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<PaginatedList<Team>> GetTeams(TeamQuery query)
        {

            var teams = query.CoachId.HasValue
                ? _teamRepository.GetAllFromCoach(query.CoachId.Value)
                : _teamRepository.GetAll().Include(x => x.MainCoach);

            teams = teams.WhereStringPropertyContains(x => x.Name, query.Name);

            teams = teams.OrderByProperty(query.OrderByColumnName, query.OrderByDirection);

            return await teams.PaginatedListAsync(query.PageNumber, query.PageSize);
        }


    }
}
