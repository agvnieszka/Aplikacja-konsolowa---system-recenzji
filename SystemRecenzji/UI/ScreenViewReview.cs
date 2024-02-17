using System.Runtime.CompilerServices;
using SystemRecenzji.Models;

namespace SystemRecenzji.UI
{
    internal class ScreenViewReview : UserModeScreen
    {
        int sel = 0;
        Movie? m;
        Review r;
        public ScreenViewReview(ScreenViewer caller, User user, Review review) : base(caller, user)
        {
            this.r = review;
            var mvs = from mv in caller.dbConnection.dbMovies
                      where mv.id == r.movie_id
                      select mv;
            m = mvs.Any() ? mvs.First() : null;
            r = review;
        }

        public override void Draw()
        {
            Console.WriteLine($" {(r.user_id == user.id ? "Twoja r" : "R")}ecenzja filmu {(m != null ? m.name : "<???>")} #{r.id}");
            Console.WriteLine("----------------");
            Console.WriteLine($"Ocena: {r.rating}/10");
            Console.WriteLine(r.desc);

            if (r.user_id == user.id || user.isAdmin)
            {
                Console.WriteLine((sel == 0 ? ">" : " ") + " Edytuj");
                Console.WriteLine((sel == 1 ? ">" : " ") + " Usuń");
            }
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
                    if (r.user_id == user.id || user.isAdmin) {
                        switch (sel)
                        {
                            case 0:
                                caller.AddToStack(new ScreenAddReview(caller, user, m, r));
                                break;
                            case 1:
                                caller.dbConnection.dbReviews.Remove(r);
                                caller.dbConnection.SaveChanges();
                                caller.RemoveFromStack(this);
                                break;
                        }
                    }
                    break;
            }
        }
    }
}