using System;
using City_Center.Clases;
using Xamarin.Forms;

namespace City_Center.PopUp
{
    public partial class WebViewTienda2 : ContentPage
    {
        public WebViewTienda2()
        {
            InitializeComponent();
            NavigationPage.SetTitleIcon(this, "logo@2x.png");
            #if __ANDROID__

            Browser.Source = VariablesGlobales.RutaTiendaGuardados;

            #endif


            #if __IOS__
            var uri = new Uri(VariablesGlobales.RutaTiendaGuardados);

            var nsurl = new Foundation.NSUrl(uri.GetComponents(UriComponents.HttpRequestUrl, UriFormat.UriEscaped));

            Browser.Source = nsurl.AbsoluteUrl.ToString();
            #endif

            //Browser.Source = VariablesGlobales.RutaTiendaGuardados;
        }
    }
}
