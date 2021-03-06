﻿using City_Center.Page;
using System;
using Plugin.FirebasePushNotification;
using Xamarin.Forms;
using DLToolkit.Forms.Controls;
using System.Threading.Tasks;
using City_Center.Clases;
using System.Net.Http;
using System.Collections.Generic;

using City_Center.Models;  
using static City_Center.Models.MensajesPendientesResultado;
using Plugin.Toasts;

namespace City_Center
{
    public partial class App : Application
    {
        public static NavigationPage NavPage { get; set; }
        public static int NumMsnCasino { get; set; }
        public static int NumMsnHotel { get; set; }


        public App()
        {
            InitializeComponent();

            FlowListView.Init();
            VariablesGlobales.ChatPresente = 0;

            #if __ANDROID__
                             GlobalResources.Current.ImagenChat = "chat";
            #endif

            #if __IOS__
                        GlobalResources.Current.ImagenChat = "chat@2x";
            #endif


            //if (isLoggedIn)
            //{
            MainPage = new MasterPage();
            //}
            //else
            //{
            //    MainPage = new NavigationPage(new Login()){BarBackgroundColor=Color.FromHex("#23144B")};
            //}

            //CrossFirebasePushNotification.Current.OnTokenRefresh += (s, p) =>
            //{
            //    System.Diagnostics.Debug.WriteLine($"TOKEN : {p.Token}");
            //};

            //CrossFirebasePushNotification.Current.OnNotificationReceived += (s, p) =>
            //{

            //    System.Diagnostics.Debug.WriteLine("Received");

            //};

            //CrossFirebasePushNotification.Current.OnNotificationOpened += (s, p) =>
            //{
            //    System.Diagnostics.Debug.WriteLine("Opened");
            //    foreach (var data in p.Data)
            //    {
            //        System.Diagnostics.Debug.WriteLine($"{data.Key} : {data.Value}");
            //    }

            //    if (!string.IsNullOrEmpty(p.Identifier))
            //    {
            //        System.Diagnostics.Debug.WriteLine($"ActionId: {p.Identifier}");
            //    }

            //};

        }

