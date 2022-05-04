using App.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Infrastructure.Configurations
{
    public class UserSurveyResultConfiguration : IEntityTypeConfiguration<UserSurveyResult>
    {
        public void Configure(EntityTypeBuilder<UserSurveyResult> builder)
        {
            builder.HasOne(x => x.Survey)
                .WithMany(x => x.RespondentsResults);

            builder.HasMany(x => x.SelectQuestionAnswers)
                .WithOne(x => x.UserResult)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.TextQuestionAnswers)
                .WithOne(x => x.UserResult)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.User)
                .WithMany(x => x.SurveysResults)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
