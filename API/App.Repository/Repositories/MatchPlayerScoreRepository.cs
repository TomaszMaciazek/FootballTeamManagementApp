using App.DataAccess.Interfaces;
using App.Model.Entities;
using App.Repository.Common.Implementation;
using App.Repository.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Repository.Repositories
{
    public interface IMatchPlayerScoreRepository : IRepository<MatchPlayerScore> { }
    public class MatchPlayerScoreRepository : BaseRepository<MatchPlayerScore>, IMatchPlayerScoreRepository
    {
        public MatchPlayerScoreRepository(IApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
