using System;
using FPCS.Data.Enums;
using System.Collections.Generic;

namespace FPCS.Data.Entities
{
	public class TypeOfAddressing : BaseEntity
	{
		/// <summary>
		/// Id способа обращения
		/// </summary>
		public Int64 TypeOfAddressingId { get; set; }
	
		/// <summary>
		/// Код способа обращения
		/// </summary>
		public String Code { get; set; }

		/// <summary>
		/// Наименование способа обращения
		/// </summary>
		public String Name { get; set; }

		public virtual ICollection<JournalAppeal> JournalAppeals { get; set; }
	}
}
