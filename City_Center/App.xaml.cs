using City_Center.Page;
using System;
using Plugin.FirebasePushNotification;
//using Prism.Unity;
using Xamarin.Forms;
using DLToolkit.Forms.Controls;
//using FFImageLoading.Forms;

namespace City_Center
{   
    public partial class App : Application
    {   
		public static NavigationPage NavPage { get; set; }
        
        public App()
        {
            InitializeComponent();

            FlowListView.Init(); 

            bool isLoggedIn = Properties.ContainsKey("IsLoggedIn") ?
                     (bool)Properties["IsLoggedIn"] : false;


            //if (isLoggedIn)
            //{
            MainPage = new MasterPage();
            //}
            //else
            //{
            //    MainPage = new NavigationPage(new Login()){BarBackgroundColor=Color.FromHex("#23144B")};
            //}

            CrossFirebasePushNotification.Current.OnTokenRefresh += (s, p) =>
            {
                System.Diagnostics.Debug.WriteLine($"TOKEN : {p.Token}");
            };

            CrossFirebasePushNotification.Current.OnNotificationReceived += (s, p) =>
            {

                System.Diagnostics.Debug.WriteLine("Received");

            };

            CrossFirebasePushNotification.Current.OnNotificationOpened += (s, p) =>
            {
                System.Diagnostics.Debug.WriteLine("Opened");
                foreach (var data in p.Data)
                {
                    System.Diagnostics.Debug.WriteLine($"{data.Key} : {data.Value}");
                }

                if (!string.IsNullOrEmpty(p.Identifier))
                {
                    System.Diagnostics.Debug.WriteLine($"ActionId: {p.Identifier}");
                }

            };

        }

        protected override void OnStart()
        {

            //// Handle when your app starts
            CrossFirebasePushNotification.Current.Subscribe("general");
            CrossFirebasePushNotification.Current.OnTokenRefresh += (s, p) =>
            {
                System.Diagnostics.Debug.WriteLine($"TOKEN REC: {p.Token}");
            };
            System.Diagnostics.Debug.WriteLine($"TOKEN: {CrossFirebasePushNotification.Current.Token}");

            CrossFirebasePushNotification.Current.OnNotificationReceived += (s, p) =>
            {
                try
                {
                    System.Diagnostics.Debug.WriteLine("Received");
                    if (p.Data.ContainsKey("body"))
                    {
                        Device.BeginInvokeOnMainThread(() =>
                        {
                           // mPage.Message = $"{p.Data["body"]}";
                        });

                    }
               }
              catch (Exception ex)
               {

               }

            };

            CrossFirebasePushNotification.Current.OnNotificationOpened += (s, p) =>
            {
                System.Diagnostics.Debug.WriteLine(p.Identifier);

                System.Diagnostics.Debug.WriteLine("Opened");
                foreach (var data in p.Data)
                {
                    System.Diagnostics.Debug.WriteLine($"{data.Key} : {data.Value}");
                }

                if (!string.IsNullOrEmpty(p.Identifier))
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                       // mPage.Message = p.Identifier;
                    });
                }
                else if (p.Data.ContainsKey("color"))
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                       //mPage.Navigation.PushAsync(new ContentPage()
                        //{
                        //    BackgroundColor = Color.FromHex($"{p.Data["color"]}")

                        //});
                   });

                }
                else if (p.Data.ContainsKey("aps.alert.title"))
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        //   mPage.Message = $"{p.Data["aps.alert.title"]}";
                    });

               }
            };
            CrossFirebasePushNotification.Current.OnNotificationDeleted += (s, p) =>
            {
                System.Diagnostics.Debug.WriteLine("Dismissed");
            };

        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            //Handle when your app resumes
        }

    }

}
