using FPCS.Core.Extensions;
using FPCS.Core.Unity;
using FPCS.Data;
using FPCS.Data.Enums;
using FPCS.Data.Repo;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FPCS.Web.Admin.Models.HandAppeal
{
    public class HandAppealCreateModel
    {
        /// <summary>
        /// Уникальный номер обращения
        /// </summary>
        [Display(Name = "Уникальный номер обращения")]
        public String AppealUniqueNumber { get; set; }

        /// <summary>
        /// Дата поступления обращения
        /// </summary>
        [Display(Name = "Дата регистрации в ОЗПЗ")]
        public DateTime? Date { get; set; }

        /// <summary>
        /// ID способа обращения
        /// </summary>
        public Int64? TypeOfAddressingId { get; set; }

        /// <summary>
        /// способ обращения
        /// </summary>
        [Display(Name = "Способ обращения")]
        public SelectList TypeOfAddressingName { get; set; }

        /// <summary>
        /// ID вида обращения
        /// </summary>
        public Int64? WayOfAddressingId { get; set; }

        /// <summary>
        /// вид обращения
        /// </summary>
        [Display(Name = "Вид обращения")]
        public SelectList WayOfAddressingName { get; set; }

        public Int64? ThemeAppealCitizensId { get; set; }

        /// <summary>
        /// Тема обращения
        /// </summary>
        [Display(Name = "Тема обращения")]
        public SelectList AppealTheme { get; set; }

        /// <summary>
        /// Содержание обращения
        /// </summary>
        [Display(Name = "Содержание обращения")]
        public String AppealContent { get; set; }

        /// <summary>
        /// ID жалобы
        /// </summary>
        public Int64? ComplaintId { get; set; }

        /// <summary>
        /// жалоба
        /// </summary>
        [Display(Name = "Признак обоснованности")]
        public SelectList ComplaintName { get; set; }


        public Int64? SMOId { get; set; }

        /// <summary>
        /// организация, ответственная за работу с обращением
        /// </summary>
        [Display(Name = "СМО, ответственная за работу с обращением")]
        public SelectList SMOOrganizationName { get; set; }

        /// <summary>
        /// Дата окончания срока рассмотрения обращения
        /// </summary>
        [Display(Name = "Дата окончания срока рассмотрения обращения")]
        public DateTime? AppealPlanEndDate { get; set; }

        /// <summary>
        /// Дата фактического закрытия обращения
        /// </summary>
        [Display(Name = "Дата фактического закрытия обращения")]
        public DateTime? AppealFactEndDate { get; set; }

        /// <summary>
        /// ID Статуса обращения
        /// </summary>
        public Int64? AppealResultId { get; set; }

        /// <summary>
        /// Статус обращения
        /// </summary>
        [Display(Name = "Статус обращения")]
        public SelectList AppealResultName { get; set; }

        /// <summary>
        /// Фамилия заявителя
        /// </summary>
        [Display(Name = "Фамилия")]
        public String ApplicantSurname { get; set; }

        /// <summary>
        /// Имя заявителя
        /// </summary>
        [Display(Name = "Имя")]
        public String ApplicantName { get; set; }

        /// <summary>
        /// Отчество заявителя
        /// </summary>
        [Display(Name = "Отчество")]
        public String ApplicantSecondName { get; set; }

        /// <summary>
        /// Дата рождения заявителя
        /// </summary>
        [Display(Name = "Дата рождения")]
        public DateTime? ApplicantBirthDate { get; set; }

        /// <summary>
        /// ЕНП заявителя
        /// </summary>
        [Display(Name = "ЕНП")]
        public String ApplicantENP { get; set; }

        /// <summary>
        /// Страховая принадлежность заявителя
        /// </summary>
        [Display(Name = "Адрес для обратного ответа")]
        public String ApplicantFeedbackAddress { get; set; }

        /// <summary>
        /// Контактный телефон заявителя
        /// </summary>
        [Display(Name = "Контактный телефон")]
        public String ApplicantPhoneNumber { get; set; }

        /// <summary>
        /// Адрес электронной почты заявителя
        /// </summary>
        [Display(Name = "Адрес электронной почты")]
        public String ApplicantEmail { get; set; }

        /// <summary>
        /// Фамилия лица, в отношении которого поступило обращение
        /// </summary>
        [Display(Name = "Фамилия")]
        public String ReceivedTreatmentPersonSurname { get; set; }

        /// <summary>
        /// Имя лица, в отношении которого поступило обращение
        /// </summary>
        [Display(Name = "Имя")]
        public String ReceivedTreatmentPersonName { get; set; }

        /// <summary>
        /// Отчество лица, в отношении которого поступило обращение
        /// </summary>
        [Display(Name = "Отчество")]
        public String ReceivedTreatmentPersonSecondName { get; set; }

        /// <summary>
        /// Дата рождения заявлица, в отношении которого поступило обращениеителя
        /// </summary>
        [Display(Name = "Дата рождения")]
        public DateTime? ReceivedTreatmentPersonBirthDate { get; set; }

        /// <summary>
        /// ЕНП лица, в отношении которого поступило обращение
        /// </summary>
        [Display(Name = "ЕНП")]
        public String ReceivedTreatmentPersonENP { get; set; }

        /// <summary>
        /// Дата регистрации в ОЗПЗ
        /// </summary>
        [Display(Name = "Дата входящей регистрации")]
        public DateTime OZPZRegistrationDate { get; set; }

        /// <summary>
        /// Номер обращения
        /// </summary>
        [Display(Name = "Номер обращения")]
        public String AppealNumber { get; set; }

        /// <summary>
        /// сотрудник
        /// </summary>
        [Display(Name = "Наименование МО")]
        public SelectList MOName { get; set; }

        /// <summary>
        /// ID медицинской организации
        /// </summary>
        public String[] MedicalOrganizationId { get; set; }

        /// <summary>
        /// Номер запроса направителя
        /// </summary>
        [Display(Name = "Номер запроса")]
        public String GuideNumber { get; set; }

        /// <summary>
        /// сотрудник
        /// </summary>
        [Display(Name = "Наименование организации")]
        public SelectList OrganizationNameList { get; set; }

        /// <summary>
        /// ID медицинской организации
        /// </summary>
        public String[] OrganizationId { get; set; }

        /// <summary>
        /// Дата исполнения
        /// </summary>
        [Display(Name = "Дата исполнения")]
        public DateTime GuideDate { get; set; }

        /// <summary>
        /// ID Проведенного мероприятия
        /// </summary>
        public Int64? PassedEventId { get; set; }

        /// <summary>
        /// Проведенные мероприятия
        /// </summary>
        [Display(Name = "Проведенные мероприятия")]
        public SelectList PassedEventName { get; set; }

        /// <summary>
        /// Вид экспертизы (МЭЭ, ЭКМП)
        /// </summary>
        [Display(Name = "Вид экспертизы")]
        public String InspectionKind { get; set; }

        /// <summary>
        /// Эксперт организатор/ Эксперт КМП (Ф.И.О., код)  
        /// </summary>
        [Display(Name = "Эксперт организатор")]
        public String InspectionExpert { get; set; }

        /// <summary>
        /// Результат. Номер
        /// </summary>
        [Display(Name = "Номер")]
        public String ReviewAppealNumber { get; set; }

        /// <summary>
        /// Результат. Код нарушения
        /// </summary>
        [Display(Name = "Код нарушения")]
        public String ReviewAppealCode { get; set; }

        /// <summary>
        /// Результат. Неоплата
        /// </summary>
        [Display(Name = "Неоплата")]
        public Decimal ReviewAppealPenalty { get; set; }

        /// <summary>
        /// Результат. Штраф, Возврат застрахованному лицу, руб.
        /// </summary>
        [Display(Name = "Возврат застрахованному лицу")]
        public Decimal ReviewAppealCashback { get; set; }

        ///// <summary>
        ///// Признак обоснованности 
        ///// </summary>
        //[Display(Name = "Признак обоснованности")]
        //public SelectList JustifiedTypes { get; set; }

        /// <summary>
        /// Комментарий
        /// </summary>
        [Display(Name = "Комментарий")]
        public String Comment { get; set; }

        /// <summary>
        /// Первая часть уникального номера
        /// </summary>
        public Int32 UniqueNumberPart1 { get; set; }


        /// <summary>
        /// ID жалобы
        /// </summary>
        public Int64? WorkerId { get; set; }

        /// <summary>
        /// сотрудник
        /// </summary>
        [Display(Name = "Сотрудник, принявший обращение")]
        public SelectList WorkerName { get; set; }

        /// <summary>
        /// Файл обращения
        /// </summary>
        [Display(Name = "Файл обращения")]
        public byte[] AppealFile { get; set; }

        public HttpPostedFileBase File { get; set; }

        public String AppealFileName { get; set; }

        /// <summary>
        /// Файл обращения
        /// </summary>
        [Display(Name = "Файл ответа")]
        public byte[] AnswerFile { get; set; }

        public HttpPostedFileBase PostedFile { get; set; }

        public String AnswerFileName { get; set; }

        /// <summary>
        /// ID медицинской организации
        /// </summary>
        public String MedicalOrganizations { get; set; }

        /// <summary>
        /// ID медицинской организации
        /// </summary>
        public String Organizations { get; set; }

        public void Init()
        {
            using (var uow = UnityManager.Instance.Resolve<IUnitOfWork>())
            {
                var typesOfAddressings = uow.GetRepo<ITypeOfAddressingRepo>()
                   .GetAll()
                   .Select(x => new Lookup<Int64> { Value = x.TypeOfAddressingId, Text = x.Name })
                   .ToList();

                var wayOfAddressings = uow.GetRepo<IWayOfAddressingRepo>()
                  .GetAll()
                  .Select(x => new Lookup<Int64> { Value = x.WayOfAddressingId, Text = x.Name })
                  .ToList();

                var themeAppealCitizens = uow.GetRepo<IThemeAppealCitizensRepo>()
                  .GetAll()
                  .Select(x => new Lookup<Int64> { Value = x.ThemeAppealCitizensId, Text = x.Name })
                  .ToList();

                var complaints = uow.GetRepo<IComplaintRepo>()
                  .GetAll()
                  .Select(x => new Lookup<Int64> { Value = x.ComplaintId, Text = x.Name })
                  .ToList();

                var smos = uow.GetRepo<ISMORepo>()
                  .GetAll()
                  .Select(x => new Lookup<Int64> { Value = x.SmoId, Text = x.SmoCode + ", " + x.ShortName})
                  .ToList();

                var apealResults = uow.GetRepo<IAppealResultRepo>()
                 .GetAll()
                 .Select(x => new Lookup<Int64> { Value = x.AppealResultId, Text = x.Name })
                 .ToList();

                var workers = uow.GetRepo<IWorkerRepo>()
                 .GetAll()
                 .Select(x => new Lookup<Int64> { Value = x.WorkerId, Text = x.Surname + " " + x.Name + " " + x.SecondName })
                 .ToList();

                var passedEvents = uow.GetRepo<IPassedEventRepo>()
                .GetAll()
                .Select(x => new Lookup<Int64> { Value = x.PassedEventId, Text = x.Name })
                .ToList();

                var medOrgs = uow.GetRepo<IMORepo>()
                .GetAll()
                .Select(x => new Lookup<Int64> { Value = x.MOId, Text = x.Name })
                .ToList();

                var organizations = uow.GetRepo<IOrganizationRepo>()
                .GetAll()
                .Select(x => new Lookup<Int64> { Value = x.OrganizationId, Text = x.Name })
                .ToList();


                TypeOfAddressingName = new SelectList(typesOfAddressings, "Value", "Text");
                WayOfAddressingName = new SelectList(wayOfAddressings, "Value", "Text");
                AppealTheme = new SelectList(themeAppealCitizens, "Value", "Text");
                ComplaintName = new SelectList(complaints, "Value", "Text");
                SMOOrganizationName = new SelectList(smos, "Value", "Text");
                AppealResultName = new SelectList(apealResults, "Value", "Text");
                WorkerName = new SelectList(workers, "Value", "Text");
                PassedEventName = new SelectList(passedEvents, "Value", "Text");
                MOName = new SelectList(medOrgs, "Value", "Text");
                OrganizationNameList = new SelectList(organizations, "Value", "Text");
            }
        }
    }
}