using FPCS.Core.Extensions;
using FPCS.Core.Unity;
using FPCS.Data;
using FPCS.Data.Enums;
using FPCS.Data.Repo;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FPCS.Web.Admin.Models.HandAppeal
{
    public class GenerateUniqueNumberModel
    {
        /// <summary>
        /// Фамилия заявителя
        /// </summary>
        [Display(Name = "Фамилия")]
        public String ApplicantSurname { get; set; }

        /// <summary>
        /// Имя заявителя
        /// </summary>
        [Display(Name = "Имя")]
        public String ApplicantName { get; set; }

        /// <summary>
        /// Отчество заявителя
        /// </summary>
        [Display(Name = "Отчество")]
        public String ApplicantSecondName { get; set; }

        /// <summary>
        /// Фамилия лица, в отношении которого поступило обращение
        /// </summary>
        [Display(Name = "Фамилия")]
        public String ReceivedTreatmentPersonSurname { get; set; }

        /// <summary>
        /// Имя лица, в отношении которого поступило обращение
        /// </summary>
        [Display(Name = "Имя")]
        public String ReceivedTreatmentPersonName { get; set; }

        /// <summary>
        /// Отчество лица, в отношении которого поступило обращение
        /// </summary>
        [Display(Name = "Отчество")]
        public String ReceivedTreatmentPersonSecondName { get; set; }

        /// <summary>
        /// Отчество лица, в отношении которого поступило обращение
        /// </summary>
        [Display(Name = "Отчество")]
        public Int32? Id { get; set; }
    }
}