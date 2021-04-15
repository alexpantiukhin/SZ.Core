using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SZ.Core.Models.Db;

namespace SZ.Core.Models.Configurations
{
    internal class TenConfiguration : IEntityTypeConfiguration<Ten>
    {
        public void Configure(EntityTypeBuilder<Ten> builder)
        {
            builder.ToTable("Tens")
                .HasKey(x => x.Id);
            //builder.Property(x => x.Id)
            //    .HasDefaultValueSql("nextval('\"" + nameof(Ten) + "\"')");

            //var check = $"[{nameof(Ten.Circle)}] > 0";
            //builder.HasCheckConstraint(Settings.Errors.SQLErrors.CheckCircleTen, check);


            builder.HasOne(x => x.ParentTen)
                .WithMany(x => x.ChildTens)
                .HasForeignKey(x => x.ParentTenId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.Creator)
                .WithMany(x => x.CreatedTens)
                .HasForeignKey(x => x.CreatorId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.Zemstvo)
                .WithMany(x => x.Tens)
                .HasForeignKey(x => x.ZemstvoId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
