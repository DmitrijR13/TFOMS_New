using System;
using System.Linq;

using FPCS.Data.Entities;
using FPCS.Data.Exceptions;
using FPCS.Data.Enums;

namespace FPCS.Data.Repo.Impl
{
    internal class TakingAppealLineRepo : RepoBase<TakingAppealLine, StudentManagementContext>, ITakingAppealLineRepo
    {
        public TakingAppealLineRepo(UnitOfWork<StudentManagementContext> unitOfWork) : base(unitOfWork) { }

        #region override methods

        public override IQueryable<TakingAppealLine> GetAll()
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
        public TakingAppealLine Add(String code, String name)
        {
            var takingAppealLine =
                Add(
                new TakingAppealLine
                {
                    Code = code,
                    Name = name,
                    UpdatedDate = DateTime.Now,
                    CreatedDate = DateTime.Now
                });

            return takingAppealLine;
        }

        /// <summary>
        /// Редактирование источника поступления
        /// </summary>
        /// <param name="takingAppealLineId">ID записи</param>
        /// <param name="code">Код записи</param>
        /// <param name="name">Наименование записи</param>
        /// <returns></returns>
        public TakingAppealLine Update(Int64 takingAppealLineId, String code, String name)
        {
            var takingAppealLine = this.Get(takingAppealLineId);
            if (takingAppealLine == null) throw new NotFoundEntityException("Запись не найдена");

            takingAppealLine.Code = code;
            takingAppealLine.Name = name;
            takingAppealLine.UpdatedDate = DateTimeOffset.Now;

            return takingAppealLine;
        }
    }
}
