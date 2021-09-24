using App.DataAccess.Interfaces;
using App.Model.Entities;
using App.Repository.Common.Implementation;
using App.Repository.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace App.Repository.Repositories
{
    public interface IIndividualChatRepository : IRepository<IndividualChat>
    {
        IQueryable<IndividualChat> GetAllEager();
        IQueryable<IndividualChat> GetByIdEager(Guid id);
    }
    public class IndividualChatRepository : BaseRepository<IndividualChat>, IIndividualChatRepository
    {
        public IndividualChatRepository(IApplicationDbContext dbContext) : base(dbContext){}

        public IQueryable<IndividualChat> GetAllEager() => _dbSet
            .AsNoTracking()
            .Include(x => x.Messages).ThenInclude(x => x.Sender)
            .Include(x => x.Users);

        public IQueryable<IndividualChat> GetByIdEager(Guid id) => _dbSet
            .AsNoTracking()
            .Include(x => x.Messages).ThenInclude(x => x.Sender)
            .Include(x => x.Users)
            .Where(x => x.Id == id);
    }
}
