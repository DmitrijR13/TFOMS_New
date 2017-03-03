using System;
using FPCS.Data.Enums;
using System.Collections.Generic;

namespace FPCS.Data.Entities
{
	public class WayOfAddressing : BaseEntity
	{
		/// <summary>
		/// Id вида обращения
		/// </summary>
		public Int64 WayOfAddressingId { get; set; }
	
		/// <summary>
		/// Код вида обращения
		/// </summary>
		public String Code { get; set; }

		/// <summary>
		/// Наименование вида обращения
		/// </summary>
		public String Name { get; set; }

        public virtual ICollection<JournalAppeal> JournalAppeals { get; set; }
    }
}
