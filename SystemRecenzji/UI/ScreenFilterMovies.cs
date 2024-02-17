using SystemRecenzji.Models;

namespace SystemRecenzji.UI
{
    public class ScreenFilterMovies(ScreenViewer caller, User user, ScreenMovieList target) : UserModeScreen(caller, user)
    {
        string title = "";
        string year = "";
        string director = "";
        int genre = -1;

        int sel = 0;

        string GenreToString()
        {
            return genre == -1 ? "Wszystkie"
                : ((Genre)genre).ToString();
        }

        public override void Draw()
        {
            Console.WriteLine(" Szukaj filmów");
            Console.WriteLine("-------");
            Console.WriteLine((sel == 0 ? ">" : " ") + " Tytuł: " + title);
            Console.WriteLine((sel == 1 ? ">" : " ") + " Rok: " + year);
            Console.WriteLine((sel == 2 ? ">" : " ") + " Reżyser: " + director);
            Console.WriteLine((sel == 3 ? ">" : " ") + " Gatunek: " + GenreToString() + (sel == 3 ? "[<- / ->] zmień" : ""));
            Console.WriteLine((sel == 4 ? ">" : " ") + " Szukaj");
        }

        public override void TakeInput(ConsoleKeyInfo consoleKeyInfo)
        {
            HandleEscQuit(consoleKeyInfo);
            switch (consoleKeyInfo.Key)
            {
                case ConsoleKey.UpArrow:
                    sel = sel-- == 0 ? 4 : sel;
                    break;
                case ConsoleKey.DownArrow:
                    sel++;
                    sel %= 5;
                    break;
                case ConsoleKey.LeftArrow:
                    if (sel == 3)
                    {
                        if (genre-- == -1)
                        {
                            genre = 3;
                        }
                    }
                    break;
                case ConsoleKey.RightArrow:
                    if (sel == 3)
                    {
                        if (genre++ == 3)
                        {
                            genre = -1;
                        }
                    }
                    break;
                case ConsoleKey.Backspace:
                    switch (sel)
                    {
                        case 0:
                            if (title.Any())
                            {
                                title = title.Remove(title.Length - 1);
                            }
                            break;
                        case 1:
                            if (year.Any())
                            {
                                year = year.Remove(year.Length - 1);
                            }
                            break;
                        case 2:
                            if (director.Any())
                            {
                                director = director.Remove(director.Length - 1);
                            }
                            break;
                    }
                    break;
                case ConsoleKey.Enter:
                    if (sel == 4)
                    {
                        target.Filter(title, year, director, genre);
                        caller.RemoveFromStack(this);
                    }
                    break;
                default:
                    switch (sel)
                    {
                        case 0:
                            title += consoleKeyInfo.KeyChar;
                            break;
                        case 1:
                            year += consoleKeyInfo.KeyChar;
                            break;
                        case 2:
                            director += consoleKeyInfo.KeyChar;
                            break;
                    }
                    break;
            }
        }
    }
}