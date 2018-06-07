using City_Center.Clases;
using Xamarin.Forms;

namespace City_Center.PopUp
{
    public partial class WebViewTienda2 : ContentPage
    {
        public WebViewTienda2()
        {
            InitializeComponent();

            Browser.Source = VariablesGlobales.RutaTiendaGuardados;
        }
    }
}
