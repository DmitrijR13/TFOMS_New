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
    public class HandAppealPrintModel
    {
        /// <summary>
        /// Уникальный номер обращения
        /// </summary>
        public String AppealUniqueNumber { get; set; }

        /// <summary>
        /// Дата поступления
        /// </summary>
        public DateTime AppealDate { get; set; }

        /// <summary>
        /// Номер обращения
        /// </summary>
        public String AppealNumber { get; set; }

        /// <summary>
        /// Дата регистрации в ОЗПЗ
        /// </summary>
        public DateTime OZPZRegistrationDate { get; set; }

        /// <summary>
        /// Наименование организации
        /// </summary>
        public String AppealOrganizationName { get; set; }

        /// <summary>
        /// Номер запроса
        /// </summary>
        public String GuideNumber { get; set; }

        /// <summary>
        /// Дата исполнения
        /// </summary>
        public DateTime GuideDate { get; set; }

        /// <summary>
        /// Заявитеь
        /// </summary>
        public String Applicant { get; set; }

        /// <summary>
        /// Страховая принадлежность заявителя
        /// </summary>
        public String ApplicantFeedbackAddress { get; set; }

        /// <summary>
        /// Контактный телефон заявителя
        /// </summary>
        public String ApplicantPhoneNumber { get; set; }

        /// <summary>
        /// Ф.И.О. застрахованного лица, в интересах которого направлено обращение
        /// </summary>
        public String ReceivedTreatmentPerson { get; set; }

        /// <summary>
        /// СМО, ответственная за работу с обращением
        /// </summary>
        public String SMOOrganizationName { get; set; }

        /// <summary>
        /// вид обращения
        /// </summary>
        public String WayOfAddressing { get; set; }

        /// <summary>
        /// Наименование МО
        /// </summary>
        public String OrganizationName { get; set; }

        /// <summary>
        /// Проведенные мероприятия
        /// </summary>
        public String PassedEvent { get; set; }

        /// <summary>
        /// Признак обоснованности
        /// </summary>
        public String ComplaintName { get; set; }

        /// <summary>
        /// Дата фактического закрытия обращения
        /// </summary>
        public DateTime? AppealFactEndDate { get; set; }

        /// <summary>
        /// Сотрудник, принявший обращение
        /// </summary>
        public String Worker { get; set; }

        /// <summary>
        /// Тема обращения
        /// </summary>
        public String AppealTheme { get; set; }
    }
}