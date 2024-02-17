using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemRecenzji.Models
{
    public class Review : TableModel
    {
        public static int nextID = 0;
        public int id;
        public int user_id;
        public int movie_id; 
        public int rating;
        public string desc;

        public override string GetCSVLine()
        {
            return $"{id},{user_id},{movie_id},{rating},{Util.CleanStringForCSV(desc)}";
        }

        public override TableModel ParseCSVLine(string a)
        {
            string[] splt = a.Split(",");
            if (nextID <= int.Parse(splt[0]))
            {
                nextID = int.Parse(splt[0]) + 1;
            }
            return new Review
            {
                id = int.Parse(splt[0]),
                user_id = int.Parse(splt[1]),
                movie_id = int.Parse(splt[2]),
                rating = int.Parse(splt[3]),
                desc = Util.UncleanStringForCSV(splt[4])
            };
        }
    }
}
