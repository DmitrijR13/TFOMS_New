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
    public class CurrentMedicalOrganizationModel
    {
        public Int64 Id { get; set; }

        public String Name { get; set; }
    }
}