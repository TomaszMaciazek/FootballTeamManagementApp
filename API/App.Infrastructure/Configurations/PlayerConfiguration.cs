using App.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Infrastructure.Configurations
{
    public class PlayerConfiguration : IEntityTypeConfiguration<Player>
    {
        public void Configure(EntityTypeBuilder<Player> builder)
        {
            builder.HasMany(p => p.Cards)
                .WithOne(c => c.Player)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(p => p.Matches)
                .WithOne(m => m.Player)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.MatchPoints)
                .WithOne(p => p.GoalScorer)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Team)
                .WithMany(x => x.Players)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasMany(x => x.TrainingScores)
                .WithOne(x => x.Player)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(c => c.Country)
                .WithMany(x => x.Players)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
