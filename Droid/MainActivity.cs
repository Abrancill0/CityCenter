using Android.App;
using Android.OS;
using Android.Content.PM;
using Android.Gms.Auth.Api;
using Android.Gms.Auth.Api.SignIn;
using City_Center.Services.Contracts;
using Xamarin.Forms;
using Xamarin.Facebook;
using Android.Content;
using Plugin.FirebasePushNotification;
using CarouselView.FormsPlugin.Android;
using FFImageLoading.Forms.Droid;
using Xamarin;
using Acr.UserDialogs;
using Plugin.CurrentActivity;
using Plugin.Permissions;
using ImageCircle.Forms.Plugin.Droid;
//using FFImageLoading.Forms.Droid;
using Android.Widget;
using Android.Gms.Common;

namespace City_Center.Droid
{
    [Activity(Label = "City_Center.Droid", Icon = "@drawable/iconoappccr", Theme = "@style/MyTheme", MainLauncher = false,ScreenOrientation = ScreenOrientation.Portrait)]

    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

			Plugin.CurrentActivity.CrossCurrentActivity.Current.Init(this, bundle);

            Xfx.XfxControls.Init();
            UserDialogs.Init(this);

			ImageCircleRenderer.Init();

            Forms.SetFlags("FastRenderers_Experimental");

            FacebookSdk.SdkInitialize(this);
            Rg.Plugins.Popup.Popup.Init(this, bundle);
            global::Xamarin.Forms.Forms.Init(this, bundle);

            CachedImageRenderer.Init(enableFastRenderer:true);

			CarouselView.FormsPlugin.Android.CarouselViewRenderer.Init();
            
            DependencyService.Register<IGoogleManager, GoogleManager>();
            DependencyService.Register<IFacebookManager, FacebookManager>();

           // FirebaseApp.InitializeApp(this);
            FormsMaps.Init(this, bundle);
            LoadApplication(new App());

            ////FirebasePushNotificationManager.ProcessIntent(this, Intent);
            CheckForGoogleServices();
        }

        //protected override void OnNewIntent(Intent intent)
        //{
        //    base.OnNewIntent(intent);
        //    FirebasePushNotificationManager.ProcessIntent(this, intent);
        //}


        public bool CheckForGoogleServices()
        {
            int resultCode = GoogleApiAvailability.Instance.IsGooglePlayServicesAvailable(this);
            if (resultCode != ConnectionResult.Success)
            {
                if (GoogleApiAvailability.Instance.IsUserResolvableError(resultCode))
                {
                    Toast.MakeText(this, GoogleApiAvailability.Instance.GetErrorString(resultCode), ToastLength.Long);
                }
                else
                {
                    Toast.MakeText(this, "This device does not support Google Play Services", ToastLength.Long);
                }
                return false;
            }
            return true;
        }



        protected override void OnActivityResult(int requestCode, Result resultCode, Android.Content.Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            if (requestCode == 1)
            {
                GoogleSignInResult result = Auth.GoogleSignInApi.GetSignInResultFromIntent(data);

                GoogleManager.Instance.OnAuthCompleted(result);
            }
            
            var manager = DependencyService.Get<IFacebookManager>();
            if (manager != null)
            {
                (manager as FacebookManager)._callbackManager.OnActivityResult(requestCode, (int)resultCode, data);
            }
        }
        
		public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
        {
            PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
        
    }
}
