using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Foundation;
using ObjCRuntime;
using TabbedAppThesis.iOS;
using TabbedAppThesis.Services;
using TabbedAppThesis.Models;

[assembly: Xamarin.Forms.Dependency(typeof(DatabaseAccess))]
namespace TabbedAppThesis.iOS
{
    public class DatabaseAccess : IDatabaseAccess
    {
        public string DatabasePath()
        {
            string docFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string libFolder = Path.Combine(docFolder, "..", "Library", "Databases");

            if (!Directory.Exists(libFolder))
            {
                Directory.CreateDirectory(libFolder);
            }

            return Path.Combine(libFolder, LoginNavigation.Constants.OFFLINE_DATABASE_NAME);

        }
    }
}