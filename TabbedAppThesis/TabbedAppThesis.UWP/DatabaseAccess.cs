using LoginNavigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TabbedAppThesis.Services;
using TabbedAppThesis.UWP;
using Windows.Storage;

[assembly: Xamarin.Forms.Dependency(typeof(DatabaseAccess))]
namespace TabbedAppThesis.UWP
{
    public class DatabaseAccess : IDatabaseAccess
    {
        public string DatabasePath()
        {
            return Path.Combine(ApplicationData.Current.LocalFolder.Path, Constants.OFFLINE_DATABASE_NAME);
        }
    }
}
