using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SZ.Core.Models.Db;

namespace SZ.Core.Models.Configurations
{
    internal class ProtocolQuestionRepeatConfiguration : IEntityTypeConfiguration<ProtocolQuestionRepeat>
    {
        public void Configure(EntityTypeBuilder<ProtocolQuestionRepeat> builder)
        {
            builder.ToTable("ProtocolQuestionRepeats")
                .HasKey(x => x.Id);

            //builder.Property(x => x.Id)
            //    .HasDefaultValueSql("nextval('\"" + nameof(ProtocolQuestionRepeat) + "\"')");

            builder.HasOne(x => x.Protocol)
                .WithMany(x => x.ProtocolQuestionRepeats)
                .HasForeignKey(x => x.ProtocolId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.QuestionRepeat)
                .WithMany(x => x.ProtocolQuestionRepeats)
                .HasForeignKey(x => x.QuestionRepeatId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.SupportQuestionNewRepeat)
                .WithMany(x => x.ProtocolSupportingQuestionRepeats)
                .HasForeignKey(x => x.SupportQuestionNewRepeatId)
                .OnDelete(DeleteBehavior.NoAction);

        }
    }
}
