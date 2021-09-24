using App.DataAccess.Interfaces;
using App.Model.Entities;
using App.Repository.Common.Implementation;
using App.Repository.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Repository.Repositories
{
    public interface ICoachCardRepository : IRepository<CoachCard>
    {
        IQueryable<CoachCard> GetAllEager();
    }
    public class CoachCardRepository : BaseRepository<CoachCard>, ICoachCardRepository
    {
        public CoachCardRepository(IApplicationDbContext dbContext) : base(dbContext) { }

        public IQueryable<CoachCard> GetAllEager() => _dbSet
            .AsNoTracking()
            .Include(x => x.Coach)
            .Include(x => x.Match);
    }
}
