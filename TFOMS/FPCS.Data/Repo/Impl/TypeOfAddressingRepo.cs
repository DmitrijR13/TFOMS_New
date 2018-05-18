using System;
using System.Linq;

using FPCS.Data.Entities;
using FPCS.Data.Exceptions;
using FPCS.Data.Enums;

namespace FPCS.Data.Repo.Impl
{
    internal class TypeOfAddressingRepo : RepoBase<TypeOfAddressing, StudentManagementContext>, ITypeOfAddressingRepo
    {
        public TypeOfAddressingRepo(UnitOfWork<StudentManagementContext> unitOfWork) : base(unitOfWork) { }

        #region override methods

        public override IQueryable<TypeOfAddressing> GetAll()
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
        public TypeOfAddressing Add(String code, String name, Boolean isUpadateDate)
        {
            var typeOfAddressing =
                Add(
                new TypeOfAddressing
                {
                    Code = code,
                    Name = name,
                    IsUpdateDateEnd = isUpadateDate,
                    UpdatedDate = DateTime.Now,
                    CreatedDate = DateTime.Now
                });

            return typeOfAddressing;
        }

        /// <summary>
        /// Редактирование источника поступления
        /// </summary>
        /// <param name="typeOfAddressingId">ID записи</param>
        /// <param name="code">Код записи</param>
        /// <param name="name">Наименование записи</param>
        /// <returns></returns>
        public TypeOfAddressing Update(Int64 typeOfAddressingId, String code, String name, Boolean isUpadateDate)
        {
            var typeOfAddressing = this.Get(typeOfAddressingId);
            if (typeOfAddressing == null) throw new NotFoundEntityException("Запись не найдена");

            typeOfAddressing.Code = code;
            typeOfAddressing.Name = name;
            typeOfAddressing.IsUpdateDateEnd = isUpadateDate;
            typeOfAddressing.UpdatedDate = DateTime.Now;

            return typeOfAddressing;
        }
    }
}
