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

            builder.HasOne(u => u.Image)
                .WithOne(i => i.User)
                .HasForeignKey<UserImage>(i => i.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(u => u.CoachDetails)
                .WithOne(c => c.User)
                .HasForeignKey<Coach>(c => c.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(u => u.IndividualChats)
                .WithMany(chat => chat.Users);

            builder.HasMany(u => u.GroupChats)
                .WithMany(chat => chat.Users);

            builder.HasMany(u => u.IndividualMessages)
                .WithOne(m => m.Sender);

            builder.HasMany(u => u.GroupMessages)
                .WithOne(m => m.Sender);

            builder.HasMany(u => u.UserSurveysTemplates)
                .WithOne(t => t.Author);

            builder.HasMany(u => u.SurveysResults)
                .WithOne(r => r.User);

            builder.HasMany(u => u.BooleanSurveyQuestionAnswers)
                .WithOne(x => x.User);

            builder.HasMany(u => u.TextSurveyQuestionAnswers)
                .WithOne(x => x.User);

            builder.HasMany(u => u.OptionsSurveyQuestionAnswers)
                .WithOne(x => x.User);

            builder.HasMany(u => u.RatingSurveyQuestionAnswers)
                .WithOne(x => x.User);

            builder.HasMany(u => u.UserTestsTemplates)
                .WithOne(t => t.Author);
            builder.HasMany(u => u.TestsResults)
                .WithOne(r => r.User);

            builder.HasMany(u => u.OptionsTestQuestionAnswers)
                .WithOne(x => x.User);

            builder.HasMany(u => u.BoolTestQuestionAnswers)
                .WithOne(x => x.User);
            #endregion
        }
    }
}
