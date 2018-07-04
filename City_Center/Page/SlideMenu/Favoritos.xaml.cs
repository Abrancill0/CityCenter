using System;
using System.Collections.Generic;
using City_Center.Clases;
using Xamarin.Forms;

namespace City_Center.Page.SlideMenu
{
    public partial class Favoritos : ContentPage
    {
        public Favoritos()
        {
            InitializeComponent();

            NavigationPage.SetTitleIcon(this, "logo@2x.png");
        }

        protected override void OnDisappearing()
        {
            ActualizaBarra.Cambio(VariablesGlobales.VentanaActual);
            GC.Collect();
            base.OnDisappearing();
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
