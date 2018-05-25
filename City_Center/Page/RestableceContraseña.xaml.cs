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
        }
  

		async void Cerrar(object sender, System.EventArgs e)
        {
            await Navigation.PopPopupAsync();
        }

    }
}
