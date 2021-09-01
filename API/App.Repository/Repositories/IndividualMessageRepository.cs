using App.DataAccess.Interfaces;
using App.Model.Entities;
using App.Repository.Common.Implementation;
using App.Repository.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace App.Repository.Repositories
{
    public interface IIndividualMessageRepository : IRepository<IndividualMessage>
    {
        IQueryable<IndividualMessage> GetAllEager();
    }

    public class IndividualMessageRepository : BaseRepository<IndividualMessage>, IIndividualMessageRepository
    {
        public IndividualMessageRepository(IApplicationDbContext dbContext) : base(dbContext) {}

        public IQueryable<IndividualMessage> GetAllEager() => _dbSet
            .AsNoTracking()
            .Include(x => x.Chat)
            .Include(x => x.Sender);
    }
}
