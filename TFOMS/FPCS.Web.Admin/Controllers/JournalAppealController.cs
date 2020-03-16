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
using FPCS.Web.Admin.Models.JournalAppeal;
using System.IO;

using System.Text;
using System.Diagnostics;
using System.Xml;
using System.Web.UI;
using ClosedXML.Excel;
using System.Reflection;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Wordprocessing;
using System.Linq.Dynamic;

namespace FPCS.Web.Admin.Controllers
{
    public class JournalAppealController : BaseController
    {
        public ActionResult Index(string message = "")
        {

            JournalAppealDateAppealModel model = new JournalAppealDateAppealModel();
            model.DateFrom = DateTime.Now.AddMonths(-1);
            model.DateTo = DateTime.Now;
            model.Message = message;
            return View(model);
        }

        public JsonResult _Index(GridOptions options, JournalAppealListOptions journalAppealListOptions, DateTime? dateFromFilter, DateTime? dateToFilter)
        {
            using (var uow = UnityManager.Resolve<IUnitOfWork>())
            {
                var repo = uow.GetRepo<IJournalAppealRepo>();
                var handAppealRepo = uow.GetRepo<IHandAppealRepo>();
                var organizationRepo = uow.GetRepo<IOrganizationRepo>();

                var organizations = organizationRepo.GetAll().ToList();
                //var handAppeals = handAppealRepo.GetAll().ToList();
                var handAppealsIds = handAppealRepo.GetAll().Select(x => x.JournalAppealId).ToList();

                string userName = User.Login;
                var dbUserRepo = uow.GetRepo<IDbUserRepo>();
                var dbUser = dbUserRepo.GetByLogin(userName);
                Int64 smo = 0;
                if (dbUser.Role != Role.Admin && dbUser.Role != Role.TfomsUser)
                {
                    var userRuepo = uow.GetRepo<IUserRepo>();
                    smo = userRuepo.Get(dbUser.DbUserId).SMOId.Value;
                }

                
                var dateFrom = DateTime.Today.AddDays(-30);

               
                var journalAppeal = repo.GetAll()
                    //.Where(x => x.CreatedDate >= dateFrom)
                    .Where(x => x.Date.HasValue && x.SourceIncomeId.HasValue && x.TypeOfAddressingId.HasValue && x.WayOfAddressingId.HasValue
                        && x.ThemeAppealCitizensId.HasValue && x.AppealOrganizationId.HasValue && x.TakingAppealLineId.HasValue 
                        && ((!String.IsNullOrEmpty(x.AcceptedBy) && handAppealsIds.Contains(x.JournalAppealId)) || !handAppealsIds.Contains(x.JournalAppealId))
                            && !String.IsNullOrEmpty(x.AppealOrganizationCode) && x.AppealPlanEndDate.HasValue)
                    .Where(x => (dateFromFilter.HasValue ? x.Date >= dateFromFilter : x.JournalAppealId != 0) 
                    && (dateToFilter.HasValue ? x.Date <= dateToFilter : x.JournalAppealId != 0) 
                    && (smo != 0 ? x.SMO.SmoId == smo : 1==1))
                    .Select(x => new
                    {
                        Id = x.JournalAppealId,
                        x.AppealUniqueNumber,
                        x.Date,
                        AppealTheme = x.ThemeAppealCitizens.Name,
                        x.AcceptedBy,
                        x.Responsible,
                        x.AppealPlanEndDate,
                        x.AppealFactEndDate,
                        ResultName = x.AppealResult != null ? x.AppealResult.Name : "",
                        AppealOrganizationCode = x.SMO.ShortName,
                        x.CreatedDate,
                        ReceivedTreatmentPerson = x.ReceivedTreatmentPersonSurname + " " + x.ReceivedTreatmentPersonName + " " + x.ReceivedTreatmentPersonSecondName,
                        Applicant = x.ApplicantSurname + " " + x.ApplicantName + " " + x.ApplicantSecondName
                    }).AsEnumerable()
                    .Select(x =>
                    {
                        string appealCode = String.Empty;
                        string appealName = String.Empty;
                        string organizationsName = String.Empty;
                        if (handAppealsIds.Contains(x.Id))
                        {

                            var handAppeal = handAppealRepo.Get(x.Id);

                            if (handAppeal != null)
                            {
                                appealCode = handAppeal.PassedEvent != null ? handAppeal.PassedEvent.Code : "";
                                appealName = handAppeal.WayOfAddressing != null ? handAppeal.WayOfAddressing.Name : "";

                                organizationsName = String.Join(",", organizations.Where(z => handAppeal.OrganizationId.Split(',').ToList().Contains(z.OrganizationId.ToString())).Select(z => z.Name).ToList());
                            }
                        }
                        return new
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
                            x.CreatedDate,
                            x.ReceivedTreatmentPerson,
                            x.Applicant,
                            AppealCode = appealCode,
                            AppealName = appealName,
                            OrganizationsName = organizationsName
                        };
                    }
                    
                    );

                                 
                options.IsSearch = true;
                var engine = new GridDynamicEngine(options, journalAppealListOptions);
                var resultTemp = engine.ApplySort(engine.ApplyFilter(journalAppeal.AsQueryable())).Select(x => new JournalAppealIndexModel
                {
                    Id = x.Id,
                    AppealUniqueNumber = x.AppealUniqueNumber,
                    Date = x.Date.Value.Date.ToString("dd/MM/yyyy"),
                    AppealTheme = x.AppealTheme,
                    AcceptedBy = x.AcceptedBy,
                    Responsible = x.Responsible,
                    AppealPlanEndDate = x.AppealPlanEndDate.Value.Date.ToString("dd/MM/yyyy"),
                    AppealFactEndDate = x.AppealFactEndDate.HasValue ? x.AppealFactEndDate.Value.Date.ToString("dd/MM/yyyy") : String.Empty,
                    ResultName = x.ResultName,
                    AppealOrganizationCode = x.AppealOrganizationCode,
                    AppealCode = x.AppealCode,
                    AppealName = x.AppealName,
                    ReceivedTreatmentPerson = x.ReceivedTreatmentPerson,
                    Applicant = x.Applicant,
                    OrganizationsName = x.OrganizationsName
                });


                int totalCount = 0;
                var sessionOptions = (JournalAppealListOptions)Session["journalAppealListOptions" + User.Login]; ;
                if (Session["journalAppealCount" + User.Login] == null
                    || (DateTime?)Session["journalAppealDateFromFilter" + User.Login] != dateFromFilter 
                    || (DateTime?)Session["journalAppealDateToFilter" + User.Login] != dateToFilter
                    || sessionOptions == null
                    || sessionOptions.AcceptedBy != journalAppealListOptions.AcceptedBy
                    || sessionOptions.AppealCode != journalAppealListOptions.AppealCode
                    || sessionOptions.AppealName != journalAppealListOptions.AppealName
                    || sessionOptions.AppealOrganizationCode != journalAppealListOptions.AppealOrganizationCode
                    || sessionOptions.AppealTheme != journalAppealListOptions.AppealTheme
                    || sessionOptions.AppealUniqueNumber != journalAppealListOptions.AppealUniqueNumber
                    || sessionOptions.Applicant != journalAppealListOptions.Applicant
                   )
                {
                    totalCount = resultTemp.Count();
                    Session["journalAppealCount" + User.Login] = totalCount;
                    Session["journalAppealDateFromFilter" + User.Login] = dateFromFilter;
                    Session["journalAppealDateToFilter" + User.Login] = dateToFilter;
                    Session["journalAppealListOptions" + User.Login] = journalAppealListOptions;
                }
                else
                {
                    totalCount = Convert.ToInt32(Session["journalAppealCount" + User.Login]);
                }

                engine = new GridDynamicEngine(options, journalAppealListOptions);
                var result = engine.CreateGridResult2(engine.ApplyPaging(resultTemp.AsQueryable()), totalCount, x => new JournalAppealIndexModel
                {
                    Id = x.Id,
                    AppealUniqueNumber = x.AppealUniqueNumber,
                    Date = x.Date,
                    AppealTheme = x.AppealTheme,
                    AcceptedBy = x.AcceptedBy,
                    Responsible = x.Responsible,
                    AppealPlanEndDate = x.AppealPlanEndDate,
                    AppealFactEndDate = x.AppealFactEndDate,
                    ResultName = x.ResultName,
                    AppealOrganizationCode = x.AppealOrganizationCode,
                    ReceivedTreatmentPerson = x.ReceivedTreatmentPerson,
                    Applicant = x.Applicant,
                    AppealCode = x.AppealCode,
                    AppealName = x.AppealName,
                    OrganizationsName = x.OrganizationsName
                });


                return Json(result);
            }
        }

