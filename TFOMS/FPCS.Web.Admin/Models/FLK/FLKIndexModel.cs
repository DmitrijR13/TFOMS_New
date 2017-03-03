using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FPCS.Web.Admin.Models.FLK
{
    public class FLKIndexModel
    {
        /// <summary>
        /// Id записи
        /// </summary>
        public Int64 Id { get; set; }

        /// <summary>
        /// Имя исходного файла
        /// </summary>
        public String BaseFileName { get; set; }

        /// <summary>
        /// Код ошибки
        /// </summary>
        public String ErrorCode { get; set; }

        /// <summary>
        /// Имя поля
        /// </summary>
        public String FieldName { get; set; }

        /// <summary>
        /// Имя базового элемента
        /// </summary>
        public String BaseEntitiyName { get; set; }

        /// <summary>
        /// Номер обращения
        /// </summary>
        public String AppealNumber { get; set; }

        /// <summary>
        /// Комментарий
        /// </summary>
        public String Comment { get; set; }

        /// <summary>
        /// Реестровый номер СМО 
        /// </summary>
        public String SmoRegNum { get; set; }
    }
}