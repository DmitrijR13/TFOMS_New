using System;

using FPCS.Data.Entities;
using System.Linq;
using FPCS.Data.Enums;

namespace FPCS.Data.Repo
{
    public interface ITypeOfAddressingRepo : IRepoBase<TypeOfAddressing>
    {
        /// <summary>
        /// Добавление нового источника поступления
        /// </summary>
        /// <param name="code">Код новой записи</param>
        /// <param name="name">Наименование новой записи</param>
        /// <returns></returns>
        TypeOfAddressing Add(String code, String name, Boolean isUpadateDate);

        /// <summary>
        /// Редактирование источника поступления
        /// </summary>
        /// <param name="typeOfAddressingId">ID записи</param>
        /// <param name="code">Код записи</param>
        /// <param name="name">Наименование записи</param>
        /// <returns></returns>
        TypeOfAddressing Update(Int64 typeOfAddressingId, String code, String name, Boolean isUpadateDate);
    }
}
