using System;

using FPCS.Data.Entities;
using System.Linq;
using FPCS.Data.Enums;

namespace FPCS.Data.Repo
{
    public interface IWorkerRepo : IRepoBase<Worker>
    {
        /// <summary>
        /// Добавление нового источника поступления
        /// </summary>
        /// <param name="code">Код новой записи</param>
        /// <param name="name">Наименование новой записи</param>
        /// <returns></returns>
        Worker Add(String surname, String name, String secondName, Boolean isHead, String phone);

        /// <summary>
        /// Редактирование источника поступления
        /// </summary>
        /// <param name="reviewAppealLineId">ID записи</param>
        /// <param name="code">Код записи</param>
        /// <param name="name">Наименование записи</param>
        /// <returns></returns>
        Worker Update(Int64 workerId, String surname, String name, String secondName, Boolean isHead, String phone);
    }
}
