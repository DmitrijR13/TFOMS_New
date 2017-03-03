using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Xml;
using System.Data;
using WcfService1.Core;
using System.Security.Cryptography;
using System.Net;
//using Oracle.DataAccess.Client;
using Npgsql;
using System.Diagnostics;
using System.Threading;
using System.Xml.Serialization;
using System.Configuration;

namespace WcfService1
{
    // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени класса "Service1" в коде, SVC-файле и файле конфигурации.
    // ПРИМЕЧАНИЕ. Чтобы запустить клиент проверки WCF для тестирования службы, выберите элементы Service1.svc или Service1.svc.cs в обозревателе решений и начните отладку.
    public class Service1 : IService1
    {
        public string connStr;
        public Service1()
        {
            connStr = ConfigurationManager.ConnectionStrings["tfomsDB"].ConnectionString;

        }
        public FLK GetImportResult(String fileName)
        {

            FLK errorFile = new FLK();
            errorFile.FNAME_I = fileName;
            List<PR> listPR = new List<PR>();
            if (fileName.Contains("_") && fileName.Split('_')[0].Length >= 2)
            {
                DataTable dt = SelectImportResult(fileName);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    PR pr = new PR();
                    pr.OSHIB = dt.Rows[i][0].ToString();
                    pr.IM_POL = dt.Rows[i][1].ToString();
                    pr.BAS_EL = dt.Rows[i][2].ToString();
                    pr.N_ZAP = dt.Rows[i][3].ToString();
                    pr.COMMENT = dt.Rows[i][4].ToString();
                    listPR.Add(pr);

                }
                errorFile.PR = listPR;
                return errorFile;
            }
            else
            {
                PR pr = new PR();
                pr.OSHIB = "277";
                pr.COMMENT = "Неверное имя файла";
                listPR.Add(pr);
                errorFile.PR = listPR;
                return errorFile;
            }
        }

