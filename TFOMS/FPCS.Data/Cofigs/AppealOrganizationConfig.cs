using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;

using FPCS.Data.Entities;

namespace FPCS.Data.Cofigs
{
    internal class AppealOrganizationConfig : EntityTypeConfiguration<AppealOrganization>
    {
        public AppealOrganizationConfig()
        {
            ToTable("appealorganization");
            HasKey(x => x.AppealOrganizationId);
            Property(x => x.AppealOrganizationId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity).HasColumnName("appealorganizationid");

            Property(x => x.Code).IsRequired().HasMaxLength(3).HasColumnName("code");
            Property(x => x.Name).IsRequired().HasMaxLength(100).HasColumnName("name");           
            Property(x => x.IsDeleted).IsRequired().HasColumnName("isdeleted");
            Property(x => x.CreatedDate).IsRequired().HasColumnName("createddate");
            Property(x => x.UpdatedDate).IsRequired().HasColumnName("updateddate");
        }
    }
}
