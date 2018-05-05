using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace City_Center.Page.SlideMenu
{
    public partial class Show : ContentPage
    {
        public Show()
        {
            InitializeComponent();
        }

        protected override void OnDisappearing()
        {

            base.OnDisappearing();

            GC.Collect();

        }

        void Tab1_Tapped(object sender, System.EventArgs e)
        {
            LabelTab1.TextColor = Color.FromHex("#EAE8ED");
            LabelTab2.TextColor = Color.FromHex("#71628A");
            LabelTab3.TextColor = Color.FromHex("#71628A");

            BV1.IsVisible = true;
            BV2.IsVisible = false;
            BV3.IsVisible = false;

            //SL1.IsVisible = true;
            //SL2.IsVisible = false;
            //SL3.IsVisible = false;
        }

        void Tab2_Tapped(object sender, System.EventArgs e)
        {
            LabelTab1.TextColor = Color.FromHex("#71628A");
            LabelTab2.TextColor = Color.FromHex("#EAE8ED");
            LabelTab3.TextColor = Color.FromHex("#71628A");

            BV1.IsVisible = false;
            BV2.IsVisible = true;
            BV3.IsVisible = false;

         
        }

        void Tab3_Tapped(object sender, System.EventArgs e)
        {
            LabelTab1.TextColor = Color.FromHex("#71628A");
            LabelTab2.TextColor = Color.FromHex("#71628A");
            LabelTab3.TextColor = Color.FromHex("#EAE8ED");

            BV1.IsVisible = false;
            BV2.IsVisible = false;
            BV3.IsVisible = true;

        }
    }
}
