using System;

using FPCS.Data.Entities;
using System.Linq;
using FPCS.Data.Enums;

namespace FPCS.Data.Repo
{
    public interface IAppealOrganizationRepo : IRepoBase<AppealOrganization>
    {
        /// <summary>
        /// Добавление нового источника поступления
        /// </summary>
        /// <param name="code">Код новой записи</param>
        /// <param name="name">Наименование новой записи</param>
        /// <returns></returns>
        AppealOrganization Add(String code, String name);

        /// <summary>
        /// Редактирование источника поступления
        /// </summary>
        /// <param name="appealOrganizationId">ID записи</param>
        /// <param name="code">Код записи</param>
        /// <param name="name">Наименование записи</param>
        /// <returns></returns>
        AppealOrganization Update(Int64 appealOrganizationId, String code, String name);
    }
}
