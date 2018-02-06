﻿using FPCS.Core.Extensions;
using FPCS.Core.Unity;
using FPCS.Data;
using FPCS.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FPCS.Web.Admin.Models.HandAppeal
{
    public class HandAppealEditModel : HandAppealCreateModel
    {
        [Required]
        public Int64 JournalAppealId { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}