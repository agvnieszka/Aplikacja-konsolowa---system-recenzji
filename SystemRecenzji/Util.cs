using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemRecenzji
{
    public class Util
    {
        public static string CleanStringForCSV(string a)
        {
            return a.Replace("\n", "\\n").Replace(",", "\\c");
        }
        public static string UncleanStringForCSV(string a)
        {
            return a.Replace("\\n", "\n").Replace("\\c", ",");
        }
    }
}
