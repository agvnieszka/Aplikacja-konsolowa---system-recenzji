using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemRecenzji.Models;

namespace SystemRecenzji.UI
{
    public class ScreenUserStart(ScreenViewer caller, User user) : UserModeScreen(caller, user)
    {
        int sel = 0;

        public override void Draw()
        {
            Console.WriteLine(" SYSTEM RECENZJI");
            Console.WriteLine("----------------");
            Console.WriteLine((sel == 0 ? ">" : " ") + " Przeglądaj filmy ");
            Console.WriteLine((sel == 1 ? ">" : " ") + " Zarządaj swoimi recenzjami ");
            Console.WriteLine((sel == 2 ? ">" : " ") + " Wyloguj");
        }

        public override void TakeInput(ConsoleKeyInfo consoleKeyInfo)
        {
            switch (consoleKeyInfo.Key)
            {
                case ConsoleKey.UpArrow:
                    sel = sel-- == 0 ? 2 : sel;
                    break;
                case ConsoleKey.DownArrow:
                    sel++;
                    sel %= 3;
                    break;
                case ConsoleKey.Enter:
                    switch (sel)
                    {
                        case 0:
                            caller.AddToStack(new ScreenMovieList(caller, user));
                            break;
                        case 1:
                            caller.AddToStack(new ScreenYourReviews(caller, user));
                            break;
                        case 2:
                            caller.RemoveFromStack(this);
                            break;
                    }
                    break;
            }
        }
    }
}
