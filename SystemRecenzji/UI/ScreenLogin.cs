using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using SystemRecenzji.Models;

namespace SystemRecenzji.UI
{
    public class ScreenLogin(ScreenViewer caller) : Screen(caller)
    {
        int sel = 0;
        string inputLogin = "";
        string inputPassword = "";

        public override void Draw()
        {
            Console.WriteLine(" LOGOWANIE");
            Console.WriteLine("----------------");
            Console.WriteLine((sel == 0 ? ">" : " ") + " Login: " + inputLogin);
            Console.WriteLine((sel == 1 ? ">" : " ") + " Hasło: " + inputPassword);
            Console.WriteLine((sel == 2 ? ">" : " ") + " Zaloguj");
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
                case ConsoleKey.Enter:
                    switch (sel)
                    {
                        case 2:
                            if (inputLogin.Count() != 0 && inputPassword.Count() != 0)
                            {
                                var loginUsers = (from x in caller.dbConnection.dbUsers
                                 where x.username == inputLogin && x.password == inputPassword
                                 select x);
                                if (loginUsers.Any())
                                {
                                    caller.RemoveFromStack(this);
                                    caller.AddToStack(new ScreenUserStart(caller, loginUsers.First()));
                                }
                                
                            }
                            break;
                    }
                    break;
                case ConsoleKey.Backspace:
                    switch (sel)
                    {
                        case 0:
                            if (inputLogin.Any())
                            {
                                inputLogin = inputLogin.Remove(inputLogin.Length - 1);
                            }
                            break;
                        case 1:
                            if (inputPassword.Any())
                            {
                                inputPassword = inputPassword.Remove(inputPassword.Length - 1);
                            }
                            break;
                    }
                    break;
                default:
                    switch (sel)
                    {
                        case 0:
                            inputLogin += consoleKeyInfo.KeyChar;
                            break;
                        case 1:
                            inputPassword += consoleKeyInfo.KeyChar;
                            break;
                    }
                    break;
            }
        }
    }
}
