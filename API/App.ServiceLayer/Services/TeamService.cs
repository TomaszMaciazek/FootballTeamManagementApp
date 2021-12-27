using App.DataAccess.Interfaces;
using App.Model.Dtos;
using App.Model.Dtos.ListItemDtos;
using App.Model.Entities;
using App.Model.ViewModels.Commands;
using App.Model.ViewModels.Queries;
using App.Repository.Repositories;
using App.ServiceLayer.Extenstions;
using App.ServiceLayer.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
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
        Task<IEnumerable<SimpleTeamDto>> GetAllAsync();
        Task<Team> GetByIdAsync(Guid id);
        Task<PaginatedList<TeamListItemDto>> GetTeams(TeamQuery query);
        Task RemoveAsync(Guid id);
        Task UpdateAsync(UpdateTeamVM entity);
    }

    public class TeamService : ITeamService
    {
        private readonly ITeamRepository _teamRepository;
        private readonly ICoachRepository _coachRepository;
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public TeamService(ITeamRepository teamRepository, ICoachRepository coachRepository, IApplicationDbContext context, IMapper mapper)
        {
            _teamRepository = teamRepository;
            _coachRepository = coachRepository;
            _context = context;
            _mapper = mapper;
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

        public async Task<IEnumerable<SimpleTeamDto>> GetAllAsync() => await _teamRepository
            .GetAll()
            .AsNoTracking()
            .ProjectTo<SimpleTeamDto>(_mapper.ConfigurationProvider)
            .ToListAsync();

        public async Task<Team> GetByIdAsync(Guid id) => await _teamRepository.GetByIdEager(id).FirstOrDefaultAsync();

        public async Task RemoveAsync(Guid id)
        {
            _teamRepository.Remove(id);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(UpdateTeamVM command)
        {
            var entity = await _teamRepository.GetById(command.Id)
                .Include(x => x.History).ThenInclude(x => x.CoachAssignedToTeamEvents)
                .Include(x => x.MainCoach).SingleOrDefaultAsync();

            entity.Name = !string.IsNullOrEmpty(command.Name) ? command.Name : entity.Name;

            if (command.MainCoachId.HasValue)
            {
                entity.MainCoach = await _coachRepository.GetById(command.MainCoachId.Value).SingleOrDefaultAsync();
            }

            _teamRepository.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<PaginatedList<TeamListItemDto>> GetTeams(TeamQuery query)
        {

            var teams = _teamRepository.GetAll()
                .Include(x => x.Players)
                .Include(x => x.MainCoach).ThenInclude(x => x.User)
                .Include(x => x.History).ThenInclude(x => x.PlayerJoinedTeamEvents)
                .AsNoTracking();

            teams = teams.WhereStringPropertyContains(x => x.Name, query.Name);
            teams = teams.WhereGuidPropertyEquals(x => x.MainCoach.Id, query.CoachId);

            teams = teams.OrderByProperty(query.OrderByColumnName, query.OrderByDirection);

            return await teams
                .ProjectTo<TeamListItemDto>(_mapper.ConfigurationProvider)
                .PaginatedListAsync(query.PageNumber, query.PageSize);
        }


    }
}
