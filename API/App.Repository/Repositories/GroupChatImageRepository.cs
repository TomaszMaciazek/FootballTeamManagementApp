using App.DataAccess.Interfaces;
using App.Model.Entities;
using App.Repository.Common.Implementation;
using App.Repository.Common.Interfaces;

namespace App.Repository.Repositories
{
    public interface IGroupChatImageRepository : IRepository<GroupChatImage> {
    }
    public class GroupChatImageRepository : BaseRepository<GroupChatImage>, IGroupChatImageRepository
    {
        public GroupChatImageRepository(IApplicationDbContext dbContext) : base(dbContext)
        {
        }

    }
}
