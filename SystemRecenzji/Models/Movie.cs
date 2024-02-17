using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemRecenzji.Models
{
    public class Movie : TableModel
    {
        public int id;
        public string name;
        public string director;
        public string cast;
        public int year;
        public Genre genre;

        public override string GetCSVLine()
        {
            return $"{id},{Util.CleanStringForCSV(name)},{Util.CleanStringForCSV(director)},{Util.CleanStringForCSV(cast)},{year},{(int)genre}";
        }

        public override TableModel ParseCSVLine(string a)
        {
            string[] splt = a.Split(",");
            return new Movie
            {
                id = int.Parse(splt[0]),
                name = Util.UncleanStringForCSV(splt[1]),
                director = Util.UncleanStringForCSV(splt[2]),
                cast = Util.UncleanStringForCSV(splt[3]),
                year = int.Parse(splt[4]),
                genre = (Genre)int.Parse(splt[5])
            };
        }
    }
}
