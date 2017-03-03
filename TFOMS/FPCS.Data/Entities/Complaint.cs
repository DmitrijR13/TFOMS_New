using System;
using FPCS.Data.Enums;
using System.Collections.Generic;

namespace FPCS.Data.Entities
{
	public class Complaint : BaseEntity
	{
		/// <summary>
		/// Id жалобы
		/// </summary>
		public Int64 ComplaintId { get; set; }

		/// <summary>
		/// Код жалобы
		/// </summary>
		public String Code { get; set; }

		/// <summary>
		/// Наименование жалобы
		/// </summary>
		public String Name { get; set; }

        public virtual ICollection<JournalAppeal> JournalAppeals { get; set; }
    }
}
