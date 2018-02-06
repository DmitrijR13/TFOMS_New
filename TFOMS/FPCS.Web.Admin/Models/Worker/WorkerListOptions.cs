using System;

using FPCS.Core.jqGrid;
using FPCS.Data.Enums;

namespace FPCS.Web.Admin.Models.Worker
{
    public class WorkerListOptions
    {
        [GridProperty(ExtensionType.All, true, FilterOperation.Contains)]
        public String Surname { get; set; }

        [GridProperty(ExtensionType.All, true, FilterOperation.Contains)]
        public String Name { get; set; }

        [GridProperty(ExtensionType.All, true, FilterOperation.Contains)]
        public String SecondName { get; set; }
    }
}