using System;
using System.Linq;

using FPCS.Data.Entities;
using FPCS.Data.Exceptions;
using FPCS.Data.Enums;

namespace FPCS.Data.Repo.Impl
{
    internal class MORepo : RepoBase<MO, StudentManagementContext>, IMORepo
    {
        public MORepo(UnitOfWork<StudentManagementContext> unitOfWork) : base(unitOfWork) { }

        #region override methods

        public override IQueryable<MO> GetAll()
        {
            return DbSet.Where(x => !x.IsDeleted/* && x.IsShow*/);
        }

        #endregion override methods

        /// <summary>
        /// Добавление новой медицинской организации
        /// </summary>
        /// <param name="code">Код новой записи</param>
        /// <param name="name">Наименование новой записи</param>
        /// <returns></returns>
        public MO Add(String code, String name)
        {
            var medOrg =
                Add(
                new MO
                {
                    Code = code,
                    Name = name,
                    UpdatedDate = DateTime.Now,
                    CreatedDate = DateTime.Now
                });

            return medOrg;
        }

        /// <summary>
        /// Редактирование медицинской организации
        /// </summary>
        /// <param name="moId">ID записи</param>
        /// <param name="code">Код записи</param>
        /// <param name="name">Наименование записи</param>
        /// <returns></returns>
        public MO Update(Int64 moId, String code, String name)
        {
            var medOrg = this.Get(moId);
            if (medOrg == null) throw new NotFoundEntityException("Запись не найдена");

            medOrg.Code = code;
            medOrg.Name = name;
            medOrg.UpdatedDate = DateTime.Now;

            return medOrg;
        }
    }
}
