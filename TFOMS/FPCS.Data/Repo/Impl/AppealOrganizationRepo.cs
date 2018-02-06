using System;
using System.Linq;

using FPCS.Data.Entities;
using FPCS.Data.Exceptions;
using FPCS.Data.Enums;

namespace FPCS.Data.Repo.Impl
{
    internal class AppealOrganizationRepo : RepoBase<AppealOrganization, StudentManagementContext>, IAppealOrganizationRepo
    {
        public AppealOrganizationRepo(UnitOfWork<StudentManagementContext> unitOfWork) : base(unitOfWork) { }

        #region override methods

        public override IQueryable<AppealOrganization> GetAll()
        {
            return DbSet.Where(x => !x.IsDeleted/* && x.IsShow*/);
        }

        #endregion override methods

        /// <summary>
        /// Добавление нового источника поступления
        /// </summary>
        /// <param name="code">Код новой записи</param>
        /// <param name="name">Наименование новой записи</param>
        /// <returns></returns>
        public AppealOrganization Add(String code, String name)
        {
            var appealOrganization =
                Add(
                new AppealOrganization
                {
                    Code = code,
                    Name = name,
                    UpdatedDate = DateTime.Now,
                    CreatedDate = DateTime.Now
                });

            return appealOrganization;
        }

        /// <summary>
        /// Редактирование источника поступления
        /// </summary>
        /// <param name="appealOrganizationId">ID записи</param>
        /// <param name="code">Код записи</param>
        /// <param name="name">Наименование записи</param>
        /// <returns></returns>
        public AppealOrganization Update(Int64 appealOrganizationId, String code, String name)
        {
            var appealOrganization = this.Get(appealOrganizationId);
            if (appealOrganization == null) throw new NotFoundEntityException("Запись не найдена");

            appealOrganization.Code = code;
            appealOrganization.Name = name;
            appealOrganization.UpdatedDate = DateTime.Now;

            return appealOrganization;
        }
    }
}
