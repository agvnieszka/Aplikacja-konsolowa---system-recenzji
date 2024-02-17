using SystemRecenzji.Models;

namespace SystemRecenzji.UI
{
    internal class ScreenViewMovie : UserModeScreen
    {

        int sel = 0;
        Movie movie;
        public ScreenViewMovie(ScreenViewer caller, User user, Movie movie) : base(caller,user)
        {
            this.movie = movie;
        }

        public override void Draw()
        {
            Console.WriteLine(movie.name);
            Console.WriteLine("-------------------");
            Console.WriteLine("Rok: " + movie.year);
            Console.WriteLine("Obsada:" + movie.cast);
            Console.WriteLine("Gatunek:" + movie.genre);
            Console.WriteLine("Reżyser: " + movie.director);
            Console.WriteLine("\n\n-------------");
            Console.WriteLine((sel == 0 ? ">" : " ") + " Przeglądaj recenzje");
            Console.WriteLine((sel == 1 ? ">" : " ") + " Dodaj recenzję");
            Console.WriteLine("[ESC] Powrót");
        }

        public override void TakeInput(ConsoleKeyInfo consoleKeyInfo)
        {
            HandleEscQuit(consoleKeyInfo);
            switch (consoleKeyInfo.Key)
            {
                case ConsoleKey.UpArrow:
                    sel = sel-- == 0 ? 1 : sel;
                    break;
                case ConsoleKey.DownArrow:
                    sel++;
                    sel %= 2;
                    break;
                case ConsoleKey.Enter:
                    switch (sel)
                    {
                        case 0:
                            caller.AddToStack(new ScreenListReviews(caller, user, movie.id));
                            break;
                        case 1:
                            caller.AddToStack(new ScreenAddReview(caller, user, movie));
                            break;
                    }
                    break;
            }
        }
    }
}