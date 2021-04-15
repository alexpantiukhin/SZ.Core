using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SZ.Core.Models.Db;

namespace SZ.Core.Models.Configurations
{
    internal class QuestionRepeatConfiguration : IEntityTypeConfiguration<QuestionRepeat>
    {
        public void Configure(EntityTypeBuilder<QuestionRepeat> builder)
        {
            builder.ToTable("QuestionRepeats")
                .HasKey(x => x.Id);
            //builder.Property(x => x.Id)
            //    .HasDefaultValueSql("nextval('\"" + nameof(QuestionRepeat) + "\"')");

            builder.Property(x => x.Essence)
                .IsRequired()
                .HasMaxLength(250);

            builder.Property(x => x.Reason)
                .IsRequired()
                .HasMaxLength(250);

            builder.Property(x => x.Description)
                .HasMaxLength(2000);

            builder.HasIndex(x => new
                {
                    x.QuestionId,
                    x.SequenceNumber
                })
                .IsUnique();


            builder.HasOne(x => x.Updater)
                .WithMany(x => x.QuestionRepeats)
                .HasForeignKey(x => x.UpdaterId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.Position)
                .WithMany(x => x.QuestionRepeats)
                .HasForeignKey(x => x.PositionId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.Question)
                .WithMany(x => x.QuestionRepeats)
                .HasForeignKey(x => x.QuestionId)
                .OnDelete(DeleteBehavior.NoAction);

            //var check1 = $"([{nameof(Question.Type)}] >= 4 AND [{nameof(Question.PositionId)}] IS NOT NULL) OR [{nameof(Question.Type)}] < 4";
            //builder.HasCheckConstraint(Settings.Errors.SQLErrors.CheckQuestionPosition, check1);
        }
    }
}
