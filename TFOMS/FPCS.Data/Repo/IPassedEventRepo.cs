using System;

using FPCS.Data.Entities;
using System.Linq;
using FPCS.Data.Enums;

namespace FPCS.Data.Repo
{
    public interface IPassedEventRepo : IRepoBase<PassedEvent>
    {
        /// <summary>
        /// Добавление нового мероприятия
        /// </summary>
        /// <param name="code">Код новой записи</param>
        /// <param name="name">Наименование новой записи</param>
        /// <returns></returns>
        PassedEvent Add(String code, String name);

        /// <summary>
        /// Редактирование мероприятия
        /// </summary>
        /// <param name="passedEventIdId">ID записи</param>
        /// <param name="code">Код записи</param>
        /// <param name="name">Наименование записи</param>
        /// <returns></returns>
        PassedEvent Update(Int64 passedEventId, String code, String name);
    }
}
