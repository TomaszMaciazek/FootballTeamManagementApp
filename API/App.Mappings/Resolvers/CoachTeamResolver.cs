using App.DataAccess.Interfaces;
using App.Model.Entities;
using App.Model.ViewModels.Commands;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;

namespace App.Mappings.Resolvers
{
    public class CoachTeamResolver : IValueResolver<CreateCoachVM, Coach, ICollection<Team>>, IValueResolver<UpdateCoachVM, Coach, ICollection<Team>>
    {
        private readonly IApplicationDbContext _context;

        public CoachTeamResolver(IApplicationDbContext context)
        {
            _context = context;
        }

        public ICollection<Team> Resolve(CreateCoachVM source, Coach destination, ICollection<Team> destMember, ResolutionContext context)
        {
            return _context.Teams.Where(x => source.TeamsIds.Contains(x.Id)).ToList();
        }

        public ICollection<Team> Resolve(UpdateCoachVM source, Coach destination, ICollection<Team> destMember, ResolutionContext context)
        {
            return _context.Teams.Where(x => source.TeamsIds.Contains(x.Id)).ToList();
        }
    }
}
