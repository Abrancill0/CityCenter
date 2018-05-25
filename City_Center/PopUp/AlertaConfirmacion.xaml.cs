using System;
using System.Collections.Generic;
using City_Center.Clases;
using Rg.Plugins.Popup.Extensions;
using Xamarin.Forms;


namespace City_Center.PopUp
{
    public partial class AlertaConfirmacion : Rg.Plugins.Popup.Pages.PopupPage
    {
        public AlertaConfirmacion()
        {
            InitializeComponent();
            Mensaje.Text = VariablesGlobales.Mensaje;
        }

      async void Handle_Tapped(object sender, System.EventArgs e)
       {
            await Navigation.PopPopupAsync(); 
        }


    }
}
