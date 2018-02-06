using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;

using FPCS.Data.Entities;

namespace FPCS.Data.Cofigs
{
    internal class MOConfig : EntityTypeConfiguration<MO>
    {
        public MOConfig()
        {
            ToTable("medical_organization");
            HasKey(x => x.MOId);
            Property(x => x.MOId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity).HasColumnName("mo_id");

            Property(x => x.Code).IsRequired().HasMaxLength(10).HasColumnName("code");
            Property(x => x.Name).IsRequired().HasMaxLength(100).HasColumnName("name");
            Property(x => x.IsDeleted).IsRequired().HasColumnName("isdeleted");
            Property(x => x.CreatedDate).IsRequired().HasColumnName("createddate");
            Property(x => x.UpdatedDate).IsRequired().HasColumnName("updateddate");
        }
    }
}
