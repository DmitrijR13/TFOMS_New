using System;
using FPCS.Data.Enums;
using System.Collections.Generic;

namespace FPCS.Data.Entities
{
    public class JournalAppeal : BaseEntity
    {
        /// <summary>
        /// Id обращения
        /// </summary>
        public Int64 JournalAppealId { get; set; }

        /// <summary>
        /// Id обращения
        /// </summary>
        public String AppealUniqueNumber { get; set; }

        /// <summary>
        /// дата поступления обращения
        /// </summary>
        public DateTimeOffset Date { get; set; }

        /// <summary>
        /// время поступления обращения
        /// </summary>
        public String Time { get; set; }

        /// <summary>
        /// ID истчоника поступления
        /// </summary>
        public Int64 SourceIncomeId { get; set; }

        public virtual SourceIncome SourceIncome { get; set; }

        /// <summary>
        /// Наименование организации поступления
        /// </summary>
        public String OrganizationName { get; set; }

        /// <summary>
        /// ID способа обращения
        /// </summary>
        public Int64 TypeOfAddressingId { get; set; }

        public virtual TypeOfAddressing TypeOfAddressing { get; set; }

        /// <summary>
        /// ID вида обращения
        /// </summary>
        public Int64 WayOfAddressingId { get; set; }

        public virtual WayOfAddressing WayOfAddressing { get; set; }


        public Int64 ThemeAppealCitizensId { get; set; }

        public virtual ThemeAppealCitizens ThemeAppealCitizens { get; set; }

        /// <summary>
        /// Содержание обращения
        /// </summary>
        public String AppealContent { get; set; }

        /// <summary>
        /// ID жалобы
        /// </summary>
        public Int64? ComplaintId { get; set; }

        public virtual Complaint Complaint { get; set; }

        /// <summary>
        /// ID организация, ответственная за работу с обращением
        /// </summary>
        public Int64 AppealOrganizationId { get; set; }

        public virtual AppealOrganization AppealOrganization { get; set; }

        public Int64? SMOId { get; set; }

        public virtual SMO SMO { get; set; }

        /// <summary>
        /// Сотрудника, принявшего обращение
        /// </summary>
        public String AcceptedBy { get; set; }

        /// <summary>
        /// ID Линии принятия обращения
        /// </summary>
        public Int64 TakingAppealLineId { get; set; }

        public virtual TakingAppealLine TakingAppealLine { get; set; }

        /// <summary>
        /// Сотрудника, принявшего обращение
        /// </summary>
        public String AppealOrganizationCode { get; set; }

        /// <summary>
        /// ID Линии рассмотрения обращения
        /// </summary>
        public Int64? ReviewAppealLineId { get; set; }

        public virtual ReviewAppealLine ReviewAppealLine { get; set; }

        /// <summary>
        /// ID Сотрудника, ответсвенного за работу с обращением
        /// </summary>
        public String Responsible { get; set; }

        /// <summary>
        /// Дата окончания срока рассмотрения обращения
        /// </summary>
        public DateTimeOffset AppealPlanEndDate { get; set; }

        /// <summary>
        /// Дата фактического закрытия обращения
        /// </summary>
        public DateTimeOffset? AppealFactEndDate { get; set; }

        /// <summary>
        /// ID Результата обращения
        /// </summary>
        public Int64? AppealResultId { get; set; }

        public virtual AppealResult AppealResult { get; set; }

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
        /// Дата рождения заявителя
        /// </summary>
        public DateTimeOffset? ApplicantBirthDate { get; set; }

        /// <summary>
        /// ЕНП заявителя
        /// </summary>
        public String ApplicantENP { get; set; }

        /// <summary>
        /// Страховая принадлежность заявителя
        /// </summary>
        public String ApplicantSMO { get; set; }

        /// <summary>
        /// Тип документа, удостоверяющего личность заявителя
        /// </summary>
        public String ApplicantTypeDocument { get; set; }

        /// <summary>
        /// Серия документа, удостоверяющего личность заявителя
        /// </summary>
        public String ApplicantDocumentSeries { get; set; }

        /// <summary>
        /// Номер документа, удостоверяющего личность заявителя
        /// </summary>
        public String ApplicantDocumentNumber { get; set; }

        /// <summary>
        /// Страховая принадлежность заявителя
        /// </summary>
        public String ApplicantFeedbackAddress{ get; set; }

        /// <summary>
        /// Контактный телефон заявителя
        /// </summary>
        public String ApplicantPhoneNumber { get; set; }

        /// <summary>
        /// Адрес электронной почты заявителя
        /// </summary>
        public String ApplicantEmail { get; set; }

        /// <summary>
        /// Фамилия лица, в отношении которого поступило обращение
        /// </summary>
        public String ReceivedTreatmentPersonSurname { get; set; }

        /// <summary>
        /// Имя лица, в отношении которого поступило обращение
        /// </summary>
        public String ReceivedTreatmentPersonName { get; set; }

        /// <summary>
        /// Отчество лица, в отношении которого поступило обращение
        /// </summary>
        public String ReceivedTreatmentPersonSecondName { get; set; }

        /// <summary>
        /// Дата рождения заявлица, в отношении которого поступило обращениеителя
        /// </summary>
        public DateTimeOffset? ReceivedTreatmentPersonBirthDate { get; set; }

        /// <summary>
        /// ЕНП лица, в отношении которого поступило обращение
        /// </summary>
        public String ReceivedTreatmentPersonENP { get; set; }

        /// <summary>
        /// Страховая принадлежность лица, в отношении которого поступило обращение
        /// </summary>
        public String ReceivedTreatmentPersonSMO { get; set; }

        /// <summary>
        /// Тип документа, удостоверяющего личность лица, в отношении которого поступило обращение
        /// </summary>
        public String ReceivedTreatmentPersonypeDocument { get; set; }

        /// <summary>
        /// Серия документа, удостоверяющего личность лица, в отношении которого поступило обращение
        /// </summary>
        public String ReceivedTreatmentPersonDocumentSeries { get; set; }

        /// <summary>
        /// Номер документа, удостоверяющего личность лица, в отношении которого поступило обращение
        /// </summary>
        public String ReceivedTreatmentPersonDocumentNumber { get; set; }
    }
}
