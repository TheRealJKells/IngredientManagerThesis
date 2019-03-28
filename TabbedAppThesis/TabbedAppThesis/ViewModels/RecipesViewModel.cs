using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using TabbedAppThesis.Models;
using TabbedAppThesis.Views;
using TabbedAppThesis.Services;
using LoginNavigation;
using Xamarin.Forms;
using System.Threading.Tasks;

namespace TabbedAppThesis.ViewModels
{
    public class RecipesViewModel : BaseViewModel
    {
        public ObservableCollection<Recipe> Recipes { get; set; }

        public RecipesViewModel()
        {
            Title = App.SessionUser.Username + "'s Recipes";
            Recipes = new ObservableCollection<Recipe>();
           
            if (App.LiteDB.GetAllRecipes().Count == 0)
            {
                var mockRecipes = new List<Recipe>
                {
                    new Recipe {Name = "Fruit salad", Description = "Yummy yummy", HowTo = "Mix fruit and salad",
                     IngredientList = new List<string> {"fruit", "salad"}, IsVegan = true, IsVegetarian = true, TimeToMake = 10},
                     new Recipe {Name = "Alex salad", Description = "Yummy yummy", HowTo = "Mix Alex and salad",
                     IngredientList = new List<string> {"Alex", "salad"}, IsVegan = true, IsVegetarian = true, TimeToMake = 12},
                };
                User user = App.LiteDB.GetUserByUsernameUser("user");
                foreach (Recipe r in mockRecipes)
                {
                    App.LiteDB.AddRecipe(r);
                }
             
                foreach(Recipe r in App.LiteDB.GetAllRecipes())
                {
                    user.RecipesCreated.Add(r.ID);
                }
                App.LiteDB.UpdateUser(user);
            } 
        }
       
        public void ExecuteLoadRecipesUser()
        {
            List<Recipe> recipes = new List<Recipe>();
            recipes = App.LiteDB.GetRecipesBySessionID();
            foreach (Recipe r in recipes)
            {
                Recipes.Add(r);
            }
        }
        public void ExecuteLoadRecipesSearch(List<string> ingredientNames)
        {
            IEnumerable<Recipe> recipes = new List<Recipe>();
            
            recipes = App.LiteDB.GetRecipesByIngredientName(ingredientNames);
            foreach (Recipe r in recipes)
            {
                Recipes.Add(r);
            }
        }
    }
}
