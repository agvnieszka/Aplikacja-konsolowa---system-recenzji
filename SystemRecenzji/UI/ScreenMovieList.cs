using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemRecenzji.Models;

namespace SystemRecenzji.UI
{
    public class ScreenMovieList(ScreenViewer caller, User user) : UserModeScreen(caller, user)
    {
        public List<Movie> loadedMovies = (from x in caller.dbConnection.dbMovies
                                           select x).ToList();

        int page = 0;
        int sel = 0;

        bool filtered = false;

        int PageCount() => (loadedMovies.Count() / 20) + 1;

        public void Filter(string title, string year, string director, int genre)
        {
            loadedMovies = (from x in caller.dbConnection.dbMovies
                            where x.name.Contains(title) && x.year.ToString().Contains(year) && x.director.Contains(director) 
                                && (genre == -1 || x.genre == (Genre)genre)
                            select x).ToList();
            filtered = true;
            page = 0;
            sel = 0;
        }

        public override void Draw()
        {
            Console.WriteLine($" {(filtered ? "Wyniki wyszukiwania" : "Filmy")} ({page+1}/{PageCount()})");
            Console.WriteLine("----------------");
            for (int x = (page * 20); x < loadedMovies.Count() && x < (page*20)+20; x++)
            {
                Movie mv = loadedMovies[x];
                Console.WriteLine((sel == (x - (page * 20)) ? ">" : " ") + $" {mv.name}");
            }

            Console.WriteLine("[S] Szukaj/filtruj");
            Console.WriteLine("[ESC] Powrót");
        }

        public override void TakeInput(ConsoleKeyInfo consoleKeyInfo)
        {
            HandleEscQuit(consoleKeyInfo);
            int selLimit = page == (PageCount() - 1) ? loadedMovies.Count() - ((page) * 20) : 20;
            switch (consoleKeyInfo.Key)
            {
                case ConsoleKey.UpArrow:
                    sel = sel-- == 0 ? selLimit-1 : sel;
                    break;
                case ConsoleKey.DownArrow:
                    sel++;
                    sel %= Math.Max(selLimit, 1);
                    break;
                case ConsoleKey.LeftArrow:
                    page = page-- == 0 ? PageCount() - 1 : page;
                    sel = 0;
                    break;
                case ConsoleKey.RightArrow:
                    page++;
                    page %= PageCount();
                    sel = 0;
                    break;
                case ConsoleKey.S:
                    caller.AddToStack(new ScreenFilterMovies(caller, user, this));
                    break;
                case ConsoleKey.Enter:
                    int reviewIdx = page * 20 + sel;
                    if (loadedMovies.Count > reviewIdx)
                    {
                        caller.AddToStack(new ScreenViewMovie(caller, user, loadedMovies[reviewIdx]));
                    }
                    break;
            }
        }
    }
}
