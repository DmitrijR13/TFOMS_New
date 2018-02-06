using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FPCS.Web.Admin.Models.SMO
{
    public class SMOIndexModel
    {
        /// <summary>
        /// ID записи
        /// </summary>
        public Int64 Id { get; set; }

        /// <summary>
        /// Код СМО
        /// </summary>
        public String SmoCode { get; set; }

        /// <summary>
        /// КПП - код причины постановки на учет
        /// </summary>
        public String KPP { get; set; }

        /// <summary>
        /// Полное наименование
        /// </summary>
        public String FullName { get; set; }

        /// <summary>
        /// Краткое наименование
        /// </summary>
        public String ShortName { get; set; }

        /// <summary>
        /// Фактисекий адрес
        /// </summary>
        public String FactAddress { get; set; }

        /// <summary>
        /// ФИО, № телефона, факса, адрес электронной почты руководителя
        /// </summary>
        public String Director { get; set; }

        /// <summary>
        /// ФИО, № телефона, факса, адрес электронной почты руководителя обособленного подразделения (филиала)
        /// </summary>
        public String FilialDirector { get; set; }

        /// <summary>
        /// Сведения о лицензии
        /// </summary>
        public String LicenseInfo { get; set; }

        /// <summary>
        /// Фамилия руководителя
        /// </summary>
        public String HeadSurname { get; set; }

        /// <summary>
        /// Имя руководителя
        /// </summary>
        public String HeadName { get; set; }

        /// <summary>
        /// Отчетсво руководителя
        /// </summary>
        public String HeadSecondName { get; set; }

        /// <summary>
        /// Должность руководителя
        /// </summary>
        public String HeadPosition { get; set; }
    }
}