using App.DataAccess.Interfaces;
using App.Model.Entities;
using App.Repository.Common.Implementation;
using App.Repository.Common.Interfaces;

namespace App.Repository.Repositories
{
    public interface IMessageRepository : IRepository<Message> { }
    public class MessageRepository : BaseRepository<Message>, IMessageRepository
    {
        public MessageRepository(IApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
