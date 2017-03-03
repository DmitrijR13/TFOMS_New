using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;

using FPCS.Data.Entities;

namespace FPCS.Data.Cofigs
{
    internal class WayOfAddressingConfig : EntityTypeConfiguration<WayOfAddressing>
    {
        public WayOfAddressingConfig()
        {
            ToTable("wayofaddressing");
            HasKey(x => x.WayOfAddressingId);
            Property(x => x.WayOfAddressingId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity).HasColumnName("wayofaddressingid");

            Property(x => x.Code).IsRequired().HasMaxLength(3).HasColumnName("code");
            Property(x => x.Name).IsRequired().HasMaxLength(100).HasColumnName("name");
            Property(x => x.IsDeleted).IsRequired().HasColumnName("isdeleted");
            Property(x => x.CreatedDate).IsRequired().HasColumnName("createddate");
            Property(x => x.UpdatedDate).IsRequired().HasColumnName("updateddate");
        }
    }
}
