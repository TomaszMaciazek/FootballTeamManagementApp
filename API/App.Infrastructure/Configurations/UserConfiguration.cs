using App.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Infrastructure.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder) 
        {
            #region Relations
            builder.HasOne(u => u.PlayerDetails)
                .WithOne(p => p.User)
                .HasForeignKey<Player>(p => p.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(u => u.CoachDetails)
                .WithOne(c => c.User)
                .HasForeignKey<Coach>(c => c.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(u => u.UserSurveysTemplates)
                .WithOne(t => t.Author);

            builder.HasMany(u => u.UserTestsTemplates)
                .WithOne(t => t.Author)
                .OnDelete(DeleteBehavior.Restrict);
            #endregion

            builder.HasIndex(u => u.Email).IsUnique();
            builder.HasIndex(u => u.Username).IsUnique();

        }
    }
}
