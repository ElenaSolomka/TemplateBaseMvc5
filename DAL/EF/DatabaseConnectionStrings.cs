using System.Collections.Generic;

namespace DAL
{
    public class DatabaseConnectionStrings
    {
        public static string NameConnectionStringFirstDatabase
        {
            get
            {
                if (gettedConnString == null)
                    gettedConnString = ConnectionStringInApplication[1];

                return gettedConnString;
            }
        }

        private static string gettedConnString;

        /// <summary>
        /// Свойство, которое возвращает все ConnectionStrings из конфигурационного файла
        /// </summary>
        public static List<string> ConnectionStringInApplication
        {
            get
            {
                var ret = new List<string>();
                var rettt = System.Configuration.ConfigurationManager.ConnectionStrings;
                foreach (var item in rettt)
                {
                    ret.Add(item.ToString());
                }

                return ret;
            }
        }
    }



}