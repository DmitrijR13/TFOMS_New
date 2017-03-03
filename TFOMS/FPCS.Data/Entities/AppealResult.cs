using System;
using FPCS.Data.Enums;
using System.Collections.Generic;

namespace FPCS.Data.Entities
{
	public class AppealResult : BaseEntity
	{
		/// <summary>
		/// Id результата обращения
		/// </summary>
		public Int64 AppealResultId { get; set; }

		/// <summary>
		/// Код результата обращения
		/// </summary>
		public String Code { get; set; }

		/// <summary>
		/// Наименование результата обращения
		/// </summary>
		public String Name { get; set; }

		public virtual ICollection<JournalAppeal> JournalAppeals { get; set; }
	}
}
