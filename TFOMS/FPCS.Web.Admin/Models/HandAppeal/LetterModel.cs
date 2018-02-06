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
    public class HandAppealLetterModel
    {
        /// <summary>
        /// Id организации
        /// </summary>
        public Int64? SMOOrganizationId { get; set; }


        /// <summary>
        /// Фамилия заявителя
        /// </summary>
        public String ApplicantSurname { get; set; }

        /// <summary>
        /// Имя заявителя
        /// </summary>
        public String ApplicantName { get; set; }

        /// <summary>
        /// Отчество заявителя
        /// </summary>
        public String ApplicantSecondName { get; set; }

        /// <summary>
        /// Адрес заявителя
        /// </summary>
        public String ApplicantFeedbackAddress { get; set; }

        /// <summary>
        /// Email заявителя
        /// </summary>
        public String ApplicantEmail { get; set; }

        /// <summary>
        /// Id организации
        /// </summary>
        public Int64? WorkerId { get; set; }
    }
}