using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using FPCS.Core.jqGrid;
using FPCS.Core.Extensions;
using FPCS.Data;
using FPCS.Data.Enums;
using FPCS.Data.Repo;
using FPCS.Web.Admin.Code;
using FPCS.Web.Admin.Models.User;
using FPCS.Core;
using FPCS.Data.Entities;

namespace FPCS.Web.Admin.Controllers
{
    [FPCSAuthorize(Role.Admin)]
    public class UserController : BaseController
    {
        public ActionResult Index()
        {
            UserIndexModel model = new UserIndexModel();

            model.IsLocked = ":;true:Да;false:Нет";
            model.Roles = ":;" + String.Join(@";", FPCS.Data.Enums.Role.Admin.ToSelectListUsingDesc().Select(x => String.Format(@"{0}: {1}", x.Value, x.Text)));

            return View(model);
        }

        public JsonResult _Index(GridOptions options, UsersListOptions usersListOptions)
        {
            using (var uow = UnityManager.Resolve<IUnitOfWork>())
            {
                var role = usersListOptions.Role;
                var isLocked = usersListOptions.IsLocked;
                if (role.HasValue)
                {
                    var dbList = uow.GetRepo<IUserRepo>()
                                .GetByRoles(role.Value)
                                .Where(x => ((isLocked.HasValue && x.IsLocked == isLocked.Value) || !isLocked.HasValue));


                    var engine = new GridDynamicEngine(options, usersListOptions);

                    usersListOptions.IsLocked = null;
                    usersListOptions.Role = null;

                    var result = engine.ApplyAll(dbList, x => new UserMiniModel
                    {
                        DbUserId = x.DbUserId,
                        FullName = x.LastName + ", " + x.FirstName,
                        Email = x.Email,
                        IsLocked = x.IsLocked ? "Заблокирован" : "Не заблокирован",
                        RoleStr = x.Role == Role.Admin ? "Администратор" :
                             x.Role == Role.TfomsUser ? "Пользователь ТФОМС" :
                             "Пользователь СМО",
                        Role = x.Role,
                        SmoRegNum = x.SMO != null ? x.SMO.SmoCode : ""
                    });

                    return Json(result);
                }
                else
                {
                    var dbList = uow.GetRepo<IUserRepo>()
                                .GetAll()
                                .Where(x => ((isLocked.HasValue && x.IsLocked == isLocked.Value) || !isLocked.HasValue));


                    var engine = new GridDynamicEngine(options, usersListOptions);

                    usersListOptions.IsLocked = null;
                    usersListOptions.Role = null;

                    var result = engine.ApplyAll(dbList, x => new UserMiniModel
                    {
                        DbUserId = x.DbUserId,
                        FullName = x.LastName + ", " + x.FirstName,
                        Email = x.Email,
                        IsLocked = x.IsLocked ? "Заблокирован" : "Не заблокирован",
                        RoleStr = x.Role == Role.Admin ? "Администратор" :
                             x.Role == Role.TfomsUser ? "Пользователь ТФОМС" :
                             "Пользователь СМО",
                        Role = x.Role,
                        SmoRegNum = x.SMO != null ? x.SMO.SmoCode : ""
                    });

                    return Json(result);
                }
            }
        }

        [HttpGet]
        public ActionResult _Details(Guid id)
        {
            using (var uow = UnityManager.Resolve<IUnitOfWork>())
            {
                var dbEntity = uow.GetRepo<IDbUserRepo>().Get(id);
                if (dbEntity == null) return ErrorPartial("User {0} not found", id);

                var model = new UserDetailsModel
                {
                    DbUserId = dbEntity.DbUserId,
                    FullName = dbEntity.FullName,
                    Email = dbEntity.Email,
                    Login = dbEntity.Login,
                    Role = dbEntity.Role.GetDescription(),
                    IsLocked = dbEntity.IsLocked
                };

                return PartialView(model);
            }
        }

        [HttpGet]
        public ActionResult _Create()
        {
            using (var uow = UnityManager.Resolve<IUnitOfWork>())
            {
                var model = new UserCreateModel { };
                model.Password = String.Empty;
                model.Login = String.Empty;
                model.Init();
                return PartialView(model);
            }
        }

