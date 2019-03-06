using System;
using TabbedAppThesis.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TabbedAppThesis.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : TabbedPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        async private void Logout_Clicked(object sender, EventArgs e)
        {
            App.IsUserLoggedIn = false;
            await Navigation.PushModalAsync(new NavigationPage(new LoginPage()));
        }
    }
}