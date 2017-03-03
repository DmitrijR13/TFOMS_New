using System;
using System.ComponentModel.DataAnnotations;

using FPCS.Data.Enums;
using System.Web.Mvc;
using FPCS.Core.Unity;
using FPCS.Data;
using FPCS.Core.Extensions;
using FPCS.Data.Repo;
using System.Linq;

namespace FPCS.Web.Admin.Models.User
{
    public class UserCreateModel
    {
        [Required]
        public Int64 DbUserId { get; set; }

        [Required]
        [Display(Name = "Имя")]
        public String FirstName { get; set; }

        [Required]
        [Display(Name = "Фамилия")]
        public String LastName { get; set; }

        [Display(Name = "Отчество")]
        public String MiddleInitial { get; set; }


        [Display(Name = "Email")]
        public String Email { get; set; }

        [Required]
        [Display(Name = "Имя пользователя")]
        public String Login { get; set; }

        [Required]
        [Display(Name = "Пароль")]
        public String Password { get; set; }

        public SelectList Smos { get; set; }

        [Required]
        [Display(Name = "СМО")]
        public Int64? SmoId { get; set; }

        [Display(Name = "Роль")]
        public Role Role { get; set; }

        public SelectList Roles { get; set; }

        public void Init()
        {
            using (var uow = UnityManager.Instance.Resolve<IUnitOfWork>())
            {
                Roles = Role.ToSelectListUsingDesc();
                var smo = uow.GetRepo<ISMORepo>();
                var listSmo = smo.GetAll().Select(x => new Lookup<Int64> { Value = x.SmoId, Text = x.SmoCode + ", " + x.ShortName }).ToList();
                Smos = new SelectList(listSmo, "Value", "Text");
            }
        }
    }
}