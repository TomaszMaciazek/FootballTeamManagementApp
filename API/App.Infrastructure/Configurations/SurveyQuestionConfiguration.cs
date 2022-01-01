using App.Model.Entities.SurveyEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Infrastructure.Configurations
{
    public class SurveyQuestionConfiguration : IEntityTypeConfiguration<SurveyQuestion>
    {
        public void Configure(EntityTypeBuilder<SurveyQuestion> builder)
        {
            builder.HasOne(x => x.Survey)
                .WithMany(x => x.Questions)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.Options)
                .WithOne(x => x.Question)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.SelectQuestionAnswers)
                .WithOne(x => x.Question)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.TextQuestionAnswers)
                .WithOne(x => x.Question)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
