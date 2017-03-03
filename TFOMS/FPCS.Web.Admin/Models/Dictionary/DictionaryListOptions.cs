using System;

using FPCS.Core.jqGrid;
using FPCS.Data.Enums;

namespace FPCS.Web.Admin.Models.Dictionary
{
    public class DictionaryListOptions
    {
        [GridProperty(ExtensionType.All, true, FilterOperation.Equal)]
        public String Code { get; set; }

        [GridProperty(ExtensionType.All,true, FilterOperation.Contains)]
        public String Name { get; set; }
    }
}