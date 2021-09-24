using App.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Infrastructure.Configurations
{
    public class CoachConfiguration : IEntityTypeConfiguration<Coach>
    {
        public void Configure(EntityTypeBuilder<Coach> builder)
        {

            builder.HasMany(p => p.Matches)
                .WithMany(m => m.Coaches)
                .UsingEntity(join => join.ToTable("CoachesMatches"));

            builder.HasMany(x => x.Trainings)
                .WithMany(x => x.Coaches)
                .UsingEntity(join => join.ToTable("CoachesTrainings"));

            builder.HasMany(x => x.Teams)
                .WithOne(x => x.MainCoach);

            builder.HasMany(p => p.Cards)
                .WithOne(c => c.Coach)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
