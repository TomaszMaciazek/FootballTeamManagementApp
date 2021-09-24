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
        IQueryable<GroupChat> GetByIdEager(Guid id);
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

        public IQueryable<GroupChat> GetByIdEager(Guid id) => _dbSet
            .AsNoTracking()
            .Include(x => x.Image)
            .Include(x => x.Messages).ThenInclude(x => x.Sender)
            .Include(x => x.Users)
            .Where(x => x.Id == id);

        public new void Remove(Guid id)
        {
            var chatToDelete = _dbSet
                .Include(x => x.Messages)
                .Include(x => x.Image).SingleOrDefault(entity => entity.Id == id);
            if (chatToDelete != null)
            {
                _dbContext.GroupMessages.RemoveRange(chatToDelete.Messages);
                _dbContext.GroupChatImages.Remove(chatToDelete.Image);
                _dbSet.Remove(chatToDelete);
            }
            else
            {
                throw new InvalidOperationException("There is no object with designated Id");
            }
        }
    }
}
