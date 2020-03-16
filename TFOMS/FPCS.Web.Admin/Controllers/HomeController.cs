using System;
using System.Web.Mvc;
using System.Linq;
using FPCS.Web.Admin.Code;
using FPCS.Data.Enums;
using FPCS.Web.Admin.Models.Home;
using FPCS.Data;
using FPCS.Data.Repo;
using System.Web;
using System.IO;
using System.Text;
//using FPCS.Web.Admin.TestService;
using FPCS.Web.Admin.TFOMSService;

namespace FPCS.Web.Admin.Controllers
{
    [FPCSAuthorize()]
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {			
            return RedirectToAction("Index", "JournalAppeal");
        }

        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase file)
        {
            try
            {
                // Verify that the user selected a file
                if (file != null && file.ContentLength > 0 && Path.GetExtension(file.FileName) == ".zip")
                {
                    // extract only the filename
                    var fileName = Path.GetFileName(file.FileName);
                    // store the file inside ~/App_Data/uploads folder
                    var path = Path.Combine("C:\\Temp\\", fileName);
                    file.SaveAs(path);

                    byte[] bts;
                    using (var stream = new FileStream(path, FileMode.Open, FileAccess.Read))
                    {
                        using (var reader = new BinaryReader(stream))
                        {
                            bts = reader.ReadBytes((int)stream.Length);
                        }
                    }
                    string fileNameToUpload = Encoding.Default.GetString(bts);

                    string userName = User.Login;
                    string password = "";
                    using (var uow = UnityManager.Resolve<IUnitOfWork>())
                    {
                        var repo = uow.GetRepo<IDbUserRepo>();

                        password = repo.GetByLogin(userName).Password;
                    }

                    Service1Client client = new Service1Client();
                    TfomsZipFile tfomsZipFile = new TfomsZipFile();
                    tfomsZipFile.filename = fileName;
                    tfomsZipFile.file = fileNameToUpload;
                    tfomsZipFile.login = userName;
                    tfomsZipFile.password = password;
                    try
                    {
                        var tfomsAnswer = client.InsertTfomsFile(tfomsZipFile);
                        string pathAnswer = @"C:\Temp\Protokol_" + fileName.Substring(0, fileName.Length - 4) + "_" + DateTime.Now.GetHashCode() + ".txt";
                        StreamWriter sw = new StreamWriter(pathAnswer);
                        foreach (var v in tfomsAnswer.PR)
                        {
                            sw.WriteLine(v.N_ZAP + " " + v.IM_POL + " " + v.COMMENT);
                        }
                        sw.Close();
                        Response.Clear();
                        Response.ClearHeaders();
                        byte[] btsAnswer = System.IO.File.ReadAllBytes(pathAnswer);
                        Response.AddHeader("Content-Type", "Application/octet-stream");
                        Response.AddHeader("Content-Length", btsAnswer.Length.ToString());
                        Response.AddHeader("Content-Disposition", "attachment; filename=Protokol_" + fileName.Substring(0, fileName.Length - 4) + "_" + DateTime.Now.ToShortDateString() + ".txt");
                        Response.BinaryWrite(btsAnswer);
                        Response.Flush();
                        Response.End();
                        System.IO.File.Delete(pathAnswer);
                    }
                    catch (Exception e)
                    {
                        return RedirectToAction("Index", "JournalAppeal", new { message = "Ошибка. Неверный формат файла" });
                    }
                }
                else
                {
                    return RedirectToAction("Index", "JournalAppeal", new { message = "Ошибка. Неверный формат файла" });
                }
                // redirect back to the index action to show the form once again
                return RedirectToAction("Index", "JournalAppeal", new { message = "Файл успешно загружен" });
            }
            catch (Exception ex)
            {

                return RedirectToAction("Index", "JournalAppeal", new { message = ex.ToString()});
            }
           
        }
    }
}
