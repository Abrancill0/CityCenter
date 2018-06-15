using System;
using System.Collections.Generic;
using City_Center.Clases;
using Xamarin.Forms;

namespace City_Center.Page
{
    public partial class DetalleDestacados : ContentPage
    {
        public DetalleDestacados()
        {
            InitializeComponent();
<<<<<<< HEAD

            NavigationPage.SetTitleIcon(this, "logo@2x.png");
=======
            NavigationPage.SetTitleIcon(this, "logo_hdpi.png");
>>>>>>> 66a2e5f3c284a595e654f62d503db44111e45e87
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
