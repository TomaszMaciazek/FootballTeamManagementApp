using App.Model.Entities.SurveyEntities.QuestionTemplates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Infrastructure.Configurations
{
    public class TextSurveyQuestionTemplateConfiguration : IEntityTypeConfiguration<TextSurveyQuestionTemplate>
    {
        public void Configure(EntityTypeBuilder<TextSurveyQuestionTemplate> builder)
        {
            builder.HasMany(x => x.QuestionAnswers)
                .WithOne(x => x.Question);
        }
    }
}
