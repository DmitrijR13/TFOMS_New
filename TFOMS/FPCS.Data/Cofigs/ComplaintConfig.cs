using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;

using FPCS.Data.Entities;

namespace FPCS.Data.Cofigs
{
    internal class ComplaintConfig : EntityTypeConfiguration<Complaint>
    {
        public ComplaintConfig()
        {
            ToTable("complaint");
            HasKey(x => x.ComplaintId);
            Property(x => x.ComplaintId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity).HasColumnName("complaintid");

            Property(x => x.Code).IsRequired().HasMaxLength(5).HasColumnName("code");
            Property(x => x.Name).IsRequired().HasMaxLength(100).HasColumnName("name");
            Property(x => x.IsDeleted).IsRequired().HasColumnName("isdeleted");
            Property(x => x.CreatedDate).IsRequired().HasColumnName("createddate");
            Property(x => x.UpdatedDate).IsRequired().HasColumnName("updateddate");
        }
    }
}
