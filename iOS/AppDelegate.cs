using System;
using CarouselView.FormsPlugin.iOS;
using City_Center.Services.Contracts;
using Facebook.CoreKit;
using FFImageLoading;
using FFImageLoading.Transformations;
using Firebase.CloudMessaging;
using Foundation;
using Google.SignIn;
using KeyboardOverlap.Forms.Plugin.iOSUnified;
//using Plugin.FirebasePushNotification;
using Plugin.Toasts;
using UIKit;
using UserNotifications;
using Xamarin;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using Xfx;

namespace City_Center.iOS
{
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate,IUNUserNotificationCenterDelegate, IMessagingDelegate //UIApplicationDelegate
    {
        public void DidRefreshRegistrationToken(Messaging messaging, string fcmToken)
        {
            System.Diagnostics.Debug.WriteLine($"FCM Token: {fcmToken}");
        }

        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            XfxControls.Init();
            ButtonCircle.FormsPlugin.iOS.ButtonCircleRenderer.Init();

            UINavigationBar.Appearance.TintColor = Color.White.ToUIColor();
           
            global::Xamarin.Forms.Forms.Init();

            Firebase.Core.App.Configure();

            KeyboardOverlapRenderer.Init();

            DependencyService.Register<ToastNotification>(); // Register your dependency
            ToastNotification.Init();

            FFImageLoading.Forms.Touch.CachedImageRenderer.Init();

			var ignore = new CircleTransformation();

            CarouselViewRenderer.Init();

            DependencyService.Register<IGoogleManager, GoogleManager>();
            DependencyService.Register<IFacebookManager, FacebookManager>();

            var googleServiceDictionary = NSDictionary.FromFile("GoogleService-Info.plist");

            SignIn.SharedInstance.ClientID = googleServiceDictionary["CLIENT_ID"].ToString();
            
			FormsMaps.Init();
            Rg.Plugins.Popup.Popup.Init();

            LoadApplication(new App());
                     
            var statusBar = UIApplication.SharedApplication.ValueForKey(new NSString("statusBar")) as UIView;
            if (statusBar.RespondsToSelector(new ObjCRuntime.Selector("setBackgroundColor:")))
            {
                statusBar.BackgroundColor = UIColor.White;
                statusBar.TintColor = UIColor.White;
            }

			ImageCircle.Forms.Plugin.iOS.ImageCircleRenderer.Init();

            //FirebasePushNotificationManager.Initialize(options, new NotificationUserCategory[]
            //{
            //    new NotificationUserCategory("message",new List<NotificationUserAction> {
            //        new NotificationUserAction("Reply","Reply",NotificationActionType.Foreground)
            //    }),
            //    new NotificationUserCategory("request",new List<NotificationUserAction> {
            //        new NotificationUserAction("Accept","Accept"),
            //        new NotificationUserAction("Reject","Reject",NotificationActionType.Destructive)
            //    })

            //});

            // Register your app for remote notifications.
            if (UIDevice.CurrentDevice.CheckSystemVersion(10, 0))
            {
                // iOS 10 or later
                var authOptions = UNAuthorizationOptions.Alert | UNAuthorizationOptions.Badge | UNAuthorizationOptions.Sound;
                UNUserNotificationCenter.Current.RequestAuthorization(authOptions, (granted, error) => {
                    Console.WriteLine(granted);
                });

                // For iOS 10 display notification (sent via APNS)
                UNUserNotificationCenter.Current.Delegate = this;

                // For iOS 10 data message (sent via FCM)

               Messaging.SharedInstance.RemoteMessageDelegate = this;

            }
            else
            {
                // iOS 9 or before
                var allNotificationTypes = UIUserNotificationType.Alert | UIUserNotificationType.Badge | UIUserNotificationType.Sound;
                var settings = UIUserNotificationSettings.GetSettingsForTypes(allNotificationTypes, null);
                UIApplication.SharedApplication.RegisterUserNotificationSettings(settings);
            }

            UIApplication.SharedApplication.RegisterForRemoteNotifications();



            return base.FinishedLaunching(app, options);
        }

        public override void OnActivated(UIApplication uiapplication)
        {
            base.OnActivated(uiapplication);
            AppEvents.ActivateApp();
        }

        public override bool OpenUrl(UIApplication application, NSUrl url, string sourceApplication, NSObject annotation)
        {
            return ApplicationDelegate.SharedInstance.OpenUrl(application, url, sourceApplication, annotation);
           
        }
        
//        public override void RegisteredForRemoteNotifications(UIApplication application, NSData deviceToken)
//        {
//#if DEBUG
//            FirebasePushNotificationManager.DidRegisterRemoteNotifications(deviceToken, FirebaseTokenType.Sandbox);
//#endif
//#if RELEASE
//                        FirebasePushNotificationManager.DidRegisterRemoteNotifications(deviceToken,FirebaseTokenType.Production);
//#endif

        //}

        //public override void FailedToRegisterForRemoteNotifications(UIApplication application, NSError error)
        //{
        //    FirebasePushNotificationManager.RemoteNotificationRegistrationFailed(error);
        //}
        // To receive notifications in foregroung on iOS 9 and below.
        // To receive notifications in background in any iOS version
        //public override void DidReceiveRemoteNotification(UIApplication application, NSDictionary userInfo, Action<UIBackgroundFetchResult> completionHandler)
        //{
        //    // If you are receiving a notification message while your app is in the background,
        //    // this callback will not be fired 'till the user taps on the notification launching the application.

        //    // If you disable method swizzling, you'll need to call this method. 
        //    // This lets FCM track message delivery and analytics, which is performed
        //    // automatically with method swizzling enabled.
        //    FirebasePushNotificationManager.DidReceiveMessage(userInfo);
        //    // Do your magic to handle the notification data
        //    System.Console.WriteLine(userInfo);
        //}

        //public override void DidEnterBackground(UIApplication application)
        //{
        //    // Use this method to release shared resources, save user data, invalidate timers and store the application state.
        //    // If your application supports background exection this method is called instead of WillTerminate when the user quits.
        //    FirebasePushNotificationManager.Disconnect();
        //}


		
    }

   
}
