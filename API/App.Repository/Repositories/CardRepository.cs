using App.DataAccess.Interfaces;
using App.Model.Entities;
using App.Repository.Common.Implementation;
using App.Repository.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace App.Repository.Repositories
{
    public interface ICardRepository : IRepository<Card> {
        IQueryable<Card> GetAllEager();
        IEnumerable<Card> GetAllFromMatch(Guid matchId);
        IEnumerable<Card> GetAllFromUser(Guid playerId);
    }
    public class CardRepository : BaseRepository<Card>, ICardRepository
    {
        public CardRepository(IApplicationDbContext dbContext) : base(dbContext){}

        public IQueryable<Card> GetAllEager() => _dbSet
            .AsNoTracking()
            .Include(x => x.Player)
            .Include(x => x.Match);

        public IEnumerable<Card> GetAllFromMatch(Guid matchId) => _dbSet
                .AsNoTracking()
                .Include(x => x.Match)
                .Include(x => x.Player)
                .Where(x => x.Match.Id == matchId)
                .ToList();

        public IEnumerable<Card> GetAllFromUser(Guid playerId) => _dbSet
                .AsNoTracking()
                .Include(x => x.Match)
                .Include(x => x.Player)
                .Where(x => x.Player.Id == playerId)
                .ToList();
    }
}
