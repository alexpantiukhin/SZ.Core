using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SZ.Core.Models.Db;

namespace SZ.Core.Models.Configurations
{
    internal class DocumentDecisionConfiguration : IEntityTypeConfiguration<DocumentDecision>
    {
        public void Configure(EntityTypeBuilder<DocumentDecision> builder)
        {
            builder.ToTable("DocumentDecisions")
                .HasKey(x => x.Id);
            //builder.Property(x => x.Id)
            //    .HasDefaultValueSql("nextval('\"" + nameof(DocumentDecision) + "\"')");

            //var check1 = "(" +
            //                    $"[{nameof(DocumentDecision.ProtocolQuestionId)}] IS NOT NULL AND " +
            //                    "(" +
            //                        $"([{nameof(DocumentDecision.DecisionAnswerId)}] IS NOT NULL AND [{nameof(DocumentDecision.DecisionBool)}] IS NULL AND [{nameof(DocumentDecision.DecisionPersonId)}] IS NULL) OR " +
            //                        $"([{nameof(DocumentDecision.DecisionBool)}] IS NOT NULL AND [{nameof(DocumentDecision.DecisionAnswerId)}] IS NULL AND [{nameof(DocumentDecision.DecisionPersonId)}] IS NULL) OR " +
            //                        $"([{nameof(DocumentDecision.DecisionPersonId)}] IS NOT NULL AND [{nameof(DocumentDecision.DecisionAnswerId)}] IS NULL AND [{nameof(DocumentDecision.DecisionBool)}] IS NULL)" +
            //                    $")" +
            //            ") OR " +
            //            "(" + 
            //                $"[{nameof(DocumentDecision.ProtocolQuestionId)}] IS NULL AND " +
            //                $"[{nameof(DocumentDecision.DecisionPersonId)}] IS NULL AND " +
            //                $"[{nameof(DocumentDecision.DecisionAnswerId)}] IS NULL AND " + 
            //                $"[{nameof(DocumentDecision.DecisionBool)}] IS NULL" +
            //            ")";
            //builder.HasCheckConstraint(Settings.Errors.SQLErrors.CheckTypeDocumentDecision, check1);


            builder.Property(x => x.DecisionArbitrary)
                .HasMaxLength(1000);

            builder.HasOne(x => x.DocumentUser)
                .WithMany(x => x.DocumentDecisions)
                .HasForeignKey(x => x.DocumentUserId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.ProtocolQuestionRepeat)
                .WithMany(x => x.DocumentDecisions)
                .HasForeignKey(x => x.ProtocolQuestionRepeatId)
                .OnDelete(DeleteBehavior.NoAction);

            //builder.HasOne(x => x.DecisionPerson)
            //    .WithMany(x => x.DocumentDecisions)
            //    .HasForeignKey(x => x.DecisionPersonId)
            //    .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.QuestionRepeatAnswer)
                .WithMany(x => x.DocumentDecisions)
                .HasForeignKey(x => x.QuestionRepeatAnswerId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.RealDecideUser)
                .WithMany(x => x.RealDecisions)
                .HasForeignKey(x => x.RealDecideUserId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
