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
using System.Linq;
using System.Collections;

namespace TabbedAppThesis.ViewModels
{
    public class RecipesViewModel : BaseViewModel
    {
        public ObservableCollection<Recipe> Recipes { get; set; }
        public Color SwitchColor { get; set; }
        public Color MainColor { get; set; }
        public Color Color { get; set; }
       


        public RecipesViewModel()
        {
            SwitchColor = Color.LightGray;
            Color = Color.White;
            Title = App.SessionUser.Username + "'s Recipes";
            TitleTwo = App.SessionUser.Username + "'s Saved Recipes";
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
       
        public Command<Recipe> RemoveCommand
        {
            get
            {
                return new Command<Recipe>((recipe) =>
                {
                    Recipes.Remove(recipe);
                }); 
            }
        }

        internal void ExecuteVeganAndVegetarianFilter()
        {
            ObservableCollection<Recipe> newList = new ObservableCollection<Recipe>();

            foreach (Recipe r in Recipes)
            {
                if (r.IsVegan && r.IsVegetarian)
                {
                    newList.Add(r);
                }
            }
            Recipes.Clear();
            foreach (Recipe r in newList)
            {
                Recipes.Add(r);
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

        public void ExecuteVeganFilter()
        {
            ObservableCollection<Recipe> newList = new ObservableCollection<Recipe>();

            foreach (Recipe r in Recipes)
            {
                if (r.IsVegan)
                {
                    newList.Add(r);
                }
            }
            Recipes.Clear();
            foreach (Recipe r in newList)
            {
                Recipes.Add(r);
            }
        }

        internal void ExecuteVegatarianFilter()
        {
            ObservableCollection<Recipe> newList = new ObservableCollection<Recipe>();

            foreach (Recipe r in Recipes)
            {
                if (r.IsVegetarian)
                {
                    newList.Add(r);
                }
            }
            Recipes.Clear();
            foreach (Recipe r in newList)
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

        public void ExecuteLoadRecipesCollection()
        {
            List<Guid> gui = new List<Guid>();

            IEnumerable<Recipe> recipes = new List<Recipe>();

            gui = App.SessionUser.RecipesUsed;

            List<Guid> guiTemp = new List<Guid>(gui);

            foreach (Guid i in guiTemp)
            {
                if (App.LiteDB.GetRecipesByRecipeID(i) == null)
                {
                    gui.Remove(i);
                }
            }
            App.SessionUser.RecipesUsed = gui;
            App.LiteDB.UpdateUser(App.SessionUser);

            recipes = App.LiteDB.GetRecipesBySessionCollection();
            for(int i = 0; i < recipes.Count(); i++)
            {
                    Recipes.Add(recipes.ElementAt(i));
               
            }
        }
    }
}
