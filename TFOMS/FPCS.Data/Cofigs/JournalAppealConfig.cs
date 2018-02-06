using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;

using FPCS.Data.Entities;

namespace FPCS.Data.Cofigs
{
    internal class JournalAppealConfig : EntityTypeConfiguration<JournalAppeal>
    {
        public JournalAppealConfig()
        {
            ToTable("journalappeal");
            HasKey(x => x.JournalAppealId);
            Property(x => x.JournalAppealId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity).HasColumnName("journalappealid");

            Property(x => x.AppealUniqueNumber).HasMaxLength(36).HasColumnName("appealuniquenumber");
            Property(x => x.Date).HasColumnName("date");
            Property(x => x.Time).HasColumnName("time_");

            Property(x => x.SourceIncomeId).HasColumnName("sourceincomeid");
            HasOptional(x => x.SourceIncome).WithMany(x => x.JournalAppeals).HasForeignKey(x => x.SourceIncomeId).WillCascadeOnDelete(false);

            Property(x => x.OrganizationName).HasColumnName("organizationname");

            Property(x => x.TypeOfAddressingId).HasColumnName("typeofaddressingid");
            HasOptional(x => x.TypeOfAddressing).WithMany(x => x.JournalAppeals).HasForeignKey(x => x.TypeOfAddressingId).WillCascadeOnDelete(false);

            Property(x => x.WayOfAddressingId).HasColumnName("wayofaddressingid");
            HasOptional(x => x.WayOfAddressing).WithMany(x => x.JournalAppeals).HasForeignKey(x => x.WayOfAddressingId).WillCascadeOnDelete(false);

            Property(x => x.ThemeAppealCitizensId).HasColumnName("appealtheme");
            HasOptional(x => x.ThemeAppealCitizens).WithMany(x => x.JournalAppeals).HasForeignKey(x => x.ThemeAppealCitizensId).WillCascadeOnDelete(false);

            Property(x => x.AppealContent).HasMaxLength(5000).HasColumnName("appealcontent");

            Property(x => x.ComplaintId).HasColumnName("complaintid");
            HasOptional(x => x.Complaint).WithMany(x => x.JournalAppeals).HasForeignKey(x => x.ComplaintId).WillCascadeOnDelete(false);

            Property(x => x.AppealOrganizationId).HasColumnName("appealorganizationid");
            HasOptional(x => x.AppealOrganization).WithMany(x => x.JournalAppeals).HasForeignKey(x => x.AppealOrganizationId).WillCascadeOnDelete(false);

            Property(x => x.SMOId).HasColumnName("smoid");
            HasOptional(x => x.SMO).WithMany(x => x.JournalAppeals).HasForeignKey(x => x.SMOId).WillCascadeOnDelete(false);

            Property(x => x.TakingAppealLineId).HasColumnName("takingappeallineid");
            HasOptional(x => x.TakingAppealLine).WithMany(x => x.JournalAppeals).HasForeignKey(x => x.TakingAppealLineId).WillCascadeOnDelete(false);

            Property(x => x.AcceptedBy).HasColumnName("acceptedby");
            Property(x => x.AppealOrganizationCode).HasColumnName("appealorganizationcode");

            Property(x => x.ReviewAppealLineId).HasColumnName("reviewappeallineid");
            HasOptional(x => x.ReviewAppealLine).WithMany(x => x.JournalAppeals).HasForeignKey(x => x.ReviewAppealLineId).WillCascadeOnDelete(false);

            Property(x => x.Responsible).HasColumnName("responsible");
            Property(x => x.AppealPlanEndDate).HasColumnName("appealplanenddate");
            Property(x => x.AppealFactEndDate).HasColumnName("appealfactenddate");

            Property(x => x.AppealResultId).HasColumnName("appealresultid");
            HasOptional(x => x.AppealResult).WithMany(x => x.JournalAppeals).HasForeignKey(x => x.AppealResultId).WillCascadeOnDelete(false);

            Property(x => x.ApplicantSurname).HasColumnName("applicantsurname");
            Property(x => x.ApplicantName).HasColumnName("applicantname");
            Property(x => x.ApplicantSecondName).HasColumnName("applicantsecondname");
            Property(x => x.ApplicantBirthDate).HasColumnName("applicantbirthdate");
            Property(x => x.ApplicantENP).HasColumnName("applicantenp");
            Property(x => x.ApplicantSMO).HasColumnName("applicantsmo");
            Property(x => x.ApplicantTypeDocument).HasColumnName("applicanttypedocument");
            Property(x => x.ApplicantDocumentSeries).HasColumnName("applicantdocumentseries");
            Property(x => x.ApplicantDocumentNumber).HasColumnName("applicantdocumentnumber");
            Property(x => x.ApplicantFeedbackAddress).HasColumnName("applicantfeedbackaddress");
            Property(x => x.ApplicantPhoneNumber).HasColumnName("applicantphonenumber");
            Property(x => x.ApplicantEmail).HasColumnName("applicantemail");
            Property(x => x.ReceivedTreatmentPersonSurname).HasColumnName("receivedtreatmentpersonsurname");
            Property(x => x.ReceivedTreatmentPersonName).HasColumnName("receivedtreatmentpersonname");
            Property(x => x.ReceivedTreatmentPersonSecondName).HasColumnName("receivedtreatmentpersonsecondname");
            Property(x => x.ReceivedTreatmentPersonBirthDate).HasColumnName("receivedtreatmentpersonbirthdate");
            Property(x => x.ReceivedTreatmentPersonENP).HasColumnName("receivedtreatmentpersonenp");
            Property(x => x.ReceivedTreatmentPersonSMO).HasColumnName("receivedtreatmentpersonsmo");
            Property(x => x.ReceivedTreatmentPersonypeDocument).HasColumnName("receivedtreatmentpersonypedocument");
            Property(x => x.ReceivedTreatmentPersonDocumentSeries).HasColumnName("receivedtreatmentpersondocumentseries");
            Property(x => x.ReceivedTreatmentPersonDocumentNumber).HasColumnName("receivedtreatmentpersondocumentnumber");

            Property(x => x.IsDeleted).IsRequired().HasColumnName("isdeleted");
            Property(x => x.CreatedDate).IsRequired().HasColumnName("createddate");
            Property(x => x.UpdatedDate).IsRequired().HasColumnName("updateddate");
        }
    }
}
