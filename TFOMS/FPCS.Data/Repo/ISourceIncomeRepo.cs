using System;

using FPCS.Data.Entities;
using System.Linq;
using FPCS.Data.Enums;

namespace FPCS.Data.Repo
{
    public interface ISourseIncomeRepo : IRepoBase<SourceIncome>
    {
        /// <summary>
        /// Добавление нового источника поступления
        /// </summary>
        /// <param name="code">Код новой записи</param>
        /// <param name="name">Наименование новой записи</param>
        /// <returns></returns>
        SourceIncome Add(String code, String name);

        /// <summary>
        /// Редактирование источника поступления
        /// </summary>
        /// <param name="sourceIncomeId">ID записи</param>
        /// <param name="code">Код записи</param>
        /// <param name="name">Наименование записи</param>
        /// <returns></returns>
        SourceIncome Update(Int64 sourceIncomeId, String code, String name);
    }
}
