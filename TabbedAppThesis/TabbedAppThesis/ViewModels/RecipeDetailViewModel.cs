using System;
using System.Collections.Generic;
using System.Text;
using TabbedAppThesis.Models;
using Xamarin.Forms;

namespace TabbedAppThesis.ViewModels
{
    public class RecipeDetailViewModel : BaseViewModel
    {
        public Recipe Recipe { get; set; }
        public RecipeDetailViewModel(Recipe recipe = null)
        {
            Title = recipe?.Name;
            Recipe = recipe;
        }

        public Command updateCommand
        {
            get
            {
                return new Command(() =>
                {
                    App.LiteDB.UpdateRecipe(Recipe);
                });
            }
        }
    }
}
