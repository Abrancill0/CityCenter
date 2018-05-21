using Android.App;
using Android.OS;

namespace City_Center.Droid
{
    [Activity(Theme = "@style/Theme.Splash", MainLauncher = true, NoHistory = true)]

    public class SplashActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            System.Threading.Thread.Sleep(100);
            StartActivity(typeof(MainActivity));
            Finish();
        }
    }
}
