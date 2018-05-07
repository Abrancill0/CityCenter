using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace City_Center.PopUp
{
    public partial class LoadingPopupPage : Rg.Plugins.Popup.Pages.PopupPage
    {
        public LoadingPopupPage()
        {
            //InitializeComponent();
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }
    }
}
