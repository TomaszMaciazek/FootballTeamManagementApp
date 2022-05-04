using App.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Infrastructure.Configurations
{
    public class TrainingScoreConfiguration : IEntityTypeConfiguration<TrainingScore>
    {
        public void Configure(EntityTypeBuilder<TrainingScore> builder)
        {
            builder.HasOne(x => x.Modifier)
                .WithMany(x => x.CreatedTrainingScores)
                .HasForeignKey(x => x.UpdatedBy)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
