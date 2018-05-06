using System;
using System.Collections.Generic;
using City_Center.Clases;
using Rg.Plugins.Popup.Extensions;
using Xamarin.Forms;

namespace City_Center.PopUp
{
    public partial class WebViewHotel : Rg.Plugins.Popup.Pages.PopupPage
    {
        public WebViewHotel()
        {
            InitializeComponent();
            string dia1 = VariablesGlobales.FechaFin.Date.ToString().Substring(0, 10);
            string dia2 = VariablesGlobales.FechaInicio.Date.ToString().Substring(0, 10);

            int dias = (int)(VariablesGlobales.FechaFin.Date - VariablesGlobales.FechaInicio.Date).TotalDays;

            Browser.Source = "https://api.pxsol.com/search/insert?Pos=Palermitano&ProductID=3035&Currency=USD&Lng=es&Type=Hotel&Start=" + dia1 + "&End" + dia2 + "&Nights="+ dias +"&Groups="+ VariablesGlobales.NumeroHuespedes +"&GroupsForm=1:2,0,0t&Device=Computer&tag=hotelesdon.com";

        }

        private void OnCloseButtonTapped(object sender, EventArgs e)
        {
            CloseAllPopup();
        }

        protected override bool OnBackgroundClicked()
        {
            CloseAllPopup();

            return false;
        }

        private async void CloseAllPopup()
        {
            await Navigation.PopAllPopupAsync();
        }
    }
}