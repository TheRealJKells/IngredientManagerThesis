using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TabbedAppThesis.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TabbedAppThesis.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ResultsPage : ContentPage
	{
        ItemsViewModel viewModel;
        List<String> ingredientList;
        public ResultsPage(List<String> myList)
        {
            InitializeComponent();
            ingredientList = myList;
            BindingContext = viewModel = new ItemsViewModel();

            for (int i = 0; i < ingredientList.Count; i++)
            {
                Label myLabel = new Label
                {
                    Text = ingredientList.ElementAt(i)
                };
                //ingredientStack.Children.Add(myLabel);
            }

        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            //if (!(args.SelectedItem is Recipe item))
            //    return;

            //await Navigation.PushAsync(new ItemDetailPage(new ItemDetailViewModel(item)));

            // Manually deselect item.
            //ItemsListView.SelectedItem = null;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            //if (viewModel.Recipes.Count == 0)
            //    viewModel.LoadItemsCommand.Execute(null);
        }
    }
    
}