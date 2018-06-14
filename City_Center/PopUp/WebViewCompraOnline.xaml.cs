using System;
using System.Collections.Generic;
using City_Center.Clases;
using Foundation;
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

            #if __ANDROID__

            Browser.Source = VariablesGlobales.RutaCompraOnline;

            #endif


            #if __IOS__
            var uri = new Uri(VariablesGlobales.RutaCompraOnline);

            var nsurl = new NSUrl(uri.GetComponents(UriComponents.HttpRequestUrl, UriFormat.UriEscaped));

            Browser.Source = nsurl.AbsoluteUrl.ToString();
            #endif


            //Browser.Source = VariablesGlobales.RutaCompraOnline;

        }

    }
}
