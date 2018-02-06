using FPCS.Core.Unity;
using FPCS.Data;
using FPCS.Data.Repo;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FPCS.Web.Admin.Models.JournalAppeal
{
    public class JournalAppealDateAppealModel
    { 
        [Display(Name = "Фильтр обращений: Дата с")]
        public DateTime? DateFrom { get; set; }

        [Display(Name = "Дата по")]
        public DateTime? DateTo { get; set; }

        [Display(Name = "Сообщение")]
        public String Message { get; set; }
    }
}