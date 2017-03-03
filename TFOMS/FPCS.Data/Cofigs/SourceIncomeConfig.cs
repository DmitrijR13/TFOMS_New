using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;

using FPCS.Data.Entities;

namespace FPCS.Data.Cofigs
{
    internal class SourceIncomeConfig : EntityTypeConfiguration<SourceIncome>
    {
        public SourceIncomeConfig()
        {
            ToTable("sourceincome");
            HasKey(x => x.SourceIncomeId);
            Property(x => x.SourceIncomeId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity).HasColumnName("sourceincomeid");

            Property(x => x.Code).IsRequired().HasMaxLength(3).HasColumnName("code");
            Property(x => x.Name).IsRequired().HasMaxLength(100).HasColumnName("name");
            Property(x => x.IsDeleted).IsRequired().HasColumnName("isdeleted");
            Property(x => x.CreatedDate).IsRequired().HasColumnName("createddate");
            Property(x => x.UpdatedDate).IsRequired().HasColumnName("updateddate");
        }
    }
}
