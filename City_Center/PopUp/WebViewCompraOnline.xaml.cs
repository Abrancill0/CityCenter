using System;
using System.Collections.Generic;
using City_Center.Clases;
using Rg.Plugins.Popup.Extensions;
using Xamarin.Forms;

namespace City_Center.PopUp
{
    public partial class WebViewCompraOnline : ContentPage
    {
        public WebViewCompraOnline()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            Browser.Source = VariablesGlobales.RutaCompraOnline;

        }

    }
}
