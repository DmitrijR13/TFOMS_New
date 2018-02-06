using System;

using FPCS.Data.Entities;
using System.Linq;
using FPCS.Data.Enums;

namespace FPCS.Data.Repo
{
    public interface IOrganizationRepo : IRepoBase<Organization>
    {
        /// <summary>
        /// Добавление новой организации
        /// </summary>
        /// <param name="code">Код новой записи</param>
        /// <param name="name">Наименование новой записи</param>
        /// <returns></returns>
        Organization Add(String name);

        /// <summary>
        /// Редактирование организации
        /// </summary>
        /// <param name="takingAppealLineId">ID записи</param>
        /// <param name="code">Код записи</param>
        /// <param name="name">Наименование записи</param>
        /// <returns></returns>
        Organization Update(Int64 organizationId, String name);
    }
}
