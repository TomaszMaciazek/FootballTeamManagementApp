using App.DataAccess.Interfaces;
using App.Model.Entities;
using App.Repository.Common.Implementation;
using App.Repository.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace App.Repository.Repositories
{
    public interface ICoachRepository : IRepository<Coach>
    {
        IQueryable<Coach> GetAllEager();
        IQueryable<Coach> GetByIdEager(Guid id);
        IQueryable<Coach> GetByUserIdEager(Guid userId);
    }

    public class CoachRepository : BaseRepository<Coach>, ICoachRepository
    {
        public CoachRepository(IApplicationDbContext dbContext) : base(dbContext){}

        public IQueryable<Coach> GetAllEager() => _dbSet
            .AsNoTracking()
            .Include(x => x.Matches)
            .Include(x => x.Teams)
            .Include(x => x.User)
            .Include(x => x.Trainings);

        public IQueryable<Coach> GetByIdEager(Guid id) => _dbSet
            .AsNoTracking()
            .Include(x => x.Matches)
            .Include(x => x.Teams)
            .Include(x => x.User)
            .Include(x => x.Trainings)
            .Where(x => x.Id == id);

        public IQueryable<Coach> GetByUserIdEager(Guid userId) => _dbSet
            .AsNoTracking()
            .Include(x => x.Matches)
            .Include(x => x.Teams)
            .Include(x => x.User)
            .Include(x => x.Trainings)
            .Where(x => x.UserId == userId);
    }
}
