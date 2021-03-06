﻿using System;

using FPCS.Core.jqGrid;
using FPCS.Data.Enums;

namespace FPCS.Web.Admin.Models.JournalAppeal
{
    public class JournalAppealListOptions
    {
        [GridProperty(ExtensionType.All, true, FilterOperation.Contains)]
        public String AppealUniqueNumber { get; set; }

        ///// <summary>
        ///// Дата обращения
        ///// </summary>
        //[GridProperty(ExtensionType.All, true, FilterOperation.Equal)]
        //public DateTime Date { get; set; }

        /// <summary>
        /// Тема обращения
        /// </summary>
        [GridProperty(ExtensionType.All, true, FilterOperation.Contains)]
        public String AppealTheme { get; set; }

        /// <summary>
        /// Сотрудник, принявший обращение
        /// </summary>
        [GridProperty(ExtensionType.All, true, FilterOperation.Contains)]
        public String AcceptedBy { get; set; }

        /// <summary>
        /// Сотрудник, ответсвенный за обращение
        /// </summary>
        [GridProperty(ExtensionType.All, true, FilterOperation.Contains)]
        public String Responsible { get; set; }

        /// <summary>
        /// Код обращения
        /// </summary>
        [GridProperty(ExtensionType.All, true, FilterOperation.Contains)]
        public String AppealCode { get; set; }

        /// <summary>
        /// Вид обращения
        /// </summary>
        [GridProperty(ExtensionType.All, true, FilterOperation.Contains)]
        public String AppealName { get; set; }

        /// <summary>
        /// Заявитель
        /// </summary>
        [GridProperty(ExtensionType.All, true, FilterOperation.Contains)]
        public String ReceivedTreatmentPerson { get; set; }

        /// <summary>
        /// Застрахованное лицо
        /// </summary>
        [GridProperty(ExtensionType.All, true, FilterOperation.Contains)]
        public String Applicant { get; set; }

        /// <summary>
        /// Направивший орган
        /// </summary>
        [GridProperty(ExtensionType.All, true, FilterOperation.Contains)]
        public String OrganizationsName { get; set; }

        ///// <summary>
        ///// Дата окончания срока рассмотрения обращения
        ///// </summary>
        //[GridProperty(ExtensionType.All, true, FilterOperation.Equal)]
        //public DateTime AppealPlanEndDate { get; set; }

        ///// <summary>
        ///// Дата фактического закрытия обращения
        ///// </summary>
        //[GridProperty(ExtensionType.All, true, FilterOperation.Equal)]
        //public DateTime AppealFactEndDate { get; set; }

        /// <summary>
        /// Результат обращения
        /// </summary>
        [GridProperty(ExtensionType.All, true, FilterOperation.Contains)]
        public String ResultName { get; set; }

        /// <summary>
        /// Смо
        /// </summary>
        [GridProperty(ExtensionType.All, true,  FilterOperation.Contains)]
        public String AppealOrganizationCode { get; set; }
    }
}