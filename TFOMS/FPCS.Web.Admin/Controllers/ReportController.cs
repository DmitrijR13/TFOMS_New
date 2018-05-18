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
using FPCS.Web.Admin.Models.Report;
using System.Configuration;
using System.Data;
using Npgsql;

namespace FPCS.Web.Admin.Controllers
{
    public class ReportController : BaseController
    {
        public ActionResult Index()
        {
            ReportIndexModel model = new ReportIndexModel();
            model.Init();
            model.DateFrom = DateTime.Now.AddDays(-1);
            model.DateTo = DateTime.Now;
            model.DateFromTemp = DateTime.Now.AddDays(-1);
            model.DateToTemp = DateTime.Now;
            return View(model);
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
        public void Report3(DateTime? dateFromPost3, DateTime? dateToPost3, Int64? smoIdPost3)
        {
            string templatePath = @"C:\Temp\TFOMSReport3.xlsx";
            string path = @"C:\Temp\TFOMSReport" + DateTime.Now.GetHashCode() + ".xlsx";
            var book = new XLWorkbook(templatePath);
            book.SaveAs(path);
            book = new XLWorkbook(path);
            book.Worksheet(1).Row(3).Cell(1).Value = "В период с " +
                (dateFromPost3.HasValue ? dateFromPost3.Value.ToShortDateString() : "...") + " по " + 
                (dateToPost3.HasValue ? dateToPost3.Value.ToShortDateString() : "...");
            using (var uow = UnityManager.Resolve<IUnitOfWork>())
            {
                var repo = uow.GetRepo<ISMORepo>();
                if (smoIdPost3.HasValue)
                {
                    var smoName = repo.Get(smoIdPost3.Value).FullName;
                    book.Worksheet(1).Row(4).Cell(3).Value = smoName;
                }
                else
                {
                    book.Worksheet(1).Row(4).Cell(3).Value = "Все СМО";
                }
            }
            int row = 8;

            using (var uow = UnityManager.Resolve<IUnitOfWork>())
            {
                var repo = uow.GetRepo<IHandAppealRepo>();
                var handAppeals = repo.GetAll().Where(x => !dateFromPost3.HasValue || (x.Date >= dateFromPost3.Value))
                    .Where(x => !dateToPost3.HasValue || (x.Date <= dateToPost3.Value))
                    .Where(x => !smoIdPost3.HasValue || (x.SMOId == smoIdPost3.Value))
                    .Select(x => new
                    {
                        TypeOfAddressing = x.TypeOfAddressing != null ? x.TypeOfAddressing.Name : "",
                        WayOfAddressing = x.WayOfAddressing != null ? x.WayOfAddressing.Name : "",
                        ThemeAppealCitizens = x.ThemeAppealCitizens != null ? x.ThemeAppealCitizens.Name : "",
                        Complaint = x.Complaint != null ? x.Complaint.Name : "",
                        Worker = x.Worker != null ? x.Worker.Surname + " " + x.Worker.Name + " " + x.Worker.SecondName : "",
                        AppealResult = x.AppealResult != null ? x.AppealResult.Name : ""
                    });

                foreach (var handAppeal in handAppeals)
                {
                    book.Worksheet(1).Row(row).Cell(1).Value = handAppeal.TypeOfAddressing;
                    book.Worksheet(1).Row(row).Cell(2).Value = handAppeal.WayOfAddressing;
                    book.Worksheet(1).Row(row).Cell(3).Value = handAppeal.ThemeAppealCitizens;
                    book.Worksheet(1).Row(row).Cell(4).Value = handAppeal.Complaint;
                    book.Worksheet(1).Row(row).Cell(5).Value = handAppeal.Worker;
                    book.Worksheet(1).Row(row).Cell(6).Value = handAppeal.AppealResult;

                    row++;

                }
            }

          
            book.Save();
            Response.Clear();
            Response.ClearHeaders();
            byte[] bts = System.IO.File.ReadAllBytes(path);
            Response.AddHeader("Content-Type", "Application/octet-stream");
            Response.AddHeader("Content-Length", bts.Length.ToString());
            Response.AddHeader("Content-Disposition", "attachment; filename=TFOMSTerfond_" + DateTime.Now.ToShortDateString() + ".xlsx");
            Response.BinaryWrite(bts);
            Response.Flush();
            Response.End();
            System.IO.File.Delete(path);
        }

        [HttpPost]
        public void Report2(DateTime? dateFromPost2, DateTime? dateToPost2, Int64? smoIdPost2)
        {
            Dictionary<string, string> codeRow = new Dictionary<string, string>();
            codeRow.Add("1.1.", "10");
            codeRow.Add("1.2.", "11");
            codeRow.Add("1.3.", "14");
            codeRow.Add("1.4.", "15");
            codeRow.Add("1.6.", "19");
            codeRow.Add("1.7.", "20");
            codeRow.Add("1.8.", "21");
            codeRow.Add("1.9.", "22");
            codeRow.Add("1.10.", "23");
            codeRow.Add("1.11.", "24");
            codeRow.Add("1.12.", "25");
            codeRow.Add("1.16.", "28");
            codeRow.Add("1.14.", "29");
            codeRow.Add("1.17.", "32");
            codeRow.Add("1.19.", "34");

            string templatePath = @"C:\Temp\TFOMSReport2.xlsx";
            string path = @"C:\Temp\TFOMSReport"+DateTime.Now.GetHashCode()+".xlsx";
            var book = new XLWorkbook(templatePath);
            book.SaveAs(path);
            book = new XLWorkbook(path);
            book.Worksheet(1).Row(4).Cell(2).Value = (dateFromPost2.HasValue ? dateFromPost2.Value.ToShortDateString() : "...") + "-" +
                (dateToPost2.HasValue ? dateToPost2.Value.ToShortDateString() : "...");
            using (var uow = UnityManager.Resolve<IUnitOfWork>())
            {
                var repo = uow.GetRepo<ISMORepo>();
                if(smoIdPost2.HasValue)
                {
                    var smoName = repo.Get(smoIdPost2.Value).FullName;
                    book.Worksheet(1).Row(3).Cell(2).Value = smoName;
                }
                else
                {
                    book.Worksheet(1).Row(3).Cell(2).Value = "Все СМО";
                }
            }

            string where = "where wayofaddressingid = 2 ";
            if (dateFromPost2.HasValue)
            {
                where += " and date>= '" + dateFromPost2.Value.ToShortDateString() + "'";
            }
            else
            {
                where += " and 1 = 1 ";
            }

            if (dateToPost2.HasValue)
            {
                where += "and date <= '"+ dateToPost2.Value.ToShortDateString() + "'";
            }

            if (smoIdPost2.HasValue)
            {
                where += " and ja.smoid = " + smoIdPost2.Value;
            }
            ReportSummary repSum = new ReportSummary();
            DataTable dt = GetDataComplaints(where);
            if(dt != null)
            {
                for(int i = 0; i < dt.Rows.Count; i++)
                {
                    if(codeRow.ContainsKey(dt.Rows[i]["code"].ToString()))
                    {
                        Int32 row = Convert.ToInt32(codeRow[dt.Rows[i]["code"].ToString()]);
                        book.Worksheet(1).Row(row).Cell(3).Value = dt.Rows[i]["write_"].ToString();
                        book.Worksheet(1).Row(row).Cell(4).Value = dt.Rows[i]["speak_"].ToString();
                        book.Worksheet(1).Row(row).Cell(5).Value = dt.Rows[i]["total"].ToString();
                        book.Worksheet(1).Row(row).Cell(6).Value = dt.Rows[i]["type_complaint"].ToString();
                    }
                    else
                    {
                        repSum.OtherWrite += Convert.ToInt32(dt.Rows[i]["write_"].ToString());
                        repSum.OtherSpeak += Convert.ToInt32(dt.Rows[i]["speak_"].ToString());
                        repSum.OtherProvable += Convert.ToInt32(dt.Rows[i]["type_complaint"].ToString());
                    }
                    repSum.Write += Convert.ToInt32(dt.Rows[i]["write_"].ToString());
                    repSum.Speak += Convert.ToInt32(dt.Rows[i]["speak_"].ToString());
                    repSum.Provable += Convert.ToInt32(dt.Rows[i]["type_complaint"].ToString());

                    if (Convert.ToInt32(dt.Rows[i]["write_"].ToString()) != 0)
                        repSum.UniqueWrite++;
                    if (Convert.ToInt32(dt.Rows[i]["speak_"].ToString()) != 0)
                        repSum.UniqueSpeak++;
                    if (Convert.ToInt32(dt.Rows[i]["type_complaint"].ToString()) != 0)
                        repSum.UniqueProvable++;
                }
            }

            book.Worksheet(1).Row(8).Cell(3).Value = repSum.Write;
            book.Worksheet(1).Row(8).Cell(4).Value = repSum.Speak;
            book.Worksheet(1).Row(8).Cell(5).Value = repSum.Speak + repSum.Write;
            book.Worksheet(1).Row(8).Cell(6).Value = repSum.Provable;

            book.Worksheet(1).Row(9).Cell(3).Value = repSum.Write;
            book.Worksheet(1).Row(9).Cell(4).Value = repSum.Speak;
            book.Worksheet(1).Row(9).Cell(5).Value = repSum.Speak + repSum.Write;
            book.Worksheet(1).Row(9).Cell(6).Value = repSum.Provable;

            book.Worksheet(1).Row(33).Cell(3).Value = repSum.OtherWrite;
            book.Worksheet(1).Row(33).Cell(4).Value = repSum.OtherSpeak;
            book.Worksheet(1).Row(33).Cell(5).Value = repSum.OtherSpeak + repSum.OtherWrite;
            book.Worksheet(1).Row(33).Cell(6).Value = repSum.OtherProvable;

            book.Save();
            Response.Clear();
            Response.ClearHeaders();
            byte[] bts = System.IO.File.ReadAllBytes(path);
            Response.AddHeader("Content-Type", "Application/octet-stream");
            Response.AddHeader("Content-Length", bts.Length.ToString());
            Response.AddHeader("Content-Disposition", "attachment; filename=TFOMSComplaintReason_"+DateTime.Now.ToShortDateString()+".xlsx");
            Response.BinaryWrite(bts);
            Response.Flush();
            Response.End();
            System.IO.File.Delete(path);
        }

        [HttpPost]
        public void Report1(DateTime? dateFromPost, DateTime? dateToPost, Int64? smoIdPost)
        {
            Dictionary<string, string> codeRow = new Dictionary<string, string>();
            codeRow.Add("1.18.", "12");
            codeRow.Add("1.4.", "13");//дважды. для консультации переопределяем
            codeRow.Add("1.5.", "17");
            codeRow.Add("1.19.", "22");
            codeRow.Add("1.1.", "24");
            codeRow.Add("1.1.2.", "25");
            codeRow.Add("1.2.", "26");
            codeRow.Add("1.3.", "27");
            codeRow.Add("1.6.", "29");
            codeRow.Add("1.7.", "30");
            codeRow.Add("1.9.", "31");
            codeRow.Add("1.10.", "32");
            codeRow.Add("1.11.", "33");
            codeRow.Add("1.12.", "34");
            codeRow.Add("1.13.", "35");
            codeRow.Add("1.14.", "36");
            codeRow.Add("1.14.1.", "37");
            codeRow.Add("1.15.", "38");


            string templatePath = @"C:\Temp\TFOMSReport1.xlsx";
            string path = @"C:\Temp\TFOMSReport" + DateTime.Now.GetHashCode() + ".xlsx";
            var book = new XLWorkbook(templatePath);
            book.SaveAs(path);
            book = new XLWorkbook(path);
            book.Worksheet(1).Row(4).Cell(2).Value = (dateFromPost.HasValue ? dateFromPost.Value.ToShortDateString() : "...") + "-" +
                (dateToPost.HasValue ? dateToPost.Value.ToShortDateString() : "...");
            using (var uow = UnityManager.Resolve<IUnitOfWork>())
            {
                var repo = uow.GetRepo<ISMORepo>();
                if (smoIdPost.HasValue)
                {
                    var smoName = repo.Get(smoIdPost.Value).FullName;
                    book.Worksheet(1).Row(3).Cell(2).Value = smoName;
                }
                else
                {
                    book.Worksheet(1).Row(3).Cell(2).Value = "Все СМО";
                }
            }

            string where = "";
            if (dateFromPost.HasValue)
            {
                where += " where date>= '" + dateFromPost.Value.ToShortDateString() + "'";
            }
            else
            {
                where += " where 1 = 1 ";
            }

            if (dateToPost.HasValue)
            {
                where += "and date <= '" + dateToPost.Value.ToShortDateString() + "'";
            }

            if (smoIdPost.HasValue)
            {
                where += " and ja.smoid = " + smoIdPost.Value;
            }
            AllAppeal sumAppeal = new AllAppeal();
            DataTable dtAppealAll = GetAllAppeal(where);
            if (dtAppealAll != null)
            {
                for (int i = 0; i < dtAppealAll.Rows.Count; i++)
                {
                    switch(dtAppealAll.Rows[i][0].ToString())
                    {
                        case "1":
                            {
                                sumAppeal.Speak += Convert.ToInt32(dtAppealAll.Rows[i][1]);
                                sumAppeal.HotLine += Convert.ToInt32(dtAppealAll.Rows[i][1]);
                                break;
                            }
                        case "2":
                            {
                                sumAppeal.Write += Convert.ToInt32(dtAppealAll.Rows[i][1]);
                                sumAppeal.Web += Convert.ToInt32(dtAppealAll.Rows[i][1]);
                                break;
                            }
                        case "3":
                            {
                                sumAppeal.Write += Convert.ToInt32(dtAppealAll.Rows[i][1]);
                                break;
                            }
                        case "4":
                            {
                                sumAppeal.Speak += Convert.ToInt32(dtAppealAll.Rows[i][1]);
                                break;
                            }
                        case "5":
                            {
                                sumAppeal.Write += Convert.ToInt32(dtAppealAll.Rows[i][1]);
                                break;
                            }
                    }
                }
            }
            book.Worksheet(1).Row(7).Cell(3).Value = sumAppeal.Speak;
            book.Worksheet(1).Row(7).Cell(4).Value = sumAppeal.Write;
            book.Worksheet(1).Row(7).Cell(5).Value = sumAppeal.Write + sumAppeal.Speak;
            book.Worksheet(1).Row(8).Cell(3).Value = sumAppeal.HotLine;
            book.Worksheet(1).Row(8).Cell(5).Value = sumAppeal.HotLine;
            book.Worksheet(1).Row(9).Cell(4).Value = sumAppeal.Web;
            book.Worksheet(1).Row(9).Cell(5).Value = sumAppeal.Web;

            where = "where wayofaddressingid = 2 ";
            if (dateFromPost.HasValue)
            {
                where += " and date>= '" + dateFromPost.Value.ToShortDateString() + "'";
            }
            else
            {
                where += " and 1 = 1 ";
            }

            if (dateToPost.HasValue)
            {
                where += "and date <= '" + dateToPost.Value.ToShortDateString() + "'";
            }

            if (smoIdPost.HasValue)
            {
                where += " and ja.smoid = " + smoIdPost.Value;
            }

            DataTable dtComplaint = GetComplaint(where);
            if (dtComplaint != null)
            {
                book.Worksheet(1).Row(10).Cell(3).Value = Convert.ToInt32(dtComplaint.Rows[0][1]);
                book.Worksheet(1).Row(10).Cell(4).Value = Convert.ToInt32(dtComplaint.Rows[0][0]);
                book.Worksheet(1).Row(10).Cell(5).Value = Convert.ToInt32(dtComplaint.Rows[0][1]) + Convert.ToInt32(dtComplaint.Rows[0][0]);
            }
               

            where = "where wayofaddressingid = 4 ";
            if (dateFromPost.HasValue)
            {
                where += " and date>= '" + dateFromPost.Value.ToShortDateString() + "'";
            }
            else
            {
                where += " and 1 = 1 ";
            }

            if (dateToPost.HasValue)
            {
                where += "and date <= '" + dateToPost.Value.ToShortDateString() + "'";
            }

            if (smoIdPost.HasValue)
            {
                where += " and ja.smoid = " + smoIdPost.Value;
            }
            ReportSummary repSum = new ReportSummary();
            DataTable dt = GetDataAppeal(where);
            if (dt != null)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (codeRow.ContainsKey(dt.Rows[i]["code"].ToString()))
                    {
                        Int32 row = Convert.ToInt32(codeRow[dt.Rows[i]["code"].ToString()]);
                        book.Worksheet(1).Row(row).Cell(3).Value = dt.Rows[i]["write_"].ToString();
                        book.Worksheet(1).Row(row).Cell(4).Value = dt.Rows[i]["speak_"].ToString();
                        book.Worksheet(1).Row(row).Cell(5).Value = dt.Rows[i]["total"].ToString();
                    }
                    else
                    {
                        repSum.OtherWrite += Convert.ToInt32(dt.Rows[i]["write_"].ToString());
                        repSum.OtherSpeak += Convert.ToInt32(dt.Rows[i]["speak_"].ToString());
                    }
                    repSum.Write += Convert.ToInt32(dt.Rows[i]["write_"].ToString());
                    repSum.Speak += Convert.ToInt32(dt.Rows[i]["speak_"].ToString());
                }
            }

