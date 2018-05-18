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
using FPCS.Web.Admin.Models.TypeOfAddressing;

namespace FPCS.Web.Admin.Controllers
{
    public class TypeOfAddressingController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult _Index(GridOptions options, DictionaryListOptions dictionaryListOptions)
        {
            using (var uow = UnityManager.Resolve<IUnitOfWork>())
            {
                var repo = uow.GetRepo<ITypeOfAddressingRepo>();

                var typeOfAddressing = repo.GetAll()
                    .Select(x => new
                    {
                        Id = x.TypeOfAddressingId,
                        x.Code,
                        x.Name,
                        x.IsUpdateDateEnd
                    })
                    .ToList();

                var engine = new GridDynamicEngine(options, dictionaryListOptions);
                var resultTemp = engine.ApplySort(engine.ApplyFilter(typeOfAddressing.AsQueryable())).Select(x => new TypeOfAddressingIndexModel
                {
                    Id = x.Id,
                    Code = x.Code,
                    Name = x.Name,
                    IsUpdateDate = x.IsUpdateDateEnd ? "Да" : "Нет"
                })
                .ToList();


                Int32 totalCount = resultTemp.Count();

                var result = engine.CreateGridResult2(engine.ApplyPaging(resultTemp.AsQueryable()), totalCount, x => new TypeOfAddressingIndexModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Code = x.Code,
                    IsUpdateDate = x.IsUpdateDate
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
                    var repo = uow.GetRepo<ITypeOfAddressingRepo>();
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
                    var repo = uow.GetRepo<ITypeOfAddressingRepo>();
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
            var model = new TypeOfAddressingCreateModel();
            return PartialView(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult _Create(TypeOfAddressingCreateModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    using (var uow = UnityManager.Resolve<IUnitOfWork>())
                    {
                        var repo = uow.GetRepo<ITypeOfAddressingRepo>();

                        var dbEntity = repo.Add(model.Code, model.Name, model.IsUpdateDate);

                        uow.Commit();

                        return JsonRes(dbEntity.TypeOfAddressingId.ToString());
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
                var dbEntity = uow.GetRepo<ITypeOfAddressingRepo>().Get(id);
                if (dbEntity == null) return ErrorPartial("Запись {0} не найден", id);


                var model = new TypeOfAddressingEditModel
                {
                    Id = dbEntity.TypeOfAddressingId,
                    Code = dbEntity.Code,
                    Name = dbEntity.Name,
                    IsUpdateDate = dbEntity.IsUpdateDateEnd
                };
                return PartialView(model);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult _Edit(TypeOfAddressingEditModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    using (var uow = UnityManager.Resolve<IUnitOfWork>())
                    {
                        var repo = uow.GetRepo<ITypeOfAddressingRepo>();

                        var dbEntity = repo.Get(model.Id);
                        if (dbEntity == null) throw new NotFoundEntityException("Запись не найдена");

                        dbEntity.Code = model.Code.Trim();
                        dbEntity.Name = model.Name;
                        dbEntity.IsUpdateDateEnd = model.IsUpdateDate;
                        dbEntity.UpdatedDate = DateTime.Now;

                        repo.Update(dbEntity);

                        uow.Commit();

                        return JsonRes(new { PersonId = dbEntity.TypeOfAddressingId });
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