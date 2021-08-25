using App.Model.Entities.TestEntities.QuestionTemplates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Infrastructure.Configurations
{
    public class OptionsTestQuestionTemplateConfiguration : IEntityTypeConfiguration<OptionsTestQuestionTemplate>
    {
        public void Configure(EntityTypeBuilder<OptionsTestQuestionTemplate> builder)
        {
            builder.HasMany(x => x.Answers)
                .WithOne(x => x.Question);

            builder.HasMany(x => x.UserAnswers)
                .WithOne(x => x.Question);
        }
    }
}
