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
using FPCS.Web.Admin.Models.FLK;

namespace FPCS.Web.Admin.Controllers
{
    public class FLKController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult _Index(GridOptions options, FLKListOptions flkListOptions)
        {
            using (var uow = UnityManager.Resolve<IUnitOfWork>())
            {
                string userName = User.Login;
                var dbUserRepo = uow.GetRepo<IDbUserRepo>();
                var dbUser = dbUserRepo.GetByLogin(userName);
                String smo = "";
                if (dbUser.Role != Role.Admin && dbUser.Role != Role.TfomsUser)
                {
                    var userRuepo = uow.GetRepo<IUserRepo>();
                    smo = userRuepo.Get(dbUser.DbUserId).SMO.SmoCode;
                }

                var repo = uow.GetRepo<IFLKRepo>();

                var flk = repo.GetAll()
                    .Where(x => (smo != "" ? x.SmoRegNum == smo : 1==1))
                    .Select(x => new
                    {
                        Id = x.FlkId,
                        x.AppealNumber,
                        x.BaseEntitiyName,
                        x.BaseFileName,
                        x.Comment,
                        x.ErrorCode,
                        x.FieldName,
                        x.SmoRegNum
                    });

                var engine = new GridDynamicEngine(options, flkListOptions);
                var resultTemp = engine.ApplySort(engine.ApplyFilter(flk.AsQueryable())).Select(x => new FLKIndexModel
                {
                    Id = x.Id,
                    AppealNumber = x.AppealNumber,
                    BaseEntitiyName = x.BaseEntitiyName,
                    BaseFileName = x.BaseFileName,
                    Comment = x.Comment,
                    ErrorCode = x.ErrorCode,
                    FieldName = x.FieldName,
                    SmoRegNum = x.SmoRegNum
                });

                int totalCount = 0;
                var sessionOptions = (FLKListOptions)Session["FLKListOptions" + User.Login]; ;
                if (Session["FLKCount" + User.Login] == null
                    || sessionOptions == null
                    || sessionOptions.AppealNumber != flkListOptions.AppealNumber
                    || sessionOptions.BaseEntitiyName != flkListOptions.BaseEntitiyName
                    || sessionOptions.BaseFileName != flkListOptions.BaseFileName
                    || sessionOptions.Comment != flkListOptions.Comment
                    || sessionOptions.ErrorCode != flkListOptions.ErrorCode
                    || sessionOptions.FieldName != flkListOptions.FieldName
                    || sessionOptions.SmoRegNum != flkListOptions.SmoRegNum
                   )
                {
                    totalCount = resultTemp.Count();
                    Session["FLKCount" + User.Login] = totalCount;
                    Session["FLKListOptions" + User.Login] = flkListOptions;
                }
                else
                {
                    totalCount = Convert.ToInt32(Session["FLKCount" + User.Login]);
                }

                var result = engine.CreateGridResult2(engine.ApplyPaging(resultTemp.AsQueryable()), totalCount, x => new FLKIndexModel
                {
                    Id = x.Id,
                    AppealNumber = x.AppealNumber,
                    BaseEntitiyName = x.BaseEntitiyName,
                    BaseFileName = x.BaseFileName,
                    Comment = x.Comment,
                    ErrorCode = x.ErrorCode,
                    FieldName = x.FieldName,
                    SmoRegNum = x.SmoRegNum
                });

                return Json(result);
            }
        }
    }
}