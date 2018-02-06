using FPCS.Core.Unity;
using FPCS.Data;
using FPCS.Data.Repo;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FPCS.Web.Admin.Models.Report
{
    public class ReportIndexModel
    {
        public SelectList Smos { get; set; }

        [Required]
        [Display(Name = "СМО")]
        public Int64? SmoId { get; set; }

        [Display(Name = "Дата с")]
        public DateTime DateFrom { get; set; }

        [Display(Name = "Дата по")]
        public DateTime DateTo { get; set; }

        [Display(Name = "Дата с")]
        public DateTime DateFromTemp { get; set; }

        [Display(Name = "Дата по")]
        public DateTime DateToTemp { get; set; }

        public void Init()
        {
            using (var uow = UnityManager.Instance.Resolve<IUnitOfWork>())
            {
                var smo = uow.GetRepo<ISMORepo>();
                var listSmo = smo.GetAll().Select(x => new Lookup<Int64> { Value = x.SmoId, Text = x.ShortName }).ToList();
                Smos = new SelectList(listSmo, "Value", "Text");
            }
        }
    }
}