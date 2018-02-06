using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;

using FPCS.Data.Entities;

namespace FPCS.Data.Cofigs
{
    internal class PassedEventConfig : EntityTypeConfiguration<PassedEvent>
    {
        public PassedEventConfig()
        {
            ToTable("passed_event");
            HasKey(x => x.PassedEventId);
            Property(x => x.PassedEventId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity).HasColumnName("passed_event_id");

            Property(x => x.Code).IsRequired().HasMaxLength(3).HasColumnName("code");
            Property(x => x.Name).IsRequired().HasMaxLength(100).HasColumnName("name");
            Property(x => x.IsDeleted).IsRequired().HasColumnName("isdeleted");
            Property(x => x.CreatedDate).IsRequired().HasColumnName("createddate");
            Property(x => x.UpdatedDate).IsRequired().HasColumnName("updateddate");
        }
    }
}
