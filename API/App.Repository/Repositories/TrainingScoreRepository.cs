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
    public interface ITrainingScoreRepository : IRepository<TrainingScore>
    {
        IQueryable<TrainingScore> GetAllEager();
        IEnumerable<TrainingScore> GetAllFromPlayer(Guid playerId);
        IEnumerable<TrainingScore> GetAllFromTraining(Guid trainingId);
    }
    public class TrainingScoreRepository : BaseRepository<TrainingScore>, ITrainingScoreRepository
    {
        public TrainingScoreRepository(IApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public IQueryable<TrainingScore> GetAllEager() => _dbSet
                .AsNoTracking()
                .Include(x => x.Player)
                .Include(x => x.Training);

        public IEnumerable<TrainingScore> GetAllFromPlayer(Guid playerId) => _dbSet
                .AsNoTracking()
                .Include(x => x.Player)
                .Include(x => x.Training)
                .Where(x => x.Player.Id == playerId)
                .ToList();

        public IEnumerable<TrainingScore> GetAllFromTraining(Guid trainingId) =>_dbSet
                .AsNoTracking()
                .Include(x => x.Player)
                .Include(x => x.Training)
                .Where(x => x.Training.Id == trainingId)
                .ToList();
    }
}
