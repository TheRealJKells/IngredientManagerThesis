using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using TabbedAppThesis.Models;
using TabbedAppThesis.ViewModels;
using System.Collections.Generic;

namespace TabbedAppThesis.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ItemDetailPage : ContentPage
    {
        RecipeDetailViewModel viewModel;

        public ItemDetailPage(RecipeDetailViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = this.viewModel = viewModel;
        }

        public ItemDetailPage()
        {
            InitializeComponent();

            var recipe = new Recipe
            {
                Name = "Item 1",
                Description = "This is an item description."
            };

            viewModel = new RecipeDetailViewModel(recipe);
            BindingContext = viewModel;
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            List<Guid> recipes = new List<Guid>();

            foreach(Guid i in App.SessionUser.RecipesUsed)
            {
                recipes.Add(i);
            }
            recipes.Add(viewModel.Recipe.ID);
            App.SessionUser.RecipesUsed = recipes;
            App.LiteDB.UpdateUser(App.SessionUser);
            NotInCollectionButton.IsVisible = false;
            NotInCollectionLabel.IsVisible = false;
            InCollectionLabal.IsVisible = true;

        }
    }
}