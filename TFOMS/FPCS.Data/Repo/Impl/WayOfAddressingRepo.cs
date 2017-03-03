using System;
using System.Linq;

using FPCS.Data.Entities;
using FPCS.Data.Exceptions;
using FPCS.Data.Enums;

namespace FPCS.Data.Repo.Impl
{
    internal class WayOfAddressingRepo : RepoBase<WayOfAddressing, StudentManagementContext>, IWayOfAddressingRepo
    {
        public WayOfAddressingRepo(UnitOfWork<StudentManagementContext> unitOfWork) : base(unitOfWork) { }

        #region override methods

        public override IQueryable<WayOfAddressing> GetAll()
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
        public WayOfAddressing Add(String code, String name)
        {
            var wayOfAddressing =
                Add(
                new WayOfAddressing
                {
                    Code = code,
                    Name = name,
                    UpdatedDate = DateTime.Now,
                    CreatedDate = DateTime.Now
                });

            return wayOfAddressing;
        }

        /// <summary>
        /// Редактирование источника поступления
        /// </summary>
        /// <param name="wayOfAddressingId">ID записи</param>
        /// <param name="code">Код записи</param>
        /// <param name="name">Наименование записи</param>
        /// <returns></returns>
        public WayOfAddressing Update(Int64 wayOfAddressingId, String code, String name)
        {
            var wayOfAddressing = this.Get(wayOfAddressingId);
            if (wayOfAddressing == null) throw new NotFoundEntityException("Запись не найдена");

            wayOfAddressing.Code = code;
            wayOfAddressing.Name = name;
            wayOfAddressing.UpdatedDate = DateTimeOffset.Now;

            return wayOfAddressing;
        }
    }
}
