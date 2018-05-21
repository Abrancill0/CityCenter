using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Plugin.Messaging;

namespace City_Center.Page
{
    public partial class DetalleShow : ContentPage
    {
        public DetalleShow()
        {
            InitializeComponent();
            NavigationPage.SetTitleIcon(this, "logo.png");
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
