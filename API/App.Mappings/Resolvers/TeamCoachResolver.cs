using App.DataAccess.Interfaces;
using App.Model.Entities;
using App.Model.ViewModels.Commands;
using AutoMapper;

namespace App.Mappings.Resolvers
{
    public class TeamCoachResolver : IValueResolver<CreateTeamVM, Team, Coach>
    {
        private readonly IApplicationDbContext _dbContext;

        public TeamCoachResolver(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Coach Resolve(CreateTeamVM source, Team destination, Coach destMember, ResolutionContext context)
        {
            if (source.MainCoachId.HasValue)
            {
                return _dbContext.Coaches.Find(source.MainCoachId.Value);
            }
            return null;
        }
    }
}
