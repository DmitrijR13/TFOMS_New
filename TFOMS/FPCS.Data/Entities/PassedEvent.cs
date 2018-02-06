using System;
using FPCS.Data.Enums;
using System.Collections.Generic;

namespace FPCS.Data.Entities
{
	public class PassedEvent : BaseEntity
	{
		/// <summary>
		/// Id Мероприятия
		/// </summary>
		public Int64 PassedEventId { get; set; }
	
		/// <summary>
		/// Код мероприятия
		/// </summary>
		public String Code { get; set; }

		/// <summary>
		/// Наименование мероприятия
		/// </summary>
		public String Name { get; set; }

        public virtual ICollection<HandAppeal> HandAppeals { get; set; }
    }
}
