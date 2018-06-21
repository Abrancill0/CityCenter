using System;
using System.Collections.Generic;
using Acr.UserDialogs;
using City_Center.Clases;
using City_Center.Helper;
using Xamarin.Forms;

namespace City_Center.Page
{
    public partial class DetalleTorneo : ContentPage
    {
		private string[] ListaOpciones;

        public DetalleTorneo()
        {
            InitializeComponent();
            NavigationPage.SetTitleIcon(this, "logo@2x.png");

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
                        Icono1.Source = "Fav";
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
                        Icono2.Source = "Fav";
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

                await ((MasterPage)Application.Current.MainPage).Detail.Navigation.PushAsync(new SeleccionTipoChat());
            
            }
            else
            {
                await Mensajes.Alerta("Es necesario que te registres para completar esta acción");
            }
        }

      async  void Handle_Clicked(object sender, System.EventArgs e)
        {
            await((MasterPage)Application.Current.MainPage).Detail.Navigation.PushAsync(new DetalleTorneo2()); 
        }
    }
}
