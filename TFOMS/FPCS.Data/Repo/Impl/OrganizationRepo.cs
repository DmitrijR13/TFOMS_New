using System;
using System.Linq;

using FPCS.Data.Entities;
using FPCS.Data.Exceptions;
using FPCS.Data.Enums;

namespace FPCS.Data.Repo.Impl
{
    internal class OrganizationRepo : RepoBase<Organization, StudentManagementContext>, IOrganizationRepo
    {
        public OrganizationRepo(UnitOfWork<StudentManagementContext> unitOfWork) : base(unitOfWork) { }

        #region override methods

        public override IQueryable<Organization> GetAll()
        {
            return DbSet.Where(x => !x.IsDeleted/* && x.IsShow*/);
        }

        #endregion override methods

        /// <summary>
        /// Добавление новой организации
        /// </summary>
        /// <param name="code">Код новой записи</param>
        /// <param name="name">Наименование новой записи</param>
        /// <returns></returns>
        public Organization Add(String name)
        {
            var organization =
                Add(
                new Organization
                {
                    Name = name,
                    UpdatedDate = DateTime.Now,
                    CreatedDate = DateTime.Now
                });

            return organization;
        }

        /// <summary>
        /// Редактирование организации
        /// </summary>
        /// <param name="organizationId">ID записи</param>
        /// <param name="code">Код записи</param>
        /// <param name="name">Наименование записи</param>
        /// <returns></returns>
        public Organization Update(Int64 organizationId, String name)
        {
            var organization = this.Get(organizationId);
            if (organization == null) throw new NotFoundEntityException("Запись не найдена");

            organization.Name = name;
            organization.UpdatedDate = DateTime.Now;

            return organization;
        }
    }
}
