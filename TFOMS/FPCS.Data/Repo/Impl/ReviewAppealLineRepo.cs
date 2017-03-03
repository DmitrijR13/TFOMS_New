using System;
using System.Linq;

using FPCS.Data.Entities;
using FPCS.Data.Exceptions;
using FPCS.Data.Enums;

namespace FPCS.Data.Repo.Impl
{
    internal class ReviewAppealLineRepo : RepoBase<ReviewAppealLine, StudentManagementContext>, IReviewAppealLineRepo
    {
        public ReviewAppealLineRepo(UnitOfWork<StudentManagementContext> unitOfWork) : base(unitOfWork) { }

        #region override methods

        public override IQueryable<ReviewAppealLine> GetAll()
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
        public ReviewAppealLine Add(String code, String name)
        {
            var reviewAppealLine =
                Add(
                new ReviewAppealLine
                {
                    Code = code,
                    Name = name,
                    UpdatedDate = DateTime.Now,
                    CreatedDate = DateTime.Now
                });

            return reviewAppealLine;
        }

        /// <summary>
        /// Редактирование источника поступления
        /// </summary>
        /// <param name="reviewAppealLineId">ID записи</param>
        /// <param name="code">Код записи</param>
        /// <param name="name">Наименование записи</param>
        /// <returns></returns>
        public ReviewAppealLine Update(Int64 reviewAppealLineId, String code, String name)
        {
            var reviewAppealLine = this.Get(reviewAppealLineId);
            if (reviewAppealLine == null) throw new NotFoundEntityException("Запись не найдена");

            reviewAppealLine.Code = code;
            reviewAppealLine.Name = name;
            reviewAppealLine.UpdatedDate = DateTimeOffset.Now;

            return reviewAppealLine;
        }
    }
}
