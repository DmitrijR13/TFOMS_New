using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FPCS.Web.Admin.Models.Dictionary
{
    public class DictionaryIndexModel
    {
        /// <summary>
        /// ID записи
        /// </summary>
        public Int64 Id { get; set; }

        /// <summary>
        /// Код записи
        /// </summary>
        public String Code { get; set; }

        /// <summary>
        /// Наименование записи
        /// </summary>
        public String Name { get; set; }

    }
}