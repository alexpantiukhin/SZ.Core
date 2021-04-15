using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SZ.Core.Models.Db;

namespace SZ.Core.Models.Configurations
{
    internal class UserTenConfiguration : IEntityTypeConfiguration<UserTen>
    {
        public void Configure(EntityTypeBuilder<UserTen> builder)
        {
            builder.ToTable("UsersTens")
                .HasKey(x => x.Id);
            //builder.Property(x => x.Id)
            //    .HasDefaultValueSql("nextval('\"" + nameof(UserTen) + "\"')");


            builder.HasOne(x => x.User)
                .WithMany(x => x.UserTens)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.Ten)
                .WithMany(x => x.UserTens)
                .HasForeignKey(x => x.TenId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.BasisEntranceDocument)
                .WithMany(x => x.EntranceUserTens)
                .HasForeignKey(x => x.BasisEntranceDocumentId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.BasisExitDocument)
                .WithMany(x => x.ExitUserTens)
                .HasForeignKey(x => x.BasisExitDocumentId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
