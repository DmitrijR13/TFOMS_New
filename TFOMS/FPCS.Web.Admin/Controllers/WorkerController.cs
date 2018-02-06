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
using FPCS.Web.Admin.Models.Worker;

namespace FPCS.Web.Admin.Controllers
{
    public class WorkerController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult _Index(GridOptions options, DictionaryListOptions dictionaryListOptions)
        {
            using (var uow = UnityManager.Resolve<IUnitOfWork>())
            {
                var repo = uow.GetRepo<IWorkerRepo>();

                var worker = repo.GetAll()
                    .Select(x => new
                    {
                        Id = x.WorkerId,
                        FIO = x.Surname + " " + x.Name + " " + x.SecondName,
                        Phone = x.Phone
                    })
                    .ToList();

                var engine = new GridDynamicEngine(options, dictionaryListOptions);
                var resultTemp = engine.ApplySort(engine.ApplyFilter(worker.AsQueryable())).Select(x => new WorkerIndexModel
                {
                    Id = x.Id,
                    FIO = x.FIO,
                    Phone = x.Phone
                })
                .ToList();

                Int32 totalCount = resultTemp.Count();

                var result = engine.CreateGridResult2(engine.ApplyPaging(resultTemp.AsQueryable()), totalCount, x => new WorkerIndexModel
                {
                    Id = x.Id,
                    FIO = x.FIO,
                    Phone = x.Phone
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
                    var repo = uow.GetRepo<IWorkerRepo>();
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
                    var repo = uow.GetRepo<IWorkerRepo>();
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
            var model = new WorkerCreateModel();
            return PartialView(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult _Create(WorkerCreateModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    using (var uow = UnityManager.Resolve<IUnitOfWork>())
                    {
                        var repo = uow.GetRepo<IWorkerRepo>();
                        var dbEntity = repo.GetAll().Where(x => x.IsHead).FirstOrDefault();
                        if (dbEntity != null)
                        {
                            dbEntity.IsHead = false;
                            repo.Update(dbEntity);
                            uow.Commit();
                        }
                    }

                    using (var uow = UnityManager.Resolve<IUnitOfWork>())
                    {
                        var repo = uow.GetRepo<IWorkerRepo>();

                        var dbEntity = repo.Add(model.Surname, model.Name, model.SecondName, model.IsHead, model.Phone);

                        uow.Commit();

                        return JsonRes(dbEntity.WorkerId.ToString());
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
                var dbEntity = uow.GetRepo<IWorkerRepo>().Get(id);
                if (dbEntity == null) return ErrorPartial("Запись {0} не найден", id);


                var model = new WorkerEditModel
                {
                    Id = dbEntity.WorkerId,
                    Surname = dbEntity.Surname,
                    Name = dbEntity.Name,
                    SecondName = dbEntity.SecondName,
                    IsHead = dbEntity.IsHead,
                    Phone = dbEntity.Phone
                };
                return PartialView(model);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult _Edit(WorkerEditModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    using (var uow = UnityManager.Resolve<IUnitOfWork>())
                    {
                        var repo = uow.GetRepo<IWorkerRepo>();
                        var dbEntity = repo.GetAll().Where(x => x.IsHead).FirstOrDefault();
                        if(dbEntity != null)
                        {
                            dbEntity.IsHead = false;
                            repo.Update(dbEntity);
                            uow.Commit();
                        }
                       
                    }

                    using (var uow = UnityManager.Resolve<IUnitOfWork>())
                    {
                        var repo = uow.GetRepo<IWorkerRepo>();

                        var dbEntity = repo.Get(model.Id);
                        if (dbEntity == null) throw new NotFoundEntityException("Запись не найдена");

                        dbEntity.Surname = model.Surname.Trim();
                        dbEntity.Name = model.Name.Trim();
                        dbEntity.SecondName = model.SecondName.Trim();
                        dbEntity.UpdatedDate = DateTime.Now;
                        dbEntity.IsHead = model.IsHead;
                        dbEntity.Phone = model.Phone;

                        repo.Update(dbEntity);

                        uow.Commit();

                        return JsonRes(new { PersonId = dbEntity.WorkerId });
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