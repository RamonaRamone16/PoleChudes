using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PoleChudes.DAL.Entities;

namespace PoleChudes.DAL.EntitiesConfigurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasMany(x => x.Matches)
                .WithOne(y => y.User)
                .HasForeignKey(y => y.UserId);

            builder.HasMany(x => x.Words)
                .WithOne(y => y.Admin)
                .HasForeignKey(y => y.AdminId);
        }
    }
}
