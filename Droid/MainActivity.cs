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
using Naxam.Controls.Platform.Droid;
using Xamarin;
using Acr.UserDialogs;
using Plugin.CurrentActivity;
using Plugin.Permissions;
using ImageCircle.Forms.Plugin.Droid;
//using FFImageLoading.Forms.Droid;

namespace City_Center.Droid
{
    [Activity(Label = "City_Center.Droid", Icon = "@drawable/icon", Theme = "@style/MyTheme", MainLauncher = false,ScreenOrientation = ScreenOrientation.Portrait)]

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

			//CrossCurrentActivity.Current.Activity.Init(this, bundle);

            Forms.SetFlags("FastRenderers_Experimental");

            //CachedImageRenderer.Init(true);

            /*BottomTabbedRenderer.IconSize = 30;
            BottomTabbedRenderer.BottomBarHeight = 45;
            BottomTabbedRenderer.ItemAlign = ItemAlignFlags.Center;

            */

            BottomTabbedRenderer.ItemAlign = ItemAlignFlags.Default;
            BottomTabbedRenderer.FontSize = 10;
            BottomTabbedRenderer.IconSize = 30;

			BottomTabbedRenderer.ItemIconTintList = null;
            BottomTabbedRenderer.ItemSpacing = 8;
            BottomTabbedRenderer.ItemPadding = new Xamarin.Forms.Thickness(8);
            BottomTabbedRenderer.BottomBarHeight = 50;
			//BottomTabbedRenderer.BackgroundColor = Android.Graphics.Color.Rgb(98, 45, 88);
			//BottomTabbedRenderer.ItemPadding = 4;


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

            FirebasePushNotificationManager.ProcessIntent(this, Intent);
            //Aqui hiba ImageCircle
            //LoadApplication(new App(new AndroidInitializer()));
        }

        protected override void OnNewIntent(Intent intent)
        {
            base.OnNewIntent(intent);
            FirebasePushNotificationManager.ProcessIntent(this, intent);
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
