using App.DataAccess.Interfaces;
using App.Model.Entities;
using App.Repository.Common.Implementation;
using App.Repository.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace App.Repository.Repositories
{
    public interface ITrainingRepository : IRepository<Training>
    {
        IQueryable<Training> GetAllEager();
        Training GetByIdEager(Guid id);
    }

    public class TrainingRepository : BaseRepository<Training>, ITrainingRepository
    {
        public TrainingRepository(IApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public IQueryable<Training> GetAllEager() => _dbSet
            .AsNoTracking()
            .Include(x => x.Coaches)
            .Include(x => x.Players)
            .Include(x => x.Scores).ThenInclude(x => x.Player);

        public Training GetByIdEager(Guid id) => _dbSet
            .AsNoTracking()
            .Include(x => x.Coaches)
            .Include(x => x.Players)
            .Include(x => x.Scores).ThenInclude(x => x.Player)
            .FirstOrDefault(x => x.Id == id);
    }
}
