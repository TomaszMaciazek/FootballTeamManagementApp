using App.DataAccess.Exceptions;
using App.DataAccess.Interfaces;
using App.Model.Entities;
using App.Repository.Common.Implementation;
using App.Repository.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace App.Repository.Repositories
{
    public interface ITeamRepository : IRepository<Team>
    {
        IQueryable<Team> GetByIdEager(Guid id);
    }
    public class TeamRepository : BaseRepository<Team>, ITeamRepository
    {
        public TeamRepository(IApplicationDbContext dbContext) : base(dbContext){}

        public IQueryable<Team> GetByIdEager(Guid id) => _dbSet
            .AsNoTracking()
            .Include(x => x.MainCoach).ThenInclude(x => x.User)
            .Include(x => x.Players).ThenInclude(x => x.User)
            .Include(x => x.Players).ThenInclude(x => x.Country)
            .Where(x => x.Id == id);

        public new void Update(Team entity)
        {
            var oldTeam = _dbContext.Teams.AsNoTracking().Include(x => x.MainCoach).SingleOrDefault(x => x.Id == entity.Id);
            if(oldTeam.MainCoach == null || oldTeam.MainCoach.Id != entity.MainCoach.Id)
            {
                entity.History.CoachAssignedToTeamEvents.Add(new CoachAssignedToTeamEvent
                {
                    Date = DateTime.Now,
                    Coach = entity.MainCoach,
                    TeamHistory = entity.History
                });
            }

            base.Update(entity);
        }

        public new void Remove(Guid id)
        {
            var team = _dbContext.Teams
                .Include(x => x.History)
                .ThenInclude(x => x.PlayerJoinedTeamEvents)
                .Include(x => x.Players)
                .SingleOrDefault(match => match.Id == id);
            if (team != null)
            {
                if (team.History.PlayerJoinedTeamEvents.Any() || team.Players.Any())
                {
                    throw new HistoryIsNotEmptyException();
                }
                _dbContext.TeamHistories.Remove(team.History);
                _dbSet.Remove(team);
            }
            else
            {
                throw new InvalidOperationException("There is no object with designated Id");
            }
        }
    }
}
