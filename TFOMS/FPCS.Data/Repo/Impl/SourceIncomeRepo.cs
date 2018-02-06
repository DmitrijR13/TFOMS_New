using System;
using System.Linq;

using FPCS.Data.Entities;
using FPCS.Data.Exceptions;
using FPCS.Data.Enums;

namespace FPCS.Data.Repo.Impl
{
    internal class SourceIncomeRepo : RepoBase<SourceIncome, StudentManagementContext>, ISourseIncomeRepo
    {
        public SourceIncomeRepo(UnitOfWork<StudentManagementContext> unitOfWork) : base(unitOfWork) { }

        #region override methods

        public override IQueryable<SourceIncome> GetAll()
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
        public SourceIncome Add(String code, String name)
        {
            var sourceIncome =
                Add(
                new SourceIncome
                {
                    Code = code,
                    Name = name,
                    UpdatedDate = DateTime.Now,
                    CreatedDate = DateTime.Now
                });

            return sourceIncome;
        }

        /// <summary>
        /// Редактирование источника поступления
        /// </summary>
        /// <param name="sourceIncomeId">ID записи</param>
        /// <param name="code">Код записи</param>
        /// <param name="name">Наименование записи</param>
        /// <returns></returns>
        public SourceIncome Update(Int64 sourceIncomeId, String code, String name)
        {
            var sourceIncome = this.Get(sourceIncomeId);
            if (sourceIncome == null) throw new NotFoundEntityException("Запись не найдена");

            sourceIncome.Code = code;
            sourceIncome.Name = name;
            sourceIncome.UpdatedDate = DateTime.Now;

            return sourceIncome;
        }
    }
}
