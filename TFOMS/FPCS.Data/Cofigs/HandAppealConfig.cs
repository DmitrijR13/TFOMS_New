using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;

using FPCS.Data.Entities;

namespace FPCS.Data.Cofigs
{
    internal class HandAppealConfig : EntityTypeConfiguration<HandAppeal>
    {
        public HandAppealConfig()
        {

            Map(x => 
            {
                x.ToTable("handappeal");
                Property(y => y.OZPZRegistrationDate).HasColumnName("ozpz_registration_date");
                Property(y => y.AppealNumber).HasColumnName("appeal_number");
                Property(y => y.GuideDate).HasColumnName("guide_date");
                Property(y => y.GuideNumber).HasColumnName("guide_number");
                Property(y => y.ReceivedFeedbackAddress).HasColumnName("received_feedback_address");
                Property(y => y.ReceivedPhoneNumber).HasColumnName("received_phone_number");
                Property(y => y.InspectionKind).HasColumnName("inspection_kind");
                Property(y => y.InspectionExpert).HasColumnName("inspection_expert");
                Property(y => y.ReviewAppealNumber).HasColumnName("review_appeal_number");
                Property(y => y.ReviewAppealCode).HasColumnName("review_appeal_code");
                Property(y => y.ReviewAppealPenalty).HasColumnName("review_appeal_penalty");
                Property(y => y.ReviewAppealCashback).HasColumnName("review_appeal_cashback");
                Property(y => y.Comment).HasColumnName("comment");
                Property(y => y.UniqueNumberPart1).HasColumnName("unique_number_part_1");
                Property(y => y.AppealFileName).HasColumnName("appeal_file_name");
                Property(y => y.AppealFile).HasColumnName("appeal_file");
                Property(y => y.AnswerFileName).HasColumnName("answer_file_name");
                Property(y => y.AnswerFile).HasColumnName("answer_file");

                Property(y => y.MedicalOrganizationId).HasColumnName("med_orgs");
                Property(y => y.OrganizationId).HasColumnName("organizations");

                Property(y => y.WorkerId).HasColumnName("worker_id");
                HasOptional(y => y.Worker).WithMany(y => y.HandAppeals).HasForeignKey(y => y.WorkerId).WillCascadeOnDelete(false);

                Property(y => y.PassedEventId).HasColumnName("passed_event_id");
                HasOptional(y => y.PassedEvent).WithMany(y => y.HandAppeals).HasForeignKey(y => y.PassedEventId).WillCascadeOnDelete(false);
            });

            //ToTable("handappeal");
           
        }
    }
}
