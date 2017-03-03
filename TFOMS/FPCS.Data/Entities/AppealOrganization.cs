using System;
using FPCS.Data.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace FPCS.Data.Entities
{
	public class AppealOrganization : BaseEntity
	{
		/// <summary>
		/// Id организации, ответственной за работу с обращением
		/// </summary>
		public Int64 AppealOrganizationId { get; set; }

		/// <summary>
		/// Код организации, ответственной за работу с обращением
		/// </summary>
		public String Code { get; set; }

		/// <summary>
		/// Наименование организации, ответственной за работу с обращением
		/// </summary>
		public String Name { get; set; }


		public virtual ICollection<JournalAppeal> JournalAppeals { get; set; }
	}
}
