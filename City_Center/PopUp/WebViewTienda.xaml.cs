using System;
using System.IO;
using Xamarin.Forms;

namespace City_Center.PopUp
{
    public partial class WebViewTienda : ContentPage
    {
        public WebViewTienda()
        {
            InitializeComponent();

            NavigationPage.SetTitleIcon(this, "logo@2x.png");
#if __ANDROID__

            Browser.Source = "https://www.tienda.citycenter-rosario.com.ar/";

#endif


#if __IOS__
            var uri = new Uri("https://www.tienda.citycenter-rosario.com.ar/");

            var nsurl = new Foundation.NSUrl(uri.GetComponents(UriComponents.HttpRequestUrl, UriFormat.UriEscaped));

            Browser.Source = nsurl.AbsoluteUrl.ToString();
#endif

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

   
        }

    }
}
