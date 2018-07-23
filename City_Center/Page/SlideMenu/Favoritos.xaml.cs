using System;
using System.Collections.Generic;
using System.Linq;
using City_Center.Clases;
using City_Center.ViewModels;
using Xamarin.Forms;

namespace City_Center.Page.SlideMenu
{
    public partial class Favoritos : ContentPage
    {
       // FavoritosViewModel FD = new FavoritosViewModel();

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
                   
                   // CarruselFavoritos.ItemsSource = ListaOpciones;

                    //CarruselFavoritos.Items = FD.FavoritoDetalle.Where(l=> l.gua_id>0);

                }

            }
            catch (Exception)
            {


            }
        }

      

    }
}