        [HttpPost]
        public ActionResult _Create(UserCreateModel model)
        {
            if (String.IsNullOrEmpty(model.Password))
            {
                if (String.IsNullOrEmpty(model.Password)) ModelState.AddModelError("Password", "*");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (model.Role != Role.User)
                        model.SmoId = null;
                    using (var uow = UnityManager.Resolve<IUnitOfWork>())
                    {
                        var repo = uow.GetRepo<IUserRepo>();
                        var dbEntity = repo.Add(model.Login, model.Password, model.Email, model.FirstName, model.LastName, model.MiddleInitial, model.SmoId, model.Role);
                        uow.Commit();

                        return JsonRes(dbEntity.DbUserId.ToString());
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }

            return PartialView(model);
        }

        [HttpGet]
        public ActionResult _Edit(Int64 id)
        {
            using (var uow = UnityManager.Resolve<IUnitOfWork>())
            {
                var dbEntity = uow.GetRepo<IUserRepo>().Get(id);
                if (dbEntity == null) return ErrorPartial("User {0} not found", id);

                var model = new UserEditModel
                {
                    DbUserId = dbEntity.DbUserId,
                    FirstName = dbEntity.FirstName,
                    LastName = dbEntity.LastName,
                    MiddleInitial = dbEntity.MiddleInitial,
                    SmoId = dbEntity.SMO != null ? dbEntity.SMO.SmoId : uow.GetRepo<ISMORepo>().GetAll().FirstOrDefault().SmoId,
                    Email = dbEntity.Email,
                    Login = dbEntity.Login,
                    Role = dbEntity.Role,
                    IsLocked = dbEntity.IsLocked,
                };
                model.Init();
                return PartialView(model);
            }
        }

        [HttpPost]
        public ActionResult _Edit(UserEditModel model)
        {
            if (String.IsNullOrEmpty(model.Password))
            {
                if (String.IsNullOrEmpty(model.Password)) ModelState.AddModelError("Password", "*");
            }

            if (ModelState.IsValid || model.SmoId == null)
            {
                try
                {
                    if (model.Role != Role.User)
                        model.SmoId = null;
                    using (var uow = UnityManager.Resolve<IUnitOfWork>())
                    {                       
                        var repo = uow.GetRepo<IUserRepo>();
                        var dbEntity = repo.Update(model.DbUserId, model.Login, model.Password, model.Email, model.FirstName, model.LastName,
                            model.MiddleInitial, model.SmoId, model.IsLocked, model.Role);
                        uow.Commit();

                        return JsonRes(dbEntity.DbUserId.ToString());                     
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }

            return PartialView(model);
        }

        [HttpGet]
        public ActionResult ProfileDetails()
        {
            using (var uow = UnityManager.Resolve<IUnitOfWork>())
            {
                var repo = uow.GetRepo<IDbUserRepo>();
                Int64 dbUserId = User.UserId;

                DbUser dbUser = repo.Get(dbUserId);

                ProfileDetailsModel model = new ProfileDetailsModel(dbUser);

                return View(model);
            }
        }

        [HttpGet]
        public ActionResult ProfileEdit()
        {
            using (var uow = UnityManager.Resolve<IUnitOfWork>())
            {
                var repo = uow.GetRepo<IDbUserRepo>();
                Int64 dbUserId = User.UserId;

                DbUser dbUser = repo.Get(dbUserId);

                ProfileEditModel model = new ProfileEditModel(dbUser);

                return View(model);
            }
        }

        [HttpPost]
        public ActionResult ProfileEdit(ProfileEditModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    using (var uow = UnityManager.Resolve<IUnitOfWork>())
                    {
                        var repo = uow.GetRepo<IDbUserRepo>();
                        var dbEntity = repo.Get(model.DbUserId);

                        dbEntity.FirstName = model.FirstName.TrimAndReduce();
                        dbEntity.LastName = model.LastName.TrimAndReduce();
                        dbEntity.MiddleInitial = model.MI.TrimAndReduce();
                        dbEntity.FullName = String.Format(@"{0}, {1}", model.LastName.TrimAndReduce(), model.FirstName.TrimAndReduce());
                        dbEntity.Email = model.Email.TrimAndReduce();
                        dbEntity.Login = model.Login;
                        dbEntity.Password = model.Password;
                        dbEntity.UpdatedDate = DateTimeOffset.Now;

                        repo.Update(dbEntity);

                        uow.Commit();

                        return RedirectToAction("ProfileDetails");
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(Int64 id)
        {
            try
            {
                using (var uow = UnityManager.Resolve<IUnitOfWork>())
                {
                    var repo = uow.GetRepo<IDbUserRepo>();
                    repo.Remove(id);

                    uow.Commit();
                    return JsonRes();
                }
            }
            catch (Exception ex)
            {
                return JsonRes(Status.Error, ex.Message);
            }
        }
    }
}
