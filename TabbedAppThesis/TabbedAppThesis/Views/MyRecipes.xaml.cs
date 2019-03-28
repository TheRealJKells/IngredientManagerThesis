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

namespace TabbedAppThesis.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MyRecipes : ContentPage
    {
        //ItemsViewModel viewModel;
        RecipesViewModel viewModel2;

        public MyRecipes()
        {
            InitializeComponent();

            //BindingContext = viewModel = new ItemsViewModel();
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



            //if (viewModel.Items.Count == 0)
            //    viewModel.LoadItemsCommand.Execute(null);

            base.OnAppearing();


        }
    }
}