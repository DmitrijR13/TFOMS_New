using System;

using FPCS.Core.jqGrid;
using FPCS.Data.Enums;

namespace FPCS.Web.Admin.Models.SMO
{
    public class SMOListOptions
    {
        [GridProperty(ExtensionType.All, true, FilterOperation.Contains)]
        public String SmoCode { get; set; }

        [GridProperty(ExtensionType.All, true, FilterOperation.Contains)]
        public String KPP { get; set; }

        [GridProperty(ExtensionType.All, true, FilterOperation.Contains)]
        public String FullName { get; set; }

        [GridProperty(ExtensionType.All, true, FilterOperation.Contains)]
        public String ShortName { get; set; }

        [GridProperty(ExtensionType.All, true, FilterOperation.Contains)]
        public String FactAddress { get; set; }

        [GridProperty(ExtensionType.All, true, FilterOperation.Contains)]
        public String Director { get; set; }

        [GridProperty(ExtensionType.All, true, FilterOperation.Contains)]
        public String FilialDirector { get; set; }

        [GridProperty(ExtensionType.All, true, FilterOperation.Contains)]
        public String LicenseInfo { get; set; }
    }
}