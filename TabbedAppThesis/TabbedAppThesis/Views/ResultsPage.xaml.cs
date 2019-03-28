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
                //ResultsStack.Children.Add(myLabel);
                //I am adding some more stuff
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

            //if (viewModel.Recipes.Count == 0)
            //    viewModel.LoadItemsCommand.Execute(null);
        }
    }
    
}