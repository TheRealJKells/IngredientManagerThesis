using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using TabbedAppThesis.Models;

namespace TabbedAppThesis.ViewModels
{
    public class MyCollectionViewModel : BaseViewModel
    {
        public ObservableCollection<Recipe> Recipes { get; set; }

        public MyCollectionViewModel()
        {
            Title = App.SessionUser.Username + "'s Collection";


        }
    }
}
