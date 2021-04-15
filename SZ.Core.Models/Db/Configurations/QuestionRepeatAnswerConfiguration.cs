using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SZ.Core.Models.Db;

namespace SZ.Core.Models.Configurations
{
    internal class QuestionRepeatAnswerConfiguration : IEntityTypeConfiguration<QuestionRepeatAnswer>
    {
        public void Configure(EntityTypeBuilder<QuestionRepeatAnswer> builder)
        {
            builder.ToTable("QuestionRepeatAnswers")
                .HasKey(x => x.Id);
            //builder.Property(x => x.Id)
            //    .HasDefaultValueSql("nextval('\"" + nameof(QuestionRepeatAnswer) + "\"')");

            builder.Property(x => x.Answer)
                .HasMaxLength(250);

            builder.HasOne(x => x.QuestionRepeat)
                .WithMany(x => x.QuestionRepeatAnswers)
                .HasForeignKey(x => x.QuestionRepeatId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.Candidat)
                .WithMany(x => x.CandidatInAnswers)
                .HasForeignKey(x => x.CandidatId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
