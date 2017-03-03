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
using FPCS.Web.Admin.Models.SMO;

namespace FPCS.Web.Admin.Controllers
{
    public class SMOController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult _Index(GridOptions options, SMOListOptions smoListOptions)
        {
            using (var uow = UnityManager.Resolve<IUnitOfWork>())
            {
                var repo = uow.GetRepo<ISMORepo>();

                var smo = repo.GetAll()
                    .Select(x => new
                    {
                        Id = x.SmoId,
                        x.Director,
                        x.FactAddress,
                        x.FilialDirector,
                        x.FullName,
                        x.KPP,
                        x.LicenseInfo,
                        x.ShortName,
                        x.SmoCode
                    })
                    .ToList();

                var engine = new GridDynamicEngine(options, smoListOptions);
                var resultTemp = engine.ApplySort(engine.ApplyFilter(smo.AsQueryable())).Select(x => new SMOIndexModel
                {
                    Id = x.Id,
                    Director = x.Director,
                    SmoCode = x.SmoCode,
                    ShortName = x.ShortName,
                    LicenseInfo = x.LicenseInfo,
                    KPP = x.KPP,
                    FullName = x.FullName,
                    FilialDirector = x.FilialDirector,
                    FactAddress = x.FactAddress
                })
                .ToList();

                Int32 totalCount = resultTemp.Count();

                var result = engine.CreateGridResult2(engine.ApplyPaging(resultTemp.AsQueryable()), totalCount, x => new SMOIndexModel
                {
                    Id = x.Id,
                    Director = x.Director,
                    SmoCode = x.SmoCode,
                    ShortName = x.ShortName,
                    LicenseInfo = x.LicenseInfo,
                    KPP = x.KPP,
                    FullName = x.FullName,
                    FilialDirector = x.FilialDirector,
                    FactAddress = x.FactAddress
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
                    var repo = uow.GetRepo<ISMORepo>();
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
                    var repo = uow.GetRepo<ISMORepo>();
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
            var model = new SMOCreateModel();
            return PartialView(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult _Create(SMOCreateModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    using (var uow = UnityManager.Resolve<IUnitOfWork>())
                    {
                        var repo = uow.GetRepo<ISMORepo>();

                        var dbEntity = repo.Add(model.SmoCode, model.KPP, model.FullName, model.ShortName, model.FactAddress,
                            model.Director, model.FilialDirector, model.LicenseInfo);

                        uow.Commit();

                        return JsonRes(dbEntity.SmoId.ToString());
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
                var dbEntity = uow.GetRepo<ISMORepo>().Get(id);
                if (dbEntity == null) return ErrorPartial("Запись {0} не найден", id);


                var model = new SMOEditModel
                {
                    Id = dbEntity.SmoId,
                    Director = dbEntity.Director,
                    LicenseInfo = dbEntity.LicenseInfo,
                    FilialDirector = dbEntity.FilialDirector,
                    FactAddress = dbEntity.FactAddress,
                    ShortName = dbEntity.ShortName,
                    FullName = dbEntity.FullName,
                    KPP = dbEntity.KPP,
                    SmoCode = dbEntity.SmoCode
                };
                return PartialView(model);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult _Edit(SMOEditModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    using (var uow = UnityManager.Resolve<IUnitOfWork>())
                    {
                        var repo = uow.GetRepo<ISMORepo>();

                        var dbEntity = repo.Get(model.Id);
                        if (dbEntity == null) throw new NotFoundEntityException("Запись не найдена");

                        dbEntity.Director = model.Director;
                        dbEntity.FactAddress = model.FactAddress;
                        dbEntity.FilialDirector = model.FilialDirector;
                        dbEntity.FullName = model.FullName;
                        dbEntity.KPP = model.KPP;
                        dbEntity.LicenseInfo = model.LicenseInfo;
                        dbEntity.ShortName = model.ShortName;
                        dbEntity.SmoCode = model.SmoCode;
                        dbEntity.UpdatedDate = DateTime.Now;

                        repo.Update(dbEntity);

                        uow.Commit();

                        return JsonRes(new { SmoId = dbEntity.SmoId });
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