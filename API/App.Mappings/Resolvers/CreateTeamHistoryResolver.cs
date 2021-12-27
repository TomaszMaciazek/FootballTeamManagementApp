using App.DataAccess.Interfaces;
using App.Model.Entities;
using App.Model.ViewModels.Commands;
using AutoMapper;
using System;
using System.Collections.Generic;

namespace App.Mappings.Resolvers
{
    public class CreateTeamHistoryResolver : IValueResolver<CreateTeamVM, Team, TeamHistory>
    {
        private readonly IApplicationDbContext _dbContext;

        public CreateTeamHistoryResolver(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public TeamHistory Resolve(CreateTeamVM source, Team destination, TeamHistory destMember, ResolutionContext context)
        {
            if (source.MainCoachId.HasValue)
            {
                var coach = _dbContext.Coaches.Find(source.MainCoachId.Value);
                return new TeamHistory
                {
                    CoachAssignedToTeamEvents = new List<CoachAssignedToTeamEvent>()
                    {
                        new CoachAssignedToTeamEvent
                        {
                            Coach = coach,
                            Date = DateTime.Now
                        }
                    }
                };
            }
            return new TeamHistory{};
        }
    }
}
