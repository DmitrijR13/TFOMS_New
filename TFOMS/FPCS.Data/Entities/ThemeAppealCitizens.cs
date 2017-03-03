using System;
using FPCS.Data.Enums;
using System.Collections.Generic;

namespace FPCS.Data.Entities
{
	public class ThemeAppealCitizens : BaseEntity
	{
		/// <summary>
		/// Id темы обращения граждан
		/// </summary>
		public Int64 ThemeAppealCitizensId { get; set; }

		/// <summary>
		/// Код темы обращения граждан
		/// </summary>
		public String Code { get; set; }

		/// <summary>
		/// Наименование темы обращения граждан
		/// </summary>
		public String Name { get; set; }

        public virtual ICollection<JournalAppeal> JournalAppeals { get; set; }
    }
}
