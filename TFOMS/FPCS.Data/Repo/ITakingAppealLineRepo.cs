using System;

using FPCS.Data.Entities;
using System.Linq;
using FPCS.Data.Enums;

namespace FPCS.Data.Repo
{
    public interface ITakingAppealLineRepo : IRepoBase<TakingAppealLine>
    {
        /// <summary>
        /// Добавление нового источника поступления
        /// </summary>
        /// <param name="code">Код новой записи</param>
        /// <param name="name">Наименование новой записи</param>
        /// <returns></returns>
        TakingAppealLine Add(String code, String name);

        /// <summary>
        /// Редактирование источника поступления
        /// </summary>
        /// <param name="takingAppealLineId">ID записи</param>
        /// <param name="code">Код записи</param>
        /// <param name="name">Наименование записи</param>
        /// <returns></returns>
        TakingAppealLine Update(Int64 takingAppealLineId, String code, String name);
    }
}
