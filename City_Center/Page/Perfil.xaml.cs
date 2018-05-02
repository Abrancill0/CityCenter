using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace City_Center.Page
{
    public partial class Perfil : ContentPage
    {
        public Perfil()
        {
            InitializeComponent();
        }

        void Tab1_Tapped(object sender, System.EventArgs e)
        {
            LabelTab1.FontAttributes = FontAttributes.Bold;
            LabelTab2.FontAttributes = FontAttributes.None;
            LabelTab3.FontAttributes = FontAttributes.None;
         
            BV1.IsVisible = true;
            BV2.IsVisible = false;
            BV3.IsVisible = false;
           
            SL1.IsVisible = true;
            SL2.IsVisible = false;
            SL3.IsVisible = false;
           
        }

        void Tab2_Tapped(object sender, System.EventArgs e)
        {
            LabelTab1.FontAttributes = FontAttributes.None;
            LabelTab2.FontAttributes = FontAttributes.Bold;
            LabelTab3.FontAttributes = FontAttributes.None;

            BV1.IsVisible = false;
            BV2.IsVisible = true;
            BV3.IsVisible = false;
           
            SL1.IsVisible = false;
            SL2.IsVisible = true;
            SL3.IsVisible = false;
           
        }

        void Tab3_Tapped(object sender, System.EventArgs e)
        {
            LabelTab1.FontAttributes = FontAttributes.None;
            LabelTab2.FontAttributes = FontAttributes.None;
            LabelTab3.FontAttributes = FontAttributes.Bold;
           
            BV1.IsVisible = false;
            BV2.IsVisible = false;
            BV3.IsVisible = true;
           
            SL1.IsVisible = false;
            SL2.IsVisible = false;
            SL3.IsVisible = true;

        }

    }
}
