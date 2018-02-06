using System;
using FPCS.Data.Enums;
using System.Collections.Generic;

namespace FPCS.Data.Entities
{
    public class HandAppeal : JournalAppeal
    {
        /// <summary>
        /// Дата регистрации в ОЗПЗ
        /// </summary>
        public DateTime OZPZRegistrationDate { get; set; }

        /// <summary>
        /// Номер обращения
        /// </summary>
        public String AppealNumber { get; set; }

        /// <summary>
        /// Номер обращения
        /// </summary>
        public String GuideNumber { get; set; }

        /// <summary>
        /// Дата регистрации в ОЗПЗ
        /// </summary>
        public DateTime GuideDate { get; set; }

        /// <summary>
        /// Адрес для обратного ответа
        /// </summary>
        public String ReceivedFeedbackAddress { get; set; }

        /// <summary>
        /// Контактный телефон заявителя
        /// </summary>
        public String ReceivedPhoneNumber { get; set; }

        /// <summary>
        /// Вид экспертизы (МЭЭ, ЭКМП)
        /// </summary>
        public String InspectionKind { get; set; }

        /// <summary>
        /// Эксперт организатор/ Эксперт КМП (Ф.И.О., код)  
        /// </summary>
        public String InspectionExpert { get; set; }

        /// <summary>
        /// Результат. Номер
        /// </summary>
        public String ReviewAppealNumber { get; set; }

        /// <summary>
        /// Результат. Код нарушения
        /// </summary>
        public String ReviewAppealCode { get; set; }

        /// <summary>
        /// Результат. Неоплата
        /// </summary>
        public Decimal ReviewAppealPenalty { get; set; }

        /// <summary>
        /// Результат. Штраф, Возврат застрахован-ному лицу, руб.
        /// </summary>
        public Decimal ReviewAppealCashback { get; set; }

        /// <summary>
        /// Комментарий
        /// </summary>
        public String Comment { get; set; }

        /// <summary>
        /// Первая часть уникального номера
        /// </summary>
        public Int32 UniqueNumberPart1 { get; set; }

        public virtual Worker Worker { get; set; }

        /// <summary>
        /// ID вида обращения
        /// </summary>
        public Int64? WorkerId { get; set; }

        /// <summary>
        /// Файл обращения
        /// </summary>
        public byte[] AppealFile { get; set; }

        /// <summary>
        /// Файл обращения
        /// </summary>
        public String AppealFileName { get; set; }

        public virtual PassedEvent PassedEvent { get; set; }

        /// <summary>
        /// ID вида обращения
        /// </summary>
        public Int64? PassedEventId { get; set; }

        public String MedicalOrganizationId { get; set; }

        public String OrganizationId { get; set; }

        /// <summary>
        /// Файл ответа
        /// </summary>
        public byte[] AnswerFile { get; set; }

        /// <summary>
        /// Файл ответа
        /// </summary>
        public String AnswerFileName { get; set; }
    }
}
