using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SZ.Core.Models.Db;

namespace SZ.Core.Models.Configurations
{
    internal class DocumentUserConfiguration : IEntityTypeConfiguration<DocumentUser>
    {
        public void Configure(EntityTypeBuilder<DocumentUser> builder)
        {
            builder.ToTable("DocumentsUsers")
                .HasKey(x => x.Id);
            //builder.Property(x => x.Id)
            //    .HasDefaultValueSql("nextval('\"" + nameof(DocumentUser) + "\"')");


            builder.HasOne(x => x.Document)
                .WithMany(x => x.DocumentUsers)
                .HasForeignKey(x => x.DocumentId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.User)
                .WithMany(x => x.DocumentUsers)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
