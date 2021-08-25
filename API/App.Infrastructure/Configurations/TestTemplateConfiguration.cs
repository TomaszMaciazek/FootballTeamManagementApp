using App.Model.Entities.TestEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Infrastructure.Configurations
{
    public class TestTemplateConfiguration : IEntityTypeConfiguration<TestTemplate>
    {
        public void Configure(EntityTypeBuilder<TestTemplate> builder)
        {
            builder.HasMany(x => x.BoolTestQuestions)
                .WithOne(x => x.Test);

            builder.HasMany(x => x.OptionsTestQuestions)
                .WithOne(x => x.Test);

            builder.HasMany(x => x.UserResults)
                .WithOne(x => x.Test);
        }
    }
}
