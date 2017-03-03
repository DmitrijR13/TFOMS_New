using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;

using FPCS.Data.Entities;

namespace FPCS.Data.Cofigs
{
    internal class ThemeAppealCitizensConfig : EntityTypeConfiguration<ThemeAppealCitizens>
    {
        public ThemeAppealCitizensConfig()
        {
            ToTable("themeappealcitizens");
            HasKey(x => x.ThemeAppealCitizensId);
            Property(x => x.ThemeAppealCitizensId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity).HasColumnName("themeappealcitizensid");

            Property(x => x.Code).IsRequired().HasMaxLength(8).HasColumnName("code");
            Property(x => x.Name).IsRequired().HasMaxLength(100).HasColumnName("name");
            Property(x => x.IsDeleted).IsRequired().HasColumnName("isdeleted");
            Property(x => x.CreatedDate).IsRequired().HasColumnName("createddate");
            Property(x => x.UpdatedDate).IsRequired().HasColumnName("updateddate");
        }
    }
}
