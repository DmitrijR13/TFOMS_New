using System;
using System.Linq;

using FPCS.Data.Entities;
using FPCS.Data.Exceptions;
using FPCS.Data.Enums;

namespace FPCS.Data.Repo.Impl
{
    internal class ThemeAppealCitizensRepo : RepoBase<ThemeAppealCitizens, StudentManagementContext>, IThemeAppealCitizensRepo
    {
        public ThemeAppealCitizensRepo(UnitOfWork<StudentManagementContext> unitOfWork) : base(unitOfWork) { }

        #region override methods

        public override IQueryable<ThemeAppealCitizens> GetAll()
        {
            return DbSet.Where(x => !x.IsDeleted/* && x.IsShow*/);
        }

        #endregion override methods

        /// <summary>
        /// Добавление нового источника поступления
        /// </summary>
        /// <param name="code">Код новой записи</param>
        /// <param name="name">Наименование новой записи</param>
        /// <returns></returns>
        public ThemeAppealCitizens Add(String code, String name)
        {
            var themeAppealCitizens =
                Add(
                new ThemeAppealCitizens
                {
                    Code = code,
                    Name = name,
                    UpdatedDate = DateTime.Now,
                    CreatedDate = DateTime.Now
                });

            return themeAppealCitizens;
        }

        /// <summary>
        /// Редактирование источника поступления
        /// </summary>
        /// <param name="themeAppealCitizensId">ID записи</param>
        /// <param name="code">Код записи</param>
        /// <param name="name">Наименование записи</param>
        /// <returns></returns>
        public ThemeAppealCitizens Update(Int64 themeAppealCitizensId, String code, String name, DateTime? dateClose)
        {
            var themeAppealCitizens = this.Get(themeAppealCitizensId);
            if (themeAppealCitizens == null) throw new NotFoundEntityException("Запись не найдена");

            themeAppealCitizens.Code = code;
            themeAppealCitizens.Name = name;
			themeAppealCitizens.DateClose = dateClose;
			themeAppealCitizens.UpdatedDate = DateTime.Now;

            return themeAppealCitizens;
        }
    }
}
