using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace City_Center.Page.SlideMenu
{
    public partial class Favoritos : ContentPage
    {
        public Favoritos()
        {
            InitializeComponent();

<<<<<<< HEAD
            NavigationPage.SetTitleIcon(this, "logo@2x.png");
=======
            NavigationPage.SetTitleIcon(this, "logo_hdpi.png");
>>>>>>> 66a2e5f3c284a595e654f62d503db44111e45e87
        }

        protected override void OnDisappearing()
        {

            base.OnDisappearing();

            GC.Collect();

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

                    image.Source = "Favorito";
                   
                }

            }
            catch (Exception)
            {


            }
        }

      

    }
}
