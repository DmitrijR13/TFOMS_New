using FPCS.Core.Extensions;
using FPCS.Core.Unity;
using FPCS.Data;
using FPCS.Data.Enums;
using FPCS.Web.Admin.Models.Dictionary;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FPCS.Web.Admin.Models.TypeOfAddressing
{
    public class TypeOfAddressingEditModel : DictionaryEditModel
    {
        /// <summary>
        /// Расчитывать дату окончания
        /// </summary>
        [Display(Name = "Расчитывать дату окончания")]
        public Boolean IsUpdateDate { get; set; }
    }
}