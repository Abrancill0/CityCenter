using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace City_Center.Page.SlideMenu
{
    public partial class Promociones : ContentPage
    {
        public Promociones()
        {
            InitializeComponent();
        }

        void Tab1_Tapped(object sender, System.EventArgs e)
        {
            LabelTab1.FontAttributes = FontAttributes.Bold;
            LabelTab2.FontAttributes = FontAttributes.None;
            LabelTab3.FontAttributes = FontAttributes.None;
            LabelTab4.FontAttributes = FontAttributes.None;
            LabelTab5.FontAttributes = FontAttributes.None;

            BV1.IsVisible = true;
            BV2.IsVisible = false;
            BV3.IsVisible = false;
            BV4.IsVisible = false;
            BV5.IsVisible = false;

        }

        void Tab2_Tapped(object sender, System.EventArgs e)
        {
            LabelTab1.FontAttributes = FontAttributes.None;
            LabelTab2.FontAttributes = FontAttributes.Bold;
            LabelTab3.FontAttributes = FontAttributes.None;
            LabelTab4.FontAttributes = FontAttributes.None;
            LabelTab5.FontAttributes = FontAttributes.None;


            BV1.IsVisible = false;
            BV2.IsVisible = true;
            BV3.IsVisible = false;
            BV4.IsVisible = false;
            BV5.IsVisible = false;

        }

        void Tab3_Tapped(object sender, System.EventArgs e)
        {
            LabelTab1.FontAttributes = FontAttributes.None;
            LabelTab2.FontAttributes = FontAttributes.None;
            LabelTab3.FontAttributes = FontAttributes.Bold;
            LabelTab4.FontAttributes = FontAttributes.None;
            LabelTab5.FontAttributes = FontAttributes.None;


            BV1.IsVisible = false;
            BV2.IsVisible = false;
            BV3.IsVisible = true;
            BV4.IsVisible = false;
            BV5.IsVisible = false;

        }

        void Tab4_Tapped(object sender, System.EventArgs e)
        {
            LabelTab1.FontAttributes = FontAttributes.None;
            LabelTab2.FontAttributes = FontAttributes.None;
            LabelTab3.FontAttributes = FontAttributes.Bold;
            LabelTab4.FontAttributes = FontAttributes.Bold;
            LabelTab5.FontAttributes = FontAttributes.None;


            BV1.IsVisible = false;
            BV2.IsVisible = false;
            BV3.IsVisible = false;
            BV4.IsVisible = true;
            BV5.IsVisible = false;

        }

        void Tab5_Tapped(object sender, System.EventArgs e)
        {
            LabelTab1.FontAttributes = FontAttributes.None;
            LabelTab2.FontAttributes = FontAttributes.None;
            LabelTab3.FontAttributes = FontAttributes.None;
            LabelTab4.FontAttributes = FontAttributes.None;
            LabelTab5.FontAttributes = FontAttributes.Bold;


            BV1.IsVisible = false;
            BV2.IsVisible = false;
            BV3.IsVisible = false;
            BV4.IsVisible = false;
            BV5.IsVisible = true;

           
        }

    }
}
