using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PoleChudes.DAL.Entities;
using System.Data.Entity.ModelConfiguration;

namespace PoleChudes.DAL.EntitiesConfigurations
{
    public class WordConfiguration : EntityTypeConfiguration<Word>
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
