﻿using System;
using System.Collections.Generic;

using Xamarin.Forms;
using City_Center.Clases;
using City_Center.ViewModels;

namespace City_Center.Page
{
    public partial class SeleccionTipoChat : ContentPage
    {
        public SeleccionTipoChat()
        {
            InitializeComponent();
            NavigationPage.SetTitleIcon(this, "logo@2x.png.png");
        }

      async  void ChatCasino(object sender, System.EventArgs e)
        {
            if (string.IsNullOrEmpty(Application.Current.Properties["RutaChatCasino"].ToString()))
            {
                Application.Current.Properties["Casino"] = 1;
            }
            else
            {
                Application.Current.Properties["Casino"] = 0;  
            }

            await Application.Current.SavePropertiesAsync();

            MainViewModel.GetInstance().Chat = new ChatviewModel();

            VariablesGlobales.TipoChat = "casino";

            base.OnDisappearing();

            string ValorHotel = Application.Current.Properties["Hotel"].ToString();

            if (ValorHotel == "0")
            {
                await Mensajes.Alerta("Ya se encuentra un chat abierto en hotel");
                return;
            }

           
            #if __ANDROID__
            await((MasterPage)Application.Current.MainPage).Detail.Navigation.PushAsync(new Chat());
            #endif

           
            #if __IOS__
            await ((MasterPage)Application.Current.MainPage).Detail.Navigation.PushAsync(new ChatIos());
            #endif

        }

        async  void ChatHotel(object sender, System.EventArgs e)
        {

            if  (string.IsNullOrEmpty(Application.Current.Properties["RutaChatHotel"].ToString()))
            {
                Application.Current.Properties["Hotel"] = 1;  
            }
            else
            {
                Application.Current.Properties["Hotel"] = 0;
            }

            await Application.Current.SavePropertiesAsync();
        
            string ValorCasino = Application.Current.Properties["Casino"].ToString();

            if (ValorCasino == "0")
            {
                await Mensajes.Alerta("Ya se encuentra un chat abierto en casino");
                return;
            }


            VariablesGlobales.TipoChat = "hotel";
            #if __ANDROID__
            await((MasterPage)Application.Current.MainPage).Detail.Navigation.PushAsync(new Chat());
            #endif


            #if __IOS__
            await ((MasterPage)Application.Current.MainPage).Detail.Navigation.PushAsync(new ChatIos());
            #endif

        }
    }
}
