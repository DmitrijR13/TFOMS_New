using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FPCS.Data.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace FPCS.Data.Entities
{
    public class BaseEntity
    {
        public Boolean IsDeleted { get; set; }

        [Column(TypeName = "date")]
        public DateTime CreatedDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime UpdatedDate { get; set; }
    }
}
