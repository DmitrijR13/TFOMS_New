using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FPCS.Web.Admin.Models.Dictionary
{
    public class DictionaryDateIndexModel:DictionaryIndexModel
	{
		/// <summary>
		/// Дата закрытия записи
		/// </summary>
		public String DateClose { get; set; }
	}
}