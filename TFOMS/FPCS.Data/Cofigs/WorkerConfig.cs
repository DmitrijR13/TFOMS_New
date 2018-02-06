using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;

using FPCS.Data.Entities;

namespace FPCS.Data.Cofigs
{
    internal class WorkerConfig : EntityTypeConfiguration<Worker>
    {
        public WorkerConfig()
        {
            ToTable("worker");
            HasKey(x => x.WorkerId);
            Property(x => x.WorkerId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity).HasColumnName("worker_id");

            Property(x => x.Surname).IsRequired().HasMaxLength(100).HasColumnName("surname");
            Property(x => x.Name).IsRequired().HasMaxLength(100).HasColumnName("name");
            Property(x => x.Phone).HasMaxLength(100).HasColumnName("phone");
            Property(x => x.IsHead).HasColumnName("is_head");
            Property(x => x.SecondName).HasMaxLength(100).HasColumnName("secondname");
            Property(x => x.IsDeleted).IsRequired().HasColumnName("isdeleted");
            Property(x => x.CreatedDate).IsRequired().HasColumnName("createddate");
            Property(x => x.UpdatedDate).IsRequired().HasColumnName("updateddate");
        }
    }
}
