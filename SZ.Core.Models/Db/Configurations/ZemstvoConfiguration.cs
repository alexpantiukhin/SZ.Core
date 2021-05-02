using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SZ.Core.Models.Db;

namespace SZ.Core.Models.Configurations
{
    internal class ZemstvoConfiguration : IEntityTypeConfiguration<Zemstvo>
    {
        public void Configure(EntityTypeBuilder<Zemstvo> builder)
        {
            builder.ToTable("Zemstvos")
                .HasKey(x => x.Id);

            //builder.Property(x => x.Id)
            //    .HasDefaultValueSql("nextval('\"" + nameof(Zemstvo) + "\"')");

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(250);

            //builder.HasOne(x => x.ParentZemstvo)
            //    .WithMany(x => x.ChildZemvstvs)
            //    .HasForeignKey(x => x.ParentZemstvoId)
            //    .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
