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
            builder.HasMany(x => x.Teams)
                .WithOne(x => x.MainCoach);

            builder.HasMany(p => p.Cards)
                .WithOne(c => c.Coach)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(c => c.Country)
                .WithMany(x => x.Coaches)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.AssignedToTeamEvents)
                .WithOne(x => x.Coach)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
