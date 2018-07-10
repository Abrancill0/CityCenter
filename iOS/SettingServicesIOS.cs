using System;
using City_Center.Helper;
using City_Center.iOS;
using Foundation;
using UIKit;

[assembly: Xamarin.Forms.Dependency(typeof(SettingServicesIOS))]
namespace City_Center.iOS
{
    public class SettingServicesIOS : ISettingsService
    {
        public void OpenSettings()
        {
          
            NSString settingsString = UIApplication.OpenSettingsUrlString;
            NSUrl url = new NSUrl(settingsString);
            UIApplication.SharedApplication.OpenUrl(url);

        }
    }
}
