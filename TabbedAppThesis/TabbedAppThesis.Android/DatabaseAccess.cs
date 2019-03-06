using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using TabbedAppThesis.Services;
using TabbedAppThesis.Models;
using LoginNavigation;
using System.IO;

namespace TabbedAppThesis.Droid
{
    class DatabaseAccess : IDatabaseAccess
    {
        public string DatabasePath()
        {
            var path = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), Constants.OFFLINE_DATABASE_NAME);

            if (!File.Exists(path))
            {
                File.Create(path).Dispose();
            }

            return path;
        }
    }
}