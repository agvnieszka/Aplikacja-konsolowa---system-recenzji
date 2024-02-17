using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemRecenzji.Models;

namespace SystemRecenzji.UI
{
    public class ScreenRegister(ScreenViewer caller) : Screen(caller)
    {
        int sel = 0;
        string inputLogin = "";
        string inputPassword = "";

        public override void Draw()
        {
            Console.WriteLine(" REJESTRACJA");
            Console.WriteLine("----------------");
            Console.WriteLine((sel == 0 ? ">" : " ") + " Login: " + inputLogin);
            Console.WriteLine((sel == 1 ? ">" : " ") + " Hasło: " + inputPassword);
            Console.WriteLine((sel == 2 ? ">" : " ") + " Zarejestruj");
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
                                caller.dbConnection.dbUsers.Add(new Models.User
                                {
                                    id = User.nextID++,
                                    username = inputLogin,
                                    password = inputPassword
                                });
                                caller.dbConnection.SaveChanges();
                                caller.RemoveFromStack(this);
                            }
                            break;
                    }
                    break;
                case ConsoleKey.Backspace:
                    switch (sel)
                    {
                        case 0:
                            if (inputLogin.Count() != 0)
                            {
                                inputLogin = inputLogin.Remove(inputLogin.Length - 1);
                            }
                            break;
                        case 1:
                            if (inputPassword.Count() != 0)
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