        protected override void OnStart()
        {

            ////// Handle when your app starts
            //CrossFirebasePushNotification.Current.Subscribe("general");
            //CrossFirebasePushNotification.Current.OnTokenRefresh += (s, p) =>
            //{
            //    System.Diagnostics.Debug.WriteLine($"TOKEN REC: {p.Token}");
            //};
            //System.Diagnostics.Debug.WriteLine($"TOKEN: {CrossFirebasePushNotification.Current.Token}");

            //CrossFirebasePushNotification.Current.OnNotificationReceived += (s, p) =>
            //{
            //    try
            //    {
            //        System.Diagnostics.Debug.WriteLine("Received");
            //        if (p.Data.ContainsKey("body"))
            //        {
            //            Device.BeginInvokeOnMainThread(() =>
            //            {
            //               // mPage.Message = $"{p.Data["body"]}";
            //            });

            //        }
            //   }
            //  catch (Exception ex)
            //   {

            //   }

            //};

            //CrossFirebasePushNotification.Current.OnNotificationOpened += (s, p) =>
            //{
            //    System.Diagnostics.Debug.WriteLine(p.Identifier);

            //    System.Diagnostics.Debug.WriteLine("Opened");
            //    foreach (var data in p.Data)
            //    {
            //        System.Diagnostics.Debug.WriteLine($"{data.Key} : {data.Value}");
            //    }

            //    if (!string.IsNullOrEmpty(p.Identifier))
            //    {
            //        Device.BeginInvokeOnMainThread(() =>
            //        {
            //           // mPage.Message = p.Identifier;
            //        });
            //    }
            //    else if (p.Data.ContainsKey("color"))
            //    {
            //        Device.BeginInvokeOnMainThread(() =>
            //        {
            //           //mPage.Navigation.PushAsync(new ContentPage()
            //            //{
            //            //    BackgroundColor = Color.FromHex($"{p.Data["color"]}")

            //            //});
            //       });

            //    }
            //    else if (p.Data.ContainsKey("aps.alert.title"))
            //    {
            //        Device.BeginInvokeOnMainThread(() =>
            //        {
            //            //   mPage.Message = $"{p.Data["aps.alert.title"]}";
            //        });

            //   }
            //};
            //CrossFirebasePushNotification.Current.OnNotificationDeleted += (s, p) =>
            //{
            //    System.Diagnostics.Debug.WriteLine("Dismissed");
            //};
            try
            {
                Task.Run(() =>
                {
                    var minutes = TimeSpan.FromSeconds(25);

                    Device.StartTimer(minutes, () =>
                    {
                        bool isLoggedIn = Properties.ContainsKey("IsLoggedIn") ?
                         (bool)Properties["IsLoggedIn"] : false;

                        if (isLoggedIn)
                        {
                            Task.Factory.StartNew(async () =>
                            {
                                bool Casino = false;
                                bool Hotel = false;

                                string ValorCasino = Application.Current.Properties["Casino"].ToString();
                                string ValorHotel = Application.Current.Properties["Hotel"].ToString();

                                if (ValorHotel == "0")
                                {
                                    Hotel = true;
                                }

                                if (ValorCasino == "0")
                                {
                                    Casino = true;
                                }

                                if (Hotel == true)
                                {

                                    var content = new FormUrlEncodedContent(new[]
                                    {
                                        new KeyValuePair<string, string>("", "")
                                    });


                                    Restcliente Mensajitos = new Restcliente();

                                    var response = await Mensajitos.GetReal<MensajesPendientesReturn>("/chat/VerificaChatApp/" + Application.Current.Properties["VariableChatHotel"].ToString() + "/" + Application.Current.Properties["Email"].ToString(), content);

                                    if (response != null)
                                    {
                                        if (response.msn.Count > 0)
                                        {
                                            #if __ANDROID__
                                                GlobalResources.Current.ImagenChat = "ChatNotificacion";
                                                
                                                var options = new NotificationOptions()
                                                {
                                                    Title = "Notificacion Chat Hotel",
                                                    Description = "Tienes una notificacion del chat de hotel",
                                                    AndroidOptions = new AndroidOptions(){SmallDrawableIcon = 2130837891}
                                                };

                                            #endif

                                            #if __IOS__
                                                GlobalResources.Current.ImagenChat = "chatnotificaciones@2x";

                                                var options = new NotificationOptions()
                                                {
                                            Title = "Notificacion Chat Hotel",
                                            Description = "Tienes una notificacion del chat de Hotel"
                                                    
                                                };
                                            #endif

                                            if (response.msn.Count > NumMsnHotel)
                                            {
                                                if (VariablesGlobales.ChatPresente == 0)
                                                {
                                                    var notificator = DependencyService.Get<IToastNotificator>();

                                                    var result = await notificator.Notify(options);
                                                }
                                              
                                            }

                                            NumMsnHotel = response.msn.Count;
                                        }
                                        else
                                        {
                                            
                                        #if __ANDROID__
                                            GlobalResources.Current.ImagenChat = "chat";
                                        #endif
                                                                                    
                                        #if __IOS__
                                            GlobalResources.Current.ImagenChat = "chat@2x";
                                        #endif

                                            NumMsnHotel = 0;
                                        }
                                    }
                                }
                                else if (Casino == true)
                                {


                                    var content = new FormUrlEncodedContent(new[]
                                    {
                                        new KeyValuePair<string, string>("", "")
                                    });


                                    Restcliente Mensajitos = new Restcliente();

                                    var response = await Mensajitos.GetReal<MensajesPendientesReturn>("/chat/VerificaChatApp/" + Application.Current.Properties["VariableChatCasino"].ToString() + "/" + Application.Current.Properties["Email"].ToString(), content);
                                    if (response != null)
                                    {
                                        if (response.msn.Count > 0)
                                        {
                                        #if __ANDROID__
                                            GlobalResources.Current.ImagenChat = "ChatNotificacion";

                                                var options = new NotificationOptions()
                                            {
                                                Title = "Notificacion Chat Casino",
                                                Description = "Tienes una notificacion del chat de casino",
                                                AndroidOptions = new AndroidOptions() { SmallDrawableIcon = 2130837891 }
                                            };

                                        #endif

                                        #if __IOS__
                                           GlobalResources.Current.ImagenChat = "chatnotificaciones@2x";

                                                var options = new NotificationOptions()
                                                {
                                                    Title = "Notificacion Chat Casino",
                                                    Description = "Tienes una notificacion del chat de casino"
                                                
                                                };

                                        #endif

                                            if (response.msn.Count > NumMsnCasino)
                                            {
                                                if (VariablesGlobales.ChatPresente == 0)
                                                {
                                                    var notificator = DependencyService.Get<IToastNotificator>();

                                                    var result = await notificator.Notify(options);
                                                }
                                            }

                                            NumMsnCasino = response.msn.Count;

                                        }
                                        else
                                        {
                                            #if __ANDROID__
                                                GlobalResources.Current.ImagenChat = "chat";
                                            #endif
                                                                                        
                                            #if __IOS__
                                                GlobalResources.Current.ImagenChat = "chat@2x";
                                            #endif

                                            NumMsnCasino = 0;
                                        }
                                    }
                                }

                            });
                        }

                        return true;
                    });

                });
            }
            catch (Exception ex)
            {

            }


        }

        protected override void OnSleep()
        {
           
        }

        protected override void OnResume()
        {
            //Handle when your app resumes

        }


        //private 


    }

}
