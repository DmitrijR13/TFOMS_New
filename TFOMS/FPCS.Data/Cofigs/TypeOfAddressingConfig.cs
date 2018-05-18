using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;

using FPCS.Data.Entities;

namespace FPCS.Data.Cofigs
{
    internal class TypeOfAddressingConfig : EntityTypeConfiguration<TypeOfAddressing>
    {
        public TypeOfAddressingConfig()
        {
            ToTable("typeofaddressing");
            HasKey(x => x.TypeOfAddressingId);
            Property(x => x.TypeOfAddressingId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity).HasColumnName("typeofaddressingid");

            Property(x => x.Code).IsRequired().HasMaxLength(3).HasColumnName("code");
            Property(x => x.Name).IsRequired().HasMaxLength(100).HasColumnName("name");
            Property(x => x.IsUpdateDateEnd).HasColumnName("is_update_date_end");
            Property(x => x.IsDeleted).IsRequired().HasColumnName("isdeleted");
            Property(x => x.CreatedDate).IsRequired().HasColumnName("createddate");
            Property(x => x.UpdatedDate).IsRequired().HasColumnName("updateddate");
        }
    }
}
