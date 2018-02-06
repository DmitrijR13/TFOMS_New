using FPCS.Core.jqGrid;
using FPCS.Data;
using FPCS.Data.Enums;
using FPCS.Data.Repo;
using FPCS.Web.Admin.Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FPCS.Data.Entities;
using FPCS.Data.Exceptions;
using FPCS.Core.Extensions;
using FPCS.Core;
using FPCS.Web.Admin.Models.Organization;
using FPCS.Web.Admin.Models.Organization;

namespace FPCS.Web.Admin.Controllers
{
    public class OrganizationController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult _Index(GridOptions options, OrganizationListOptions dictionaryListOptions)
        {
            using (var uow = UnityManager.Resolve<IUnitOfWork>())
            {
                var repo = uow.GetRepo<IOrganizationRepo>();

                var organization = repo.GetAll()
                    .Select(x => new
                    {
                        Id = x.OrganizationId,
                        x.Name
                    })
                    .ToList();

                var engine = new GridDynamicEngine(options, dictionaryListOptions);
                var resultTemp = engine.ApplySort(engine.ApplyFilter(organization.AsQueryable())).Select(x => new OrganizationIndexModel
                {
                    Id = x.Id,
                    Name = x.Name
                })
                .ToList();

                Int32 totalCount = resultTemp.Count();
                
                var result = engine.CreateGridResult2(engine.ApplyPaging(resultTemp.AsQueryable()), totalCount, x => new OrganizationIndexModel
                {
                    Id = x.Id,
                    Name = x.Name
                });

                return Json(result);
            }
        }


       
        [HttpPost]
        public ActionResult DeleteAll(String id)
        {
            try
            {
                if (String.IsNullOrEmpty(id)) return JsonRes(Status.Error, "Не выбраны записи для удаления");

                var ids = id.Split(',');
                using (var uow = UnityManager.Resolve<IUnitOfWork>())
                {
                    Int32 errorsCount = 0;
                    var repo = uow.GetRepo<IOrganizationRepo>();
                    foreach (var item in ids)
                    {
                        try
                        {
                            repo.Remove(Convert.ToInt64(item));
                        }
                        catch (Exception ex)
                        {
                            errorsCount++;
                        }
                    }

                    uow.Commit();
                    if (errorsCount == 0) return JsonRes();
                    else return JsonRes(Status.Error, "Ошибка при удалении");
                }
            }
            catch (Exception ex)
            {
                return JsonRes(Status.Error, ex.Message);
            }
        }

        [HttpPost]
        public ActionResult Delete(Int64 id)
        {
            try
            {
                using (var uow = UnityManager.Resolve<IUnitOfWork>())
                {
                    var repo = uow.GetRepo<IOrganizationRepo>();
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

        [HttpGet]
        public PartialViewResult _Create()
        {
            var model = new OrganizationCreateModel();
            return PartialView(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult _Create(OrganizationCreateModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    using (var uow = UnityManager.Resolve<IUnitOfWork>())
                    {
                        var repo = uow.GetRepo<IOrganizationRepo>();

                        var dbEntity = repo.Add(model.Name);

                        uow.Commit();

                        return JsonRes(dbEntity.OrganizationId.ToString());
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
        public PartialViewResult _Edit(Int64 id)
        {
            using (var uow = UnityManager.Resolve<IUnitOfWork>())
            {
                var dbEntity = uow.GetRepo<IOrganizationRepo>().Get(id);
                if (dbEntity == null) return ErrorPartial("Запись {0} не найден", id);


                var model = new OrganizationEditModel
                {
                    Id = dbEntity.OrganizationId,
                    Name = dbEntity.Name
                };
                return PartialView(model);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult _Edit(OrganizationEditModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    using (var uow = UnityManager.Resolve<IUnitOfWork>())
                    {
                        var repo = uow.GetRepo<IOrganizationRepo>();

                        var dbEntity = repo.Get(model.Id);
                        if (dbEntity == null) throw new NotFoundEntityException("Запись не найдена");

                        dbEntity.Name = model.Name;
                        dbEntity.UpdatedDate = DateTime.Now;

                        repo.Update(dbEntity);

                        uow.Commit();

                        return JsonRes(new { PersonId = dbEntity.OrganizationId });
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }

            return PartialView(model);
        }
    }
}