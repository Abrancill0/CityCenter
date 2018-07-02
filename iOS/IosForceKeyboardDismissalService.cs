using System;
using City_Center.Helper;
using City_Center.iOS;
using MonoTouch.Dialog;
using UIKit;

[assembly: Xamarin.Forms.Dependency(typeof(IosForceKeyboardDismissalService))]
namespace City_Center.iOS
{
	public class IosForceKeyboardDismissalService : IForceKeyboardDismissalService
    {
        public void DismissKeyboard()
        {
            UIApplication.SharedApplication.InvokeOnMainThread(() =>
            {
                var window = UIApplication.SharedApplication.KeyWindow;
                var vc = window.RootViewController;
                while (vc.PresentedViewController != null)
                {
                    vc = vc.PresentedViewController;
                }

                vc.View.EndEditing(true);

                //UIApplication.SharedApplication.KeyWindow.EndEditing(true);
            });

        }
    }
}
