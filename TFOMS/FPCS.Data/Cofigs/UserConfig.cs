using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;

using FPCS.Data.Entities;

namespace FPCS.Data.Cofigs
{
    internal class UserConfig : EntityTypeConfiguration<User>
    {
        public UserConfig()
        {
            Map(x => { x.ToTable("user", "users"); x.MapInheritedProperties(); });
        }
    }
}
