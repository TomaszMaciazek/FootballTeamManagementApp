using App.DataAccess.Interfaces;
using App.Model.Entities;
using App.Repository.Common.Implementation;
using App.Repository.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace App.Repository.Repositories
{
    public interface ITeamRepository : IRepository<Team>
    {
        IQueryable<Team> GetAllEager();
        IQueryable<Team> GetAllFromCoach(Guid coachId);
    }
    public class TeamRepository : BaseRepository<Team>, ITeamRepository
    {
        public TeamRepository(IApplicationDbContext dbContext) : base(dbContext){}

        public IQueryable<Team> GetAllEager() => _dbSet
            .AsNoTracking()
            .Include(x => x.MainCoach)
            .Include(x => x.Players);

        public IQueryable<Team> GetAllFromCoach(Guid coachId) => _dbSet
                .AsNoTracking()
                .Include(x => x.MainCoach)
                .Where(x => x.MainCoach.Id == coachId && x.IsActive);
    }
}
