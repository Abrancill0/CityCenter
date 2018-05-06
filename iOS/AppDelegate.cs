using System;
using System.Collections.Generic;
using CarouselView.FormsPlugin.iOS;
using City_Center.Services.Contracts;
using Facebook.CoreKit;
using FFImageLoading.Forms.Touch;
using Foundation;
using Google.SignIn;
using Plugin.FirebasePushNotification;
using Plugin.FirebasePushNotification.Abstractions;
using UIKit;
using Xamarin;
using Xamarin.Forms;
using Xfx;

namespace City_Center.iOS
{
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate //UIApplicationDelegate
    {
       
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
           
            XfxControls.Init();
            ButtonCircle.FormsPlugin.iOS.ButtonCircleRenderer.Init();

            CachedImageRenderer.Init();
          
            global::Xamarin.Forms.Forms.Init();
            CarouselViewRenderer.Init();

            DependencyService.Register<IGoogleManager, GoogleManager>();
            DependencyService.Register<IFacebookManager, FacebookManager>();

            var googleServiceDictionary = NSDictionary.FromFile("GoogleService-Info.plist");
           
            SignIn.SharedInstance.ClientID = googleServiceDictionary["CLIENT_ID"].ToString();
            FormsMaps.Init();
            Rg.Plugins.Popup.Popup.Init();
            LoadApplication(new App());

            FirebasePushNotificationManager.Initialize(options, new NotificationUserCategory[]
            {
                new NotificationUserCategory("message",new List<NotificationUserAction> {
                    new NotificationUserAction("Reply","Reply",NotificationActionType.Foreground)
                }),
                new NotificationUserCategory("request",new List<NotificationUserAction> {
                    new NotificationUserAction("Accept","Accept"),
                    new NotificationUserAction("Reject","Reject",NotificationActionType.Destructive)
                })

            });

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


        public override void RegisteredForRemoteNotifications(UIApplication application, NSData deviceToken)
        {
#if DEBUG
            FirebasePushNotificationManager.DidRegisterRemoteNotifications(deviceToken, FirebaseTokenType.Sandbox);
#endif
#if RELEASE
                        FirebasePushNotificationManager.DidRegisterRemoteNotifications(deviceToken,FirebaseTokenType.Production);
#endif

        }

        public override void FailedToRegisterForRemoteNotifications(UIApplication application, NSError error)
        {
            FirebasePushNotificationManager.RemoteNotificationRegistrationFailed(error);
        }
        // To receive notifications in foregroung on iOS 9 and below.
        // To receive notifications in background in any iOS version
        public override void DidReceiveRemoteNotification(UIApplication application, NSDictionary userInfo, Action<UIBackgroundFetchResult> completionHandler)
        {
            // If you are receiving a notification message while your app is in the background,
            // this callback will not be fired 'till the user taps on the notification launching the application.

            // If you disable method swizzling, you'll need to call this method. 
            // This lets FCM track message delivery and analytics, which is performed
            // automatically with method swizzling enabled.
            FirebasePushNotificationManager.DidReceiveMessage(userInfo);
            // Do your magic to handle the notification data
            System.Console.WriteLine(userInfo);
        }

        public override void DidEnterBackground(UIApplication application)
        {
            // Use this method to release shared resources, save user data, invalidate timers and store the application state.
            // If your application supports background exection this method is called instead of WillTerminate when the user quits.
            FirebasePushNotificationManager.Disconnect();
        }

    }

   
}
