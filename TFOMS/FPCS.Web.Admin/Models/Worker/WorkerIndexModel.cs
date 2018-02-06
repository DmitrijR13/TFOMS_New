using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FPCS.Web.Admin.Models.Worker
{
    public class WorkerIndexModel
    {
        /// <summary>
        /// ID записи
        /// </summary>
        public Int64 Id { get; set; }

        /// <summary>
        /// ФИО сотрудника
        /// </summary>
        public String FIO { get; set; }

        /// <summary>
        /// Телефон
        /// </summary>
        public String Phone { get; set; }

    }
}