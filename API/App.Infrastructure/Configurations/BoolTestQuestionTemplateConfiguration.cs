using App.Model.Entities.TestEntities.QuestionTemplates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Infrastructure.Configurations
{
    public class BoolTestQuestionTemplateConfiguration : IEntityTypeConfiguration<BoolTestQuestionTemplate>
    {
        public void Configure(EntityTypeBuilder<BoolTestQuestionTemplate> builder)
        {
            builder.HasMany(x => x.UsersAnswers)
                .WithOne(x => x.Question);
        }
    }
}
