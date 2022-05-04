using App.Model.Entities.TestEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Infrastructure.Configurations
{
    public class UserTestResultConfiguration : IEntityTypeConfiguration<UserTestResult>
    {
        public void Configure(EntityTypeBuilder<UserTestResult> builder)
        {
            builder.HasOne(x => x.Test)
                .WithMany(x => x.UserResults)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.QuestionAnswers)
                .WithOne(x => x.UserResult)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.User)
                .WithMany(x => x.TestsResults)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
