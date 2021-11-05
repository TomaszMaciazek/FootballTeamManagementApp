using App.DataAccess.Interfaces;
using App.Model.Entities;
using App.Repository.Common.Implementation;
using App.Repository.Common.Interfaces;

namespace App.Repository.Repositories
{
    public interface INewsRepository : IRepository<News> { }
    public class NewsRepository : BaseRepository<News>, INewsRepository
    {
        public NewsRepository(IApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
