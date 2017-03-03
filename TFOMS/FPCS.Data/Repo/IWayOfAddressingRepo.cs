using System;

using FPCS.Data.Entities;
using System.Linq;
using FPCS.Data.Enums;

namespace FPCS.Data.Repo
{
    public interface IWayOfAddressingRepo : IRepoBase<WayOfAddressing>
    {
        /// <summary>
        /// Добавление нового источника поступления
        /// </summary>
        /// <param name="code">Код новой записи</param>
        /// <param name="name">Наименование новой записи</param>
        /// <returns></returns>
        WayOfAddressing Add(String code, String name);

        /// <summary>
        /// Редактирование источника поступления
        /// </summary>
        /// <param name="wayOfAddressingId">ID записи</param>
        /// <param name="code">Код записи</param>
        /// <param name="name">Наименование записи</param>
        /// <returns></returns>
        WayOfAddressing Update(Int64 wayOfAddressingId, String code, String name);
    }
}
