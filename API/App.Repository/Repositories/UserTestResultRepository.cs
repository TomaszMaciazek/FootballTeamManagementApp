using App.DataAccess.Interfaces;
using App.Model.Entities.TestEntities;
using App.Repository.Common.Implementation;
using App.Repository.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace App.Repository.Repositories
{
    public interface IUserTestResultRepository : IRepository<UserTestResult>
    {
    }

    public class UserTestResultRepository : BaseRepository<UserTestResult>, IUserTestResultRepository
    {
        public UserTestResultRepository(IApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
