using System;
using System.Collections.Generic;
using City_Center.Clases;
using Xamarin.Forms;
using Plugin.Messaging;

namespace City_Center.Page
{
    public partial class DetalleShow : ContentPage
    {
        public DetalleShow()
        {
            InitializeComponent();
            NavigationPage.SetTitleIcon(this, "logo.png");
        }

		void CambiaIcono(object sender, System.EventArgs e)
        {
            try
            {
                bool isLoggedIn = Application.Current.Properties.ContainsKey("IsLoggedIn") ?
                                     (bool)Application.Current.Properties["IsLoggedIn"] : false;

                if (isLoggedIn)
                {
                    if (Icono1.BackgroundColor != Color.Transparent)
                    {
                        Icono1.BackgroundColor = Color.Transparent;
                        Icono1.Source = "FavoritoOK";
                    }
                    else
                    {
                        Icono1.BackgroundColor = Color.White;
                        Icono1.Source = "Favorito";
                    }

                }

            }
            catch (Exception)
            {


            }
        }

        void CambiaIcono2(object sender, System.EventArgs e)
        {
            try
            {
                bool isLoggedIn = Application.Current.Properties.ContainsKey("IsLoggedIn") ?
                                     (bool)Application.Current.Properties["IsLoggedIn"] : false;

                if (isLoggedIn)
                {
                    
                    if (Icono2.BackgroundColor != Color.Transparent)
                    {
                        Icono2.BackgroundColor = Color.Transparent;
                        Icono2.Source = "Favorito";
                    }
                    else
                    {
                        Icono2.BackgroundColor = Color.White;
                        Icono2.Source = "FavoritoOK";
                    }

                }

            }
            catch (Exception)
            {


            }
        }

        async void Chat_click(object sender, System.EventArgs e)
        {
            bool isLoggedIn = Application.Current.Properties.ContainsKey("IsLoggedIn") ?
                                    (bool)Application.Current.Properties["IsLoggedIn"] : false;

            if (isLoggedIn)
            {
                #if __ANDROID__
                await ((MasterPage)Application.Current.MainPage).Detail.Navigation.PushAsync(new SeleccionTipoChat());
                #endif
            }
            else
            {
                await Mensajes.Alerta("Es necesario que te registres para completar esta acción");
            }
        }
    }
}
