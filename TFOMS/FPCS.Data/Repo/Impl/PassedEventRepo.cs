using System;
using System.Linq;

using FPCS.Data.Entities;
using FPCS.Data.Exceptions;
using FPCS.Data.Enums;

namespace FPCS.Data.Repo.Impl
{
    internal class PassedEventRepo : RepoBase<PassedEvent, StudentManagementContext>, IPassedEventRepo
    {
        public PassedEventRepo(UnitOfWork<StudentManagementContext> unitOfWork) : base(unitOfWork) { }

        #region override methods

        public override IQueryable<PassedEvent> GetAll()
        {
            return DbSet.Where(x => !x.IsDeleted/* && x.IsShow*/);
        }

        #endregion override methods

        /// <summary>
        /// Добавление нового мероприятия
        /// </summary>
        /// <param name="code">Код новой записи</param>
        /// <param name="name">Наименование новой записи</param>
        /// <returns></returns>
        public PassedEvent Add(String code, String name)
        {
            var passedEvent =
                Add(
                new PassedEvent
                {
                    Code = code,
                    Name = name,
                    UpdatedDate = DateTime.Now,
                    CreatedDate = DateTime.Now
                });

            return passedEvent;
        }

        /// <summary>
        /// Редактирование источника поступления
        /// </summary>
        /// <param name="passedEventId">ID записи</param>
        /// <param name="code">Код записи</param>
        /// <param name="name">Наименование записи</param>
        /// <returns></returns>
        public PassedEvent Update(Int64 passedEventId, String code, String name)
        {
            var passedEvent = this.Get(passedEventId);
            if (passedEvent == null) throw new NotFoundEntityException("Запись не найдена");

            passedEvent.Code = code;
            passedEvent.Name = name;
            passedEvent.UpdatedDate = DateTime.Now;

            return passedEvent;
        }
    }
}
