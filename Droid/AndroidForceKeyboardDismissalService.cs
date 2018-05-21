
using Android.Views.InputMethods;
using City_Center.Droid;
using City_Center.Helper;
using Plugin.CurrentActivity;

[assembly: Xamarin.Forms.Dependency(typeof(AndroidForceKeyboardDismissalService))]
namespace City_Center.Droid
{
	public class AndroidForceKeyboardDismissalService : IForceKeyboardDismissalService
    {
        public void DismissKeyboard()
        {
            InputMethodManager imm = InputMethodManager.FromContext(CrossCurrentActivity.Current.Activity.ApplicationContext);

            imm.HideSoftInputFromWindow(
                CrossCurrentActivity.Current.Activity.Window.DecorView.WindowToken, HideSoftInputFlags.NotAlways);
        }
    }
}
