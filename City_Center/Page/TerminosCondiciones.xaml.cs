using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace City_Center.Page
{
    public partial class TerminosCondiciones : ContentPage
    {
        public TerminosCondiciones()
        {
            InitializeComponent();
       
            NavigationPage.SetTitleIcon(this, "logo.png");
        }

        void Tab1_Tapped(object sender, System.EventArgs e)
        {
            LabelTab1.TextColor = Color.FromHex("#EAE8ED");
            LabelTab2.TextColor = Color.FromHex("#71628A");

            BV1.IsVisible = true;
            BV2.IsVisible = false;

            SL1.IsVisible = true;
            SL2.IsVisible = false;

        }

        void Tab2_Tapped(object sender, System.EventArgs e)
        {
            LabelTab1.TextColor = Color.FromHex("#71628A");
            LabelTab2.TextColor = Color.FromHex("#EAE8ED");

            BV1.IsVisible = false;
            BV2.IsVisible = true;

            SL1.IsVisible = false;
            SL2.IsVisible = true;

        }
    }
}
