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
    public interface ICoachService
    {
        Task<bool> ActivateAsync(Guid id);
        Task AddAsync(Coach entity);
        Task<bool> DeactivateAsync(Guid id);
        Task<List<Coach>> GetAllAsync();
        Task<Coach> GetByIdAsync(Guid id);
        Task<Coach> GetByIdEager(Guid id);
        Task<Coach> GetByUserId(Guid userId);
        Task RemoveAsync(Guid id);
        Task UpdateAsync(UpdateCoachVM entity);
        Task<PaginatedList<Coach>> GetCoaches(CoachQuery query);
    }

    public class CoachService : ICoachService
    {

        private readonly ICoachRepository _coachRepository;
        private readonly ICountryRepository _countryRepository;
        private readonly ITeamRepository _teamRepository;
        private readonly IApplicationDbContext _context;

        public CoachService(
            ICoachRepository coachRepository, 
            ICountryRepository countryRepository, 
            ITeamRepository teamRepository, 
            IApplicationDbContext context
            )
        {
            _coachRepository = coachRepository;
            _countryRepository = countryRepository;
            _teamRepository = teamRepository;
            _context = context;
        }

        public async Task<bool> ActivateAsync(Guid id)
        {
            var objectToActivate = await _coachRepository.GetById(id).SingleOrDefaultAsync();
            if (objectToActivate == null)
            {
                return false;
            }

            objectToActivate.IsActive = true;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task AddAsync(Coach entity)
        {
            _coachRepository.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeactivateAsync(Guid id)
        {
            var objectToDeactivate = await _coachRepository.GetById(id).SingleOrDefaultAsync();
            if (objectToDeactivate == null)
            {
                return false;
            }

            objectToDeactivate.IsActive = false;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Coach>> GetAllAsync() => await _coachRepository.GetAll().ToListAsync();

        public async Task<Coach> GetByIdAsync(Guid id) => await _coachRepository.GetById(id).FirstOrDefaultAsync();

        public async Task<Coach> GetByIdEager(Guid id) => await _coachRepository.GetByIdEager(id).FirstOrDefaultAsync();

        public async Task RemoveAsync(Guid id)
        {
            _coachRepository.Remove(id);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(UpdateCoachVM command)
        {
            var coach = await _coachRepository.GetAll()
                .Include(x => x.User)
                .Include(x => x.Country)
                .Include(x => x.Teams)
                .SingleOrDefaultAsync(x => x.Id == command.Id);
            coach.BirthDate = command.BirthDate ?? coach.BirthDate;
            coach.User.Email = !string.IsNullOrEmpty(command.Email) ? command.Email : coach.User.Email;
            coach.User.Username = !string.IsNullOrEmpty(command.Email) ? command.Email.ToLower() : coach.User.Username;
            coach.User.Name = !string.IsNullOrEmpty(command.Name) ? command.Name : coach.User.Name;
            coach.User.MiddleName = !string.IsNullOrEmpty(command.MiddleName) ? command.Name : coach.User.MiddleName;
            coach.User.Surname = !string.IsNullOrEmpty(command.Surname) ? command.Name : coach.User.Surname;
            coach.Country = command.CountryId.HasValue ? _countryRepository.GetById(command.CountryId.Value).FirstOrDefault() : coach.Country;
            coach.StartedWorking = command.StartedWorking ?? coach.StartedWorking;
            coach.FinishedWorking = command.FinishedWorking ?? coach.FinishedWorking;
            if(command.TeamsIds != null){
                coach.Teams.Clear();
                coach.Teams = _teamRepository.GetAll().Where(x => command.TeamsIds.Contains(x.Id)).ToList();
            }
            _coachRepository.Update(coach);
            await _context.SaveChangesAsync();
        }

        public async Task<Coach> GetByUserId(Guid userId) => await _coachRepository.GetByUserIdEager(userId).FirstOrDefaultAsync();

        public async Task<PaginatedList<Coach>> GetCoaches(CoachQuery query) {
            var coaches = _coachRepository.GetAll()
                .Include(x => x.User)
                .Include(x => x.Country)
                .Where(x => x.IsActive);

            coaches = coaches.WhereStringPropertyContains(x => x.User.Email, query.Email);

            coaches = coaches.WhereStringPropertyContains(x => x.User.Name, query.Firstname);

            coaches = coaches.WhereStringPropertyContains(x => x.User.Surname, query.Surname);

            coaches = coaches.WhereIntPropertyEquals(x => x.Country.Id, query.CountryId);

            coaches = query.Gender.HasValue ? coaches.Where(x => x.Gender == query.Gender.Value) : coaches;

            coaches = coaches.OrderByProperty(query.OrderByColumnName, query.OrderByDirection);

            return await coaches.PaginatedListAsync(query.PageNumber, query.PageSize);
        }
    }
}
