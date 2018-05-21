using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using static City_Center.Models.EventosResultado;

namespace City_Center.Page.SlideMenu
{
    public partial class Show : ContentPage
    {

        ObservableCollection<EventosDetalle> items;

        public Show()
        {
            InitializeComponent();

            NavigationPage.SetTitleIcon(this, "logo.png");
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

		void CambiaIcono(object sender, System.EventArgs e)
        {
            try
            {
                bool isLoggedIn = Application.Current.Properties.ContainsKey("IsLoggedIn") ?
                                     (bool)Application.Current.Properties["IsLoggedIn"] : false;

                if (isLoggedIn)
                {

                    Image image = sender as Image;

                    image.Source = "FavoritoOK";
                }

            }
            catch (Exception)
            {


            }
        }


        void CambiaIcono2(object sender, System.EventArgs e)
        {
            try
            {
                bool isLoggedIn = Application.Current.Properties.ContainsKey("IsLoggedIn") ?
                                     (bool)Application.Current.Properties["IsLoggedIn"] : false;

                if (isLoggedIn)
                {

                    Image image = sender as Image;

					image.Source = "Favorito";
                }

            }
            catch (Exception)
            {


            }
        }

    }
}
