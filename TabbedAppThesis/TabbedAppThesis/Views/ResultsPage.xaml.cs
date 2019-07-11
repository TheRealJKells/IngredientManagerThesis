using System;
using System.Collections.Generic;
using System.Linq;
using TabbedAppThesis.Models;
using TabbedAppThesis.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TabbedAppThesis.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ResultsPage : ContentPage
	{
        //ItemsViewModel viewModel;
        RecipesViewModel viewModel2;
        List<String> ingredientList;
        public ResultsPage()
        {
            InitializeComponent();
        }

        public ResultsPage(List<String> myList)
        {
            InitializeComponent();
            ingredientList = myList;
            //BindingContext = viewModel = new ItemsViewModel();
            BindingContext = viewModel2 = new RecipesViewModel();

            for (int i = 0; i < ingredientList.Count; i++)
            {
                Label myLabel = new Label
                {
                    Text = ingredientList.ElementAt(i)
                };
            }

        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            if (!(args.SelectedItem is Recipe recipe))
                return;

            await Navigation.PushAsync(new ItemDetailPage(new RecipeDetailViewModel(recipe)));

            //Manually deselect item.
            //ItemsListView.SelectedItem = null;
        }

        protected override void OnAppearing()
        {

            base.OnAppearing();
            viewModel2.Recipes.Clear();
            viewModel2.ExecuteLoadRecipesSearch(ingredientList);
            noRecipes.IsVisible = viewModel2.Recipes.Count == 0 ? true : false;
            

            //if (viewModel.Recipes.Count == 0)
            //    viewModel.LoadItemsCommand.Execute(null);
        }

        private void Vegan_Clicked(object sender, EventArgs e)
        {
            viewModel2.Recipes.Clear();
            viewModel2.ExecuteLoadRecipesSearch(ingredientList);
            Button b = sender as Button;
            if (b.BackgroundColor == Color.LightSkyBlue)
            {
                if (Vegetarian.BackgroundColor == Color.DarkBlue)
                {
                    viewModel2.ExecuteVeganAndVegetarianFilter();
                }
                else
                {
                    viewModel2.ExecuteVeganFilter();
                }
                b.BackgroundColor = Color.DarkBlue;
                b.TextColor = Color.White;
            }
            else
            {
                if (Vegetarian.BackgroundColor == Color.DarkBlue)
                {
                    viewModel2.ExecuteVegatarianFilter();
                }
                else
                {

                }
                b.BackgroundColor = Color.LightSkyBlue;
                b.TextColor = Color.Black;

            }
        }

        private void Vegetarian_Clicked(object sender, EventArgs e)
        {
            viewModel2.Recipes.Clear();
            viewModel2.ExecuteLoadRecipesSearch(ingredientList);
            Button b = sender as Button;
            if (b.BackgroundColor == Color.LightSkyBlue)
            {
                if (Vegan.BackgroundColor == Color.DarkBlue)
                {
                    viewModel2.ExecuteVeganAndVegetarianFilter();
                }
                else
                {
                    viewModel2.ExecuteVegatarianFilter();
                }
                b.BackgroundColor = Color.DarkBlue;
                b.TextColor = Color.White;
            }
            else
            {
                if (Vegan.BackgroundColor == Color.DarkBlue)
                {
                    viewModel2.ExecuteVeganFilter();
                }
                else
                {
                   
                }
                b.BackgroundColor = Color.LightSkyBlue;
                b.TextColor = Color.Black;
            }
        }
    }
}