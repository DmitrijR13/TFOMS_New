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
    public class UniqueNumberModel
    {
        /// <summary>
        /// Уникальный номер обращения
        /// </summary>
        public String AppealUniqueNumber { get; set; }

        /// <summary>
        /// Первая часть уникального номера
        /// </summary>
        public Int32 UniqueNumberPart1 { get; set; }

        /// <summary>
        /// Вторая часть уникального номера
        /// </summary>
        public Int32 UniqueNumberPart2 { get; set; }

        /// <summary>
        /// Третья часть уникального номера
        /// </summary>
        public Int32 UniqueNumberPart3 { get; set; }
    }
}