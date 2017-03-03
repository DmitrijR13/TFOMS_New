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
            // Verify that the user selected a file
            if (file != null && file.ContentLength > 0)
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
                }
                catch(Exception e)
                {
                    return RedirectToAction("Index", "JournalAppeal", new { message = "Ошибка. Неверный формат файла" });
                }
            }
            // redirect back to the index action to show the form once again
            return RedirectToAction("Index", "JournalAppeal", new { message = "Файл успешно загружен" });
        }
    }
}
