using System;

using FPCS.Core.jqGrid;
using FPCS.Data.Enums;

namespace FPCS.Web.Admin.Models.Organization
{
    public class OrganizationListOptions
    {
        [GridProperty(ExtensionType.All,true, FilterOperation.Contains)]
        public String Name { get; set; }
    }
}