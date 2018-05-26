using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using static City_Center.Models.EventosResultado;
using GalaSoft.MvvmLight.Helpers;
using City_Center.ViewModels;

namespace City_Center.Page.SlideMenu
{
    public partial class Show : ContentPage
    {
        ShowsViewModel showsito = new ShowsViewModel();

        public Show()
        {
            InitializeComponent();

            NavigationPage.SetTitleIcon(this, "logowhite.png");
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            //if (showsito.EventosDetalle !=null)
            //{
            //    ListaShow.ItemsSource = null;
            //    ListaShow.ItemsSource = showsito.EventosDetalle;
            //}
  
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

                    if (image.BackgroundColor != Color.Transparent)
                    {
                        image.BackgroundColor = Color.Transparent;
                        image.Source = "FavoritoOK";                        
                    }
                    else
                    {
                        image.BackgroundColor = Color.White;
                        image.Source = "Favorito";   
                    }

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

                    if (image.BackgroundColor != Color.Transparent)
                    {
                        image.BackgroundColor = Color.Transparent;
                        image.Source = "Favorito";
                    }
                    else
                    {
                        image.BackgroundColor = Color.White;
                        image.Source = "FavoritoOK";
                    }

                }

            }
            catch (Exception)
            {


            }
        }

    }
}
