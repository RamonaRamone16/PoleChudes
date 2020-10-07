using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PoleChudes.DAL.Entities;
using System.Data.Entity.ModelConfiguration;

namespace PoleChudes.DAL.EntitiesConfigurations
{
    public class MatchConfiguration : EntityTypeConfiguration<Match>
    {
        public void Configure(EntityTypeBuilder<Match> builder)
        {
            builder.HasOne(x => x.User)
                .WithMany(y => y.Matches)
                .HasForeignKey(x => x.UserId);

            builder.HasOne(x => x.Word)
                .WithMany(y => y.Matches)
                .HasForeignKey(x => x.WordId);
        }
    }
}
