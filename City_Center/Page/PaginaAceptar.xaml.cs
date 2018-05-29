using System;
using System.Collections.Generic;
using Acr.UserDialogs;
using City_Center.Clases;
using Xamarin.Forms;

namespace City_Center.Page
{
    public partial class PaginaAceptar : ContentPage
    {
        public PaginaAceptar()
        {
            InitializeComponent();
        }

       async void Handle_Clicked(object sender, System.EventArgs e)
        {
            UserDialogs.Instance.ShowLoading("Iniciando Sesion...", MaskType.Black);


            MasterPage fpm = new MasterPage();
            //fpm.Master = new DetailPage(); // You have to create a Master ContentPage()
            //App.NavPage = new NavigationPage(new CustomTabPage()) { BarBackgroundColor = Color.FromHex("#23144B") };

            //fpm.Detail = App.NavPage; // You have to create a Detail ContenPage()
            Application.Current.MainPage = fpm;

            UserDialogs.Instance.HideLoading();

            await Mensajes.Alerta("Bienvenido " + Application.Current.Properties["NombreCompleto"].ToString());
 
        }
    }
}
