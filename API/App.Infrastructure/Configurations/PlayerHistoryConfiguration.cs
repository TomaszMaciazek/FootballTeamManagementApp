using App.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Infrastructure.Configurations
{
    public class PlayerHistoryConfiguration : IEntityTypeConfiguration<PlayerHistory>
    {
        public void Configure(EntityTypeBuilder<PlayerHistory> builder)
        {
            builder.HasOne(x => x.Player)
                .WithOne(x => x.History)
                .HasForeignKey<PlayerHistory>(x => x.PlayerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.PlayerJoinedTeamEvents)
                .WithOne(x => x.PlayerHistory)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.MatchPlayerPerformances)
                .WithOne(x => x.PlayerHistory)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.PlayerLeftTeamEvents)
                .WithOne(x => x.PlayerHistory)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
