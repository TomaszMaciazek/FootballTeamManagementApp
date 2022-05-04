using App.DataAccess.Interfaces;
using App.Model.Entities;
using App.Model.ViewModels.Commands;
using AutoMapper;
using System.Linq;

namespace App.Mappings.Resolvers
{
    public class CoachCountryResolver : IValueResolver<CreateCoachVM, Coach, Country>
    {
        private readonly IApplicationDbContext _dbContext;

        public CoachCountryResolver(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Country Resolve(CreateCoachVM source, Coach destination, Country destMember, ResolutionContext context)
        {
            return _dbContext.Countries.FirstOrDefault(x => x.Id == source.CountryId);
        }
    }
}
