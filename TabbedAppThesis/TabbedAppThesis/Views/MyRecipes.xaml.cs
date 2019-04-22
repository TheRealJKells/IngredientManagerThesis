using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using TabbedAppThesis.Models;
using TabbedAppThesis.Views;
using TabbedAppThesis.ViewModels;
using LoginNavigation;

namespace TabbedAppThesis.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MyRecipes : ContentPage
    {
        RecipesViewModel viewModel2;

        public MyRecipes()
        {
            InitializeComponent();

            BindingContext = viewModel2 = new RecipesViewModel();
           
            
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            if (!(args.SelectedItem is Recipe recipe))
            {
                return;
            }

            await Navigation.PushAsync(new ItemDetailPage(new RecipeDetailViewModel(recipe)));

            // Manually deselect item.
            ItemsListView.SelectedItem = null;
        }

        async void AddItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new NewItemPage()));
        }

        protected override void OnAppearing()
        {

            viewModel2.Recipes.Clear();
            viewModel2.ExecuteLoadRecipesUser();
            noRecipes.IsVisible = viewModel2.Recipes.Count == 0 ? true : false;
            base.OnAppearing();
        }

        private void Button_Clicked_Delete(object sender, EventArgs e)
        {
            var vm = BindingContext as RecipesViewModel;
            var button = sender as Button;
            var recipe = button.BindingContext as Recipe;

            vm.RemoveCommand.Execute(recipe);
          
            App.LiteDB.DeleteRecipe(recipe.ID);

            for (int i = 0; i < App.SessionUser.RecipesCreated.Count; i++)
            {
                if (App.SessionUser.RecipesCreated.ElementAt(i) == recipe.ID)
                {
                    App.SessionUser.RecipesCreated.RemoveAt(i);
                    break;
                }
            }
            App.LiteDB.UpdateUser(App.SessionUser);
            OnAppearing();
           
        }

        private async void Button_Clicked_Edit(object sender, EventArgs e)
        {
            var vm = BindingContext as RecipesViewModel;
            var button = sender as Button;
            var recipe = button.BindingContext as Recipe;
            await Navigation.PushAsync(new EditRecipePage(new RecipeDetailViewModel(recipe)));
            
        }
    }
}