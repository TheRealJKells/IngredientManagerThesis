using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using TabbedAppThesis.Models;
using TabbedAppThesis.ViewModels;

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
    }
}