using System;
using System.Collections.Generic;
using System.Text;

namespace TabbedAppThesis.ViewModels
{
    public class SearchViewModel : BaseViewModel
    {
        public string WelcomeText { get; set; }
        public SearchViewModel()
        {
            Title = "Recipe Search";
            WelcomeText = "Welcome " + App.SessionUser.Username + "!";
        }

        
        
    }
}
