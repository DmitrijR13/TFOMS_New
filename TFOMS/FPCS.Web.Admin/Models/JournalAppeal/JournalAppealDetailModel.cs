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

namespace FPCS.Web.Admin.Models.JournalAppeal
{
    public class JournalAppealDetailModel
    {
        public Int64 Id { get; set; }

        /// <summary>
        /// Уникальный номер обращения
        /// </summary>
        [Display(Name = "Уникальный номер обращения")]
        public String AppealUniqueNumber { get; set; }

        /// <summary>
        /// Дата поступления обращения
        /// </summary>
        [Display(Name = "Дата поступления обращения")]
        public String Date { get; set; }

        /// <summary>
        /// Время поступления обращения
        /// </summary>
        [Display(Name = "Время поступления обращения")]
        public String Time { get; set; }

        /// <summary>
        /// Истчоник поступления
        /// </summary>
        [Display(Name = "Источник поступления")]
        public String SourceIncomeName { get; set; }

        /// <summary>
        /// Наименование организации поступления
        /// </summary>
        [Display(Name = "Наименование организации поступления")]
        public String OrganizationName { get; set; }

        /// <summary>
        /// способ обращения
        /// </summary>
        [Display(Name = "Способ обращения")]
        public String TypeOfAddressingName { get; set; }

        /// <summary>
        /// вид обращения
        /// </summary>
        [Display(Name = "Вид обращения")]
        public String WayOfAddressingName { get; set; }

        /// <summary>
        /// Тема обращения
        /// </summary>
        [Display(Name = "Тема обращения")]
        public String AppealTheme { get; set; }

        /// <summary>
        /// Содержание обращения
        /// </summary>
        [Display(Name = "Содержание обращения")]
        public String AppealContent { get; set; }

        /// <summary>
        /// жалоба
        /// </summary>
        [Display(Name = "Жалоба")]
        public String ComplaintName { get; set; }


        /// <summary>
        /// организация, ответственная за работу с обращением
        /// </summary>
        [Display(Name = "Организация, ответственная за работу с обращением")]
        public String AppealOrganizationName { get; set; }

        /// <summary>
        /// Код организации, ответственной за работу с обращением
        /// </summary>
        [Display(Name = "Код организации, ответственной за работу с обращением")]
        public String AppealOrganizationCode { get; set; }

        /// <summary>
        /// Линии принятия обращения
        /// </summary>
        [Display(Name = "Линия принятия обращения")]
        public String TakingAppealLineName { get; set; }

        /// <summary>
        /// Сотрудник, принявший обращение
        /// </summary>
        [Display(Name = "Сотрудник, принявший обращение")]
        public String AcceptedBy { get; set; }

        /// <summary>
        /// Линии рассмотрения обращения
        /// </summary>
        [Display(Name = "Линия рассмотрения обращения")]
        public String ReviewAppealLineName { get; set; }

        /// <summary>
        /// ID Сотрудника, ответсвенного за работу с обращением
        /// </summary>
        [Display(Name = "Сотрудник, ответственный за работу с обращением")]
        public String Responsible { get; set; }

        /// <summary>
        /// Дата окончания срока рассмотрения обращения
        /// </summary>
        [Display(Name = "Дата окончания срока рассмотрения обращения")]
        public String AppealPlanEndDate { get; set; }

        /// <summary>
        /// Дата фактического закрытия обращения
        /// </summary>
        [Display(Name = "Дата фактического закрытия обращения")]
        public String AppealFactEndDate { get; set; }

        /// <summary>
        /// Результат обращения
        /// </summary>
        [Display(Name = "Результат обращения")]
        public String AppealResultName { get; set; }

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
        /// Дата рождения заявителя
        /// </summary>
        [Display(Name = "Дата рождения")]
        public String ApplicantBirthDate { get; set; }

        /// <summary>
        /// ЕНП заявителя
        /// </summary>
        [Display(Name = "ЕНП")]
        public String ApplicantENP { get; set; }

        /// <summary>
        /// Страховая принадлежность заявителя
        /// </summary>
        [Display(Name = "Страховая принадлежность")]
        public String ApplicantSMO { get; set; }

        /// <summary>
        /// Тип документа, удостоверяющего личность заявителя
        /// </summary>
        [Display(Name = "Тип документа, удостоверяющего личность")]
        public String ApplicantTypeDocument { get; set; }

        /// <summary>
        /// Серия документа, удостоверяющего личность заявителя
        /// </summary>
        [Display(Name = "Серия документа, удостоверяющего личность")]
        public String ApplicantDocumentSeries { get; set; }

        /// <summary>
        /// Номер документа, удостоверяющего личность заявителя
        /// </summary>
        [Display(Name = "Номер документа, удостоверяющего личность")]
        public String ApplicantDocumentNumber { get; set; }

        /// <summary>
        /// Страховая принадлежность заявителя
        /// </summary>
        [Display(Name = "Адрес для обратного ответа")]
        public String ApplicantFeedbackAddress { get; set; }

        /// <summary>
        /// Контактный телефон заявителя
        /// </summary>
        [Display(Name = "Контактный телефон")]
        public String ApplicantPhoneNumber { get; set; }

        /// <summary>
        /// Адрес электронной почты заявителя
        /// </summary>
        [Display(Name = "Адрес электронной почты")]
        public String ApplicantEmail { get; set; }

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
        /// Дата рождения заявлица, в отношении которого поступило обращениеителя
        /// </summary>
        [Display(Name = "Дата рождения")]
        public String ReceivedTreatmentPersonBirthDate { get; set; }

        /// <summary>
        /// ЕНП лица, в отношении которого поступило обращение
        /// </summary>
        [Display(Name = "ЕНП")]
        public String ReceivedTreatmentPersonENP { get; set; }

        /// <summary>
        /// Страховая принадлежность лица, в отношении которого поступило обращение
        /// </summary>
        [Display(Name = "Страховая принадлежность")]
        public String ReceivedTreatmentPersonSMO { get; set; }

        /// <summary>
        /// Тип документа, удостоверяющего личность лица, в отношении которого поступило обращение
        /// </summary>
        [Display(Name = "Тип документа, удостоверяющего личность")]
        public String ReceivedTreatmentPersonypeDocument { get; set; }

        /// <summary>
        /// Серия документа, удостоверяющего личность лица, в отношении которого поступило обращение
        /// </summary>
        [Display(Name = "Серия документа, удостоверяющего личность")]
        public String ReceivedTreatmentPersonDocumentSeries { get; set; }

        /// <summary>
        /// Номер документа, удостоверяющего личность лица, в отношении которого поступило обращение
        /// </summary>
        [Display(Name = "Номер документа, удостоверяющего личность")]
        public String ReceivedTreatmentPersonDocumentNumber { get; set; }
    }
}