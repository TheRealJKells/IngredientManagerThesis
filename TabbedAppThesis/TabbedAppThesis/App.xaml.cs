using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using TabbedAppThesis.Views;
using TabbedAppThesis.Services;
using LiteDB;
using LoginNavigation;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace TabbedAppThesis
{
    public partial class App : Application
    {
        public static bool IsUserLoggedIn { get; internal set; }
        public static User sessionUser { get; internal set; }
        static LiteDBHelper db;
        

        public App()
        {
            InitializeComponent();

            if (!IsUserLoggedIn)
            {
                MainPage = new NavigationPage(new LoginPage());
            }
            else
            {
                MainPage = new MainPage();
            }
        }

        public static LiteDBHelper LiteDB
        {
            get
            {
                if (db == null)
                {
                    db = new LiteDBHelper(DependencyService.Get<IDatabaseAccess>().DatabasePath());
                }
                return db;
            }
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
