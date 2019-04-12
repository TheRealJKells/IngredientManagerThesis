using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using TabbedAppThesis.Models;
using LoginNavigation;

namespace TabbedAppThesis.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewItemPage : ContentPage
    {
        public Recipe Recipe { get; set; }

        public NewItemPage()
        {
            InitializeComponent();

            Recipe = new Recipe
            {

                Name = "Recipe name",
                Description = "This is a recipe description.",
                TimeToMake = 0,
                IsVegan = false,
                IsVegetarian = false,
                IngredientList = new List<string>(),
                HowTo = "Example How To"
            };

            BindingContext = this;
        }

        async void Save_Clicked(object sender, EventArgs e)
        {
            App.LiteDB.AddRecipe(Recipe);
            List<Recipe> recipes = new List<Recipe>(App.LiteDB.GetRecipesBySessionID());
            List<Guid> id = new List<Guid>();
            foreach (Recipe r in recipes)
            {
                id.Add(r.ID);
            }
            id.Add(Recipe.ID);
            //User user = new User();
            //user = App.LiteDB.GetUserByID(App.SessionUser.ID);
            App.SessionUser.RecipesCreated = id;
            App.LiteDB.UpdateUser(App.SessionUser);
            await Navigation.PopModalAsync();
        }

        async void GoBack_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}