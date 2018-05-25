using System;
using System.Collections.Generic;
using City_Center.ViewModels;
using Xamarin.Forms;

namespace City_Center.Page
{
    public partial class Gastronomia : ContentPage
    {
        public Gastronomia()
        {
            InitializeComponent();

           
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

        }


        // MainViewModel.GetInstance().Gastronomia = new GastronomiaViewModel();

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            GC.Collect();

        }

        void Tab1_Tapped(object sender, System.EventArgs e)
        {
            LabelTab1.TextColor = Color.FromHex("#FFE8E1");
            LabelTab2.TextColor = Color.FromHex("#FFA081");
            LabelTab3.TextColor = Color.FromHex("#FFA081");

            BV1.IsVisible = true;
            BV2.IsVisible = false;
            BV3.IsVisible = false;

        }

        void Tab2_Tapped(object sender, System.EventArgs e)
        {
            LabelTab1.TextColor = Color.FromHex("#FFA081");
            LabelTab2.TextColor = Color.FromHex("#FFE8E1");
            LabelTab3.TextColor = Color.FromHex("#FFA081");

            BV1.IsVisible = false;
            BV2.IsVisible = true;
            BV3.IsVisible = false;

        }

        void Tab3_Tapped(object sender, System.EventArgs e)
        {
            LabelTab1.TextColor = Color.FromHex("#FFA081");
            LabelTab2.TextColor = Color.FromHex("#FFA081");
            LabelTab3.TextColor = Color.FromHex("#FFE8E1");
            BV1.IsVisible = false;
            BV2.IsVisible = false;
            BV3.IsVisible = true;


        }

    }
}
