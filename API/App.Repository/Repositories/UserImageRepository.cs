using App.DataAccess.Interfaces;
using App.Model.Entities;
using App.Repository.Common.Implementation;
using App.Repository.Common.Interfaces;

namespace App.Repository.Repositories
{
    public interface IUserImageRepository : IRepository<UserImage> { }
    public class UserImageRepository : BaseRepository<UserImage>, IUserImageRepository
    {
        public UserImageRepository(IApplicationDbContext dbContext) : base(dbContext){}
    }


}
