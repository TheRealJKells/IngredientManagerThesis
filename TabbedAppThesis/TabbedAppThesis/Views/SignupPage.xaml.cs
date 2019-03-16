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
                RecipesCreated = new List<int>(),
                RecipesUsed = new List<int>()
            };

            // Sign up logic goes here

            var signUpSucceeded = AreDetailsValid(user);
            if (signUpSucceeded)
            {
                App.LiteDB.Adduser(user);
                await Navigation.PushModalAsync(new NavigationPage(new LoginPage()));
            }
            else if (isUserTaken)
            {
                messageLabel.Text = "That username is already taken";
            }
            else if (isEmailTaken)
            {
                messageLabel.Text = "That email is already being used";
            }
            else
                messageLabel.Text = "Login failed";
        }

        bool AreDetailsValid(User user)
        {
            isEmailTaken = false;
            if (!App.LiteDB.GetUserByEmail(user))
            {
                isEmailTaken = true;
            }
            isUserTaken = false;

            if (!App.LiteDB.getUserByUsername(user.Username))
            {
                isEmailTaken = true;
            }
            return (!string.IsNullOrWhiteSpace(user.Username) 
                && !string.IsNullOrWhiteSpace(user.Password) 
                && !string.IsNullOrWhiteSpace(user.Email) 
                && user.Email.Contains("@"))
                && !isUserTaken
                &&!isEmailTaken;
        }
    }
}