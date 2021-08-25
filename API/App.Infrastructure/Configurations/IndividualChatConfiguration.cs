using App.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Infrastructure.Configurations
{
    public class IndividualChatConfiguration : IEntityTypeConfiguration<IndividualChat>
    {
        public void Configure(EntityTypeBuilder<IndividualChat> builder)
        {
            builder.HasMany(x => x.Messages)
                .WithOne(x => x.Chat)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
