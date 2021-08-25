using App.Model.Entities.SurveyEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Infrastructure.Configurations
{
    public class SurveyTemplateConfiguration : IEntityTypeConfiguration<SurveyTemplate>
    {
        public void Configure(EntityTypeBuilder<SurveyTemplate> builder)
        {
            builder.HasMany(x => x.BoolQuestionTemplates)
                .WithOne(x => x.Survey);

            builder.HasMany(x => x.OptionsQuestionTemplates)
                .WithOne(x => x.Survey);

            builder.HasMany(x => x.TextQuestionTemplates)
                .WithOne(x => x.Survey);

            builder.HasMany(x => x.RatingQuestionTemplates)
                .WithOne(x => x.Survey);

            builder.HasMany(x => x.RespondentsResults)
                .WithOne(x => x.Survey);
        }
    }
}
