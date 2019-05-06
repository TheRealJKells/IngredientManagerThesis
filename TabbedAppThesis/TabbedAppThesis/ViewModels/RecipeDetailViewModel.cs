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
        public string AuthorName { get; set; }
        public string TimeToMakeMinutes { get; set; }
        public RecipeDetailViewModel(Recipe recipe = null)
        {
            AuthorName = "Author: " + App.SessionUser.Username;
            TimeToMakeMinutes = recipe?.TimeToMake + " minutes";
            Title = recipe?.Name;
            Recipe = recipe;
        }

        public bool IsInCollection
        {
           
            get
            {
                bool retVal = false;
                if (App.SessionUser.RecipesUsed.Contains(Recipe.ID))
                {
                    retVal = true;
                }

                return retVal;
            }
        }

        public bool AddToCollection
        {
            get
            {
                bool retVal = false;
                if (!App.SessionUser.RecipesUsed.Contains(Recipe.ID))
                {
                    retVal = true;
                }

                return retVal;
            }
        }

        public string IsVegan
        {
            get
            {
                string mystring = string.Empty;
                if (Recipe.IsVegan == true)
                    mystring = "Yes";
                else
                    mystring = "No";
                return mystring;
            }
        }
        public string IsVegetarian
        {
            get
            {
                string mystring = string.Empty;
                if (Recipe.IsVegetarian == true)
                    mystring = "Yes";
                else
                    mystring = "No";

                return mystring;
            }
        }
        public string StringIngredientList
        {
           
            get
            {
                int count = 1;
                string mystring = string.Empty;
                foreach (string s in Recipe.IngredientList)
                {
                    mystring = mystring + s;
                    if (count != Recipe.IngredientList.Count)
                    {
                        mystring = mystring + ", ";
                    }
                    count++;
                }
                return mystring;
            }
        }
    }
}
