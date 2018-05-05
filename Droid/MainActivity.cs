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

            Xfx.XfxControls.Init();
            UserDialogs.Init(this);

            Forms.SetFlags("FastRenderers_Experimental");

            //CachedImageRenderer.Init(true);

            /*BottomTabbedRenderer.IconSize = 30;
            BottomTabbedRenderer.BottomBarHeight = 45;
            BottomTabbedRenderer.ItemAlign = ItemAlignFlags.Center;

            */

            BottomTabbedRenderer.ItemAlign = ItemAlignFlags.Top;
            BottomTabbedRenderer.FontSize = 10;
            BottomTabbedRenderer.IconSize = 45;
            BottomTabbedRenderer.ItemSpacing = 8;
            BottomTabbedRenderer.ItemPadding = new Xamarin.Forms.Thickness(8);
            BottomTabbedRenderer.BottomBarHeight = 50;

            FacebookSdk.SdkInitialize(this);

            global::Xamarin.Forms.Forms.Init(this, bundle);

            CachedImageRenderer.Init(enableFastRenderer:true);

            CarouselViewRenderer.Init();

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

    }
}
