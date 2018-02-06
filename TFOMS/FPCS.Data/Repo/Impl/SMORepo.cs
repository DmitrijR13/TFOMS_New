using System;
using System.Linq;

using FPCS.Data.Entities;
using FPCS.Data.Exceptions;
using FPCS.Data.Enums;

namespace FPCS.Data.Repo.Impl
{
    internal class SMORepo : RepoBase<SMO, StudentManagementContext>, ISMORepo
    {
        public SMORepo(UnitOfWork<StudentManagementContext> unitOfWork) : base(unitOfWork) { }

        #region override methods

        public override IQueryable<SMO> GetAll()
        {
            return DbSet.Where(x => !x.IsDeleted/* && x.IsShow*/);
        }

        #endregion override methods

        /// <summary>
        /// Добавление новой СМО
        /// </summary>
        public SMO Add(String smoCode, String kpp, String fullName, String shortName, String factAddress, String director, String filialDirector, String licenseInfo, 
            String headSurname, String headName, String headSecondName, String headPosition)
        {
            var smo =
                Add(
                new SMO
                {
                    SmoCode = smoCode,
                    KPP = kpp,
                    FullName = fullName,
                    ShortName = shortName,
                    FactAddress = factAddress,
                    Director = director,
                    FilialDirector = filialDirector,
                    LicenseInfo = licenseInfo,
                    UpdatedDate = DateTime.Now,
                    CreatedDate = DateTime.Now,
                    HeadName = headName,
                    HeadPosition = headPosition,
                    HeadSecondName = headSecondName,
                    HeadSurname = headSurname
                });

            return smo;
        }

        /// <summary>
        /// Редактирование СМО
        /// </summary>
        public SMO Update(Int64 smoId, String smoCode, String kpp, String fullName, String shortName, String factAddress, String director, String filialDirector, String licenseInfo,
            String headSurname, String headName, String headSecondName, String headPosition)
        {
            var smo = this.Get(smoId);
            if (smo == null) throw new NotFoundEntityException("Запись не найдена");

            smo.SmoCode = smoCode;
            smo.KPP = kpp;
            smo.FullName = fullName;
            smo.ShortName = shortName;
            smo.FactAddress = factAddress;
            smo.Director = director;
            smo.LicenseInfo = licenseInfo;
            smo.FilialDirector = filialDirector;
            smo.UpdatedDate = DateTime.Now;
            smo.HeadName = headName;
            smo.HeadPosition = headPosition;
            smo.HeadSecondName = headSecondName;
            smo.HeadSurname = headSurname;

            return smo;
        }
    }
}
