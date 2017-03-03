using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FPCS.Web.Admin.Models.JournalAppeal
{
    public class JournalAppealIndexModel
    {
        /// <summary>
        /// ID записи
        /// </summary>
        public Int64 Id { get; set; }

        /// <summary>
        /// Уникальный номер обращения
        /// </summary>
        public String AppealUniqueNumber { get; set; }

        /// <summary>
        /// Дата обращения
        /// </summary>
        public String Date { get; set; }

        /// <summary>
        /// Тема обращения
        /// </summary>
        public String AppealTheme { get; set; }

        /// <summary>
        /// Сотрудник, принявший обращение
        /// </summary>
        public String AcceptedBy { get; set; }

        /// <summary>
        /// Сотрудник, ответсвенный за обращение
        /// </summary>
        public String Responsible { get; set; }

        /// <summary>
        /// Дата окончания срока рассмотрения обращения
        /// </summary>
        public String AppealPlanEndDate { get; set; }

        /// <summary>
        /// Дата фактического закрытия обращения
        /// </summary>
        public String AppealFactEndDate { get; set; }

        /// <summary>
        /// Результат обращения
        /// </summary>
        public String ResultName { get; set; }

        /// <summary>
        /// Код организации, ответственной за работу с обращением
        /// </summary>
        public String AppealOrganizationCode { get; set; }
    }
}