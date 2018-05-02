using System;
using System.Collections.Generic;
using City_Center.ViewModels;
using Xamarin.Forms;
using City_Center.Models;
using static City_Center.Models.SalaPokerResultado;

namespace City_Center.Page
{
    public partial class Casino : ContentPage
    {
        string[] ListaOpciones;

        public Casino()
        {
            InitializeComponent();
            ListaOpciones = new string[] { "http://lorempixel.com/400/200/sports/", "http://lorempixel.com/400/200/sports/1", "https://picsum.photos/400/200"};
            listaCasino.ItemsSource = ListaOpciones;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

        }

        void Tab1_Tapped(object sender, System.EventArgs e)
        {
            LabelTab1.FontAttributes = FontAttributes.Bold;
            LabelTab2.FontAttributes = FontAttributes.None;
            LabelTab3.FontAttributes = FontAttributes.None;
            LabelTab4.FontAttributes = FontAttributes.None;

            BV1.IsVisible = true;
            BV2.IsVisible = false;
            BV3.IsVisible = false;
            BV4.IsVisible = false;

            SL1.IsVisible = true;
            SL2.IsVisible = false;
            SL3.IsVisible = false;
            SL4.IsVisible = false;
        }

        void Tab2_Tapped(object sender, System.EventArgs e)
        {
            LabelTab1.FontAttributes = FontAttributes.None;
            LabelTab2.FontAttributes = FontAttributes.Bold;
            LabelTab3.FontAttributes = FontAttributes.None;
            LabelTab4.FontAttributes = FontAttributes.None;

            BV1.IsVisible = false;
            BV2.IsVisible = true;
            BV3.IsVisible = false;
            BV4.IsVisible = false;

            SL1.IsVisible = false;
            SL2.IsVisible = true;
            SL3.IsVisible = false;
            SL4.IsVisible = false;
        }

        void Tab3_Tapped(object sender, System.EventArgs e)
        {
            LabelTab1.FontAttributes = FontAttributes.None;
            LabelTab2.FontAttributes = FontAttributes.None;
            LabelTab3.FontAttributes = FontAttributes.Bold;
            LabelTab4.FontAttributes = FontAttributes.None;

            BV1.IsVisible = false;
            BV2.IsVisible = false;
            BV3.IsVisible = true;
            BV4.IsVisible = false;

            SL1.IsVisible = false;
            SL2.IsVisible = false;
            SL3.IsVisible = true;
            SL4.IsVisible = false;
        }

        void Tab4_Tapped(object sender, System.EventArgs e)
        {
            LabelTab1.FontAttributes = FontAttributes.None;
            LabelTab2.FontAttributes = FontAttributes.None;
            LabelTab3.FontAttributes = FontAttributes.None;
            LabelTab4.FontAttributes = FontAttributes.Bold;

            BV1.IsVisible = false;
            BV2.IsVisible = false;
            BV3.IsVisible = false;
            BV4.IsVisible = true;

            SL1.IsVisible = false;
            SL2.IsVisible = false;
            SL3.IsVisible = false;
            SL4.IsVisible = true;
        }


        void Handle_ItemSelected(object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
        {
            try
            {
                var Seleccion = e.SelectedItem;

                if (Seleccion != null)
                {
                    var selection = e.SelectedItem as SalaPokerDetalle;

                    Img1provisional.Source = selection.spo_imagen;


                }
            }
            catch (Exception ex)
            {

            }
           

        }

        void Handle_ItemSelected2(object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
        {
            try
            {
                var Seleccion = e.SelectedItem;

                if (Seleccion != null)
                {
                    var selection = e.SelectedItem;

                    Img2provisional.Source = selection.ToString();
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}