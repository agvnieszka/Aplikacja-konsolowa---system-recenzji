using SystemRecenzji.Models;

namespace SystemRecenzji.UI
{
    public class ScreenListReviews(ScreenViewer caller, User user, int filterByMovie, bool filterByThisUser = false) : UserModeScreen(caller, user)
    {


        public List<Review> loadedReviews = (from x in caller.dbConnection.dbReviews
                                             where (!filterByThisUser || (filterByThisUser && x.user_id == user.id)) 
                                                && (filterByMovie == -1 || (filterByMovie != -1 && filterByMovie == x.movie_id))
                                             select x).ToList();

        int page = 0;
        int sel = 0;
        bool reloadNextRender = false;

        int PageCount() => (loadedReviews.Count() / 20) + 1;

        public void ReloadReviews()
        {
            loadedReviews = (from x in caller.dbConnection.dbReviews
                             where (!filterByThisUser || (filterByThisUser && x.user_id == user.id))
                                && (filterByMovie == -1 || (filterByMovie != -1 && filterByMovie == x.movie_id))
                             select x).ToList();
        }

        public override void Draw()
        {
            if (reloadNextRender)
            {
                ReloadReviews();
                reloadNextRender = false;
            }
            Console.WriteLine($" {(filterByThisUser ? "Twoje r" : "R")}ecenzje ({page + 1}/{PageCount()})");
            Console.WriteLine("----------------");
            for (int x = (page * 20); x < loadedReviews.Count() && x < (page*20)+20; x++)
            {
                Review r = loadedReviews[x];
                var mvs = from mv in caller.dbConnection.dbMovies
                          where mv.id == r.movie_id
                          select mv;
                Movie? m = mvs.Any() ? mvs.First() : null;
                Console.WriteLine((sel == (x - (page * 20)) ? ">" : " ") + $" #{r.id}: {(m != null ? m.name : "<???>")}");

            }

            Console.WriteLine("[ESC] Powrót");
        }

        public override void TakeInput(ConsoleKeyInfo consoleKeyInfo)
        {
            HandleEscQuit(consoleKeyInfo);
            int selLimit = page == (PageCount() - 1) ? loadedReviews.Count() - (page * 20) : 20;
            switch (consoleKeyInfo.Key)
            {
                case ConsoleKey.UpArrow:
                    sel = sel-- == 0 ? selLimit - 1 : sel;
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
                case ConsoleKey.Enter:
                    int reviewIdx = page * 20 + sel;
                    if (loadedReviews.Count > reviewIdx)
                    {
                        caller.AddToStack(new ScreenViewReview(caller, user, loadedReviews[reviewIdx]));
                        reloadNextRender = true;
                    }
                    break;
            }
        }
    }
}