        public string CheckLogin(string login, string password)
        {
            DataTable dt = SelectSMOByLP(login, password);
            if (dt.Rows.Count > 0)
            {
                return dt.Rows[0][0].ToString();
            }
            else
            {
                return "";
            }
        }
        public FLK InsertTfomsFile(TfomsZipFile tfomsZipFile)
        {
            string smoCodeUser = CheckLogin(tfomsZipFile.login, tfomsZipFile.password);
            //   StreamWriter swr = new StreamWriter("C:\\FileStore\\testOMS\\OSNlog" + DateTime.Now.GetHashCode() + ".log");
            bool toImport = true;
            string smoId = "";
            FLK errorFile = new FLK();
            List<PR> listPR = new List<PR>();
            if (tfomsZipFile.filename.Contains("_") && tfomsZipFile.filename.Split('_')[0].Length >= 2)
            {
                string rn_smo = tfomsZipFile.filename.Split('_')[0];
                Dictionary<string, string> smoIds = SelectSMOIds();
                if (smoCodeUser != "")
                {
                    if (smoIds.ContainsKey(rn_smo) && rn_smo == smoCodeUser)
                    {
                        try
                        {
                            TfomsAnswer answer = new TfomsAnswer();
                            string filename = tfomsZipFile.filename;
                            byte[] buffer = Encoding.Default.GetBytes(tfomsZipFile.file);
                            string senderId = rn_smo;
                            string str = DateTime.Now.Year + "" + DateTime.Now.Month.ToString("00") + DateTime.Now.Day.ToString("00");
                            string path = "C:\\FileStore\\" + senderId + "\\" + str + "\\" + filename;
                            string dirpath = "C:\\FileStore\\" + senderId + "\\" + str;
                            errorFile.FNAME_I = filename;
                            errorFile.FNAME = filename.Split('.')[0] + "Result.xml";
                            if (!Directory.Exists(dirpath))
                                Directory.CreateDirectory(dirpath);
                            if (!File.Exists(path))
                            {
                                File.WriteAllBytes(path, buffer);

                                // Формируем параметры вызова 7z
                                ProcessStartInfo startInfo2 = new ProcessStartInfo();
                                startInfo2.FileName = @"C:\Temp\7-Zip\7z.exe";
                                // Распаковать (для полных путей - x)
                                startInfo2.Arguments = " e";
                                // На все отвечать yes
                                startInfo2.Arguments += " -y";
                                // Файл, который нужно распаковать
                                startInfo2.Arguments += " " + "\"" + path + "\"";
                                // Папка распаковки
                                startInfo2.Arguments += " -o" + "\"" + dirpath + "\"";
                                startInfo2.WindowStyle = ProcessWindowStyle.Hidden;
                                int sevenZipExitCode = 0;
                                using (Process sevenZip = Process.Start(startInfo2))
                                {
                                    sevenZip.WaitForExit();
                                    sevenZipExitCode = sevenZip.ExitCode;
                                }
                                // Если с первого раза не получилось,
                                //пробуем еще раз через 1 секунду
                                if (sevenZipExitCode != 0 && sevenZipExitCode != 1)
                                {
                                    using (Process sevenZip = Process.Start(startInfo2))
                                    {
                                        Thread.Sleep(1000);
                                        sevenZip.WaitForExit();
                                        switch (sevenZip.ExitCode)
                                        {

                                            // case 2: throw new Exception("Фатальная ошибка");
                                            // case 7: throw new Exception("Ошибка в командной строке");
                                            //  case 8:
                                            //       throw new Exception("Недостаточно памяти для выполнения операции");
                                            //    case 225:
                                            //       throw new Exception("Пользователь отменил выполнение операции");
                                            //   default: throw new Exception("Архиватор 7z вернул недокументированный код ошибки: " + sevenZip.ExitCode.ToString());
                                        }
                                    }
                                }
                                string fileInfo = "1111";
                                DirectoryInfo dirIncoming = new DirectoryInfo(dirpath);
                                foreach (var xmlFile in dirIncoming.GetFiles())
                                {
                                    List<IRP> irps = new List<IRP>();
                                    fileInfo += xmlFile.FullName + " " + xmlFile.Extension + " ";
                                    if (xmlFile.Extension.ToLower() == ".xml")
                                    {

                                        using (StreamReader reader = new StreamReader(xmlFile.FullName, Encoding.Default))
                                        {
                                            XmlDocument soapDoc = new XmlDocument();
                                            soapDoc.Load(reader);
                                            XmlNodeList xmlNodeL = soapDoc.GetElementsByTagName("IRP");
                                            XmlNodeList xmlNodeLZGLV = soapDoc.GetElementsByTagName("ZGLV");
                                            ZGLV zglv = new ZGLV();
                                            if (xmlNodeLZGLV.Count == 1)
                                            {
                                                foreach (XmlElement element in xmlNodeLZGLV[0])
                                                {
                                                    if (element.Name == "SMO")
                                                    {
                                                        zglv.SMO = element.InnerText.Trim();

                                                    }
                                                }
                                            }
                                            if (smoIds.ContainsKey(zglv.SMO))
                                            {
                                                smoId = smoIds[zglv.SMO];
                                            }
                                            else
                                            {
                                                PR pr = new PR();
                                                pr.OSHIB = "278";
                                                pr.IM_POL = "SMO";
                                                pr.BAS_EL = "ZGLV";
                                                pr.COMMENT = "Реестровый номер СМО заполнен неверно";
                                                listPR.Add(pr);
                                                toImport = false;

                                            }
                                            foreach (XmlNode xmlNode in xmlNodeL)
                                            {
                                                XmlSerializer serializerNode = new XmlSerializer(typeof(IRP));
                                                XmlDocument nodeDoc = new XmlDocument();
                                                string nodeInnerXml = "<IRP>" + xmlNode.InnerXml + "</IRP>";
                                                nodeDoc.LoadXml(nodeInnerXml);
                                                using (var nodeReader = new XmlNodeReader(xmlNode))
                                                {
                                                    //  nodeReader.MoveToContent();
                                                    //   nodeReader.ReadStartElement();
                                                    while (nodeReader.Read())
                                                    {
                                                        try
                                                        {
                                                            IRP irp = (IRP)serializerNode.Deserialize(nodeReader);
                                                            irps.Add(irp);
                                                        }
                                                        catch (Exception e)
                                                        {
                                                            PR pr = new PR();
                                                            pr.OSHIB = "40";
                                                            pr.COMMENT = "Некорректно указаны даты, либо не заполнены обязательные поля. Сбой попытки сериализации вложенной xml " + path + " " + "(" + e.Message + ")";
                                                            listPR.Add(pr);
                                                        }
                                                    }
                                                }


                                            }
                                            foreach (IRP irp in irps)
                                            {
                                                irp.Has_Errors = "No";
                                            }

                                            try
                                            {

                                                //Справочники 
                                                Dictionary<string, string> themeAppealCitizens = SelectThemeAppealCitizens();
                                                Dictionary<string, string> complaint = SelectComplaint();


                                                if (!themeAppealCitizens.ContainsKey("Error"))
                                                {
                                                    foreach (IRP irp in irps)
                                                    {
                                                        //проверка на заполненность
                                                        if (irp.N_IRP.Length < 16)
                                                        {
                                                            PR pr = new PR();
                                                            pr.OSHIB = "273";
                                                            pr.IM_POL = "N_IRP";
                                                            pr.BAS_EL = "N_IRP";
                                                            pr.N_ZAP = irp.N_IRP;
                                                            pr.COMMENT = "Уникальный номер обращения заполнен неверно";
                                                            listPR.Add(pr);
                                                            irp.Has_Errors = "Yes";

                                                        }

                                                        if (irp.IRP_TYPE < 1 || irp.IRP_TYPE > 5)
                                                        {
                                                            irp.Has_Errors = "Yes";
                                                            PR pr = new PR();
                                                            pr.OSHIB = "63";
                                                            pr.IM_POL = "IRP_TYPE";
                                                            pr.BAS_EL = "IRP_TYPE";
                                                            pr.N_ZAP = irp.N_IRP;
                                                            pr.COMMENT = "Тип идентификатора не поддерживается";
                                                            listPR.Add(pr);
                                                        }

                                                        if (irp.WAY < 1 || irp.WAY > 5)
                                                        {
                                                            irp.Has_Errors = "Yes";
                                                            PR pr = new PR();
                                                            pr.OSHIB = "63";
                                                            pr.IM_POL = "WAY";
                                                            pr.BAS_EL = "WAY";
                                                            pr.N_ZAP = irp.N_IRP;
                                                            pr.COMMENT = "Тип идентификатора не поддерживается";
                                                            listPR.Add(pr);
                                                        }

                                                        if (irp.HOW < 1 || irp.HOW > 5)
                                                        {
                                                            irp.Has_Errors = "Yes";
                                                            PR pr = new PR();
                                                            pr.OSHIB = "63";
                                                            pr.IM_POL = "HOW";
                                                            pr.BAS_EL = "HOW";
                                                            pr.N_ZAP = irp.N_IRP;
                                                            pr.COMMENT = "Тип идентификатора не поддерживается";
                                                            listPR.Add(pr);
                                                        }

                                                        if (irp.OTV_T < 1 || irp.OTV_T > 4)
                                                        {
                                                            irp.Has_Errors = "Yes";
                                                            PR pr = new PR();
                                                            pr.OSHIB = "63";
                                                            pr.IM_POL = "OTV_T";
                                                            pr.BAS_EL = "OTV_T";
                                                            pr.N_ZAP = irp.N_IRP;
                                                            pr.COMMENT = "Тип идентификатора не поддерживается";
                                                            listPR.Add(pr);
                                                        }

                                                        if (!complaint.ContainsKey("Error"))
                                                        {
                                                            if (irp.ZH_D != null && irp.ZH_D != "" && complaint.ContainsKey(irp.ZH_D))
                                                            {
                                                                irp.ZH_D = complaint[irp.ZH_D];
                                                                //проверка на заполненность

                                                            }
                                                            else if (irp.ZH_D != null && irp.ZH_D != "")
                                                            {
                                                                irp.Has_Errors = "Yes";
                                                                PR pr = new PR();
                                                                pr.OSHIB = "63";
                                                                pr.IM_POL = "ZH_D";
                                                                pr.BAS_EL = "ZH_D";
                                                                pr.N_ZAP = irp.N_IRP;
                                                                pr.COMMENT = "Тип идентификатора не поддерживается";
                                                                listPR.Add(pr);
                                                            }
                                                        }

                                                        if (themeAppealCitizens.ContainsKey(irp.THEME))
                                                        {
                                                            irp.THEME = themeAppealCitizens[irp.THEME];
                                                            //проверка на заполненность

                                                        }
                                                        else
                                                        {
                                                            irp.Has_Errors = "Yes";
                                                            PR pr = new PR();
                                                            pr.OSHIB = "63";
                                                            pr.IM_POL = "THEME";
                                                            pr.BAS_EL = "THEME";
                                                            pr.N_ZAP = irp.N_IRP;
                                                            pr.COMMENT = "Тип идентификатора не поддерживается";
                                                            listPR.Add(pr);
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    PR pr = new PR();
                                                    pr.OSHIB = "274";
                                                    pr.COMMENT = "Cервис передачи обращений не может связаться с базой данных, повторите попытку позднее";
                                                    listPR.Add(pr);
                                                    //вставляем сюда ответ, что сервис не может получить связь с базой
                                                }
                                                List<string> sugUNs = SelectAUN("_" + DateTime.Now.Year.ToString().Substring(2, 2));
                                                if (!sugUNs.Contains("Error"))
                                                {
                                                    foreach (IRP irp in irps)
                                                    {
                                                        if (sugUNs.Contains(irp.N_IRP))
                                                        {
                                                            irp.Is_imported = "Yes";
                                                            PR pr = new PR();
                                                            pr.OSHIB = "275";
                                                            pr.IM_POL = "N_IRP";
                                                            pr.BAS_EL = "N_IRP";
                                                            pr.N_ZAP = irp.N_IRP;
                                                            pr.COMMENT = "Обращение с таким номером уже присутствует в базе";
                                                            listPR.Add(pr);
                                                            // вставляем сюда ответ, такое обращение с таким номером уже было импортировано
                                                        }
                                                        else
                                                        {
                                                            irp.Is_imported = "No";
                                                        }
                                                    }


                                                    //вставить сюда методы проверки на заполненность обращения

                                                    //процедура импорта обращений в базу
                                                    List<InsertResult> results = InsertSug(irps, smoId, toImport);
                                                    foreach (InsertResult res in results)
                                                    {
                                                        PR pr = new PR();
                                                        pr.OSHIB = "0";
                                                        pr.N_ZAP = res.sugNum;
                                                        pr.COMMENT = res.sugResult;
                                                        listPR.Add(pr);

                                                    }

                                                    // парсим ответ от импортера
                                                }
                                                else
                                                {
                                                    PR pr = new PR();
                                                    pr.OSHIB = "274";
                                                    pr.COMMENT = "Cервис передачи обращений не может связаться с базой данных, повторите попытку позднее";
                                                    listPR.Add(pr);

                                                    //вставляем сюда ответ, что сервис не может получить связь с базой
                                                }

                                            }
                                            catch (Exception e)
                                            {
                                                PR pr = new PR();
                                                pr.OSHIB = "40";
                                                pr.COMMENT = "Некорректно указаны даты, либо не заполнены обязательные поля. Сбой попытки сериализации вложенной xml " + path + " " + "(" + e.Message + ")";
                                                listPR.Add(pr);

                                            }

                                        }

                                    }


                                }
                                foreach (var xmlFile in dirIncoming.GetFiles())
                                {
                                    if (xmlFile.Extension.ToLower() == ".xml")
                                    {
                                        xmlFile.Delete();
                                    }
                                }

                                answer.file = fileInfo;



                            }
                            else
                            {
                                PR pr = new PR();
                                pr.OSHIB = "275";
                                pr.COMMENT = "Такой файл вы сегодня отправляли";
                                listPR.Add(pr);

                            }
                            errorFile.PR = listPR;

                            List<InsertResult> importResults = InsertResult(errorFile, rn_smo);

                            foreach (InsertResult insres in importResults)
                            {
                                PR pr = new PR();
                                pr.OSHIB = "111";
                                pr.COMMENT = insres.request;
                                //   listPR.Add(pr);
                            }

                            return errorFile;
                        }
                        catch (Exception e)
                        {
                            PR pr = new PR();
                            pr.OSHIB = "276";
                            pr.COMMENT = "Неизвестный формат входящего файла. Критическая ошибка сервиса";
                            // pr.COMMENT = e.Message;
                            listPR.Add(pr);
                            errorFile.PR = listPR;
                            List<InsertResult> importResults = InsertResult(errorFile, rn_smo);

                            return errorFile;

                        }
                    }
                    else
                    {
                        PR pr = new PR();
                        pr.OSHIB = "277";
                        pr.COMMENT = "Неверное имя файла. Данные импортированы не будут";
                        // pr.COMMENT = e.Message;
                        listPR.Add(pr);
                        errorFile.PR = listPR;
                        errorFile.FNAME_I = tfomsZipFile.filename;

                        return errorFile;
                    }
                }
                else
                {
                    PR pr = new PR();
                    pr.OSHIB = "278";
                    pr.COMMENT = "Неправильно указаны логин/пароль, либо к указанному пользователю не привязана СМО";
                    // pr.COMMENT = e.Message;
                    listPR.Add(pr);
                    errorFile.PR = listPR;
                    errorFile.FNAME_I = tfomsZipFile.filename;

                    return errorFile;
                }
                //**
            }
            else
            {
                PR pr = new PR();
                pr.OSHIB = "277";
                pr.COMMENT = "Неверное имя файла. Данные импортированы не будут";
                listPR.Add(pr);
                errorFile.PR = listPR;

                return errorFile;
            }
        }

        #region Получаем DataTable по обращениям
        public List<string> SelectAUN(string year)
        {
            List<string> uns = new List<string>();
            //   string connStr = "Server=localhost;Database=tfoms;User ID=postgres;Password=12345;CommandTimeout=180000;Port=5435";
            //string connStr = "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1521))(CONNECT_DATA=(SID=ezhkh)));User Id = b4_GKH_Samara; Password = ACTANONVERBA";
            string cmdText = "SELECT appealuniquenumber from dbo.journalappeal where appealuniquenumber like '%" + year + "%'";
            //string cmdText = "SELECT id FROM realty_object where mu_name LIKE '%Волжский р-н%'";
            //string cmdText = "SELECT * FROM employees";
            NpgsqlConnection conn = new NpgsqlConnection(connStr);
            NpgsqlCommand cmd = new NpgsqlCommand(cmdText, conn);
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            conn.Open();
            try
            {
                da.Fill(dt);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    uns.Add(dt.Rows[i][0].ToString());
                }
                return uns;


            }
            catch (Exception e)
            {
                string err = e.Message;
                uns.Add("Error");
                return uns;
            }
            finally
            {
                conn.Close();
            }

        }
        #endregion

        #region Получаем DataTable по результатам импорта
        public DataTable SelectImportResult(string fileName)
        {

            string cmdText = "SELECT errorcode, fieldname, baseentitiyname, appealnumber, comment, rnsmo from dbo.flk where basefilename = '" + fileName + "'";

            NpgsqlConnection conn = new NpgsqlConnection(connStr);
            NpgsqlCommand cmd = new NpgsqlCommand(cmdText, conn);
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            conn.Open();
            try
            {
                da.Fill(dt);

                return dt;


            }
            catch (Exception e)
            {
                string err = e.Message;

                return dt;
            }
            finally
            {
                conn.Close();
            }

        }
        #endregion

        #region Получаем DataTable по логину/паролю
        public DataTable SelectSMOByLP(string login, string password)
        {

            string cmdText = "SELECT s.smocode FROM dbo.smo s INNER JOIN users.user u on u.smoid = s.smoid where login = '" + login + "' and password = '" + password + "'";

            NpgsqlConnection conn = new NpgsqlConnection(connStr);
            NpgsqlCommand cmd = new NpgsqlCommand(cmdText, conn);
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            conn.Open();
            try
            {
                da.Fill(dt);

                return dt;


            }
            catch (Exception e)
            {
                string err = e.Message;

                return dt;
            }
            finally
            {
                conn.Close();
            }

        }
        #endregion

        #region Получаем словарь по темам обращений
        public Dictionary<string, string> SelectThemeAppealCitizens()
        {
            Dictionary<string, string> themeAppealCitizens = new Dictionary<string, string>();
            // string connStr = "Server=localhost;Database=tfoms;User ID=postgres;Password=12345;CommandTimeout=180000;Port=5435;";
            //string connStr = "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1521))(CONNECT_DATA=(SID=ezhkh)));User Id = b4_GKH_Samara; Password = ACTANONVERBA";
            string cmdText = "SELECT themeappealcitizensId, code from dbo.themeappealcitizens";
            //string cmdText = "SELECT id FROM realty_object where mu_name LIKE '%Волжский р-н%'";
            //string cmdText = "SELECT * FROM employees";
            NpgsqlConnection conn = new NpgsqlConnection(connStr);
            NpgsqlCommand cmd = new NpgsqlCommand(cmdText, conn);
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            //conn.Open();
            try
            {
                da.Fill(dt);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (!themeAppealCitizens.ContainsKey(dt.Rows[i][1].ToString()))
                    {
                        themeAppealCitizens.Add(dt.Rows[i][1].ToString(), dt.Rows[i][0].ToString());
                    }
                }
                return themeAppealCitizens;


            }
            catch (Exception e)
            {
                string err = e.Message;
                themeAppealCitizens.Add("Error", "Error");

                return themeAppealCitizens;

            }
            finally
            {
                conn.Close();

            }

        }
        #endregion

