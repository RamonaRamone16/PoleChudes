using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PoleChudes.DAL.Entities;

namespace PoleChudes.DAL.EntitiesConfigurations
{
    public class WordConfiguration : IEntityTypeConfiguration<Word>
    {
        public void Configure(EntityTypeBuilder<Word> builder)
        {
            builder.HasOne(x => x.Admin)
                .WithMany(y => y.Words)
                .HasForeignKey(x => x.AdminId);

            builder.HasMany(x => x.Matches)
                .WithOne(y => y.Word)
                .HasForeignKey(y => y.WordId);
        }
    }
}
