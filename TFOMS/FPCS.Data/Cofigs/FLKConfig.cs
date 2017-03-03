using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;

using FPCS.Data.Entities;

namespace FPCS.Data.Cofigs
{
    internal class FLKConfig : EntityTypeConfiguration<FLK>
    {
        public FLKConfig()
        {
            ToTable("flk");
            HasKey(x => x.FlkId);
            Property(x => x.FlkId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity).HasColumnName("flkid");

            Property(x => x.BaseFileName).IsRequired().HasColumnName("basefilename");
            Property(x => x.ErrorCode).IsRequired().HasColumnName("errorcode");
            Property(x => x.FieldName).HasColumnName("fieldname");
            Property(x => x.BaseEntitiyName).HasColumnName("baseentitiyname");
            Property(x => x.AppealNumber).HasColumnName("appealnumber");
            Property(x => x.Comment).HasColumnName("comment");
            Property(x => x.SmoRegNum).HasMaxLength(8).HasColumnName("rnsmo");

            Property(x => x.IsDeleted).IsRequired().HasColumnName("isdeleted");
            Property(x => x.CreatedDate).IsRequired().HasColumnName("createddate");
            Property(x => x.UpdatedDate).IsRequired().HasColumnName("updateddate");
        }
    }
}
