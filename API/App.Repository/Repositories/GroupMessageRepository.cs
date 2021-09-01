using App.DataAccess.Interfaces;
using App.Model.Entities;
using App.Repository.Common.Implementation;
using App.Repository.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace App.Repository.Repositories
{
    public interface IGroupMessageRepository : IRepository<GroupMessage>
    {
        IQueryable<GroupMessage> GetAllEager();
    }
    public class GroupMessageRepository : BaseRepository<GroupMessage>, IGroupMessageRepository
    {
        public GroupMessageRepository(IApplicationDbContext dbContext) : base(dbContext){}

        public IQueryable<GroupMessage> GetAllEager() => _dbSet
            .AsNoTracking()
            .Include(x => x.Chat)
            .Include(x => x.Sender);
    }
}
