using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WcfService1.Core
{
    public static class Configuration
    {
        /// <summary>
        /// Соль
        /// </summary>
        public static String Salt
        {
            get
            {
                return "testsalt";
            }
        }

        /// <summary>
        /// Текущий день
        /// </summary>
        public static String CurrentDay
        {
            get
            {
                return DateTime.Now.ToString("dd");
            }
        }
    }
}