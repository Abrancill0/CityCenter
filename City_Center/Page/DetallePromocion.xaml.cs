using System;
using System.Collections.Generic;
using City_Center.Clases;
using Xamarin.Forms;

namespace City_Center.Page
{
    public partial class DetallePromocion : ContentPage
    {
        public DetallePromocion()
        {
            InitializeComponent();
            NavigationPage.SetTitleIcon(this, "logo@2x.png");
        }

        void Handle_Clicked(object sender, System.EventArgs e)
        {
            if (SLR.IsVisible==false)
            {
                SLR.IsVisible = true;
                SLP.IsVisible = false;
                FlechaAbajo.IsVisible = false;
                FlechaArriba.IsVisible = true;
            }
            else
            {
                SLR.IsVisible = false;
                SLP.IsVisible = true;
                FlechaAbajo.IsVisible = true;
                FlechaArriba.IsVisible = false;
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
    }
}
