using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;

using FPCS.Data.Entities;

namespace FPCS.Data.Cofigs
{
    internal class SMOConfig : EntityTypeConfiguration<SMO>
    {
        public SMOConfig()
        {
            ToTable("smo");
            HasKey(x => x.SmoId);
            Property(x => x.SmoId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(x => x.SmoCode).IsRequired();
            Property(x => x.KPP).IsRequired();
            Property(x => x.FullName);
            Property(x => x.ShortName);
            Property(x => x.FactAddress);
            Property(x => x.Director);
            Property(x => x.FilialDirector);
            Property(x => x.LicenseInfo);
        }
    }
}
