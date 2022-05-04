using App.DataAccess.Interfaces;
using App.Model.Dtos;
using App.Model.Dtos.ListItemDtos;
using App.Model.Entities;
using App.Model.ViewModels.Commands;
using App.Repository.Repositories;
using App.ServiceLayer.Common;
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
using App.DataAccess.Exceptions;

namespace App.ServiceLayer.Services
{
    public interface ICoachService
    {
        Task<bool> ActivateAsync(Guid id);
        Task AddAsync(Coach entity);
        Task<bool> DeactivateAsync(Guid id);
        Task<IEnumerable<SimpleCoachDto>> GetAllAsync();
        Task<CoachDto> GetByIdAsync(Guid id);
        Task<Coach> GetByUserId(Guid userId);
        Task RemoveAsync(Guid id);
        Task UpdateAsync(UpdateCoachVM entity);
        Task<PaginatedList<CoachListItemDto>> GetCoaches(CoachQuery query);
        Task<IEnumerable<SimpleCoachDto>> GetWorkingCoaches(DateTime? date);
        Task ToggleWorking(Guid id);
    }

    public class CoachService : ICoachService
    {

        private readonly ICoachRepository _coachRepository;
        private readonly ICountryRepository _countryRepository;
        private readonly ITeamRepository _teamRepository;
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CoachService(ICoachRepository coachRepository, ICountryRepository countryRepository, ITeamRepository teamRepository, IApplicationDbContext context, IMapper mapper)
        {
            _coachRepository = coachRepository;
            _countryRepository = countryRepository;
            _teamRepository = teamRepository;
            _context = context;
            _mapper = mapper;
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

        public async Task<IEnumerable<SimpleCoachDto>> GetAllAsync() => await _coachRepository.GetAll().AsNoTracking()
            .ProjectTo<SimpleCoachDto>(_mapper.ConfigurationProvider)
            .ToListAsync();

        public async Task<CoachDto> GetByIdAsync(Guid id) => await _coachRepository.GetAll()
            .AsNoTracking()
            .Include(x => x.User)
            .Include(x => x.Country)
            .Include(x => x.Teams)
            .Where(x => x.Id == id)
            .ProjectTo<CoachDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync();

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

        public async Task<PaginatedList<CoachListItemDto>> GetCoaches(CoachQuery query) {
            var coaches = _coachRepository.GetAll()
                .Include(x => x.User)
                .Include(x => x.Country)
                .Include(x => x.Teams)
                .AsNoTracking();

            if (!string.IsNullOrEmpty(query.QueryString))
            {
                coaches = coaches.Where(x =>
                x.User.Name.ToLower().Contains(query.QueryString.ToLower()) ||
                x.User.MiddleName.ToLower().Contains(query.QueryString.ToLower()) ||
                x.User.Surname.ToLower().Contains(query.QueryString.ToLower()) ||
                $"{x.User.Surname} {x.User.Name} {x.User.MiddleName}".ToLower().Contains(query.QueryString.ToLower()) ||
                $"{x.User.Surname} {x.User.MiddleName} {x.User.Name}".ToLower().Contains(query.QueryString.ToLower()) ||
                $"{x.User.Name} {x.User.MiddleName} {x.User.Surname}".ToLower().Contains(query.QueryString.ToLower()) ||
                $"{x.User.Name} {x.User.Surname}".ToLower().Contains(query.QueryString.ToLower()) ||
                $"{x.User.Name} {x.User.Surname} {x.User.MiddleName}".ToLower().Contains(query.QueryString.ToLower())
                );
            }

            if(query.TeamsIds != null && query.TeamsIds.Any())
            {
                coaches = coaches.Where(x => x.Teams.Any(y => query.TeamsIds.Any(x => x == y.Id)));
            }

            if (query.CountriesIds != null && query.CountriesIds.Any())
            {
                coaches = coaches.Where(x => query.CountriesIds.Any(y => y == x.Country.Id));
            }

            if (query.IsStillWorking.HasValue)
            {
                coaches = query.IsStillWorking.Value == true
                    ? coaches.Where(x => !x.FinishedWorking.HasValue)
                    : coaches.Where(x => x.FinishedWorking.HasValue);
            }

            coaches = query.Gender.HasValue ? coaches.Where(x => x.Gender == query.Gender.Value) : coaches;

            if (query.OrderByColumnName.ToLower() == "coachname")
            {

                coaches = query.OrderByDirection == "asc"
                    ? coaches.OrderBy(x => x.User.Surname).ThenBy(x => x.User.Name).ThenBy(x => x.User.MiddleName)
                    : coaches.OrderByDescending(x => x.User.Surname).ThenByDescending(x => x.User.Name).ThenByDescending(x => x.User.MiddleName);
            }
            else
            {
                coaches = coaches.OrderByProperty(query.OrderByColumnName, query.OrderByDirection);
            }

            return await coaches
                .ProjectTo<CoachListItemDto>(_mapper.ConfigurationProvider)
                .PaginatedListAsync(query.PageNumber, query.PageSize);
        }

        public async Task<IEnumerable<SimpleCoachDto>> GetWorkingCoaches(DateTime? date = null)
        {
            var coaches = _coachRepository.GetAll().Include(x => x.User).AsNoTracking();
                
                coaches = date.HasValue 
                    ? coaches.Where(x => !x.FinishedWorking.HasValue || (x.StartedWorking.HasValue && x.StartedWorking.Value.Date <= date.Value.Date && x.FinishedWorking.HasValue && x.FinishedWorking.Value.Date >= date.Value.Date))
                    : coaches.Where(x => !x.FinishedWorking.HasValue);

            return await coaches.ProjectTo<SimpleCoachDto>(_mapper.ConfigurationProvider).ToListAsync();
        }

        public async Task ToggleWorking(Guid id)
        {
            var coach = await _coachRepository.GetById(id).Include(x => x.Teams).SingleOrDefaultAsync();

            if (coach != null)
            {
                if (coach.FinishedWorking.HasValue)
                {
                    coach.FinishedWorking = null;
                }
                else
                {
                    coach.FinishedWorking =  DateTime.Now;
                    foreach(var team in coach.Teams)
                    {
                        team.MainCoach = null;
                    }
                }
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new NotFoundException();
            }
        }
    }
}
