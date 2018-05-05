using System;
using System.Collections.Generic;

using Xamarin.Forms;
using static City_Center.Models.SalasEventosResultado;

namespace City_Center.Page
{
    public partial class MasInfo : ContentPage
    {
        public MasInfo()
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
            LabelTab1.TextColor = Color.FromHex("#E7E9F6");
            LabelTab2.TextColor = Color.FromHex("#8E99D3");
            LabelTab3.TextColor = Color.FromHex("#8E99D3");

            BV1.IsVisible = true;
            BV2.IsVisible = false;
            BV3.IsVisible = false;

            SL1.IsVisible = true;
            SL2.IsVisible = false;
            SL3.IsVisible = false;
        }

        void Tab2_Tapped(object sender, System.EventArgs e)
        {
            LabelTab1.TextColor = Color.FromHex("#8E99D3");
            LabelTab2.TextColor = Color.FromHex("#E7E9F6");
            LabelTab3.TextColor = Color.FromHex("#8E99D3");

            BV1.IsVisible = false;
            BV2.IsVisible = true;
            BV3.IsVisible = false;

            SL1.IsVisible = false;
            SL2.IsVisible = true;
            SL3.IsVisible = false;
        }

        void Tab3_Tapped(object sender, System.EventArgs e)
        {
            LabelTab1.TextColor = Color.FromHex("#8E99D3");
            LabelTab2.TextColor = Color.FromHex("#8E99D3");
            LabelTab3.TextColor = Color.FromHex("#E7E9F6");

            BV1.IsVisible = false;
            BV2.IsVisible = false;
            BV3.IsVisible = true;

            SL1.IsVisible = false;
            SL2.IsVisible = false;
            SL3.IsVisible = true;

        }

        void Handle_ItemSelected(object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
        {
            try
            {
                var Seleccion = e.SelectedItem;

                if (Seleccion != null)
                {
                    var selection = e.SelectedItem as SalasEventosDetalle;

                    Img1provisional.Source = selection.gal_imagen;
                    Img1provisional2.Source = selection.gal_imagen;

                }
            }
            catch (Exception ex)
            {

            }


        }

        void Handle_Clicked(object sender, System.EventArgs e)
        {
            if (Frame1.IsVisible == false)
            {
                Stack1.IsVisible = false;
                Frame1.IsVisible = true;

            }
            else
            {

                Stack1.IsVisible = true;
                Frame1.IsVisible = false;
            }

        }

            void Handle_Clicked2(object sender, System.EventArgs e)
            {
                if (Frame2.IsVisible == false)
                {
                    Stack2.IsVisible = false;
                    Frame2.IsVisible = true;

                }
                else
                {

                    Stack2.IsVisible = true;
                    Frame2.IsVisible = false;
                }

        }
    }
}