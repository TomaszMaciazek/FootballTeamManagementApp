﻿using App.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Infrastructure.Configurations
{
    public class MatchPlayerScoreConfiguration : IEntityTypeConfiguration<MatchPlayerScore>
    {
        public void Configure(EntityTypeBuilder<MatchPlayerScore> builder)
        {
            builder.HasOne(x => x.Player)
                .WithMany(x => x.MatchScores)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Match)
                .WithMany(x => x.PlayerScores)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
