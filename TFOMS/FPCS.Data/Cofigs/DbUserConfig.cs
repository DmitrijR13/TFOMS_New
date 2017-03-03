using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;

using FPCS.Data.Entities;

namespace FPCS.Data.Cofigs
{
    internal class DbUserConfig : EntityTypeConfiguration<DbUser>
    {
        public DbUserConfig()
        {
            HasKey(x => x.DbUserId);
            Property(x => x.DbUserId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity).HasColumnName("dbuserid");

            Property(x => x.UnqDbUserId).IsRequired().HasColumnName("unqdbuserid");

            Property(x => x.Login).IsRequired().HasMaxLength(100).HasColumnName("login");
            Property(x => x.Password).IsRequired().HasMaxLength(100).HasColumnName("password");
            Property(x => x.Email).HasMaxLength(100).HasColumnName("email");
            Property(x => x.FirstName).IsRequired().HasMaxLength(100).HasColumnName("firstname");
            Property(x => x.LastName).IsRequired().HasMaxLength(100).HasColumnName("lastname");
            Property(x => x.FullName).IsRequired().HasMaxLength(200).HasColumnName("fullname");
            Property(x => x.MiddleInitial).HasMaxLength(50).HasColumnName("middleinitial");
            Property(x => x.IsLocked).IsRequired().HasColumnName("islocked");
            Property(x => x.IsDeleted).IsRequired().HasColumnName("isdeleted");
            Property(x => x.CreatedDate).IsRequired().HasColumnName("createddate");
            Property(x => x.UpdatedDate).IsRequired().HasColumnName("updateddate");

            Property(x => x.Role).IsRequired().HasColumnName("role");
        }
    }
}
