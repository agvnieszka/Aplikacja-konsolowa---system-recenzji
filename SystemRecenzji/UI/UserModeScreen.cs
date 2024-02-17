using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemRecenzji.Models;

namespace SystemRecenzji.UI
{
    public abstract class UserModeScreen : Screen
    {
        protected User user;
        public UserModeScreen(ScreenViewer caller, User user) : base(caller)
        {
            this.user = user;
        }
    }
}
