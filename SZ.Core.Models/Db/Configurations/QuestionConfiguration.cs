using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SZ.Core.Models.Db;

namespace SZ.Core.Models.Configurations
{
    internal class QuestionConfiguration : IEntityTypeConfiguration<Question>
    {
        public void Configure(EntityTypeBuilder<Question> builder)
        {
            builder.ToTable("Questions")
                .HasKey(x => x.Id);

            //builder.Property(x => x.Id)
            //    .HasDefaultValueSql("nextval('\"" + nameof(Question) + "\"')");

            //builder.Property(x => x.Essence)
            //    .IsRequired()
            //    .HasMaxLength(250);

            //builder.Property(x => x.Description)
            //    .HasMaxLength(2000);



            builder.HasOne(x => x.Initiator)
                .WithMany(x => x.Questions)
                .HasForeignKey(x => x.InitiatorId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.Zemstvo)
                .WithMany(x => x.Questions)
                .HasForeignKey(x => x.ZemstvoId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.DelegateTen)
                .WithMany(x => x.DelegateQuestions)
                .HasForeignKey(x => x.DelegatTenId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.SupportQuestionRepeat)
                .WithMany(x => x.SupportQuestions)
                .HasForeignKey(x => x.SupportQuestionRepeatId)
                .OnDelete(DeleteBehavior.NoAction);

            //builder.HasOne(x => x.Position)
            //    .WithMany(x => x.Questions)
            //    .HasForeignKey(x => x.PositionId)
            //    .OnDelete(DeleteBehavior.NoAction);

            //var check1 = $"([{nameof(Question.Type)}] >= 4 AND [{nameof(Question.PositionId)}] IS NOT NULL) OR [{nameof(Question.Type)}] < 4";
            //builder.HasCheckConstraint(Settings.Errors.SQLErrors.CheckQuestionPosition, check1);
        }
    }
}
