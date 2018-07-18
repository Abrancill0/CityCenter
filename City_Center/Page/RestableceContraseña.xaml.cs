using System;
using System.Collections.Generic;
using City_Center.Clases;
using Rg.Plugins.Popup.Extensions;
using Xamarin.Forms;

namespace City_Center.Page
{
    public partial class RestableceContraseña : Rg.Plugins.Popup.Pages.PopupPage
    {
        public RestableceContraseña()
        {
            InitializeComponent();
            App.NavPage.BarBackgroundColor = Color.FromHex("#23144B");
        }
  
        protected override void OnAppearing()
        {
            base.OnAppearing();
            App.NavPage.BarBackgroundColor = Color.FromHex("#23144B");
        }

		async void Cerrar(object sender, System.EventArgs e)
        {
            await Navigation.PopPopupAsync();
        }

        async void EviarCorreo(object sender, System.EventArgs e)
        {
            await Navigation.PopPopupAsync();
        }

        void Handle_Focused(object sender, System.EventArgs e)
        {
         
        }

        void Handle_Unfocused(object sender, System.EventArgs e)
        {
           
        }

    }
}
