using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SZ.Core.Models.Db;

namespace SZ.Core.Models.Configurations
{
    internal class DocumentConfiguration : IEntityTypeConfiguration<Document>
    {
        public void Configure(EntityTypeBuilder<Document> builder)
        {
            builder.ToTable("Documents")
                .HasKey(x => x.Id);
            //builder.Property(x => x.Id)
            //    .h("nextval('\"" + nameof(Document) + "\"')");

            builder.HasOne(x => x.UserCreator)
                .WithMany(x => x.CreatedDocuments)
                .HasForeignKey(x => x.UserCreatorId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.UserPositionAcceptor)
                .WithMany(x => x.AcceptorsDocuments)
                .HasForeignKey(x => x.UserPositionAcceptorId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.UserPositionChecker)
                .WithMany(x => x.CheckedDocuments)
                .HasForeignKey(x => x.UserPositionCheckerId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.UserPositionCreator)
                .WithMany(x => x.CreatorDocuments)
                .HasForeignKey(x => x.UserPositionCreatorId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.UserPositionRecipient)
                .WithMany(x => x.RecipientsDocuments)
                .HasForeignKey(x => x.UserPositionRecipientId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.ProtocolTen)
                .WithMany(x => x.Protocols)
                .HasForeignKey(x => x.ProtocolTenId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.Zemstvo)
                .WithMany(x => x.Documents)
                .HasForeignKey(x => x.ZemstvoId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.Position)
                .WithMany(x => x.Documents)
                .HasForeignKey(x => x.PositionId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
