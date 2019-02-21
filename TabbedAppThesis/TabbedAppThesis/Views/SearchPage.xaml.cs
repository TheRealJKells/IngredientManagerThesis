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
    public partial class SearchPage : ContentPage
    {
        //ItemsViewModel viewModel;

        SearchViewModel viewModel;

        public SearchPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new SearchViewModel();
        }

        private void Button_Clicked_1(object sender, EventArgs e)
        {
            Entry newIngredientEntry = new Entry
            {
                Placeholder = "Enter in an ingredient"
            };
            ingredientList.Children.Add(newIngredientEntry);
        }

        private void Button_Clicked_2(object sender, EventArgs e)
        {
            if (ingredientList.Children.Count > 1)
            {
                ingredientList.Children.RemoveAt(ingredientList.Children.Count - 1);
            }
        }

        //async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        //{
        //    if (!(args.SelectedItem is Recipe item))
        //        return;

        //    await Navigation.PushAsync(new ItemDetailPage(new ItemDetailViewModel(item)));

        //    //Manually deselect item.
        //    ItemsListView.SelectedItem = null;
        //}

        async void AddItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new NewItemPage()));
        }

        //    protected override void OnAppearing()
        //    {
        //        base.OnAppearing();

        //        if (viewModel.Recipes.Count == 0)
        //            viewModel.LoadItemsCommand.Execute(null);
        //    }

        async void Button_Clicked(object sender, EventArgs e)
        {

            List<String> cIngredientList = new List<String>();


            foreach (Entry en in ingredientList.Children)
            {
                if (en.Text != null)
                {
                    cIngredientList.Add(en.Text);
                }
            }
            //DisplayAlert("Success", "Successully stored ingredients!", "Cool");


            await Navigation.PushAsync(new ResultsPage(cIngredientList));
        }
    }
}