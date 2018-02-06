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

namespace FPCS.Web.Admin.Models.SMO
{
    public class SMOEditModel
    {
        [Required]
        public Int64 Id { get; set; }

        /// <summary>
        /// Код СМО
        /// </summary>
        [MaxLength(10)]
        [Required]
        [Display(Name = "Код СМО")]
        public String SmoCode { get; set; }

        /// <summary>
        /// КПП - код причины постановки на учет
        /// </summary>
        [MaxLength(9)]
        [Required]
        [Display(Name = "КПП")]
        public String KPP { get; set; }

        /// <summary>
        /// Полное наименование
        /// </summary>
        [MaxLength(100)]
        [Required]
        [Display(Name = "Полное наименование")]
        public String FullName { get; set; }

        /// <summary>
        /// Краткое наименование
        /// </summary>
        [MaxLength(100)]
        [Required]
        [Display(Name = "Краткое наименование")]
        public String ShortName { get; set; }

        /// <summary>
        /// Фактисекий адрес
        /// </summary>
        [MaxLength(150)]
        [Required]
        [Display(Name = "Фактический адрес")]
        public String FactAddress { get; set; }

        /// <summary>
        /// ФИО, № телефона, факса, адрес электронной почты руководителя
        /// </summary>
        [MaxLength(150)]
        [Required]
        [Display(Name = "Данные о руководителе")]
        public String Director { get; set; }

        /// <summary>
        /// ФИО, № телефона, факса, адрес электронной почты руководителя обособленного подразделения (филиала)
        /// </summary>
        [MaxLength(150)]
        [Display(Name = "Данные о руководителе филиала")]
        public String FilialDirector { get; set; }

        /// <summary>
        /// Сведения о лицензии
        /// </summary>
        [MaxLength(100)]
        [Required]
        [Display(Name = "Сведения о лицензии")]
        public String LicenseInfo { get; set; }

        /// <summary>
        /// Фамилия руководителя
        /// </summary>
        [Display(Name = "Фамилия руководителя")]

        public String HeadSurname { get; set; }

        /// <summary>
        /// Имя руководителя
        /// </summary>
        [Display(Name = "Имя руководителя")]

        public String HeadName { get; set; }

        /// <summary>
        /// Отчество руководителя
        /// </summary>
        [Display(Name = "Отчество руководителя")]

        public String HeadSecondName { get; set; }

        /// <summary>
        /// Должность руководителя
        /// </summary>
        [Display(Name = "Должность руководителя")]

        public String HeadPosition { get; set; }
    }
}