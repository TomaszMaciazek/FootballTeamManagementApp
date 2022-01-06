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

            builder.HasMany(u => u.IndividualChats)
                .WithMany(chat => chat.Users)
                .UsingEntity(join => join.ToTable("IndividualChatsMembers"));

            builder.HasMany(u => u.GroupChats)
                .WithMany(chat => chat.Users)
                .UsingEntity(join => join.ToTable("GroupChatsMembers"));

            builder.HasMany(u => u.IndividualMessages)
                .WithOne(m => m.Sender);

            builder.HasMany(u => u.GroupMessages)
                .WithOne(m => m.Sender);

            builder.HasMany(u => u.UserSurveysTemplates)
                .WithOne(t => t.Author);

            builder.HasMany(u => u.UserTestsTemplates)
                .WithOne(t => t.Author)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(u => u.TokenRefresh)
                .WithOne(x => x.User)
                .HasForeignKey<UserTokenRefresh>(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            #endregion

            builder.HasIndex(u => u.Email).IsUnique();
            builder.HasIndex(u => u.Username).IsUnique();

        }
    }
}
