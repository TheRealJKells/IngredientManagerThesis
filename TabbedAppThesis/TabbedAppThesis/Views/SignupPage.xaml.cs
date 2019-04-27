using LoginNavigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TabbedAppThesis.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SignupPage : ContentPage
	{
        private bool isEmailTaken;
        private bool isUserTaken;
		public SignupPage ()
		{
			InitializeComponent ();
		}

        async void OnSignUpButtonClicked(object sender, EventArgs e)
        {
            var user = new User()
            {
                Username = usernameEntry.Text,
                Password = passwordEntry.Text,
                Email = emailEntry.Text,
                RecipesCreated = new List<Guid>(),
                RecipesUsed = new List<Guid>()
            };

            // Sign up logic goes here

            var signUpSucceeded = AreDetailsValid(user);
            if (signUpSucceeded)
            {
                App.LiteDB.Adduser(user);
                MessageLabel.Text = string.Empty;
                await Navigation.PushModalAsync(new NavigationPage(new LoginPage()));

            }
            else if (isUserTaken)
            {
                MessageLabel.Text = "That username is already taken";
            }
            else if (isEmailTaken)
            {
                MessageLabel.Text = "That email is already being used";
            }
            else
                MessageLabel.Text = "Login failed";
        }

        bool AreDetailsValid(User user)
        {
            isEmailTaken = false;
            if (!App.LiteDB.GetUserByEmail(user))
            {
                isEmailTaken = true;
            }
            isUserTaken = false;

            if (!App.LiteDB.GetUserByUsername(user.Username))
            {
                isUserTaken = true;
            }
            return (!string.IsNullOrWhiteSpace(user.Username) 
                && !string.IsNullOrWhiteSpace(user.Password) 
                && !string.IsNullOrWhiteSpace(user.Email) 
                && user.Email.Contains("@"))
                && !isUserTaken
                && !isEmailTaken;
        }
    }
}