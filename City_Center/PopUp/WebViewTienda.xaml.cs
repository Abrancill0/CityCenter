using System;
using System.Collections.Generic;
using Rg.Plugins.Popup.Extensions;
using Xamarin.Forms;

namespace City_Center.PopUp
{
    public partial class WebViewTienda : ContentPage
    {
        public WebViewTienda()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            Browser.Source = "http://cc.comprogapp.com/es/tienda";

        }

    }
}