            book.Worksheet(1).Row(21).Cell(3).Value = repSum.OtherWrite;
            book.Worksheet(1).Row(21).Cell(4).Value = repSum.OtherSpeak;
            book.Worksheet(1).Row(21).Cell(5).Value = repSum.OtherSpeak + repSum.OtherWrite;

            book.Worksheet(1).Row(11).Cell(3).Value = repSum.Write;
            book.Worksheet(1).Row(11).Cell(4).Value = repSum.Speak;
            book.Worksheet(1).Row(11).Cell(5).Value = repSum.Speak + repSum.Write;


            where = "where wayofaddressingid = 1 ";
            if (dateFromPost.HasValue)
            {
                where += " and date>= '" + dateFromPost.Value.ToShortDateString() + "'";
            }
            else
            {
                where += " and 1 = 1 ";
            }

            if (dateToPost.HasValue)
            {
                where += "and date <= '" + dateToPost.Value.ToShortDateString() + "'";
            }

            if (smoIdPost.HasValue)
            {
                where += " and ja.smoid = " + smoIdPost.Value;
            }
            repSum = new ReportSummary();
            dt = GetDataConsult(where);
            if (dt != null)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (codeRow.ContainsKey(dt.Rows[i]["code"].ToString()))
                    {
                        Int32 row = Convert.ToInt32(codeRow[dt.Rows[i]["code"].ToString()]);
                        if (row == 13)
                            row = 28;
                        book.Worksheet(1).Row(row).Cell(3).Value = dt.Rows[i]["write_"].ToString();
                        book.Worksheet(1).Row(row).Cell(4).Value = dt.Rows[i]["speak_"].ToString();
                        book.Worksheet(1).Row(row).Cell(5).Value = dt.Rows[i]["total"].ToString();
                    }
                    else
                    {
                        repSum.OtherWrite += Convert.ToInt32(dt.Rows[i]["write_"].ToString());
                        repSum.OtherSpeak += Convert.ToInt32(dt.Rows[i]["speak_"].ToString());
                    }
                    repSum.Write += Convert.ToInt32(dt.Rows[i]["write_"].ToString());
                    repSum.Speak += Convert.ToInt32(dt.Rows[i]["speak_"].ToString());
                }
            }

            book.Worksheet(1).Row(39).Cell(3).Value = repSum.OtherWrite;
            book.Worksheet(1).Row(39).Cell(4).Value = repSum.OtherSpeak;
            book.Worksheet(1).Row(39).Cell(5).Value = repSum.OtherSpeak + repSum.OtherWrite;

            book.Worksheet(1).Row(23).Cell(3).Value = repSum.Write;
            book.Worksheet(1).Row(23).Cell(4).Value = repSum.Speak;
            book.Worksheet(1).Row(23).Cell(5).Value = repSum.Speak + repSum.Write;

            where = "where wayofaddressingid = 3 ";
            if (dateFromPost.HasValue)
            {
                where += " and date>= '" + dateFromPost.Value.ToShortDateString() + "'";
            }
            else
            {
                where += " and 1 = 1 ";
            }

            if (dateToPost.HasValue)
            {
                where += "and date <= '" + dateToPost.Value.ToShortDateString() + "'";
            }

            if (smoIdPost.HasValue)
            {
                where += " and ja.smoid = " + smoIdPost.Value;
            }

            DataTable dtSuggestion = GetSuggestion(where);
            if(dtSuggestion != null)
            {
                book.Worksheet(1).Row(40).Cell(3).Value = Convert.ToInt32(dtSuggestion.Rows[0][1]);
                book.Worksheet(1).Row(40).Cell(4).Value = Convert.ToInt32(dtSuggestion.Rows[0][0]);
                book.Worksheet(1).Row(40).Cell(5).Value = Convert.ToInt32(dtSuggestion.Rows[0][1]) + Convert.ToInt32(dtSuggestion.Rows[0][0]);
            }
            


            book.Save();
            Response.Clear();
            Response.ClearHeaders();
            byte[] bts = System.IO.File.ReadAllBytes(path);
            Response.AddHeader("Content-Type", "Application/octet-stream");
            Response.AddHeader("Content-Length", bts.Length.ToString());
            Response.AddHeader("Content-Disposition", "attachment; filename=TFOMSAppealSitizens_" + DateTime.Now.ToShortDateString() + ".xlsx");
            Response.BinaryWrite(bts);
            Response.Flush();
            Response.End();
            System.IO.File.Delete(path);
        }

        [HttpGet]
        public virtual ActionResult Download(string fileGuid, string fileName)
        {
            if (TempData[fileGuid] != null)
            {
                byte[] data = TempData[fileGuid] as byte[];
                return File(data, "application/vnd.ms-excel", fileName);
            }
            else
            {
                // Problem - Log the error, generate a blank file,
                //           redirect to another controller action - whatever fits with your application
                return new EmptyResult();
            }
        }


        private DataTable GetDataComplaints(String where)
        {
            string connStr = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            string cmdText = @"SELECT code, name, write_, speak_, write_ + speak_ as total, type_complaint FROM
                                (SELECT tas.code, tas.name, sum(Case WHEN ja.typeofaddressingid in (2,3,5) THEN 1 ELSE 0 END) as write_, 
                                    sum(Case WHEN ja.typeofaddressingid in (1,4) THEN 1 ELSE 0 END) as speak_,
                                        sum(Case WHEN ja.complaintid in (1,2,3) THEN 1 ELSE 0 END) as type_complaint
                                FROM dbo.themeappealcitizens tas
                                LEFT JOIN dbo.journalappeal ja on ja.appealtheme = tas.themeappealcitizensid " + 
                                where +                          
                                " group by 1,2) t order by 1,2";
            //stream.WriteLine(cmdText);
            NpgsqlConnection conn = new NpgsqlConnection(connStr);
            NpgsqlCommand cmd = new NpgsqlCommand(cmdText, conn);
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            try
            {
                da.Fill(dt);
                return dt;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        private DataTable GetDataAppeal(String where)
        {
            string connStr = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            string cmdText = @"SELECT code, name, write_, speak_, write_ + speak_ as total FROM
                                (SELECT tas.code, tas.name, sum(Case WHEN ja.typeofaddressingid in (2,3,5) THEN 1 ELSE 0 END) as write_, 
                                    sum(Case WHEN ja.typeofaddressingid in (1,4) THEN 1 ELSE 0 END) as speak_
                                FROM dbo.themeappealcitizens tas
                                LEFT JOIN dbo.journalappeal ja on ja.appealtheme = tas.themeappealcitizensid " +
                                where +
                                " group by 1,2) t order by 1,2";
            //stream.WriteLine(cmdText);
            NpgsqlConnection conn = new NpgsqlConnection(connStr);
            NpgsqlCommand cmd = new NpgsqlCommand(cmdText, conn);
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            try
            {
                da.Fill(dt);
                return dt;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        private DataTable GetDataConsult(String where)
        {
            string connStr = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            string cmdText = @"SELECT code, name, write_, speak_, write_ + speak_ as total FROM
                                (SELECT tas.code, tas.name, sum(Case WHEN ja.typeofaddressingid in (2,3,5) THEN 1 ELSE 0 END) as write_, 
                                    sum(Case WHEN ja.typeofaddressingid in (1,4) THEN 1 ELSE 0 END) as speak_
                                FROM dbo.themeappealcitizens tas
                                LEFT JOIN dbo.journalappeal ja on ja.appealtheme = tas.themeappealcitizensid " +
                                where +
                                " group by 1,2) t order by 1,2";
            //stream.WriteLine(cmdText);
            NpgsqlConnection conn = new NpgsqlConnection(connStr);
            NpgsqlCommand cmd = new NpgsqlCommand(cmdText, conn);
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            try
            {
                da.Fill(dt);
                return dt;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        private DataTable GetAllAppeal(String where)
        {
            string connStr = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            string cmdText = @"SELECT typeofaddressingid, count(*) FROM dbo.journalappeal ja " +
                                where +
                                " group by 1";
            //stream.WriteLine(cmdText);
            NpgsqlConnection conn = new NpgsqlConnection(connStr);
            NpgsqlCommand cmd = new NpgsqlCommand(cmdText, conn);
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            try
            {
                da.Fill(dt);
                return dt;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        private DataTable GetComplaint(String where)
        {
            string connStr = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            string cmdText = @"SELECT coalesce(sum(Case WHEN typeofaddressingid in (2,3,5) THEN 1 ELSE 0 END), 0) as write_, 
                                    coalesce(sum(Case WHEN typeofaddressingid in (1,4) THEN 1 ELSE 0 END), 0) as speak_
                                FROM dbo.journalappeal " +
                                where;
            //stream.WriteLine(cmdText);
            NpgsqlConnection conn = new NpgsqlConnection(connStr);
            NpgsqlCommand cmd = new NpgsqlCommand(cmdText, conn);
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            try
            {
                da.Fill(dt);
                return dt;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        private DataTable GetSuggestion(String where)
        {
            string connStr = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            string cmdText = @"SELECT coalesce(sum(Case WHEN typeofaddressingid in (2,3,5) THEN 1 ELSE 0 END), 0) as write_, 
                                    coalesce(sum(Case WHEN typeofaddressingid in (1,4) THEN 1 ELSE 0 END), 0) as speak_
                                FROM dbo.journalappeal " +
                                where;
            //stream.WriteLine(cmdText);
            NpgsqlConnection conn = new NpgsqlConnection(connStr);
            NpgsqlCommand cmd = new NpgsqlCommand(cmdText, conn);
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            try
            {
                da.Fill(dt);
                return dt;
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
    
    public class ReportSummary
    {
        public ReportSummary()
        {
            Write = 0;
            Speak = 0;
            Provable = 0;
            UniqueWrite = 0;
            UniqueSpeak = 0;
            UniqueProvable = 0;
            OtherWrite = 0;
            OtherSpeak = 0;
            OtherProvable = 0;
        }
        public Int64 Write { get; set; }

        public Int64 Speak { get; set; }

        public Int64 Provable { get; set; }

        public Int64 UniqueWrite { get; set; }

        public Int64 UniqueSpeak { get; set; }

        public Int64 UniqueProvable { get; set; }

        public Int64 OtherWrite { get; set; }

        public Int64 OtherSpeak { get; set; }

        public Int64 OtherProvable { get; set; }
    }

    public class AllAppeal
    {
        public AllAppeal()
        {
            Write = 0;
            Speak = 0;
            HotLine = 0;
            Web = 0;
        }
        public Int64 Write { get; set; }

        public Int64 Speak { get; set; }

        public Int64 HotLine { get; set; }

        public Int64 Web { get; set; }
    }
}