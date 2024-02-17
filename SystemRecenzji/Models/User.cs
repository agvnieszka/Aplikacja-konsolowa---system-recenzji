using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemRecenzji.Models
{
    public class User : TableModel
    {
        public static int nextID = 0;

        public int id;
        public string username;
        public string password;
        public bool isAdmin = false;

        public override string GetCSVLine()
        {
            return $"{id},{Util.CleanStringForCSV(username)},{Util.CleanStringForCSV(password)},{(isAdmin ? 1 : 0)}";
        }

        public override TableModel ParseCSVLine(string a)
        {
            string[] spl = a.Split(",");
            if (nextID <= int.Parse(spl[0]))
            {
                nextID = int.Parse(spl[0]) + 1;
            }
            return new User
            {
                id = int.Parse(spl[0]),
                username = Util.UncleanStringForCSV(spl[1]),
                password = Util.UncleanStringForCSV(spl[2]),
                isAdmin = spl[3] == "1" ? true : false
            };
        }
    }
}
