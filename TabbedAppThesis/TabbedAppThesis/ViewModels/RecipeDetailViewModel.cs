using System;
using System.Collections.Generic;
using System.Text;
using TabbedAppThesis.Models;

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
    }
}
