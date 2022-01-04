using App.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Infrastructure.Configurations
{
    public class TeamConfiguration : IEntityTypeConfiguration<Team>
    {
        public void Configure(EntityTypeBuilder<Team> builder)
        {
            builder.HasMany(x => x.MatchPoints)
                .WithOne(x => x.Team);

            builder.HasMany(x => x.PlayersCards)
                .WithOne(x => x.Team);
        }
    }
}
