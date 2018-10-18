using System;
using City_Center.Droid;
using City_Center.Helper;

[assembly: Xamarin.Forms.Dependency(typeof(SettingsServiceAndroid))]
namespace City_Center.Droid
{
    public class SettingsServiceAndroid : ISettingsService
    {
        public void OpenSettings()
        {
            Xamarin.Forms.Forms.Context.StartActivity(new Android.Content.Intent(Android.Provider.Settings.ActionLocat‌​ionSourceSettings));
        }
    }
}
