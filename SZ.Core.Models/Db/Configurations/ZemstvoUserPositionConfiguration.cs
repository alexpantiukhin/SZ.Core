using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SZ.Core.Models.Db;

namespace SZ.Core.Models.Configurations
{
    internal class ZemstvoUserPositionConfiguration : IEntityTypeConfiguration<ZemstvoUserPosition>
    {
        public void Configure(EntityTypeBuilder<ZemstvoUserPosition> builder)
        {
            builder.ToTable("ZemstvosUsersPositions")
                .HasKey(x => x.Id);

            //builder.Property(x => x.Id)
            //    .HasDefaultValueSql("nextval('\"" + nameof(ZemstvoUserPosition) + "\"')");

            builder.HasOne(x => x.Position)
                .WithMany(x => x.ZemstvoUsers)
                .HasForeignKey(x => x.PositionId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.DocumentElection)
                .WithMany(x => x.ElectionUserPositions)
                .HasForeignKey(x => x.DocumentElectionId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.DocumentRecall)
                .WithMany(x => x.RecallUserPositions)
                .HasForeignKey(x => x.DocumentRecallId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.Creator)
                .WithMany(x => x.CreatedUserPositions)
                .HasForeignKey(x => x.UserCreatorId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.User)
                .WithMany(x => x.ZemstvoPositions)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.Zemstvo)
                .WithMany(x => x.UsersPositions)
                .HasForeignKey(x => x.ZemstvoId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
