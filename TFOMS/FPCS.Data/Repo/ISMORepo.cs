using System;

using FPCS.Data.Entities;
using System.Linq;
using FPCS.Data.Enums;

namespace FPCS.Data.Repo
{
    public interface ISMORepo : IRepoBase<SMO>
    {
        /// <summary>
        /// Добавление новой СМО
        /// </summary>
        SMO Add(String smoCode, String kpp, String fullName, String shortName, String factAddress, String director, String filialDirector, String licenseInfo, String headSurname, String headName, String headSecondName, String headPosition);

        /// <summary>
        /// Редактирование СМО
        /// </summary>
        SMO Update(Int64 smoId, String smoCode, String kpp, String fullName, String shortName, String factAddress, String director, String filialDirector, String licenseInfo, String headSurname, String headName, String headSecondName, String headPosition);
    }
}
