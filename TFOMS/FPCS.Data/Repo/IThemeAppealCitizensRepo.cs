using System;

using FPCS.Data.Entities;
using System.Linq;
using FPCS.Data.Enums;

namespace FPCS.Data.Repo
{
    public interface IThemeAppealCitizensRepo : IRepoBase<ThemeAppealCitizens>
    {
        /// <summary>
        /// Добавление новой темы обращения
        /// </summary>
        /// <param name="code">Код новой записи</param>
        /// <param name="name">Наименование новой записи</param>
        /// <returns></returns>
        ThemeAppealCitizens Add(String code, String name);

        /// <summary>
        /// Редактирование темы обращения
        /// </summary>
        /// <param name="themeAppealCitizensId">ID записи</param>
        /// <param name="code">Код записи</param>
        /// <param name="name">Наименование записи</param>
        /// <returns></returns>
        ThemeAppealCitizens Update(Int64 wayOfAddressingId, String code, String name, DateTime? dateClose);
    }
}
