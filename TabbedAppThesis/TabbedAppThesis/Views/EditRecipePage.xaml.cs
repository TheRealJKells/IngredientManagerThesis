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
    public partial class EditRecipePage : ContentPage
    {
        RecipeDetailViewModel viewModel;

        public EditRecipePage(RecipeDetailViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = this.viewModel = viewModel;
        }

        public EditRecipePage()
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

        private async void Save_Clicked(object sender, EventArgs e)
        {
            string eachIngredient = string.Empty;
            List<string> tempIngredientList = new List<string>();
            if (myIngredientList.Text != null)
            {
                foreach (char c in myIngredientList.Text)
                {
                    if (c == ',')
                    {
                        tempIngredientList.Add(eachIngredient);
                        eachIngredient = string.Empty;
                    }
                    else if (c == ' ')
                    {
                        continue;
                    }
                    else
                        eachIngredient = eachIngredient + c;
                }
                if (eachIngredient != string.Empty)
                {
                    tempIngredientList.Add(eachIngredient);
                }
            }
            Recipe recipe = viewModel.Recipe as Recipe;
            if (myIngredientList.Text != null)
            {
                recipe.IngredientList = tempIngredientList;
            }
            App.LiteDB.UpdateRecipe(recipe);
            await Navigation.PopAsync();


        }
    }
}