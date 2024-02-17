using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemRecenzji.Models;

namespace SystemRecenzji.UI
{
    public  class ScreenViewer
    {
        public DB dbConnection = new DB();
        private List<Screen> screenStack = [];

        public void AddToStack(Screen screen)
        {
            screenStack.Add(screen);
        }
        public void RemoveFromStack(Screen screen)
        {
            screenStack.Remove(screen);
        }

        public void RunUntilDone()
        {
            while (screenStack.Count != 0)
            {
                Screen a = screenStack.Last();
                Console.Clear();
                a.Draw();
                a.TakeInput(Console.ReadKey());
            }
        }
    }
}
