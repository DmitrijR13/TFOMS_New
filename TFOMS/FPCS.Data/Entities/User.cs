using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FPCS.Data.Entities
{
    public class User : DbUser
    {

        /// <summary>
        /// ID Линии принятия обращения
        /// </summary>
        [Column("smoid")]
        public Int64? SMOId { get; set; }

        public virtual SMO SMO { get; set; }
    }
}
