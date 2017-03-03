using System;

using FPCS.Data.Entities;
using System.Linq;
using FPCS.Data.Enums;

namespace FPCS.Data.Repo
{
    public interface IAppealResultRepo : IRepoBase<AppealResult>
    {
        /// <summary>
        /// Добавление нового источника поступления
        /// </summary>
        /// <param name="code">Код новой записи</param>
        /// <param name="name">Наименование новой записи</param>
        /// <returns></returns>
        AppealResult Add(String code, String name);

        /// <summary>
        /// Редактирование источника поступления
        /// </summary>
        /// <param name="appealResultId">ID записи</param>
        /// <param name="code">Код записи</param>
        /// <param name="name">Наименование записи</param>
        /// <returns></returns>
        AppealResult Update(Int64 appealResultId, String code, String name);
    }
}
