using App.DataAccess.Interfaces;
using App.Model.Entities;
using App.Model.ViewModels.Commands;
using AutoMapper;
using System.Linq;

namespace App.Mappings.Resolvers
{
    public class PlayerTeamResolver : IValueResolver<CreatePlayerVM, Player, Team>, IValueResolver<UpdatePlayerVM, Player, Team>
    {
        private readonly IApplicationDbContext _context;

        public PlayerTeamResolver(IApplicationDbContext context)
        {
            _context = context;
        }

        public Team Resolve(CreatePlayerVM source, Player destination, Team destMember, ResolutionContext context)
        {
            return source.TeamId.HasValue ? _context.Teams.FirstOrDefault(x => x.Id == source.TeamId.Value) : null;
        }

        public Team Resolve(UpdatePlayerVM source, Player destination, Team destMember, ResolutionContext context)
        {
            return source.TeamId.HasValue ? _context.Teams.FirstOrDefault(x => x.Id == source.TeamId.Value) : null;
        }
    }
}
