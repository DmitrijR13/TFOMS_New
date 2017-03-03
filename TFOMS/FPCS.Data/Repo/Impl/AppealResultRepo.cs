using System;
using System.Linq;

using FPCS.Data.Entities;
using FPCS.Data.Exceptions;
using FPCS.Data.Enums;

namespace FPCS.Data.Repo.Impl
{
    internal class AppealResultRepo : RepoBase<AppealResult, StudentManagementContext>, IAppealResultRepo
    {
        public AppealResultRepo(UnitOfWork<StudentManagementContext> unitOfWork) : base(unitOfWork) { }

        #region override methods

        public override IQueryable<AppealResult> GetAll()
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
        public AppealResult Add(String code, String name)
        {
            var appealResult =
                Add(
                new AppealResult
                {
                    Code = code,
                    Name = name,
                    UpdatedDate = DateTime.Now,
                    CreatedDate = DateTime.Now
                });

            return appealResult;
        }

        /// <summary>
        /// Редактирование источника поступления
        /// </summary>
        /// <param name="appealResultId">ID записи</param>
        /// <param name="code">Код записи</param>
        /// <param name="name">Наименование записи</param>
        /// <returns></returns>
        public AppealResult Update(Int64 appealResultId, String code, String name)
        {
            var appealResult = this.Get(appealResultId);
            if (appealResult == null) throw new NotFoundEntityException("Запись не найдена");

            appealResult.Code = code;
            appealResult.Name = name;
            appealResult.UpdatedDate = DateTimeOffset.Now;

            return appealResult;
        }
    }
}
