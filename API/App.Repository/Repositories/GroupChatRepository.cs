using App.DataAccess.Interfaces;
using App.Model.Entities;
using App.Repository.Common.Implementation;
using App.Repository.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace App.Repository.Repositories
{
    public interface IGroupChatRepository : IRepository<GroupChat> {
        IQueryable<GroupChat> GetAllEager();
        GroupChat GetByIdEager(Guid id);
    }
    public class GroupChatRepository : BaseRepository<GroupChat>, IGroupChatRepository
    {
        public GroupChatRepository(IApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public IQueryable<GroupChat> GetAllEager() => _dbSet
            .AsNoTracking()
            .Include(x => x.Image)
            .Include(x => x.Messages).ThenInclude(x => x.Sender)
            .Include(x => x.Users);

        public GroupChat GetByIdEager(Guid id) => _dbSet
            .AsNoTracking()
            .Include(x => x.Image)
            .Include(x => x.Messages).ThenInclude(x => x.Sender)
            .Include(x => x.Users)
            .FirstOrDefault(x => x.Id == id);
    }
}
