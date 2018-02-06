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
using FPCS.Web.Admin.Models.Dictionary;

namespace FPCS.Web.Admin.Controllers
{
    public class MOController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult _Index(GridOptions options, DictionaryListOptions dictionaryListOptions)
        {
            using (var uow = UnityManager.Resolve<IUnitOfWork>())
            {
                var repo = uow.GetRepo<IMORepo>();

                var mo = repo.GetAll()
                    .Select(x => new
                    {
                        Id = x.MOId,
                        x.Code,
                        x.Name
                    })
                    .ToList();

                var engine = new GridDynamicEngine(options, dictionaryListOptions);
                var resultTemp = engine.ApplySort(engine.ApplyFilter(mo.AsQueryable())).Select(x => new DictionaryIndexModel
                {
                    Id = x.Id,
                    Code = x.Code,
                    Name = x.Name
                })
                .ToList();

                Int32 totalCount = resultTemp.Count();
                
                var result = engine.CreateGridResult2(engine.ApplyPaging(resultTemp.AsQueryable()), totalCount, x => new DictionaryIndexModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Code = x.Code
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
                    var repo = uow.GetRepo<IMORepo>();
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
                    var repo = uow.GetRepo<IMORepo>();
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
            var model = new DictionaryCreateModel();
            return PartialView(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult _Create(DictionaryCreateModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    using (var uow = UnityManager.Resolve<IUnitOfWork>())
                    {
                        var repo = uow.GetRepo<IMORepo>();

                        var dbEntity = repo.Add(model.Code, model.Name);

                        uow.Commit();

                        return JsonRes(dbEntity.MOId.ToString());
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
                var dbEntity = uow.GetRepo<IMORepo>().Get(id);
                if (dbEntity == null) return ErrorPartial("Запись {0} не найден", id);


                var model = new DictionaryEditModel
                {
                    Id = dbEntity.MOId,
                    Code = dbEntity.Code,
                    Name = dbEntity.Name
                };
                return PartialView(model);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult _Edit(DictionaryEditModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    using (var uow = UnityManager.Resolve<IUnitOfWork>())
                    {
                        var repo = uow.GetRepo<IMORepo>();

                        var dbEntity = repo.Get(model.Id);
                        if (dbEntity == null) throw new NotFoundEntityException("Запись не найдена");

                        dbEntity.Code = model.Code.Trim();
                        dbEntity.Name = model.Name;
                        dbEntity.UpdatedDate = DateTime.Now;

                        repo.Update(dbEntity);

                        uow.Commit();

                        return JsonRes(new { PersonId = dbEntity.MOId });
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