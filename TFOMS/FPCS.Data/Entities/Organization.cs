using System;
using FPCS.Data.Enums;
using System.Collections.Generic;

namespace FPCS.Data.Entities
{
	public class Organization : BaseEntity
	{
		/// <summary>
		/// Id Источника поступления
		/// </summary>
		public Int64 OrganizationId { get; set; }

		/// <summary>
		/// Наименование источника поступления
		/// </summary>
		public String Name { get; set; }
    }
}
