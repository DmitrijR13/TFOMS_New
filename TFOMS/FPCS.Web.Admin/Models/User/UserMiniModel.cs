using System;

using FPCS.Data.Enums;

namespace FPCS.Web.Admin.Models.User
{
    public class UserMiniModel
    {
        public Int64 DbUserId { get; set; }

        public String FullName { get; set; }

        public String Email { get; set; }

        public Role Role { get; set; }

        public String RoleStr { get; set; }


        public String IsLocked { get; set; }

        public String SmoRegNum { get; set; }
    }
}