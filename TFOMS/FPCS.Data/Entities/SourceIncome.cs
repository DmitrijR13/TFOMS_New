using System;
using FPCS.Data.Enums;
using System.Collections.Generic;

namespace FPCS.Data.Entities
{
	public class SourceIncome :BaseEntity
	{
		/// <summary>
		/// Id Источника поступления
		/// </summary>
		public Int64 SourceIncomeId { get; set; }
	
		/// <summary>
		/// Код источника поступления
		/// </summary>
		public String Code { get; set; }

		/// <summary>
		/// Наименование источника поступления
		/// </summary>
		public String Name { get; set; }

        public virtual ICollection<JournalAppeal> JournalAppeals { get; set; }
    }
}
