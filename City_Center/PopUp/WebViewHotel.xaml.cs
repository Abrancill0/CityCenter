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
            Browser.Source = "https://api.pxsol.com/search/insert?Pos=Palermitano&ProductID=3035&Currency=USD&Lng=es&Type=Hotel&Start="+ "2018-12-01" +"&End" + "2018-12-02" + "&Nights=1&Groups=1&GroupsForm=1:2,0,0t&Device=Computer&tag=hotelesdon.com";

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
