using App.DataAccess.Interfaces;
using App.Model.Entities;
using App.Repository.Common.Implementation;
using App.Repository.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace App.Repository.Repositories
{
    public interface IGroupChatImageRepository : IRepository<GroupChatImage> {
        GroupChatImage GetByChatId(Guid chatId);
    }
    public class GroupChatImageRepository : BaseRepository<GroupChatImage>, IGroupChatImageRepository
    {
        public GroupChatImageRepository(IApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public GroupChatImage GetByChatId(Guid chatId) => _dbSet
            .AsNoTracking()
            .FirstOrDefault(x => x.ChatId == chatId);
    }
}
