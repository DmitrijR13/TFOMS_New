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
    public class ThemeAppealCitizensController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult _Index(GridOptions options, DictionaryDateListOptions dictionaryListOptions)
        {
            using (var uow = UnityManager.Resolve<IUnitOfWork>())
            {
                var repo = uow.GetRepo<IThemeAppealCitizensRepo>();

                var themeAppealCitizens = repo.GetAll()
                    .Select(x => new
                    {
                        Id = x.ThemeAppealCitizensId,
                        x.Code,
                        x.Name,
						x.DateClose
                    })
                    .ToList();

                var engine = new GridDynamicEngine(options, dictionaryListOptions);
				var resultTemp = engine.ApplySort(engine.ApplyFilter(themeAppealCitizens.AsQueryable())).Select(x => new DictionaryDateIndexModel
				{
					Id = x.Id,
					Code = x.Code,
					Name = x.Name,
					DateClose = x.DateClose.HasValue ? x.DateClose.Value.ToShortDateString() : ""

				})
                .ToList();

                Int32 totalCount = resultTemp.Count();

                var result = engine.CreateGridResult2(engine.ApplyPaging(resultTemp.AsQueryable()), totalCount, x => new DictionaryDateIndexModel
				{
                    Id = x.Id,
                    Name = x.Name,
                    Code = x.Code,
					DateClose = x.DateClose
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
                    var repo = uow.GetRepo<IThemeAppealCitizensRepo>();
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
                    var repo = uow.GetRepo<IThemeAppealCitizensRepo>();
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
            var model = new DictionaryDateCreateModel();
            return PartialView(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult _Create(DictionaryDateCreateModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    using (var uow = UnityManager.Resolve<IUnitOfWork>())
                    {
                        var repo = uow.GetRepo<IThemeAppealCitizensRepo>();

                        var dbEntity = repo.Add(model.Code, model.Name);

                        uow.Commit();

                        return JsonRes(dbEntity.ThemeAppealCitizensId.ToString());
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
                var dbEntity = uow.GetRepo<IThemeAppealCitizensRepo>().Get(id);
                if (dbEntity == null) return ErrorPartial("Запись {0} не найден", id);


                var model = new DictionaryDateEditModel
                {
                    Id = dbEntity.ThemeAppealCitizensId,
                    Code = dbEntity.Code,
                    Name = dbEntity.Name,
					DateClose = dbEntity.DateClose
                };
                return PartialView(model);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult _Edit(DictionaryDateEditModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    using (var uow = UnityManager.Resolve<IUnitOfWork>())
                    {
                        var repo = uow.GetRepo<IThemeAppealCitizensRepo>();

                        var dbEntity = repo.Get(model.Id);
                        if (dbEntity == null) throw new NotFoundEntityException("Запись не найдена");

                        dbEntity.Code = model.Code.Trim();
                        dbEntity.Name = model.Name;
                        dbEntity.UpdatedDate = DateTime.Now;
						dbEntity.DateClose = model.DateClose;


						repo.Update(dbEntity);

                        uow.Commit();

                        return JsonRes(new { PersonId = dbEntity.ThemeAppealCitizensId });
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