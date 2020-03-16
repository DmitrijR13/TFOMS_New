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
using FPCS.Web.Admin.Models.HandAppeal;
using System.IO;

using System.Linq.Dynamic;
using AutoMapper;
using System.Text.RegularExpressions;
using ClosedXML.Excel;
using Xceed.Words.NET;
using Slepov.Russian.Morpher;

namespace FPCS.Web.Admin.Controllers
{
    public class MemoryPostedFile : HttpPostedFileBase
    {
        private readonly byte[] fileBytes;

        public MemoryPostedFile(byte[] fileBytes, string fileName = null)
        {
            this.fileBytes = fileBytes;
            this.FileName = fileName;
            this.InputStream = new MemoryStream(fileBytes);
        }

        public override int ContentLength => fileBytes.Length;

        public override string FileName { get; }

        public override Stream InputStream { get; }
    }


    public class HandAppealController : BaseController
    {
        public JsonResult IsUpdateDateEnd(int id)
        {
            using (var uow = UnityManager.Resolve<IUnitOfWork>())
            {
                var repo = uow.GetRepo<ITypeOfAddressingRepo>();

                var typeOfAddressing = repo.Get(id);
                if(typeOfAddressing != null)
                {
                    var data = new 
                    {
                        Obj = typeOfAddressing.IsUpdateDateEnd
                    };

                    return Json(data, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var data = new
                    {
                        Obj = false
                    };

                    return Json(data, JsonRequestBehavior.AllowGet);
                }
            }
        }

        public ActionResult Index(string message = "")
        {
            HandAppealDateAppealModel model = new HandAppealDateAppealModel();
            model.DateFrom = DateTime.Today.AddDays(-30);
            model.DateTo = DateTime.Today;
            model.Message = message;
            return View(model);
        }

        public JsonResult _Index(GridOptions options, HandAppealListOptions handAppealListOptions, DateTime? dateFromFilter, DateTime? dateToFilter)
        {
            using (var uow = UnityManager.Resolve<IUnitOfWork>())
            {
                var repo = uow.GetRepo<IHandAppealRepo>();
                var organizationRepo = uow.GetRepo<IOrganizationRepo>();

                var organizations = organizationRepo.GetAll().ToList();

                //var handAppealr = repo.GetAll().ToList();

                string userName = User.Login;
                var dbUserRepo = uow.GetRepo<IDbUserRepo>();
                var dbUser = dbUserRepo.GetByLogin(userName);
                Int64 smo = 0;
                if (dbUser.Role != Role.Admin && dbUser.Role != Role.TfomsUser)
                {
                    var userRuepo = uow.GetRepo<IUserRepo>();
                    smo = userRuepo.Get(dbUser.DbUserId).SMOId.Value;
                }

                var handAppeal = repo.GetAll()
                   .Where(x => (dateFromFilter.HasValue ? x.Date >= dateFromFilter : x.JournalAppealId != 0)
                   && (dateToFilter.HasValue ? x.Date <= dateToFilter : x.JournalAppealId != 0)
                   && (smo != 0 ? x.SMO.SmoId == smo : 1 == 1))
                   .Select(x => new
                   {
                       Id = x.JournalAppealId,
                       x.AppealUniqueNumber,
                       x.Date,
                       AppealTheme = x.ThemeAppealCitizens.Name,
                       AcceptedBy = x.Worker != null ? x.Worker.Surname + " " + x.Worker.Name + " " + x.Worker.SecondName : "",
                       x.Responsible,
                       x.AppealPlanEndDate,
                       x.AppealFactEndDate,
                       ResultName = x.AppealResult != null ? x.AppealResult.Name : "",
                       AppealOrganizationCode = x.SMO.ShortName,
                       x.AppealFileName,
                       ReceivedTreatmentPerson = x.ReceivedTreatmentPersonSurname + " " + x.ReceivedTreatmentPersonName + " " + x.ReceivedTreatmentPersonSecondName,
                       Applicant = x.ApplicantSurname + " " + x.ApplicantName + " " + x.ApplicantSecondName,
                       x.TypeOfAddressingId,
                       x.WayOfAddressingId,
                       x.ThemeAppealCitizensId,
                       x.AnswerFileName,
                       AppealCode = x.ThemeAppealCitizens != null ? x.ThemeAppealCitizens.Code : "",
                       AppealName = x.WayOfAddressing != null ? x.WayOfAddressing.Name : "",
                       x.OrganizationId
                   }).AsEnumerable()
                    .Select(x => new
                    {
                        x.Id,
                        x.AppealUniqueNumber,
                        x.Date,
                        x.AppealTheme,
                        x.AcceptedBy,
                        x.Responsible,
                        x.AppealPlanEndDate,
                        x.AppealFactEndDate,
                        x.ResultName,
                        x.AppealOrganizationCode,
                        x.AppealFileName,
                        x.ReceivedTreatmentPerson,
                        x.Applicant,
                        x.TypeOfAddressingId,
                        x.WayOfAddressingId,
                        x.ThemeAppealCitizensId,
                        x.AnswerFileName,
                        x.AppealCode,
                        x.AppealName,
                        OrganizationsName = String.Join(",", organizations.Where(y => x.OrganizationId.Split(',').ToList().Contains(y.OrganizationId.ToString())).Select(y => y.Name).ToList())
                    });

                options.IsSearch = true;
                var engine = new GridDynamicEngine(options, handAppealListOptions);
                var resultTemp = engine.ApplySort(engine.ApplyFilter(handAppeal.AsQueryable())).Select(x => new HandAppealIndexModel
                {
                    Id = x.Id,
                    AppealUniqueNumber = x.AppealUniqueNumber,
                    Date = x.Date.HasValue ? x.Date.Value.Date.ToString("dd/MM/yyyy") : String.Empty,
                    AppealTheme = x.AppealTheme,
                    AcceptedBy = x.AcceptedBy,
                    AppealPlanEndDate = x.AppealPlanEndDate.HasValue ? x.AppealPlanEndDate.Value.Date.ToString("dd/MM/yyyy") : String.Empty,
                    AppealFactEndDate = x.AppealFactEndDate.HasValue ? x.AppealFactEndDate.Value.Date.ToString("dd/MM/yyyy") : String.Empty,
                    ResultName = x.ResultName,
                    AppealOrganizationCode = x.AppealOrganizationCode,
                    AppealFileName = x.AppealFileName,
                    ReceivedTreatmentPerson = x.ReceivedTreatmentPerson,
                    Applicant = x.Applicant,
                    TypeOfAddressingId = x.TypeOfAddressingId,
                    WayOfAddressingId = x.WayOfAddressingId,
                    ThemeAppealCitizensId = x.ThemeAppealCitizensId,
                    AnswerFileName = x.AnswerFileName,
                    AppealCode = x.AppealCode,
                    AppealName = x.AppealName,
                    OrganizationsName = x.OrganizationsName
                });


                int totalCount = 0;
                var sessionOptions = (HandAppealListOptions)Session["handAppealListOptions" + User.Login]; ;
                if (Session["handAppealCount" + User.Login] == null
                    || (DateTime?)Session["handAppealDateFromFilter" + User.Login] != dateFromFilter
                    || (DateTime?)Session["handAppealDateToFilter" + User.Login] != dateToFilter
                    || sessionOptions == null
                    || sessionOptions.AcceptedBy != handAppealListOptions.AcceptedBy
                    || sessionOptions.AppealCode != handAppealListOptions.AppealCode
                    || sessionOptions.AppealName != handAppealListOptions.AppealName
                    || sessionOptions.AppealOrganizationCode != handAppealListOptions.AppealOrganizationCode
                    || sessionOptions.AppealTheme != handAppealListOptions.AppealTheme
                    || sessionOptions.AppealUniqueNumber != handAppealListOptions.AppealUniqueNumber
                    || sessionOptions.Applicant != handAppealListOptions.Applicant
                   )
                {
                    totalCount = resultTemp.Count();
                    Session["handAppealCount" + User.Login] = totalCount;
                    Session["handAppealDateFromFilter" + User.Login] = dateFromFilter;
                    Session["handAppealDateToFilter" + User.Login] = dateToFilter;
                    Session["handAppealListOptions" + User.Login] = handAppealListOptions;
                }
                else
                {
                    totalCount = Convert.ToInt32(Session["handAppealCount" + User.Login]);
                }

                var result = engine.CreateGridResult2(engine.ApplyPaging(resultTemp.AsQueryable()), totalCount, x => new HandAppealIndexModel
                {
                    Id = x.Id,
                    AppealUniqueNumber = x.AppealUniqueNumber,
                    Date = x.Date,
                    AppealTheme = x.AppealTheme,
                    AcceptedBy = x.AcceptedBy,
                    AppealPlanEndDate = x.AppealPlanEndDate,
                    AppealFactEndDate = x.AppealFactEndDate,
                    ResultName = x.ResultName,
                    AppealOrganizationCode = x.AppealOrganizationCode,
                    AppealFileName = x.AppealFileName,
                    ReceivedTreatmentPerson = x.ReceivedTreatmentPerson,
                    Applicant = x.Applicant,
                    TypeOfAddressingId = x.TypeOfAddressingId,
                    WayOfAddressingId = x.WayOfAddressingId,
                    ThemeAppealCitizensId = x.ThemeAppealCitizensId,
                    AnswerFileName = x.AnswerFileName,
                    AppealCode = x.AppealCode,
                    AppealName = x.AppealName,
                    OrganizationsName = x.OrganizationsName
                });

               

                return Json(result);
            }
        }

        [HttpPost]
        public ActionResult Delete(Int64 id)
        {
            try
            {
                using (var uow = UnityManager.Resolve<IUnitOfWork>())
                {
                    var repo = uow.GetRepo<IHandAppealRepo>();
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
            //Int32 uniqueNumberPart1 = 0;
            //using (var uow = UnityManager.Resolve<IUnitOfWork>())
            //{
            //    var repo = uow.GetRepo<IHandAppealRepo>();
            //    uniqueNumberPart1 = repo.GetMaxUniqueNumberPart1();
            //}

            var model = new HandAppealCreateModel();
            model.Init();
            model.AppealPlanEndDate = DateTime.Now;
            model.Date = DateTime.Now;
            model.OZPZRegistrationDate = DateTime.Now;
            model.GuideDate = DateTime.Now;
            model.UniqueNumberPart1 = 0;
            model.AppealUniqueNumber = "";


            return PartialView(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult _Create(HandAppealCreateModel model, HttpPostedFileBase uploadImage)
        {
            if(String.IsNullOrEmpty(model.AppealUniqueNumber))
            {
                using (var uow = UnityManager.Resolve<IUnitOfWork>())
                {
                    var repo = uow.GetRepo<IHandAppealRepo>();
                    Int32 uniqueNumberPart1 = repo.GetMaxUniqueNumberPart1();
                    model.UniqueNumberPart1 = uniqueNumberPart1;
                    model.AppealUniqueNumber = uniqueNumberPart1.ToString();
                }
            }
            String baseNumber = String.Empty;
            if(model.JournalAppealId != 0)
            {
                using (var uow = UnityManager.Resolve<IUnitOfWork>())
                {
                    var repo = uow.GetRepo<IHandAppealRepo>();
                    baseNumber = repo.Get(model.JournalAppealId).AppealUniqueNumber;
                }
            }
           
            if (!CheckUniqueNumber(model.AppealUniqueNumber, baseNumber))
            {
                ModelState.AddModelError("AppealUniqueNumber", "Данный номер уже используется");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    using (var uow = UnityManager.Resolve<IUnitOfWork>())
                    {
                        var repo = uow.GetRepo<IHandAppealRepo>();
                        var repoSmo = uow.GetRepo<ISMORepo>();
                        var repoWorker = uow.GetRepo<IWorkerRepo>();
                        // Выполняем сопоставление
                        HandAppeal handAppeal = Mapper.Map<HandAppealCreateModel, HandAppeal>(model);
                        handAppeal.AppealOrganizationCode = handAppeal.SMOId != null ? repoSmo.Get(handAppeal.SMOId).SmoCode : "";
                        handAppeal.CreatedDate = DateTime.Today;
                        handAppeal.UpdatedDate = DateTime.Today;
                        handAppeal.ApplicantSMO = handAppeal.SMOId != null ? repoSmo.Get(handAppeal.SMOId).SmoCode : "";
                        handAppeal.ReceivedTreatmentPersonSMO = handAppeal.SMOId != null ? repoSmo.Get(handAppeal.SMOId).SmoCode : "";
                        handAppeal.Responsible = "";

                        if (model.WorkerId.HasValue)
                        {
                            var worker = repoWorker.Get(model.WorkerId);
                            handAppeal.AcceptedBy = worker.Surname + " " + worker.Name + " " + worker.SecondName;
                        }
                        else
                        {
                            handAppeal.AcceptedBy = "";
                        }
                        handAppeal.SourceIncomeId = 2;
                        handAppeal.AppealOrganizationId = 1;
                        handAppeal.TakingAppealLineId = 6;
                        if(handAppeal.ApplicantSecondName == null)
                        {
                            handAppeal.ApplicantSecondName = "";
                        }
                        if (handAppeal.ReceivedTreatmentPersonSecondName == null)
                        {
                            handAppeal.ReceivedTreatmentPersonSecondName = "";
                        }
                        if (model.File != null)
                        {
                            byte[] imageData = null;
                            // считываем переданный файл в массив байтов
                            using (var binaryReader = new BinaryReader(model.File.InputStream))
                            {
                                imageData = binaryReader.ReadBytes(model.File.ContentLength);
                            }
                            // установка массива байтов
                            handAppeal.AppealFile = imageData;
                            handAppeal.AppealFileName = model.File.FileName;
                        }
                        if (model.PostedFile != null)
                        {
                            byte[] imageData = null;
                            // считываем переданный файл в массив байтов
                            using (var binaryReader = new BinaryReader(model.PostedFile.InputStream))
                            {
                                imageData = binaryReader.ReadBytes(model.PostedFile.ContentLength);
                            }
                            // установка массива байтов
                            handAppeal.AnswerFile = imageData;
                            handAppeal.AnswerFileName = model.PostedFile.FileName;
                        }
                        String medicalOrganizations = String.Empty;
                        if (model.MedicalOrganizationId != null)
                        {
                            medicalOrganizations = String.Join(",", model.MedicalOrganizationId.ToList());
                        }
                        handAppeal.MedicalOrganizationId = medicalOrganizations;

                        String organizations = String.Empty;
                        if (model.OrganizationId != null)
                        {
                            organizations = String.Join(",", model.OrganizationId.ToList());
                        }
                        handAppeal.OrganizationId = organizations;

                        handAppeal.UniqueNumberPart1 = GetIntUniqueNumber(model.AppealUniqueNumber);
                        handAppeal.JournalAppealId = model.JournalAppealId;
                        if(handAppeal.JournalAppealId == 0)
                        {
                            var dbEntity = repo.Add(handAppeal);
                            uow.Commit();
                            model.JournalAppealId = dbEntity.JournalAppealId;
                        }
                        else
                        {
                            repo.Update(handAppeal);
                            uow.Commit();
                        }

                       
                        return JsonRes(model.JournalAppealId);
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }

            model.Init();
            return PartialView(model);
        }


        [HttpGet]
        public PartialViewResult _Edit(Int64 id)
        {
            using (var uow = UnityManager.Resolve<IUnitOfWork>())
            {
                var repo = uow.GetRepo<IHandAppealRepo>();
                var medOrgsRepo = uow.GetRepo<IMORepo>();
                var orgsRepo = uow.GetRepo<IOrganizationRepo>();

                var dbEntity = repo.Get(id);
                if (dbEntity == null) return ErrorPartial("Обращение {0} не найдено", id);
 
                // Выполняем сопоставление
                HandAppealEditModel model = Mapper.Map<HandAppeal, HandAppealEditModel>(dbEntity);
                model.Init();

                if (!String.IsNullOrEmpty(dbEntity.MedicalOrganizationId))
                {
                    model.MedicalOrganizations = dbEntity.MedicalOrganizationId;
                }

                if (!String.IsNullOrEmpty(dbEntity.OrganizationId))
                {
                    model.Organizations = dbEntity.OrganizationId;
                }

                return PartialView(model);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult _Edit(HandAppealEditModel model)
        {
            String baseNumber = String.Empty;
            using (var uow = UnityManager.Resolve<IUnitOfWork>())
            {
                var repo = uow.GetRepo<IHandAppealRepo>();
                baseNumber = repo.Get(model.JournalAppealId).AppealUniqueNumber;
            }


            if (String.IsNullOrEmpty(model.AppealUniqueNumber))
            {
                using (var uow = UnityManager.Resolve<IUnitOfWork>())
                {
                    var repo = uow.GetRepo<IHandAppealRepo>();
                    Int32 uniqueNumberPart1 = repo.GetMaxUniqueNumberPart1();
                    model.AppealUniqueNumber = uniqueNumberPart1.ToString();
                }
            }
            if (!CheckUniqueNumber(model.AppealUniqueNumber, baseNumber))
            {
                ModelState.AddModelError("AppealUniqueNumber", "Данный номер уже используется");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    byte[] file;
                    String fileName = "";
                    using (var uow = UnityManager.Resolve<IUnitOfWork>())
                    {
                        var repo = uow.GetRepo<IHandAppealRepo>();

                        var dbEntity = repo.Get(model.JournalAppealId);
                        if (dbEntity == null) throw new NotFoundEntityException("Запись не найдена");

                        file = dbEntity.AppealFile;
                        fileName = dbEntity.AppealFileName;
                    }

                    byte[] postedFile;
                    String answerFileName = "";
                    using (var uow = UnityManager.Resolve<IUnitOfWork>())
                    {
                        var repo = uow.GetRepo<IHandAppealRepo>();

                        var dbEntity = repo.Get(model.JournalAppealId);
                        if (dbEntity == null) throw new NotFoundEntityException("Запись не найдена");

                        postedFile = dbEntity.AnswerFile;
                        answerFileName = dbEntity.AnswerFileName;
                    }

                    using (var uow = UnityManager.Resolve<IUnitOfWork>())
                    {
                        var repo = uow.GetRepo<IHandAppealRepo>();
                        var repoSmo = uow.GetRepo<ISMORepo>();
                        var repoWorker = uow.GetRepo<IWorkerRepo>();

                        var dbEntity = Mapper.Map<HandAppealEditModel, HandAppeal>(model);
                        if (dbEntity == null) throw new NotFoundEntityException("Запись не найдена");

                        // Выполняем сопоставление
                        dbEntity.AppealOrganizationCode = model.SMOId != null ? repoSmo.Get(model.SMOId).SmoCode : "";
                        dbEntity.UpdatedDate = DateTime.Today;
                        dbEntity.SourceIncomeId = 2;
                        dbEntity.AppealOrganizationId = 1;
                        dbEntity.TakingAppealLineId = 6;
                        dbEntity.ApplicantSMO = model.SMOId != null ? repoSmo.Get(model.SMOId).SmoCode : "";
                        dbEntity.ReceivedTreatmentPersonSMO = model.SMOId != null ? repoSmo.Get(model.SMOId).SmoCode : "";
                        dbEntity.Responsible = "";

                        if (model.WorkerId.HasValue)
                        {
                            var worker = repoWorker.Get(model.WorkerId);
                            dbEntity.AcceptedBy = worker.Surname + " " + worker.Name + " " + worker.SecondName;
                        }
                        else
                        {
                            dbEntity.AcceptedBy = "";
                        }


                        if(model.File != null)
                        {
                            byte[] imageData = null;
                            // считываем переданный файл в массив байтов
                            using (var binaryReader = new BinaryReader(model.File.InputStream))
                            {
                                imageData = binaryReader.ReadBytes(model.File.ContentLength);
                            }
                            // установка массива байтов
                            dbEntity.AppealFile = imageData;
                            dbEntity.AppealFileName = model.File.FileName;
                        }
                        else if(file != null)
                        {
                            dbEntity.AppealFile = file;
                            dbEntity.AppealFileName = fileName;
                        }

                        if (model.PostedFile != null)
                        {
                            byte[] imageData = null;
                            // считываем переданный файл в массив байтов
                            using (var binaryReader = new BinaryReader(model.PostedFile.InputStream))
                            {
                                imageData = binaryReader.ReadBytes(model.PostedFile.ContentLength);
                            }
                            // установка массива байтов
                            dbEntity.AnswerFile = imageData;
                            dbEntity.AnswerFileName = model.PostedFile.FileName;
                        }
                        else if (postedFile != null)
                        {
                            dbEntity.AnswerFile = postedFile;
                            dbEntity.AnswerFileName = answerFileName;
                        }

                        String medicalOrganizations = String.Empty;
                        if (model.MedicalOrganizationId != null)
                        {
                            medicalOrganizations = String.Join(",", model.MedicalOrganizationId.ToList());
                        }
                        dbEntity.MedicalOrganizationId = medicalOrganizations;

                        String organizations = String.Empty;
                        if (model.OrganizationId != null)
                        {
                            organizations = String.Join(",", model.OrganizationId.ToList());
                        }
                        dbEntity.OrganizationId = organizations;

                        dbEntity.UniqueNumberPart1 = GetIntUniqueNumber(model.AppealUniqueNumber);

                        repo.Update(dbEntity);
                        uow.Commit();

                        return JsonRes(new { JournalAppealIdId = dbEntity.JournalAppealId });
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            model.Init();
            return PartialView(model);
        }

        [HttpPost]
        public ActionResult Print(HandAppealPrintModel model)
        {
            try
            {
                string templatePath = @"C:\Temp\HandAppealReport.xlsx";
                string path = @"C:\Temp\TFOMSAppealReport" + DateTime.Now.GetHashCode() + ".xlsx";
                var book = new XLWorkbook(templatePath);
                book.SaveAs(path);
                book = new XLWorkbook(path);
                book.Worksheet(1).Row(2).Cell(3).Value = model.AppealUniqueNumber;
                book.Worksheet(1).Row(3).Cell(3).Value = model.AppealDate;
                book.Worksheet(1).Row(4).Cell(3).Value = model.AppealNumber + " от " + model.OZPZRegistrationDate.ToShortDateString();
                book.Worksheet(1).Row(5).Cell(3).Value = model.AppealOrganizationName;
                book.Worksheet(1).Row(6).Cell(3).Value = model.GuideNumber + " от " + model.GuideDate.ToShortDateString();
                book.Worksheet(1).Row(7).Cell(3).Value = model.Applicant;
                book.Worksheet(1).Row(8).Cell(3).Value = model.ApplicantFeedbackAddress + "\r\n" + model.ApplicantPhoneNumber;
                book.Worksheet(1).Row(9).Cell(3).Value = model.ReceivedTreatmentPerson;
                book.Worksheet(1).Row(10).Cell(3).Value = model.SMOOrganizationName;
                book.Worksheet(1).Row(11).Cell(3).Value = model.WayOfAddressing;
                book.Worksheet(1).Row(12).Cell(3).Value = model.OrganizationName;
                book.Worksheet(1).Row(13).Cell(3).Value = model.PassedEvent;
                book.Worksheet(1).Row(14).Cell(3).Value = model.ComplaintName;
                book.Worksheet(1).Row(15).Cell(3).Value = model.AppealFactEndDate;
                book.Worksheet(1).Row(16).Cell(3).Value = model.Worker;
                using (var uow = UnityManager.Resolve<IUnitOfWork>())
                {
                    var repo = uow.GetRepo<IWorkerRepo>();
                    var entity = repo.GetAll().FirstOrDefault(x => x.IsHead);
                    if (entity != null)
                        book.Worksheet(1).Row(17).Cell(3).Value = entity.Surname + " " + entity.Name + " " + entity.SecondName;
                }

                book.Worksheet(2).Row(1).Cell(1).Value = "АРХИВНОЕ ДЕЛО № " + model.AppealUniqueNumber;
                book.Worksheet(2).Row(2).Cell(1).Value = model.OZPZRegistrationDate.ToShortDateString();
                book.Worksheet(2).Row(3).Cell(5).Value = model.AppealOrganizationName;
                book.Worksheet(2).Row(6).Cell(4).Value = model.Applicant;
                book.Worksheet(2).Row(8).Cell(4).Value = model.ReceivedTreatmentPerson;
                book.Worksheet(2).Row(12).Cell(4).Value = model.SMOOrganizationName;
                book.Worksheet(2).Row(15).Cell(4).Value = model.AppealTheme;
                book.Worksheet(2).Row(19).Cell(4).Value = model.OrganizationName;
                book.Worksheet(2).Row(24).Cell(5).Value = model.AppealNumber + " от " + model.OZPZRegistrationDate.ToShortDateString();

                //book.Save();
                //Response.Clear();
                //Response.ClearHeaders();
                //byte[] bts = System.IO.File.ReadAllBytes(path);
                //Response.AddHeader("Content-Type", "Application/octet-stream");
                //Response.AddHeader("Content-Length", bts.Length.ToString());
                //Response.AddHeader("Content-Disposition", "attachment; filename=TFOMSComplaintReason_" + DateTime.Now.ToShortDateString() + ".xlsx");
                //Response.BinaryWrite(bts);
                //Response.Flush();
                //Response.End();
                string handle = Guid.NewGuid().ToString();
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    book.SaveAs(memoryStream);
                    memoryStream.Position = 0;
                    TempData[handle] = memoryStream.ToArray();
                    System.IO.File.Delete(path);
                }

                // Note we are returning a filename as well as the handle
                return new JsonResult()
                {
                    Data = new { FileGuid = handle, FileName = "TFOMSAppealReport" + DateTime.Now.GetHashCode() + ".xlsx" }
                };
            }
            catch (Exception ex)
            {
                return null;
            }
           
        }

        public ActionResult PrintLetter(HandAppealLetterModel model)
        {
            try
            {
                string fileNameTemplate = @"C:\Temp\HandAppealLetter.docx";

                DocX letter = DocX.Load(fileNameTemplate);

                // Grab a reference to our document template:
                //DocX letter = this.GetRejectionLetterTemplate();

                using (var uow = UnityManager.Resolve<IUnitOfWork>())
                {
                    if(model.SMOOrganizationId.HasValue)
                    {
                        var repo = uow.GetRepo<ISMORepo>();
                        var smo = repo.Get(model.SMOOrganizationId.Value);
                        if(smo != null)
                        {
                            letter.ReplaceText("<SMOHeadPosition>", "Руководителю");

                            String smoHeadIo = String.Empty;
                            smoHeadIo += String.IsNullOrEmpty(smo.HeadName) ? "" : " " + smo.HeadName.Trim();
                            smoHeadIo += String.IsNullOrEmpty(smo.HeadSecondName) ? "" : " " + smo.HeadSecondName.Trim();

                            letter.ReplaceText("<SMO>", smo.FullName);
                            letter.ReplaceText("<SMOHeadIO>", smoHeadIo);

                        }
                        else
                        {
                            letter.ReplaceText("<SMOHeadPosition>", "");
                            letter.ReplaceText("<SMO>", "");
                            letter.ReplaceText("<SMOHeadIO>", "");
                        }
                    }
                    else
                    {
                        letter.ReplaceText("<SMOHeadPosition>", "");
                        letter.ReplaceText("<SMO>", "");
                        letter.ReplaceText("<SMOHeadIO>", "");
                    }

                    String aplicantFio = String.Empty;
                    aplicantFio += String.IsNullOrEmpty(model.ApplicantSurname) ? "" : model.ApplicantSurname.Trim();
                    aplicantFio += String.IsNullOrEmpty(model.ApplicantName) ? "" : " " + model.ApplicantName.Trim().Substring(0, 1) + ".";
                    aplicantFio += String.IsNullOrEmpty(model.ApplicantSecondName) ? "" : " " + model.ApplicantSecondName.Trim().Substring(0, 1) + ".";
                    letter.ReplaceText("<ApplicantFIO>", aplicantFio);

                    String aplicantFioFull = String.Empty;
                    aplicantFioFull += String.IsNullOrEmpty(model.ApplicantSurname) ? "" : model.ApplicantSurname.Trim();
                    aplicantFioFull += String.IsNullOrEmpty(model.ApplicantName) ? "" : " " + model.ApplicantName.Trim();
                    aplicantFioFull += String.IsNullOrEmpty(model.ApplicantSecondName) ? "" : " " + model.ApplicantSecondName.Trim();

                    if(!String.IsNullOrEmpty(aplicantFioFull))
                    {
                        Склонятель склонятель = new Склонятель("SonFhyB1DbaxkkAQ4tfrhQ==");
                        IСклоняемое получатель = склонятель.Проанализировать(aplicantFioFull);

                        String applicantFIORod = получатель.Родительный;
                     

                        String applicantRod = String.Empty;
                        applicantRod = String.IsNullOrEmpty(model.ApplicantSurname) ? "" : applicantFIORod.Split(' ')[0];
                        applicantRod += String.IsNullOrEmpty(model.ApplicantName) ? "" : (" " + model.ApplicantName.Trim().Substring(0, 1) + ".");
                        applicantRod += String.IsNullOrEmpty(model.ApplicantSecondName) ? "" : (" " + model.ApplicantSecondName.Trim().Substring(0, 1) + ".");
                        letter.ReplaceText("<ApplicantFIORod>", applicantRod);
                    }
                    else
                    {
                        letter.ReplaceText("<ApplicantFIORod>", "");
                    }

                    letter.ReplaceText("<ApplicantInfo>", !String.IsNullOrEmpty(model.ApplicantFeedbackAddress) 
                        ? model.ApplicantFeedbackAddress.Trim()
                            : String.IsNullOrEmpty(model.ApplicantEmail)
                                ? "" : model.ApplicantEmail.Trim());

                    if (model.WorkerId.HasValue)
                    {
                        var repo = uow.GetRepo<IWorkerRepo>();

                        var worker = repo.Get(model.WorkerId.Value);

                        if (worker != null)
                        {
                            String workerInfo = String.Empty;
                            workerInfo += String.IsNullOrEmpty(worker.Surname) ? "" : worker.Surname.Trim();
                            workerInfo += String.IsNullOrEmpty(worker.Name) ? "" : " " + worker.Name.Trim().Substring(0, 1) + ".";
                            workerInfo += String.IsNullOrEmpty(worker.SecondName) ? "" : " " + worker.SecondName.Trim().Substring(0, 1) + ".";
                            workerInfo += String.IsNullOrEmpty(worker.Phone) ? "" : ", " + worker.Phone.Trim();
                            letter.ReplaceText("<Worker>", workerInfo);
                        }
                        else
                        {
                            letter.ReplaceText("<Worker>", "");
                        }
                        
                    }
                    else
                    {
                        letter.ReplaceText("<Worker>", "");
                    }
                    
                }



                //сохраняем
                string handle = Guid.NewGuid().ToString();
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    letter.SaveAs(memoryStream);
                    memoryStream.Position = 0;
                    TempData[handle] = memoryStream.ToArray();
                    //System.IO.File.Delete(path);
                }

                // Note we are returning a filename as well as the handle
                return new JsonResult()
                {
                    Data = new { FileGuid = handle, FileName = "TFOMSLetter" + DateTime.Now.GetHashCode() + ".docx" }
                };
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        private Int32 GetIntUniqueNumber(String uniqueNumber)
        {
            Int32 out_;
            String value = String.Empty;
            foreach(char c in uniqueNumber)
            {
                if (Int32.TryParse(c.ToString(), out out_))
                {
                    value += c;
                }
                else
                {
                    break;
                }
            }

            return value.Length > 0 ? Convert.ToInt32(value) : 0;
        }

        //[HttpPost]
        //public JsonResult GenerateUniqueNumber(GenerateUniqueNumberModel model)
        //{
        //    UniqueNumberModel returnModel = Generate(model);
        //    return Json(returnModel);
        //}

        //private List<string> CheckUniqueNumber(HandAppealCreateModel model)
        //{
        //    List<string> error = new List<string>();
        //    string appealUniqueNum = model.AppealUniqueNumber;
        //    Regex regexFullNumber = new Regex(@"^([0-9]+)/([0-9]+)-([0-9]+)$");
        //    Regex regexMidNumber = new Regex(@"^([0-9]+)/([0-9]+)$");
        //    Regex regexShortNumber = new Regex(@"^([0-9]+)$");

        //    String applicantSurname = !String.IsNullOrEmpty(model.ApplicantSurname) ? model.ApplicantSurname.Trim().ToUpper() : "";
        //    String applicantName = !String.IsNullOrEmpty(model.ApplicantName) ? model.ApplicantName.Trim().ToUpper() : "";
        //    String applicantSecondName = !String.IsNullOrEmpty(model.ApplicantSecondName) ? model.ApplicantSecondName.Trim().ToUpper() : "";
        //    String receivedTreatmentPersonName = !String.IsNullOrEmpty(model.ReceivedTreatmentPersonName) ? model.ReceivedTreatmentPersonName.Trim().ToUpper() : "";
        //    String receivedTreatmentPersonSecondName = !String.IsNullOrEmpty(model.ReceivedTreatmentPersonSecondName) ? model.ReceivedTreatmentPersonSecondName.Trim().ToUpper() : "";
        //    String receivedTreatmentPersonSurname = !String.IsNullOrEmpty(model.ReceivedTreatmentPersonSurname) ? model.ReceivedTreatmentPersonSurname.Trim().ToUpper() : "";

        //    if (applicantSurname == "")
        //    {
        //        error.Add($"Не указана фамилия заявителя");
        //    }
        //    if (applicantName == "")
        //    {
        //        error.Add($"Не указано имя заявителя");
        //    }
        //    if (receivedTreatmentPersonName == "")
        //    {
        //        error.Add($"Не указано имя отправителя");
        //    }
        //    if (receivedTreatmentPersonSurname == "")
        //    {
        //        error.Add($"Не указана фамилия заявителя");
        //    }
        //    if (applicantSurname == "" || applicantName == "" || receivedTreatmentPersonName == "" || receivedTreatmentPersonSurname == "")
        //    {
        //        return error;
        //    }

        //    Int32 uniqueNumberPart1 = 1;
        //    Int32 uniqueNumberPart2 = 1;
        //    Int32 uniqueNumberPart3 = 1;


        //    Match matchFull = regexFullNumber.Match(appealUniqueNum);
        //    Match matchMid = regexMidNumber.Match(appealUniqueNum);
        //    Match matchShort = regexShortNumber.Match(appealUniqueNum);
        //    if (matchFull.Success || matchMid.Success || matchShort.Success)
        //    {
        //        if(matchFull.Success)
        //        {
        //            uniqueNumberPart1 = Convert.ToInt32(appealUniqueNum.Split('/')[0]);
        //            uniqueNumberPart2 = Convert.ToInt32(appealUniqueNum.Split('/')[1].Split('-')[0]);
        //            uniqueNumberPart3 = Convert.ToInt32(appealUniqueNum.Split('/')[1].Split('-')[1]);
        //        }
        //        else if(matchMid.Success)
        //        {
        //            uniqueNumberPart1 = Convert.ToInt32(appealUniqueNum.Split('/')[0]);
        //            uniqueNumberPart2 = Convert.ToInt32(appealUniqueNum.Split('/')[1]);
        //        }
        //        else
        //        {
        //            uniqueNumberPart1 = Convert.ToInt32(appealUniqueNum);
        //        }

        //        using (var uow = UnityManager.Resolve<IUnitOfWork>())
        //        {
        //            var repo = uow.GetRepo<IHandAppealRepo>();
        //            var applicantList = repo.GetByApplicantFIO(model.ApplicantSurname.Trim().ToUpper(), model.ApplicantName.Trim().ToUpper(), model.ApplicantSecondName.Trim().ToUpper()).ToList();
        //            try
        //            {
        //                if (applicantList.Count == 0 || applicantList.Max(x => x.UniqueNumberPart1) == 0 || applicantList.Max(x => x.UniqueNumberPart1) == uniqueNumberPart1)
        //                {
        //                    if (!(applicantList.Count == 0 || applicantList.Max(x => x.UniqueNumberPart1) == 0) 
        //                        || ((applicantList.Count == 0 || applicantList.Max(x => x.UniqueNumberPart1) == 0) && !NumberIsFind(ref error, uniqueNumberPart1)))
        //                    {
        //                        var receivedTreatmentPersonList = repo.GetByReceivedTreatmentPersonFIO(uniqueNumberPart1,
        //                                                        model.ReceivedTreatmentPersonSurname.Trim().ToUpper(),
        //                                                            model.ReceivedTreatmentPersonName.Trim().ToUpper(),
        //                                                                model.ReceivedTreatmentPersonSecondName.Trim().ToUpper()).ToList();

        //                        if (receivedTreatmentPersonList.Count == 0 || receivedTreatmentPersonList.Max(x => x.UniqueNumberPart2) == 0
        //                            || receivedTreatmentPersonList.Max(x => x.UniqueNumberPart2) == uniqueNumberPart2)
        //                        {
        //                            if ((!(receivedTreatmentPersonList.Count == 0 || receivedTreatmentPersonList.Max(x => x.UniqueNumberPart2) == 0) 
        //                                || (receivedTreatmentPersonList.Count == 0 || receivedTreatmentPersonList.Max(x => x.UniqueNumberPart2) == 0)
        //                            && !NumberIsFind(ref error, uniqueNumberPart1, uniqueNumberPart2)))
        //                            {
        //                                if (!NumberIsFind(ref error, uniqueNumberPart1, uniqueNumberPart2, uniqueNumberPart3, model.UniqueNumberPart3))
        //                                {
        //                                    model.UniqueNumberPart1 = uniqueNumberPart1;
        //                                    model.UniqueNumberPart2 = uniqueNumberPart2;
        //                                    model.UniqueNumberPart3 = uniqueNumberPart3;
        //                                }
        //                                else
        //                                {
        //                                    error.Add($"Данная цифра после тире уже естьв этой связке заявитель/отправитель");
        //                                }
        //                            }
        //                            else
        //                            {
        //                                error.Add($"Данный номер присвоен другому отправителю");
        //                            }


        //                        }
        //                        else
        //                        {
        //                            error.Add($"Данному представителю присвоен уникальный номер: {receivedTreatmentPersonList.Max(x => x.UniqueNumberPart2)}");
        //                        }
        //                    }
        //                    else
        //                    {
        //                        error.Add($"Данный номер присвоен другому заявителю");
        //                    }
        //                }
        //                else
        //                {
        //                    error.Add($"Данному заявителю присвоен уникальный номер: {applicantList.Max(x => x.UniqueNumberPart1)}");
        //                }
        //            }
        //            catch (Exception ex)
        //            {

        //                throw;
        //            }

        //        }

        //    }
        //    else
        //    {
        //        error.Add($"Введен некорректный номер");
        //    }

        //    return error;
        //}

        private Boolean CheckUniqueNumber(String uniqueNumber, String baseNumber = "")
        {
            using (var uow = UnityManager.Resolve<IUnitOfWork>())
            {
                var repo = uow.GetRepo<IHandAppealRepo>();
                return repo.CheckUniqueNumber(uniqueNumber, baseNumber);
            }
               
        }

        //private Boolean NumberIsFind(ref List<string> error, params Int32[] uniqueNumberParts)
        //{
        //    using (var uow = UnityManager.Resolve<IUnitOfWork>())
        //    {
        //        var repo = uow.GetRepo<IHandAppealRepo>();

        //        return !repo.CheckUniqueNumberPart(uniqueNumberParts);
        //    }
        //}

        public void GetFile(Int64 journalAppealId, Boolean isAnswer)
        {
            try
            {
                using (var uow = UnityManager.Resolve<IUnitOfWork>())
                {
                    var repo = uow.GetRepo<IHandAppealRepo>();
                    var file = repo.GetAll().Where(x => x.JournalAppealId == journalAppealId).FirstOrDefault();
                    byte[] bts = isAnswer ? file.AnswerFile : file.AppealFile;
                    Response.AddHeader("Content-Type", "Application/octet-stream");
                    Response.AddHeader("Content-Length", bts.Length.ToString());
                    Response.AddHeader("Content-Disposition", "attachment; filename=" + (isAnswer ? file.AnswerFileName : file.AppealFileName));
                    Response.BinaryWrite(bts);
                    Response.Flush();
                    Response.End();
                }
            }
            catch (Exception ex)
            {
               
            }
        }

        //private UniqueNumberModel Generate(GenerateUniqueNumberModel model)
        //{
        //    using (var uow = UnityManager.Resolve<IUnitOfWork>())
        //    {
        //        var repo = uow.GetRepo<IHandAppealRepo>();
        //        UniqueNumberModel returnModel = new UniqueNumberModel();

        //        String applicantSurname = !String.IsNullOrEmpty(model.ApplicantSurname) ? model.ApplicantSurname.Trim().ToUpper() : "";
        //        String applicantName = !String.IsNullOrEmpty(model.ApplicantName) ? model.ApplicantName.Trim().ToUpper() : "";
        //        String applicantSecondName = !String.IsNullOrEmpty(model.ApplicantSecondName) ? model.ApplicantSecondName.Trim().ToUpper() : "";
        //        String receivedTreatmentPersonName = !String.IsNullOrEmpty(model.ReceivedTreatmentPersonName) ? model.ReceivedTreatmentPersonName.Trim().ToUpper() : "";
        //        String receivedTreatmentPersonSecondName = !String.IsNullOrEmpty(model.ReceivedTreatmentPersonSecondName) ? model.ReceivedTreatmentPersonSecondName.Trim().ToUpper() : "";
        //        String receivedTreatmentPersonSurname = !String.IsNullOrEmpty(model.ReceivedTreatmentPersonSurname) ? model.ReceivedTreatmentPersonSurname.Trim().ToUpper() : "";

        //        if (applicantSurname == "" || applicantName == "" || receivedTreatmentPersonName == "" || receivedTreatmentPersonSurname == "")
        //        {
        //            return returnModel;
        //        }


        //        if (model.Id.HasValue)
        //        {
        //            var baseEntity = repo.Get(model.Id.Value);
        //            if (baseEntity.ApplicantName.Trim().ToUpper() == applicantName
        //                    && baseEntity.ApplicantSecondName.Trim().ToUpper() == applicantSecondName
        //                        && baseEntity.ApplicantSurname.Trim().ToUpper() == applicantSurname
        //                            && baseEntity.ReceivedTreatmentPersonName.Trim().ToUpper() == receivedTreatmentPersonName
        //                                && baseEntity.ReceivedTreatmentPersonSecondName.Trim().ToUpper() == receivedTreatmentPersonSecondName
        //                                    && baseEntity.ReceivedTreatmentPersonSurname.Trim().ToUpper() == receivedTreatmentPersonSurname)
        //            {
        //                returnModel.UniqueNumberPart1 = baseEntity.UniqueNumberPart1;
        //                returnModel.UniqueNumberPart2 = baseEntity.UniqueNumberPart2;
        //                returnModel.UniqueNumberPart3 = baseEntity.UniqueNumberPart3;
        //                returnModel.AppealUniqueNumber = baseEntity.AppealUniqueNumber;

        //                return returnModel;
        //            }
        //        }

        //        var dbEntity = repo.GetByApplicantFIO(applicantSurname, applicantName, applicantSecondName);
        //        if (dbEntity == null || dbEntity.Count() == 0)
        //        {
        //            Int32 uniqueNumberPart1 = repo.GetMaxUniqueNumberPart1();

        //            returnModel.UniqueNumberPart1 = uniqueNumberPart1;
        //            returnModel.UniqueNumberPart2 = 1;
        //            returnModel.UniqueNumberPart3 = 1;
        //            returnModel.AppealUniqueNumber = uniqueNumberPart1 + "";
        //        }
        //        else
        //        {
        //            Int32 uniqueNumberPart1 = dbEntity.Max(x => x.UniqueNumberPart1);

        //            if (uniqueNumberPart1 == 0)
        //                uniqueNumberPart1++;

        //            var dbEntity2 = repo.GetByReceivedTreatmentPersonFIO(uniqueNumberPart1,
        //                receivedTreatmentPersonSurname,
        //                    receivedTreatmentPersonName,
        //                        receivedTreatmentPersonSecondName);

        //            if (dbEntity2 == null || dbEntity2.Count() == 0)
        //            {
        //                Int32 uniqueNumberPart2 = repo.GetMaxUniqueNumberPart2(uniqueNumberPart1);

        //                returnModel.UniqueNumberPart1 = uniqueNumberPart1;
        //                returnModel.UniqueNumberPart2 = uniqueNumberPart2;
        //                returnModel.UniqueNumberPart3 = 1;
        //                returnModel.AppealUniqueNumber = uniqueNumberPart1 + (uniqueNumberPart2 != 1 ? "/" + uniqueNumberPart2 : "");
        //            }
        //            else
        //            {
        //                Int32 uniqueNumberPart2 = dbEntity2.Max(x => x.UniqueNumberPart2);

        //                if (uniqueNumberPart2 == 0)
        //                    uniqueNumberPart2++;

        //                Int32 uniqueNumberPart3 = repo.GetMaxUniqueNumberPart3(uniqueNumberPart1, uniqueNumberPart2);

        //                returnModel.UniqueNumberPart1 = uniqueNumberPart1;
        //                returnModel.UniqueNumberPart2 = uniqueNumberPart2;
        //                returnModel.UniqueNumberPart3 = uniqueNumberPart3;
        //                returnModel.AppealUniqueNumber = uniqueNumberPart1 +
        //                                                    (uniqueNumberPart2 != 1 ? "/" + uniqueNumberPart2 : "") +
        //                                                        (uniqueNumberPart3 != 1 ? "-" + uniqueNumberPart3 : "");
        //            }
        //        }

        //        return returnModel;
        //    }
        //}
    }
}