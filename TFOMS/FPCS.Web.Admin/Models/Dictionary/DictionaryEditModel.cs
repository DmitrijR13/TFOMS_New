using FPCS.Core.Extensions;
using FPCS.Core.Unity;
using FPCS.Data;
using FPCS.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FPCS.Web.Admin.Models.Dictionary
{
    public class DictionaryEditModel
    {
        [Required]
        public Int64 Id { get; set; }

        [Required]
        [Display(Name = "Код")]
        public String Code { get; set; }

        [Required]
        [Display(Name = "Наименование")]
        public String Name { get; set; }
    }
}