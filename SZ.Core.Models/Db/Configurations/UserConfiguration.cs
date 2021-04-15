using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SZ.Core.Models.Db;

namespace SZ.Core.Models.Configurations
{
    internal class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.FirstName)
                .HasMaxLength(SZDb.LengthRequirements.Users.FirstName);

            builder.Property(x => x.SecondName)
                .HasMaxLength(SZDb.LengthRequirements.Users.SecondName);

            builder.Property(x => x.Patronym)
                .HasMaxLength(SZDb.LengthRequirements.Users.Patronym);

            builder.Property(x => x.Region)
                .HasMaxLength(SZDb.LengthRequirements.Users.Region);

            builder.Property(x => x.Room)
                .HasMaxLength(SZDb.LengthRequirements.Users.Room);

            builder.Property(x => x.Street)
                .HasMaxLength(SZDb.LengthRequirements.Users.Street);

            builder.Property(x => x.City)
                .HasMaxLength(SZDb.LengthRequirements.Users.City);

            builder.Property(x => x.House)
                .HasMaxLength(SZDb.LengthRequirements.Users.House);

            builder.Property(x => x.Building)
                .HasMaxLength(SZDb.LengthRequirements.Users.Building);

            builder.Property(x => x.Flat)
                .HasMaxLength(SZDb.LengthRequirements.Users.Flat);

            builder.HasMany(x => x.BlockUsers)
                .WithOne(x => x.Blocker)
                .HasForeignKey(x => x.BlockerId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
