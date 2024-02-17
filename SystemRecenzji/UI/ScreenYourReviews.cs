using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemRecenzji.Models;

namespace SystemRecenzji.UI
{

    public class ScreenYourReviews(ScreenViewer caller, User user) : ScreenListReviews(caller, user, -1, true)
    {
        
    }
}
