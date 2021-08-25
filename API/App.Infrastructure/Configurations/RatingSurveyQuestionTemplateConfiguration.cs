using App.Model.Entities.SurveyEntities.QuestionTemplates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Infrastructure.Configurations
{
    public class RatingSurveyQuestionTemplateConfiguration : IEntityTypeConfiguration<RatingSurveyQuestionTemplate>
    {
        public void Configure(EntityTypeBuilder<RatingSurveyQuestionTemplate> builder)
        {
            builder.HasMany(x => x.UsersAnswers)
                .WithOne(x => x.Question);
        }
    }
}