        [HttpGet]
        public PartialViewResult _Detail(Int32 id)
        {
            using (var uow = UnityManager.Resolve<IUnitOfWork>())
            {
                var repo = uow.GetRepo<IJournalAppealRepo>();
                var dbEntity = repo.Get(id);
                if (dbEntity == null) return ErrorPartial("Обращение {0} не найдено", id);

                var model = new JournalAppealDetailModel
                {
                    Id = dbEntity.JournalAppealId,
                    AppealUniqueNumber = dbEntity.AppealUniqueNumber,
                    Date = dbEntity.Date.Value.Date.ToString("dd/MM/yyyy"),
                    Time = dbEntity.Time,
                    SourceIncomeName = dbEntity.SourceIncome != null ? dbEntity.SourceIncome.Name : String.Empty,
                    OrganizationName = dbEntity.OrganizationName,
                    TypeOfAddressingName = dbEntity.TypeOfAddressing != null ? dbEntity.TypeOfAddressing.Name : String.Empty,
                    WayOfAddressingName = dbEntity.WayOfAddressing != null ? dbEntity.WayOfAddressing.Name : String.Empty,
                    AppealTheme = dbEntity.ThemeAppealCitizens.Name,
                    AppealContent = dbEntity.AppealContent,
                    ComplaintName = dbEntity.Complaint != null ? dbEntity.Complaint.Name : String.Empty,
                    AppealOrganizationName = dbEntity.AppealOrganization != null ? dbEntity.AppealOrganization.Name : String.Empty,
                    AppealOrganizationCode = dbEntity.SMO.ShortName,
                    TakingAppealLineName = dbEntity.TakingAppealLine != null ? dbEntity.TakingAppealLine.Name : String.Empty,
                    AcceptedBy = dbEntity.AcceptedBy,
                    ReviewAppealLineName = dbEntity.ReviewAppealLine != null ? dbEntity.ReviewAppealLine.Name : String.Empty,
                    Responsible = dbEntity.Responsible,
                    AppealPlanEndDate = dbEntity.AppealPlanEndDate.Value.Date.ToString("dd/MM/yyyy"),
                    AppealFactEndDate = dbEntity.AppealFactEndDate.HasValue ? dbEntity.AppealFactEndDate.Value.Date.ToString("dd/MM/yyyy") : String.Empty,
                    AppealResultName = dbEntity.AppealResult != null ? dbEntity.AppealResult.Name : String.Empty,
                    ApplicantSurname = dbEntity.ApplicantSurname,
                    ApplicantName = dbEntity.ApplicantName,
                    ApplicantSecondName = dbEntity.ApplicantSecondName,
                    ApplicantBirthDate = dbEntity.ApplicantBirthDate.HasValue ? dbEntity.ApplicantBirthDate.Value.Date.ToString("dd/MM/yyyy") : String.Empty,
                    ApplicantENP = dbEntity.ApplicantENP,
                    ApplicantSMO = dbEntity.ApplicantSMO,
                    ApplicantTypeDocument = dbEntity.ApplicantTypeDocument,
                    ApplicantDocumentSeries = dbEntity.ApplicantDocumentSeries,
                    ApplicantDocumentNumber = dbEntity.ApplicantDocumentNumber,
                    ApplicantFeedbackAddress = dbEntity.ApplicantFeedbackAddress,
                    ApplicantPhoneNumber = dbEntity.ApplicantPhoneNumber,
                    ApplicantEmail = dbEntity.ApplicantEmail,
                    ReceivedTreatmentPersonSurname = dbEntity.ReceivedTreatmentPersonSurname,
                    ReceivedTreatmentPersonName = dbEntity.ReceivedTreatmentPersonName,
                    ReceivedTreatmentPersonSecondName = dbEntity.ReceivedTreatmentPersonSecondName,
                    ReceivedTreatmentPersonBirthDate = dbEntity.ReceivedTreatmentPersonBirthDate.HasValue ? dbEntity.ReceivedTreatmentPersonBirthDate.Value.Date.ToString("dd/MM/yyyy") : String.Empty,
                    ReceivedTreatmentPersonENP = dbEntity.ReceivedTreatmentPersonENP,
                    ReceivedTreatmentPersonSMO = dbEntity.ReceivedTreatmentPersonSMO,
                    ReceivedTreatmentPersonypeDocument = dbEntity.ReceivedTreatmentPersonypeDocument,
                    ReceivedTreatmentPersonDocumentSeries = dbEntity.ReceivedTreatmentPersonDocumentSeries,
                    ReceivedTreatmentPersonDocumentNumber = dbEntity.ReceivedTreatmentPersonDocumentNumber
                };

                return PartialView(model);
            }
        }

