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

                    image.Source = "FavoritoOK";
                }

            }
            catch (Exception)
            {


            }
        }
    }
}
