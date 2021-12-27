using App.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Infrastructure.Configurations
{
    public class TeamHistoryConfiguration : IEntityTypeConfiguration<TeamHistory>
    {
        public void Configure(EntityTypeBuilder<TeamHistory> builder)
        {
            builder.HasOne(x => x.Team)
                .WithOne(x => x.History)
                .HasForeignKey<TeamHistory>(x => x.TeamId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.PlayerJoinedTeamEvents)
                .WithOne(x => x.TeamHistory)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.CoachAssignedToTeamEvents)
                .WithOne(x => x.TeamHistory)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.TeamPlayersPlayedMatchEvents)
                .WithOne(x => x.TeamHistory)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.PlayerLeftTeamEvents)
                .WithOne(x => x.TeamHistory)
                .OnDelete(DeleteBehavior.Restrict);
                
        }
    }
}