        [HttpPost]
        public ActionResult _PrintReport1(string fName)
        {
            var ms = Session[fName] as MemoryStream;
            if (ms == null)
                    return new EmptyResult();
            Session[fName] = null;
            return File(ms, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fName);


            //string path = @"C:\Temp\TFOMSReport1.xlsx";
            //byte[] bts = System.IO.File.ReadAllBytes(path);
            //Response.Clear();
            //Response.ClearHeaders();
            //Response.AddHeader("Content-Type", "Application/octet-stream");
            //Response.AddHeader("Content-Length", bts.Length.ToString());
            //Response.AddHeader("Content-Disposition", "attachment; filename=Test.xlsx");
            //Response.BinaryWrite(bts);
            //Response.Flush();
            //Response.End();
            //return JsonRes();
        }

        public ActionResult GenerateExcelReport()
          {
            string path = @"C:\Temp\TFOMSReport1.xlsx";
            Workbook book = new Workbook(path);

            MemoryStream ms = new MemoryStream();
            book.Save(ms);
            ms.Position = 0;
            var fName = path;
            Session[fName] = ms;
            return Json(new { success = true, fName }, JsonRequestBehavior.AllowGet);
        }


    [HttpPost]
        public ActionResult _PrintReport2()
        {
            string path = @"C:\Temp\TFOMSReport2.xlsx";
            Response.Clear();
            Response.ClearHeaders();
            byte[] bts = System.IO.File.ReadAllBytes(path);
            Response.AddHeader("Content-Type", "Application/octet-stream");
            Response.AddHeader("Content-Length", bts.Length.ToString());
            Response.AddHeader("Content-Disposition", "attachment; filename=TFOMSReport2.xlsx");
            Response.BinaryWrite(bts);
            Response.Flush();
            Response.End();
            return JsonRes();
        }

        [HttpGet]
        public void _PrintReport3()
        {

        }
    }
}