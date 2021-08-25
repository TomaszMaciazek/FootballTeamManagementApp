using App.Model.Entities.SurveyEntities.QuestionTemplates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Infrastructure.Configurations
{
    public class OptionsSurveyQuestionTemplateConfiguration : IEntityTypeConfiguration<OptionsSurveyQuestionTemplate>
    {
        public void Configure(EntityTypeBuilder<OptionsSurveyQuestionTemplate> builder)
        {
            builder.HasMany(x => x.AnswerTemplates)
                .WithOne(x => x.Question);

            builder.HasMany(x => x.UserAnswers)
                .WithOne(x => x.Question);
        }
    }
}
