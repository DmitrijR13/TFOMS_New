using System;
using FPCS.Data.Enums;
using System.Collections.Generic;

namespace FPCS.Data.Entities
{
	public class Worker : BaseEntity
	{
		/// <summary>
		/// Id линии рассмотрения обращения
		/// </summary>
		public Int64 WorkerId { get; set; }

		/// <summary>
		/// Фамилия
		/// </summary>
		public String Surname { get; set; }

		/// <summary>
		/// Имя
		/// </summary>
		public String Name { get; set; }

        /// <summary>
		/// Отчество
		/// </summary>
		public String SecondName { get; set; }

        /// <summary>
		/// Глава
		/// </summary>
		public Boolean IsHead { get; set; }

        /// <summary>
		/// Телефон
		/// </summary>
		public String Phone { get; set; }

        public virtual ICollection<HandAppeal> HandAppeals { get; set; }
	}
}