        #region Получаем словарь по ид СМО
        public Dictionary<string, string> SelectSMOIds()
        {
            Dictionary<string, string> smoIds = new Dictionary<string, string>();
            // string connStr = "Server=localhost;Database=tfoms;User ID=postgres;Password=12345;CommandTimeout=180000;Port=5435;";
            //string connStr = "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1521))(CONNECT_DATA=(SID=ezhkh)));User Id = b4_GKH_Samara; Password = ACTANONVERBA";
            string cmdText = "SELECT smoid, smocode from dbo.smo";
            //string cmdText = "SELECT id FROM realty_object where mu_name LIKE '%Волжский р-н%'";
            //string cmdText = "SELECT * FROM employees";
            NpgsqlConnection conn = new NpgsqlConnection(connStr);
            NpgsqlCommand cmd = new NpgsqlCommand(cmdText, conn);
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            //conn.Open();
            try
            {
                da.Fill(dt);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (!smoIds.ContainsKey(dt.Rows[i][1].ToString()))
                    {
                        smoIds.Add(dt.Rows[i][1].ToString(), dt.Rows[i][0].ToString());
                    }
                }
                return smoIds;


            }
            catch (Exception e)
            {
                string err = e.Message;
                smoIds.Add("Error", "Error");

                return smoIds;

            }
            finally
            {
                conn.Close();

            }

        }
        # endregion

        #region Получаем словарь по жалобам
        public Dictionary<string, string> SelectComplaint()
        {
            Dictionary<string, string> complaint = new Dictionary<string, string>();
            //   string connStr = "Server=localhost;Database=tfoms;User ID=postgres;Password=12345;CommandTimeout=180000;Port=5435;";
            //string connStr = "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1521))(CONNECT_DATA=(SID=ezhkh)));User Id = b4_GKH_Samara; Password = ACTANONVERBA";
            string cmdText = "SELECT complaintid, code from dbo.complaint";
            //string cmdText = "SELECT id FROM realty_object where mu_name LIKE '%Волжский р-н%'";
            //string cmdText = "SELECT * FROM employees";
            NpgsqlConnection conn = new NpgsqlConnection(connStr);
            NpgsqlCommand cmd = new NpgsqlCommand(cmdText, conn);
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            //conn.Open();
            try
            {
                da.Fill(dt);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (!complaint.ContainsKey(dt.Rows[i][1].ToString()))
                    {
                        complaint.Add(dt.Rows[i][1].ToString(), dt.Rows[i][0].ToString());
                    }
                }
                return complaint;


            }
            catch (Exception e)
            {
                string err = e.Message;
                complaint.Add("Error", "Error");
                return complaint;
            }
            finally
            {
                conn.Close();
            }

        }
        # endregion

        #region Инсертим обращения из xml
        public List<InsertResult> InsertSug(List<IRP> tfomsFile, string smoId, bool toImport)
        {
            List<InsertResult> listInsRes = new List<InsertResult>();
            foreach (IRP irp in tfomsFile)
            {
                if (irp.Has_Errors == "No" && irp.Is_imported != "Yes")
                {
                    InsertResult insRes = new InsertResult();
                    insRes.sugNum = irp.N_IRP;
                    //   string connStr = "Server=localhost;Database=tfoms;User ID=postgres;Password=12345;CommandTimeout=180000;Port=5435";
                    // string connStr = "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1521))(CONNECT_DATA=(SID=ezhkh)));User Id = b4_GKH_Samara; Password = ACTANONVERBA";
                    string cmdText1 = @"INSERT INTO dbo.journalappeal(appealuniquenumber, date, time_, sourceincomeid, organizationname, typeofaddressingid, wayofaddressingid, appealtheme, 
            appealcontent, complaintid, appealorganizationid, appealorganizationcode, 
            takingappeallineid, acceptedby, reviewappeallineid, responsible, 
            appealplanenddate, appealfactenddate, appealresultid, applicantsurname, 
            applicantname, applicantsecondname, applicantbirthdate, applicantenp, 
            applicantsmo, applicanttypedocument, applicantdocumentseries, 
            applicantdocumentnumber, applicantfeedbackaddress, applicantphonenumber, 
            applicantemail, receivedtreatmentpersonsurname, receivedtreatmentpersonname, 
            receivedtreatmentpersonsecondname, receivedtreatmentpersonbirthdate, 
            receivedtreatmentpersonenp, receivedtreatmentpersonsmo, receivedtreatmentpersonypedocument, 
            receivedtreatmentpersondocumentseries, receivedtreatmentpersondocumentnumber,
            isdeleted, createddate, updateddate, smoid)
   VALUES ('" + irp.N_IRP + @"', '" + irp.DATE_CREATESomeDate + @"', :timecreate, " + irp.WAY + @", :wayn, 
            " + irp.IRP_TYPE + @", " + irp.HOW + @", " + irp.THEME + @", :text, 
            :complaintid, " + irp.OTV_T + @", '" + irp.OTV_KON + @"', 3, 
            '" + irp.EMPLOYEE_1 + @"', 4, :employeeit, '" + irp.DATA_PLANSomeDate + @"', :appealfactenddate, :appealresultid, :zsvzf, :zsvzi, 
            :zsvzo, :applicantbirthdate, :zsvzenp, :zsvzsmo, 
            :zsvzdoctype, :zsvzdocser, :zsvzdocnum, 
            :zsvadr, :zsvphone, :zsvemail, 
            :insvinf, :insvini, :insvino, 
            :receivedtreatmentpersonbirthdate, :insvinenp, 
            :insvinsmo, :insvindoctype, :insvindocser, 
            :insvindocnum, false, CURRENT_DATE, 
            CURRENT_DATE, :smoidattr)";
                    //string cmdText = "SELECT id FROM realty_object where mu_name LIKE '%Волжский р-н%'";
                    //string cmdText = "SELECT * FROM employees";
                    NpgsqlConnection conn = new NpgsqlConnection(connStr);
                    NpgsqlCommand cmd = new NpgsqlCommand(cmdText1, conn);


                    #region Check and Add param
                    if (irp.IN_SV != null)
                    {
                        if (!String.IsNullOrEmpty(irp.IN_SV.IN_F))
                        {
                            cmd.Parameters.AddWithValue("insvinf", irp.IN_SV.IN_F);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("insvinf", DBNull.Value);
                        }
                        if (!String.IsNullOrEmpty(irp.IN_SV.IN_I))
                        {
                            cmd.Parameters.AddWithValue("insvini", irp.IN_SV.IN_I);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("insvini", DBNull.Value);
                        }
                        if (!String.IsNullOrEmpty(irp.IN_SV.IN_O))
                        {
                            cmd.Parameters.AddWithValue("insvino", irp.IN_SV.IN_O);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("insvino", DBNull.Value);
                        }
                        if (!String.IsNullOrEmpty(irp.IN_SV.IN_ENP))
                        {
                            cmd.Parameters.AddWithValue("insvinenp", irp.IN_SV.IN_ENP);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("insvinenp", DBNull.Value);
                        }
                        if (!String.IsNullOrEmpty(irp.IN_SV.IN_SMO))
                        {
                            cmd.Parameters.AddWithValue("insvinsmo", irp.IN_SV.IN_SMO);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("insvinsmo", DBNull.Value);
                        }
                        if (!String.IsNullOrEmpty(irp.IN_SV.IN_DOCTYPE))
                        {
                            cmd.Parameters.AddWithValue("insvindoctype", irp.IN_SV.IN_DOCTYPE);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("insvindoctype", DBNull.Value);
                        }
                        if (!String.IsNullOrEmpty(irp.IN_SV.IN_DOCSER))
                        {
                            cmd.Parameters.AddWithValue("insvindocser", irp.IN_SV.IN_DOCSER);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("insvindocser", DBNull.Value);
                        }
                        if (!String.IsNullOrEmpty(irp.IN_SV.IN_DOCNUM))
                        {
                            cmd.Parameters.AddWithValue("insvindocnum", irp.IN_SV.IN_DOCNUM);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("insvindocnum", DBNull.Value);
                        }
                        if (irp.IN_SV.IN_DRSomeDate.HasValue)
                        {
                            cmd.Parameters.AddWithValue("receivedtreatmentpersonbirthdate", irp.IN_SV.IN_DRSomeDate.Value);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("receivedtreatmentpersonbirthdate", DBNull.Value);
                        }
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("insvinf", DBNull.Value);
                        cmd.Parameters.AddWithValue("insvini", DBNull.Value);
                        cmd.Parameters.AddWithValue("insvino", DBNull.Value);
                        cmd.Parameters.AddWithValue("insvinenp", DBNull.Value);
                        cmd.Parameters.AddWithValue("insvinsmo", DBNull.Value);
                        cmd.Parameters.AddWithValue("insvindoctype", DBNull.Value);
                        cmd.Parameters.AddWithValue("insvindocser", DBNull.Value);
                        cmd.Parameters.AddWithValue("insvindocnum", DBNull.Value);
                        cmd.Parameters.AddWithValue("receivedtreatmentpersonbirthdate", DBNull.Value);
                    }


                    if (irp.Z_SV != null)
                    {
                        if (!String.IsNullOrEmpty(irp.Z_SV.Z_F))
                        {
                            cmd.Parameters.AddWithValue("zsvzf", irp.Z_SV.Z_F);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("zsvzf", DBNull.Value);
                        }
                        if (!String.IsNullOrEmpty(irp.Z_SV.Z_I))
                        {
                            cmd.Parameters.AddWithValue("zsvzi", irp.Z_SV.Z_I);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("zsvzi", DBNull.Value);
                        }
                        if (!String.IsNullOrEmpty(irp.Z_SV.Z_O))
                        {
                            cmd.Parameters.AddWithValue("zsvzo", irp.Z_SV.Z_O);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("zsvzo", DBNull.Value);
                        }
                        if (!String.IsNullOrEmpty(irp.Z_SV.Z_ENP))
                        {
                            cmd.Parameters.AddWithValue("zsvzenp", irp.Z_SV.Z_ENP);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("zsvzenp", DBNull.Value);
                        }
                        if (!String.IsNullOrEmpty(irp.Z_SV.Z_SMO))
                        {
                            cmd.Parameters.AddWithValue("zsvzsmo", irp.Z_SV.Z_SMO);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("zsvzsmo", DBNull.Value);
                        }
                        if (!String.IsNullOrEmpty(irp.Z_SV.Z_DOCTYPE))
                        {
                            cmd.Parameters.AddWithValue("zsvzdoctype", irp.Z_SV.Z_DOCTYPE);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("zsvzdoctype", DBNull.Value);
                        }
                        if (!String.IsNullOrEmpty(irp.Z_SV.Z_DOCSER))
                        {
                            cmd.Parameters.AddWithValue("zsvzdocser", irp.Z_SV.Z_DOCSER);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("zsvzdocser", DBNull.Value);
                        }
                        if (!String.IsNullOrEmpty(irp.Z_SV.Z_DOCNUM))
                        {
                            cmd.Parameters.AddWithValue("zsvzdocnum", irp.Z_SV.Z_DOCNUM);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("zsvzdocnum", DBNull.Value);
                        }
                        if (!String.IsNullOrEmpty(irp.Z_SV.ADR))
                        {
                            cmd.Parameters.AddWithValue("zsvadr", irp.Z_SV.ADR);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("zsvadr", DBNull.Value);
                        }
                        if (!String.IsNullOrEmpty(irp.Z_SV.PHONE))
                        {
                            cmd.Parameters.AddWithValue("zsvphone", irp.Z_SV.PHONE);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("zsvphone", DBNull.Value);
                        }
                        if (!String.IsNullOrEmpty(irp.Z_SV.EMAIL))
                        {
                            cmd.Parameters.AddWithValue("zsvemail", irp.Z_SV.EMAIL);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("zsvemail", DBNull.Value);
                        }
                        if (irp.Z_SV.Z_DRSomeDate.HasValue)
                        {
                            cmd.Parameters.AddWithValue("applicantbirthdate", irp.Z_SV.Z_DRSomeDate.Value);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("applicantbirthdate", DBNull.Value);
                        }
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("zsvzf", DBNull.Value);
                        cmd.Parameters.AddWithValue("zsvzi", DBNull.Value);
                        cmd.Parameters.AddWithValue("zsvzo", DBNull.Value);
                        cmd.Parameters.AddWithValue("zsvzenp", DBNull.Value);
                        cmd.Parameters.AddWithValue("zsvzsmo", DBNull.Value);
                        cmd.Parameters.AddWithValue("zsvzdoctype", DBNull.Value);
                        cmd.Parameters.AddWithValue("zsvzdocser", DBNull.Value);
                        cmd.Parameters.AddWithValue("zsvzdocnum", DBNull.Value);
                        cmd.Parameters.AddWithValue("zsvadr", DBNull.Value);
                        cmd.Parameters.AddWithValue("zsvphone", DBNull.Value);
                        cmd.Parameters.AddWithValue("zsvemail", DBNull.Value);
                        cmd.Parameters.AddWithValue("applicantbirthdate", DBNull.Value);
                    }


                    if (!String.IsNullOrEmpty(irp.EMPLOYEE_IT))
                    {
                        cmd.Parameters.AddWithValue("employeeit", irp.EMPLOYEE_IT);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("employeeit", DBNull.Value);
                    }
                    if (!String.IsNullOrEmpty(irp.TEXT))
                    {
                        cmd.Parameters.AddWithValue("text", irp.TEXT);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("text", DBNull.Value);
                    }
                    if (!String.IsNullOrEmpty(irp.TIME_CREATE))
                    {
                        cmd.Parameters.AddWithValue("timecreate", irp.TIME_CREATE);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("timecreate", DBNull.Value);
                    }
                    if (!String.IsNullOrEmpty(irp.WAY_N))
                    {
                        cmd.Parameters.AddWithValue("wayn", irp.WAY_N);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("wayn", DBNull.Value);
                    }

                    if (smoId != "")
                    {
                        cmd.Parameters.AddWithValue("smoidattr", smoId);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("smoidattr", DBNull.Value);
                    }

                   
                    
                    if (irp.DATE_CLOSESomeDate.HasValue)
                    {
                        cmd.Parameters.AddWithValue("appealfactenddate", irp.DATE_CLOSESomeDate.Value);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("appealfactenddate", DBNull.Value);
                    }
                    if (irp.RESULTSomeValue.HasValue)
                    {
                        cmd.Parameters.AddWithValue("appealresultid", irp.RESULTSomeValue.Value);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("appealresultid", DBNull.Value);
                    }
                    if (!String.IsNullOrEmpty(irp.ZH_D))
                    {
                        cmd.Parameters.AddWithValue("complaintid", irp.ZH_D);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("complaintid", DBNull.Value);
                    }

                    #endregion

                    // NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    string str = "";
                    conn.Open();
                    // toImport = true;
                    try
                    {
                        cmd.ExecuteNonQuery();
                        insRes.sugResult = "Запись добавлена в базу";
                        listInsRes.Add(insRes);



                    }
                    catch (Exception e)
                    {
                        string err = e.Message;
                        insRes.sugResult = err;
                        listInsRes.Add(insRes);
                        // return null;
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
                else if (irp.Has_Errors == "No" && irp.Is_imported == "Yes")
                {
                    InsertResult insRes = new InsertResult();
                    insRes.sugNum = irp.N_IRP;
                    //   string connStr = "Server=localhost;Database=tfoms;User ID=postgres;Password=12345;CommandTimeout=180000;Port=5435";
                    // string connStr = "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1521))(CONNECT_DATA=(SID=ezhkh)));User Id = b4_GKH_Samara; Password = ACTANONVERBA";
                    string cmdText1 = @"UPDATE dbo.journalappeal SET date = '" + irp.DATE_CREATESomeDate + @"', time_ = :timecreate,
            sourceincomeid = " + irp.WAY + @", organizationname = :wayn, typeofaddressingid = " + irp.IRP_TYPE + @", wayofaddressingid = " + irp.HOW + @",
            appealtheme = " + irp.THEME + @", appealcontent = :text, complaintid = :complaintid, appealorganizationid = " + irp.OTV_T + @", appealorganizationcode = '" + irp.OTV_KON + @"', 
            takingappeallineid = 3, acceptedby = '" + irp.EMPLOYEE_1 + @"', reviewappeallineid = 4, responsible = :employeeit, appealplanenddate = '" + irp.DATA_PLANSomeDate + @"',
            appealfactenddate = :appealfactenddate, appealresultid = :appealresultid, applicantsurname = :zsvzf, applicantname = :zsvzi, applicantsecondname = :zsvzo,
            applicantbirthdate = :applicantbirthdate, applicantenp = :zsvzenp, applicantsmo = :zsvzsmo, applicanttypedocument = :zsvzdoctype, applicantdocumentseries = :zsvzdocser, 
            applicantdocumentnumber = :zsvzdocnum, applicantfeedbackaddress = :zsvadr, applicantphonenumber = :zsvphone, applicantemail = :zsvemail, receivedtreatmentpersonsurname = :insvinf,
            receivedtreatmentpersonname = :insvini, receivedtreatmentpersonsecondname = :insvino, receivedtreatmentpersonbirthdate = :receivedtreatmentpersonbirthdate, 
            receivedtreatmentpersonenp = :insvinenp, receivedtreatmentpersonsmo = :insvinsmo, receivedtreatmentpersonypedocument = :insvindoctype, 
            receivedtreatmentpersondocumentseries = :insvindocser, receivedtreatmentpersondocumentnumber = :insvindocnum,
            isdeleted = false, updateddate = CURRENT_DATE, smoid = :smoidattr where appealuniquenumber = '" + irp.N_IRP + "'";
                    //string cmdText = "SELECT id FROM realty_object where mu_name LIKE '%Волжский р-н%'";
                    //string cmdText = "SELECT * FROM employees";
                    NpgsqlConnection conn = new NpgsqlConnection(connStr);
                    NpgsqlCommand cmd = new NpgsqlCommand(cmdText1, conn);

                    if (irp.IN_SV != null)
                    {
                        if (!String.IsNullOrEmpty(irp.IN_SV.IN_F))
                        {
                            cmd.Parameters.AddWithValue("insvinf", irp.IN_SV.IN_F);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("insvinf", DBNull.Value);
                        }
                        if (!String.IsNullOrEmpty(irp.IN_SV.IN_I))
                        {
                            cmd.Parameters.AddWithValue("insvini", irp.IN_SV.IN_I);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("insvini", DBNull.Value);
                        }
                        if (!String.IsNullOrEmpty(irp.IN_SV.IN_O))
                        {
                            cmd.Parameters.AddWithValue("insvino", irp.IN_SV.IN_O);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("insvino", DBNull.Value);
                        }
                        if (!String.IsNullOrEmpty(irp.IN_SV.IN_ENP))
                        {
                            cmd.Parameters.AddWithValue("insvinenp", irp.IN_SV.IN_ENP);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("insvinenp", DBNull.Value);
                        }
                        if (!String.IsNullOrEmpty(irp.IN_SV.IN_SMO))
                        {
                            cmd.Parameters.AddWithValue("insvinsmo", irp.IN_SV.IN_SMO);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("insvinsmo", DBNull.Value);
                        }
                        if (!String.IsNullOrEmpty(irp.IN_SV.IN_DOCTYPE))
                        {
                            cmd.Parameters.AddWithValue("insvindoctype", irp.IN_SV.IN_DOCTYPE);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("insvindoctype", DBNull.Value);
                        }
                        if (!String.IsNullOrEmpty(irp.IN_SV.IN_DOCSER))
                        {
                            cmd.Parameters.AddWithValue("insvindocser", irp.IN_SV.IN_DOCSER);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("insvindocser", DBNull.Value);
                        }
                        if (!String.IsNullOrEmpty(irp.IN_SV.IN_DOCNUM))
                        {
                            cmd.Parameters.AddWithValue("insvindocnum", irp.IN_SV.IN_DOCNUM);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("insvindocnum", DBNull.Value);
                        }
                        if (irp.IN_SV.IN_DRSomeDate.HasValue)
                        {
                            cmd.Parameters.AddWithValue("receivedtreatmentpersonbirthdate", irp.IN_SV.IN_DRSomeDate.Value);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("receivedtreatmentpersonbirthdate", DBNull.Value);
                        }
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("insvinf", DBNull.Value);
                        cmd.Parameters.AddWithValue("insvini", DBNull.Value);
                        cmd.Parameters.AddWithValue("insvino", DBNull.Value);
                        cmd.Parameters.AddWithValue("insvinenp", DBNull.Value);
                        cmd.Parameters.AddWithValue("insvinsmo", DBNull.Value);
                        cmd.Parameters.AddWithValue("insvindoctype", DBNull.Value);
                        cmd.Parameters.AddWithValue("insvindocser", DBNull.Value);
                        cmd.Parameters.AddWithValue("insvindocnum", DBNull.Value);
                        cmd.Parameters.AddWithValue("receivedtreatmentpersonbirthdate", DBNull.Value);
                    }


                    if (irp.Z_SV != null)
                    {
                        if (!String.IsNullOrEmpty(irp.Z_SV.Z_F))
                        {
                            cmd.Parameters.AddWithValue("zsvzf", irp.Z_SV.Z_F);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("zsvzf", DBNull.Value);
                        }
                        if (!String.IsNullOrEmpty(irp.Z_SV.Z_I))
                        {
                            cmd.Parameters.AddWithValue("zsvzi", irp.Z_SV.Z_I);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("zsvzi", DBNull.Value);
                        }
                        if (!String.IsNullOrEmpty(irp.Z_SV.Z_O))
                        {
                            cmd.Parameters.AddWithValue("zsvzo", irp.Z_SV.Z_O);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("zsvzo", DBNull.Value);
                        }
                        if (!String.IsNullOrEmpty(irp.Z_SV.Z_ENP))
                        {
                            cmd.Parameters.AddWithValue("zsvzenp", irp.Z_SV.Z_ENP);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("zsvzenp", DBNull.Value);
                        }
                        if (!String.IsNullOrEmpty(irp.Z_SV.Z_SMO))
                        {
                            cmd.Parameters.AddWithValue("zsvzsmo", irp.Z_SV.Z_SMO);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("zsvzsmo", DBNull.Value);
                        }
                        if (!String.IsNullOrEmpty(irp.Z_SV.Z_DOCTYPE))
                        {
                            cmd.Parameters.AddWithValue("zsvzdoctype", irp.Z_SV.Z_DOCTYPE);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("zsvzdoctype", DBNull.Value);
                        }
                        if (!String.IsNullOrEmpty(irp.Z_SV.Z_DOCSER))
                        {
                            cmd.Parameters.AddWithValue("zsvzdocser", irp.Z_SV.Z_DOCSER);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("zsvzdocser", DBNull.Value);
                        }
                        if (!String.IsNullOrEmpty(irp.Z_SV.Z_DOCNUM))
                        {
                            cmd.Parameters.AddWithValue("zsvzdocnum", irp.Z_SV.Z_DOCNUM);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("zsvzdocnum", DBNull.Value);
                        }
                        if (!String.IsNullOrEmpty(irp.Z_SV.ADR))
                        {
                            cmd.Parameters.AddWithValue("zsvadr", irp.Z_SV.ADR);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("zsvadr", DBNull.Value);
                        }
                        if (!String.IsNullOrEmpty(irp.Z_SV.PHONE))
                        {
                            cmd.Parameters.AddWithValue("zsvphone", irp.Z_SV.PHONE);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("zsvphone", DBNull.Value);
                        }
                        if (!String.IsNullOrEmpty(irp.Z_SV.EMAIL))
                        {
                            cmd.Parameters.AddWithValue("zsvemail", irp.Z_SV.EMAIL);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("zsvemail", DBNull.Value);
                        }
                        if (irp.Z_SV.Z_DRSomeDate.HasValue)
                        {
                            cmd.Parameters.AddWithValue("applicantbirthdate", irp.Z_SV.Z_DRSomeDate.Value);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("applicantbirthdate", DBNull.Value);
                        }
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("zsvzf", DBNull.Value);
                        cmd.Parameters.AddWithValue("zsvzi", DBNull.Value);
                        cmd.Parameters.AddWithValue("zsvzo", DBNull.Value);
                        cmd.Parameters.AddWithValue("zsvzenp", DBNull.Value);
                        cmd.Parameters.AddWithValue("zsvzsmo", DBNull.Value);
                        cmd.Parameters.AddWithValue("zsvzdoctype", DBNull.Value);
                        cmd.Parameters.AddWithValue("zsvzdocser", DBNull.Value);
                        cmd.Parameters.AddWithValue("zsvzdocnum", DBNull.Value);
                        cmd.Parameters.AddWithValue("zsvadr", DBNull.Value);
                        cmd.Parameters.AddWithValue("zsvphone", DBNull.Value);
                        cmd.Parameters.AddWithValue("zsvemail", DBNull.Value);
                        cmd.Parameters.AddWithValue("applicantbirthdate", DBNull.Value);
                    }


                    if (!String.IsNullOrEmpty(irp.EMPLOYEE_IT))
                    {
                        cmd.Parameters.AddWithValue("employeeit", irp.EMPLOYEE_IT);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("employeeit", DBNull.Value);
                    }
                    if (!String.IsNullOrEmpty(irp.TEXT))
                    {
                        cmd.Parameters.AddWithValue("text", irp.TEXT);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("text", DBNull.Value);
                    }
                    if (!String.IsNullOrEmpty(irp.TIME_CREATE))
                    {
                        cmd.Parameters.AddWithValue("timecreate", irp.TIME_CREATE);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("timecreate", DBNull.Value);
                    }
                    if (!String.IsNullOrEmpty(irp.WAY_N))
                    {
                        cmd.Parameters.AddWithValue("wayn", irp.WAY_N);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("wayn", DBNull.Value);
                    }

                    if (smoId != "")
                    {
                        cmd.Parameters.AddWithValue("smoidattr", smoId);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("smoidattr", DBNull.Value);
                    }



                    if (irp.DATE_CLOSESomeDate.HasValue)
                    {
                        cmd.Parameters.AddWithValue("appealfactenddate", irp.DATE_CLOSESomeDate.Value);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("appealfactenddate", DBNull.Value);
                    }
                    if (irp.RESULTSomeValue.HasValue)
                    {
                        cmd.Parameters.AddWithValue("appealresultid", irp.RESULTSomeValue.Value);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("appealresultid", DBNull.Value);
                    }
                    if (!String.IsNullOrEmpty(irp.ZH_D))
                    {
                        cmd.Parameters.AddWithValue("complaintid", irp.ZH_D);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("complaintid", DBNull.Value);
                    }



                    // NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    string str = "";
                    conn.Open();
                    // toImport = true;
                    try
                    {
                        cmd.ExecuteNonQuery();
                        insRes.sugResult = "Запись обновлена";
                        listInsRes.Add(insRes);



                    }
                    catch (Exception e)
                    {
                        string err = e.Message;
                        insRes.sugResult = err;
                        listInsRes.Add(insRes);
                        // return null;
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
            }
            return listInsRes;

        }
        #endregion

        #region Инсертим результат обращения
        public List<InsertResult> InsertResult(FLK errorFile, string rnum_smo)
        {
            List<InsertResult> listInsRes = new List<InsertResult>();
            foreach (PR err in errorFile.PR)
            {
                if (rnum_smo.Length > 1)
                {
                    InsertResult insRes = new InsertResult();
                    insRes.sugNum = err.N_ZAP;
                    //   string connStr = "Server=localhost;Database=tfoms;User ID=postgres;Password=12345;CommandTimeout=180000;Port=5435";
                    // string connStr = "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1521))(CONNECT_DATA=(SID=ezhkh)));User Id = b4_GKH_Samara; Password = ACTANONVERBA";
                    string cmdText1 = @"INSERT INTO dbo.flk(basefilename, errorcode, fieldname, baseentitiyname, appealnumber, comment, rnsmo, isdeleted, createddate, updateddate)
    VALUES ('" + errorFile.FNAME_I + @"', '" + err.OSHIB + @"', '" + err.IM_POL + @"', '" + err.BAS_EL + @"', '" + err.N_ZAP + @"',  
            '" + err.COMMENT + @"', '" + rnum_smo + @"', false, CURRENT_DATE, CURRENT_DATE)";
                    insRes.request = cmdText1;

                    //string cmdText = "SELECT id FROM realty_object where mu_name LIKE '%Волжский р-н%'";
                    //string cmdText = "SELECT * FROM employees";
                    NpgsqlConnection conn = new NpgsqlConnection(connStr);
                    NpgsqlCommand cmd = new NpgsqlCommand(cmdText1, conn);
                    // NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    string str = "";
                    conn.Open();
                    try
                    {
                        cmd.ExecuteNonQuery();
                        insRes.sugResult = "Запись добавлена в базу";

                        listInsRes.Add(insRes);
                        // return str;


                    }
                    catch (Exception e)
                    {
                        string error = e.Message;

                        insRes.sugResult = error;
                        listInsRes.Add(insRes);
                        // return null;
                    }
                    finally
                    {

                        conn.Close();
                    }
                }
            }

            return listInsRes;

        }
        # endregion


        private void CheckToken(String token)
        {
            String newToken = Crypt.GetHashStringMD5(WcfService1.Core.Configuration.Salt +
                                                     WcfService1.Core.Configuration.CurrentDay)
                                  .ToString();

            if (token != newToken)
            {
                throw new Exception("НЕСАНКЦИОННИРОВАННЫЙ ВХОД!");
            }
        }


    }

    public class Item
    {
        [XmlElement("Period")]
        public string Period { get; set; }

        [XmlElement("Name")]
        public string Name { get; set; }
    }

    [XmlRoot("TfomsFile")]
    public class TfomsFile
    {
        [XmlElement("Item", typeof(Item))]
        public List<Item> ItemList { get; set; }
    }

    public class InsertResult
    {
        public string sugNum;
        public string sugResult;
        public string request;

    }

    //  [XmlRoot("FLK")]
    [DataContract(Namespace = "")]
    public class FLK
    {
        [DataMember]
        //  [XmlElement("FNAME")]
        public string FNAME { get; set; }

        //  [XmlElement("FNAME_I")]
        [DataMember]
        public string FNAME_I { get; set; }

        // [XmlElement("PR")]
        [DataMember]
        public List<PR> PR { get; set; }
    }

    [DataContract(Namespace = "")]
    public class PR
    {
        //  [XmlElement("OSHIB")]
        [DataMember]
        public string OSHIB { get; set; }

        //  [XmlElement("IM_POL")]
        [DataMember]
        public string IM_POL { get; set; }

        //  [XmlElement("BAS_EL")]
        [DataMember]
        public string BAS_EL { get; set; }

        //  [XmlElement("N_ZAP")]
        [DataMember]
        public string N_ZAP { get; set; }

        //   [XmlElement("COMMENT")]
        [DataMember]
        public string COMMENT { get; set; }
    }

    public class IN_SV
    {
        [XmlElement("IN_F")]
        public string IN_F { get; set; }

        [XmlElement("IN_I")]
        public string IN_I { get; set; }

        [XmlElement("IN_O")]
        public string IN_O { get; set; }

        [XmlIgnore]
        public DateTime? IN_DRSomeDate { get; set; }

        [XmlElement("IN_DR")]
        public string IN_DR
        {
            get { return this.IN_DRSomeDate.HasValue ? IN_DRSomeDate.Value.ToString("dd.MM.yyyy") : null; }
            set
            {
                DateTime dt = new DateTime();
                IN_DRSomeDate = DateTime.TryParse(value, out dt) ? (DateTime?)dt : null;
            }
        }

        [XmlElement("IN_ENP")]
        public string IN_ENP { get; set; }

        [XmlElement("IN_SMO")]
        public string IN_SMO { get; set; }

        [XmlElement("IN_DOCTYPE")]
        public string IN_DOCTYPE { get; set; }

        [XmlElement("IN_DOCSER")]
        public string IN_DOCSER { get; set; }

        [XmlElement("IN_DOCNUM")]
        public string IN_DOCNUM { get; set; }
    }

    public class Z_SV
    {
        [XmlElement("Z_F")]
        public string Z_F { get; set; }

        [XmlElement("Z_I")]
        public string Z_I { get; set; }

        [XmlElement("Z_O")]
        public string Z_O { get; set; }

        [XmlIgnore]
        public DateTime? Z_DRSomeDate { get; set; }

        [XmlElement("Z_DR")]
        public string Z_DR
        {
            get { return this.Z_DRSomeDate.HasValue ? Z_DRSomeDate.Value.ToString("dd.MM.yyyy") : null; }
            set
            {
                DateTime dt = new DateTime();
                Z_DRSomeDate = DateTime.TryParse(value, out dt) ? (DateTime?)dt : null;
            }
        }

        [XmlElement("Z_ENP")]
        public string Z_ENP { get; set; }

        [XmlElement("Z_SMO")]
        public string Z_SMO { get; set; }

        [XmlElement("Z_DOCTYPE")]
        public string Z_DOCTYPE { get; set; }

        [XmlElement("Z_DOCSER")]
        public string Z_DOCSER { get; set; }

        [XmlElement("Z_DOCNUM")]
        public string Z_DOCNUM { get; set; }

        [XmlElement("ADR")]
        public string ADR { get; set; }

        [XmlElement("PHONE")]
        public string PHONE { get; set; }

        [XmlElement("E-MAIL")]
        public string EMAIL { get; set; }
    }

    public class IRP
    {

        [XmlIgnore]
        public string Is_imported { get; set; }

        [XmlIgnore]
        public string Has_Errors { get; set; }

        [XmlElement("N_IRP")]
        public string N_IRP { get; set; }

        [XmlElement("TF_ID")]
        public string TF_ID { get; set; }

        [XmlElement("IRP_TYPE")]
        public Int16 IRP_TYPE { get; set; }

        [XmlIgnore]
        public DateTime DATE_CREATESomeDate { get; set; }

        [XmlElement("DATE_CREATE")]
        public string DATE_CREATE
        {
            get { return this.DATE_CREATESomeDate.ToString("dd.MM.yyyy"); }
            set { this.DATE_CREATESomeDate = DateTime.Parse(value); }
        }

        [XmlElement("TIME_CREATE")]
        public string TIME_CREATE { get; set; }

        [XmlElement("WAY")]
        public Int16 WAY { get; set; }

        [XmlElement("WAY_N")]
        public string WAY_N { get; set; }

        [XmlElement("HOW")]
        public Int16 HOW { get; set; }

        [XmlElement("THEME")]
        public string THEME { get; set; }

        [XmlElement("TEXT")]
        public string TEXT { get; set; }

        [XmlElement("ZH_D")]
        public string ZH_D { get; set; }

        [XmlElement("OTV_T")]
        public Int16 OTV_T { get; set; }

        [XmlElement("OTV_KON")]
        public string OTV_KON { get; set; }

        [XmlElement("EMPLOYEE_1")]
        public string EMPLOYEE_1 { get; set; }

        [XmlElement("EMPLOYEE_IT")]
        public string EMPLOYEE_IT { get; set; }

        [XmlElement("Z_SV", typeof(Z_SV))]
        public Z_SV Z_SV { get; set; }

        [XmlElement("IN_SV", typeof(IN_SV))]
        public IN_SV IN_SV { get; set; }

        [XmlIgnore]
        public DateTime DATA_PLANSomeDate { get; set; }

        [XmlElement("DATA_PLAN")]
        public string DATA_PLAN
        {
            get { return this.DATA_PLANSomeDate.ToString("dd.MM.yyyy"); }
            set { this.DATA_PLANSomeDate = DateTime.Parse(value); }
        }

        [XmlIgnore]
        public DateTime? DATE_CLOSESomeDate { get; set; }

        [XmlElement("DATE_CLOSE")]
        public string DATE_CLOSE
        {
            get { return this.DATE_CLOSESomeDate.HasValue ? DATE_CLOSESomeDate.Value.ToString("dd.MM.yyyy") : null; }
            set
            {
                DateTime dt = new DateTime();
                DATE_CLOSESomeDate = DateTime.TryParse(value, out dt) ? (DateTime?)dt : null;
            }
        }

        [XmlIgnore]
        public Int16? RESULTSomeValue { get; set; }

        [XmlElement("RESULT")]
        public string RESULT
        {
            get { return this.RESULTSomeValue.HasValue ? RESULTSomeValue.Value.ToString() : null; }
            set
            {
                Int16 int16 = new Int16();
                RESULTSomeValue = Int16.TryParse(value, out int16) ? (Int16?)int16 : null;
            }
        }

        //[XmlElement("RESULT")]
        //public Int16? RESULT { get; set; }
    }

    public class ZGLV
    {
        [XmlElement("VERSION")]
        public string VERSION { get; set; }

        [XmlIgnore]
        public DateTime DATASomeDate { get; set; }

        [XmlElement("DATA")]
        public string DATA
        {
            get { return this.DATASomeDate.ToString("dd.MM.yyyy"); }
            set { this.DATASomeDate = DateTime.Parse(value); }
        }

        [XmlElement("YEAR")]
        public Int32 YEAR { get; set; }

        [XmlElement("MONTH")]
        public Int16 MONTH { get; set; }

        [XmlElement("DAY")]
        public Int16 DAY { get; set; }

        [XmlElement("TIME")]
        public string TIME { get; set; }

        [XmlElement("SMO")]
        public string SMO { get; set; }

        [XmlElement("FILENAME")]
        public string FILENAME { get; set; }
    }


    [XmlRoot("IRP_LIST")]
    public class IRP_LIST
    {
        [XmlElement("ZGLV", typeof(ZGLV))]
        public ZGLV ZGLV { get; set; }

        [XmlArrayItem("IRP", typeof(IRP[]))]
        public IRP[] IRP { get; set; }
    }



}