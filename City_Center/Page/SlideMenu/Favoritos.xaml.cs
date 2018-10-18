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

        FavoritosViewModel Fav = new FavoritosViewModel();

        public Favoritos()
        {
            InitializeComponent();

            NavigationPage.SetTitleIcon(this, "logo@2x.png");

        }



        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            //if (Fav.FavoritoDetalle != null)
            //{
            //    if (Fav.FavoritoDetalle.Count > 1)
            //    {
            //        Indicador.IsVisible = true;
            //    }
            //    else
            //    {
            //        Indicador.IsVisible = false;
            //    }
            //}
            try
            {
                if (CarruselFavoritos.ItemsCount > 1)
                {
                    Indicador.IsVisible = true;
                }
                else
                {
                    Indicador.IsVisible = false;
                }
            }
            catch (Exception ex)
            {

            }


        }


        protected override void OnAppearing()
        {
            base.OnAppearing();

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

                    //CarruselFavoritos.Items(CarruselFavoritos.CurrentIndex);

                    CarruselFavoritos.Items.RemoveAt(CarruselFavoritos.CurrentIndex);

                   // CarruselFavoritos.Items.RemoveAt(CarruselFavoritos.CurrentIndex);


                }

            }
            catch (Exception)
            {


            }
        }


        void Handle_PropertyChanging(object sender, Xamarin.Forms.PropertyChangingEventArgs e)
        {
            try
            {
                if (CarruselFavoritos.ItemsCount > 1)
                {
                    Indicador.IsVisible = true;
                }
                else
                {
                    Indicador.IsVisible = false;
                }
            }
            catch (Exception ex)
            {

            }        }
    }
}
