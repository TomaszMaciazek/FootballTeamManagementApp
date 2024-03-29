﻿using App.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Infrastructure.Configurations
{
    public class MatchConfiguration : IEntityTypeConfiguration<Match>
    {
        public void Configure(EntityTypeBuilder<Match> builder)
        {
            builder.HasMany(x => x.Points)
                .WithOne(x => x.Match)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.PlayersCards)
                .WithOne(x => x.Match)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.CoachesCards)
                .WithOne(x => x.Match)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.Players)
                .WithOne(x => x.Match)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.TeamPlayersPlayedMatchEvents)
                .WithOne(x => x.Match)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
