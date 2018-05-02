using System;
using System.Collections.Generic;
using Rg.Plugins.Popup.Extensions;
using Xamarin.Forms;

namespace City_Center.PopUp
{
    public partial class WebViewHotel : Rg.Plugins.Popup.Pages.PopupPage
    {
        public WebViewHotel()
        {
            InitializeComponent();
            Browser.Source = "http://www.pullmancitycenterrosario.reservadirecto.com/?pos=PullmanCityCenterRosario&lng=es&cur=ARS&tag=developer";

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
