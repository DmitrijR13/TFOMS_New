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
                    })
                    .ToList();

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
                })
                .ToList();

                Int32 totalCount = resultTemp.Count();

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