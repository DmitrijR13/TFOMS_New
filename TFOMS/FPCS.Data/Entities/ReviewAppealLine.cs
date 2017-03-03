using System;
using FPCS.Data.Enums;
using System.Collections.Generic;

namespace FPCS.Data.Entities
{
	public class ReviewAppealLine : BaseEntity
	{
		/// <summary>
		/// Id линии рассмотрения обращения
		/// </summary>
		public Int64 ReviewAppealLineId { get; set; }

		/// <summary>
		/// Код линии рассмотрения обращения
		/// </summary>
		public String Code { get; set; }

		/// <summary>
		/// Наименование линии рассмотрения обращения
		/// </summary>
		public String Name { get; set; }

		public virtual ICollection<JournalAppeal> JournalAppeals { get; set; }
	}
}
