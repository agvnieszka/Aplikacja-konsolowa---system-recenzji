using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemRecenzji.UI
{
    public class ScreenStart(ScreenViewer caller) : Screen(caller)
    {
        int sel = 0;

        public override void Draw()
        {
            Console.WriteLine(" SYSTEM RECENZJI");
            Console.WriteLine("----------------");
            Console.WriteLine((sel == 0 ? ">" : " ") + " Zaloguj");
            Console.WriteLine((sel == 1 ? ">" : " ") + " Zarejestruj");
        }

        public override void TakeInput(ConsoleKeyInfo consoleKeyInfo)
        {
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
                            caller.AddToStack(new ScreenLogin(caller));
                            break;
                        case 1:
                            caller.AddToStack(new ScreenRegister(caller));
                            break;
                    }
                    break;
            }
        }
    }
}
