using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace City_Center.Page
{
    public partial class CambiaContraseña : ContentPage
    {
        public CambiaContraseña()
        {
            InitializeComponent();
            App.NavPage.BarBackgroundColor = Color.FromHex("#23144B");
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            App.NavPage.BarBackgroundColor = Color.FromHex("#23144B");

        }
    }
}
