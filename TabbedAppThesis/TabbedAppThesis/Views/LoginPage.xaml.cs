﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using TabbedAppThesis.Models;
using LoginNavigation;

namespace TabbedAppThesis.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LoginPage : ContentPage
	{
        public LoginPage()
        {
            InitializeComponent();
        }

        async void OnSignUpButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SignupPage());
        }

        async void OnLoginButtonClicked(object sender, EventArgs e)
        {
            IsBusy = true;
            var user = new User
            {
                Username = usernameEntry.Text,
                Password = passwordEntry.Text
            };

            var isValid = AreCredentialsCorrect(user);
            if (isValid)
            {
                App.IsUserLoggedIn = true;
                //Navigation.InsertPageBefore(new TabbedAppThesis.Views.MainPage(), this);
                //await Navigation.PopAsync();
                await Navigation.PushModalAsync(new MainPage());
                IsBusy = false;
            }
            else
            {
                messageLabel.Text = "Login failed";
                passwordEntry.Text = string.Empty;
                IsBusy = false;
            }
        }

        bool AreCredentialsCorrect(User user)
        {
            bool retVal = true;

            //get user
            User loginUser = App.LiteDB.GetUser(user);

            if (loginUser == null)
            {
                retVal = false;
            }

            //return user.Username == Constants.Username && user.Password == Constants.Password;
            return retVal;
        }
    }
}