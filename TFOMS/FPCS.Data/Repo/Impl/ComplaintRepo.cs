using System;
using System.Linq;

using FPCS.Data.Entities;
using FPCS.Data.Exceptions;
using FPCS.Data.Enums;

namespace FPCS.Data.Repo.Impl
{
    internal class ComplaintRepo : RepoBase<Complaint, StudentManagementContext>, IComplaintRepo
    {
        public ComplaintRepo(UnitOfWork<StudentManagementContext> unitOfWork) : base(unitOfWork) { }

        #region override methods

        public override IQueryable<Complaint> GetAll()
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
        public Complaint Add(String code, String name)
        {
            var complaint =
                Add(
                new Complaint
                {
                    Code = code,
                    Name = name,
                    UpdatedDate = DateTime.Now,
                    CreatedDate = DateTime.Now
                });

            return complaint;
        }

        /// <summary>
        /// Редактирование источника поступления
        /// </summary>
        /// <param name="complaintId">ID записи</param>
        /// <param name="code">Код записи</param>
        /// <param name="name">Наименование записи</param>
        /// <returns></returns>
        public Complaint Update(Int64 complaintId, String code, String name)
        {
            var complaint = this.Get(complaintId);
            if (complaint == null) throw new NotFoundEntityException("Запись не найдена");

            complaint.Code = code;
            complaint.Name = name;
            complaint.UpdatedDate = DateTime.Now;

            return complaint;
        }
    }
}
