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

namespace FPCS.Web.Admin.Models.Worker
{
    public class WorkerEditModel
    {
        [Required]
        public Int64 Id { get; set; }

        /// <summary>
        /// Фамилия
        /// </summary>
        [MaxLength(10)]
        [Required]
        [Display(Name = "Фамилия")]
        public String Surname { get; set; }

        /// <summary>
        /// Имя
        /// </summary>
        [MaxLength(9)]
        [Required]
        [Display(Name = "Имя")]
        public String Name { get; set; }

        /// <summary>
        /// Отчество
        /// </summary>
        [MaxLength(100)]
        [Display(Name = "Отчество")]
        public String SecondName { get; set; }

        /// <summary>
        /// Глава
        /// </summary>
        [Display(Name = "Начальник")]
        public Boolean IsHead { get; set; }

        /// <summary>
        /// Телефон
        /// </summary>
        [Display(Name = "Телефон")]
        public String Phone { get; set; }
    }
}