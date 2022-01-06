using App.Model.Entities.TestEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Infrastructure.Configurations
{
    public class TestQuestionConfiguration : IEntityTypeConfiguration<TestQuestion>
    {
        public void Configure(EntityTypeBuilder<TestQuestion> builder)
        {
            builder.HasOne(x => x.Test)
                .WithMany(x => x.Questions)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.Options)
                .WithOne(x => x.Question)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.QuestionAnswers)
                .WithOne(x => x.Question);
        }
    }
}
