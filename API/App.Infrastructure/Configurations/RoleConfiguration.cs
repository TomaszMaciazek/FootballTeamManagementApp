using App.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Infrastructure.Configurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasMany(x => x.Users)
                .WithMany(x => x.Roles);

            builder.HasMany(x => x.Claims)
                .WithOne(x => x.Role);
        }
    }
}
