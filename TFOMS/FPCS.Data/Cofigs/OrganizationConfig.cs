using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;

using FPCS.Data.Entities;

namespace FPCS.Data.Cofigs
{
    internal class Organizationonfig : EntityTypeConfiguration<Organization>
    {
        public Organizationonfig()
        {
            ToTable("organizations");
            HasKey(x => x.OrganizationId);
            Property(x => x.OrganizationId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity).HasColumnName("organization_id");

            Property(x => x.Name).IsRequired().HasMaxLength(100).HasColumnName("name");
            Property(x => x.IsDeleted).IsRequired().HasColumnName("isdeleted");
            Property(x => x.CreatedDate).IsRequired().HasColumnName("createddate");
            Property(x => x.UpdatedDate).IsRequired().HasColumnName("updateddate");
        }
    }
}
