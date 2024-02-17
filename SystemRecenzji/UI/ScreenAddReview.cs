using SystemRecenzji.Models;

namespace SystemRecenzji.UI
{
    public class ScreenAddReview : UserModeScreen
    {
        Movie movie;
        string reviewText = "";
        int rating = 0;
        int sel = 0;
        Review? editExisting = null;
        public ScreenAddReview(ScreenViewer caller, User user, Movie movie, Review? editExisting = null) : base(caller, user)
        {
            this.movie = movie;
            this.editExisting = editExisting;
            if (editExisting != null)
            {
                reviewText = editExisting.desc;
                rating = editExisting.rating;
            }
        }

        public override void Draw()
        {
            Console.WriteLine($" {(editExisting != null ? "Edytuj" : "Dodaj")} recenzję dla filmu {movie.name}");
            Console.WriteLine("----------------");
            Console.WriteLine((sel == 0 ? ">" : " ") + " Treść:\n " + reviewText);
            Console.WriteLine((sel == 1 ? ">" : " ") + " Ocena:\n " + rating + "/10" + (sel == 1 ? "[<- / ->] Zmień" : ""));
            Console.WriteLine("---");
            Console.WriteLine((sel == 2 ? ">" : " ") + " Dodaj");
        }

        public override void TakeInput(ConsoleKeyInfo consoleKeyInfo)
        {
            HandleEscQuit(consoleKeyInfo);
            switch (consoleKeyInfo.Key)
            {
                case ConsoleKey.UpArrow:
                    sel = sel-- == 0 ? 2 : sel;
                    break;
                case ConsoleKey.DownArrow:
                    sel++;
                    sel %= 3;
                    break;
                case ConsoleKey.LeftArrow:
                    if (sel == 1)
                    {
                        rating = rating-- == 0 ? 10 : rating;
                    }
                    break;
                case ConsoleKey.RightArrow:
                    if (sel == 1)
                    {
                        rating++;
                        rating %= 11;
                    }
                    break;
                case ConsoleKey.Backspace:
                    if (sel == 0)
                    {
                        if (reviewText.Any())
                        {
                            reviewText = reviewText.Remove(reviewText.Length - 1);
                        }
                        break;
                    }
                    break;
                case ConsoleKey.Enter:
                    if (sel == 2)
                    {
                        if (editExisting != null)
                        {
                            editExisting.rating = rating;
                            editExisting.desc = reviewText;
                        }
                        else
                        {
                            Review nRev = new Review
                            {
                                id = Review.nextID++,
                                movie_id = movie.id,
                                user_id = user.id,
                                rating = rating,
                                desc = reviewText
                            };
                            caller.dbConnection.dbReviews.Add(nRev);
                        }
                        caller.dbConnection.SaveChanges();
                        caller.RemoveFromStack(this);
                    }
                    break;
                default:
                    if (sel == 0)
                    {
                        reviewText += consoleKeyInfo.KeyChar;
                    }
                    break;
            }

        }
    }
}