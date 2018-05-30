using System;
using System.Collections.Generic;
using City_Center.Clases;
using Xamarin.Forms;

namespace City_Center.Page
{
    public partial class RecompensasWin : ContentPage
    {
        public RecompensasWin()
        {
              InitializeComponent();
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
