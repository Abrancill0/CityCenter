using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace City_Center.Page
{
    public partial class Torneos : ContentPage
    {
        public Torneos()
        {
            InitializeComponent();
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
