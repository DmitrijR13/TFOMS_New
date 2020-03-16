using System;

using FPCS.Core.jqGrid;
using FPCS.Data.Enums;

namespace FPCS.Web.Admin.Models.Dictionary
{
    public class DictionaryDateListOptions:DictionaryListOptions
    {
        [GridProperty(ExtensionType.All, true, FilterOperation.Equal)]
        public String DateClose { get; set; }
    }
}