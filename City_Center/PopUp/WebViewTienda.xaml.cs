using System.IO;
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


            //string fileName = "http://cc.comprogapp.com/es/tienda";
            //string localHtmlUrl = Path.Combine(NSBundle.MainBundle.BundlePath, fileName);
            //NSUrl finalUrl = new NSUrl("#anchorname", new NSUrl(localHtmlUrl, false));
            //Browser.Source=new NSUrlRequest(finalUrl));


           // NSUrl.FromFilename("http://cc.comprogapp.com/es/tienda");

            Browser.Source = "http://cc.comprogapp.com/es/tienda";

        }

    }
}
