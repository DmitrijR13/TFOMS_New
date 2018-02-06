using System;
using System.Linq;

using FPCS.Data.Entities;
using FPCS.Data.Exceptions;
using FPCS.Data.Enums;

namespace FPCS.Data.Repo.Impl
{
    internal class WorkerRepo : RepoBase<Worker, StudentManagementContext>, IWorkerRepo
    {
        public WorkerRepo(UnitOfWork<StudentManagementContext> unitOfWork) : base(unitOfWork) { }

        #region override methods

        public override IQueryable<Worker> GetAll()
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
        public Worker Add(String surname, String name, String secondName, Boolean isHead, String phone)
        {
            var worker =
                Add(
                new Worker
                {
                    Surname = surname,
                    Name = name,
                    SecondName = secondName,
                    UpdatedDate = DateTime.Now,
                    CreatedDate = DateTime.Now,
                    IsHead = isHead,
                    Phone = phone
                });

            return worker;
        }

        /// <summary>
        /// Редактирование источника поступления
        /// </summary>
        /// <param name="reviewAppealLineId">ID записи</param>
        /// <param name="code">Код записи</param>
        /// <param name="name">Наименование записи</param>
        /// <returns></returns>
        public Worker Update(Int64 workerId, String surname, String name, String secondName, Boolean isHead, String phone)
        {
            var worker = this.Get(workerId);
            if (worker == null) throw new NotFoundEntityException("Запись не найдена");

            worker.Surname = surname;
            worker.Name = name;
            worker.SecondName = secondName;
            worker.UpdatedDate = DateTime.Now;
            worker.IsHead = isHead;
            worker.Phone = phone;

            return worker;
        }
    }
}
