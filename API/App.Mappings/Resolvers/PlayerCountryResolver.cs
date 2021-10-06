using App.DataAccess.Interfaces;
using App.Model.Entities;
using App.Model.ViewModels.Commands;
using AutoMapper;
using System.Linq;

namespace App.Mappings.Resolvers
{
    public class PlayerCountryResolver : IValueResolver<CreatePlayerVM, Player, Country>
    {
        private readonly IApplicationDbContext _dbContext;

        public PlayerCountryResolver(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Country Resolve(CreatePlayerVM source, Player destination, Country destMember, ResolutionContext context)
        {
            return _dbContext.Countries.FirstOrDefault(x => x.Id == source.CountryId);
        }
    }
}
