using System;
using FPCS.Data.Enums;
using System.Collections.Generic;

namespace FPCS.Data.Entities
{
	public class TakingAppealLine : BaseEntity
	{
		/// <summary>
		/// Id линии принятия обращения
		/// </summary>
		public Int64 TakingAppealLineId { get; set; }

		/// <summary>
		/// Код линии принятия обращения
		/// </summary>
		public String Code { get; set; }

		/// <summary>
		/// Наименование линии принятия обращения
		/// </summary>
		public String Name { get; set; }

		public virtual ICollection<JournalAppeal> JournalAppeals { get; set; }
	}
}
