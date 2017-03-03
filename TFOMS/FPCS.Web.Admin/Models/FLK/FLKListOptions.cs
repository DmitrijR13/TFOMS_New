using System;

using FPCS.Core.jqGrid;
using FPCS.Data.Enums;

namespace FPCS.Web.Admin.Models.FLK
{
    public class FLKListOptions
    {
        [GridProperty(ExtensionType.All, true, FilterOperation.Contains)]
        public String BaseFileName { get; set; }

        /// <summary>
        /// Код ошибки
        /// </summary>
        [GridProperty(ExtensionType.All, true, FilterOperation.Contains)]
        public String ErrorCode { get; set; }

        /// <summary>
        /// Имя поля
        /// </summary>
        [GridProperty(ExtensionType.All, true, FilterOperation.Contains)]
        public String FieldName { get; set; }

        /// <summary>
        /// Имя базового элемента
        /// </summary>
        [GridProperty(ExtensionType.All, true, FilterOperation.Contains)]
        public String BaseEntitiyName { get; set; }

        /// <summary>
        /// Номер обращения
        /// </summary>
        [GridProperty(ExtensionType.All, true, FilterOperation.Contains)]
        public String AppealNumber { get; set; }

        /// <summary>
        /// Комментарий
        /// </summary>
        [GridProperty(ExtensionType.All, true, FilterOperation.Contains)]
        public String Comment { get; set; }

        /// <summary>
        /// Реестровый номер СМО
        /// </summary>
        [GridProperty(ExtensionType.All, true, FilterOperation.Contains)]
        public String SmoRegNum { get; set; }
    }
}