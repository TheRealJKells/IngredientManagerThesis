using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TabbedAppThesis.Models;
using TabbedAppThesis.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TabbedAppThesis.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UsedRecipesPage : ContentPage
    {

        RecipesViewModel viewmodel;

        public UsedRecipesPage()
        {
            InitializeComponent();

            BindingContext = viewmodel = new RecipesViewModel();
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

        private void Button_Clicked_Delete(object sender, EventArgs e)
        {
            var vm = BindingContext as RecipesViewModel;
            var button = sender as Button;
            var recipe = button.BindingContext as Recipe;

            vm.RemoveCommand.Execute(recipe);

            //App.LiteDB.DeleteRecipe(recipe.ID);

            for (int i = 0; i < App.SessionUser.RecipesCreated.Count; i++)
            {
                if (App.SessionUser.RecipesUsed.ElementAt(i) == recipe.ID)
                {
                    App.SessionUser.RecipesUsed.RemoveAt(i);
                    break;
                }
            }
            App.LiteDB.UpdateUser(App.SessionUser);
            OnAppearing();

        }

        protected override void OnAppearing()
        {
            viewmodel.Recipes.Clear();
            viewmodel.ExecuteLoadRecipesCollection();
            noRecipes.IsVisible = viewmodel.Recipes.Count == 0 ? true : false;
            base.OnAppearing();
        }
    }
}