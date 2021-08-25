using App.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Infrastructure.Configurations
{
    public class GroupChatConfiguration : IEntityTypeConfiguration<GroupChat>
    {
        public void Configure(EntityTypeBuilder<GroupChat> builder)
        {
            builder.HasMany(x => x.Messages)
                .WithOne(x => x.Chat)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Image)
                .WithOne(x => x.Chat)
                .HasForeignKey<GroupChatImage>(x => x.ChatId);
        }
    }
}
