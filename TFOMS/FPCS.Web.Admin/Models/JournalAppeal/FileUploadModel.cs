using System;

using FPCS.Core.jqGrid;
using FPCS.Data.Enums;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Xml.Linq;

namespace FPCS.Web.Admin.Models.JournalAppeal
{
    public class FileUploadModel
    {
        public HttpPostedFileBase File { get; set; }
    }
}