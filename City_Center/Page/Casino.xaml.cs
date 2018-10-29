using System;
using System.Collections.Generic;
using City_Center.ViewModels;
using Xamarin.Forms;
using City_Center.Models;
using static City_Center.Models.SalaPokerResultado;
using City_Center.Clases;
using System.Net.Http;
using static City_Center.Models.TarjetasResultado;
using System.Threading.Tasks;

namespace City_Center.Page
{
    public partial class Casino : ContentPage
    {
        string[] ListaOpciones;

        CasinoViewModel Casinito = new CasinoViewModel();

        public Casino()
        {

            InitializeComponent();
          
            ListaOpciones = new string[] { "vip1", "vip2", "svip1", "svip2" };

            NavigationPage.SetTitleIcon(this, "logo@2x.png");
            Img2provisional.Source = "vip1";
            listaCasino.ItemsSource = ListaOpciones;


           //  MainViewModel.GetInstance().Casino = new CasinoViewModel();
            // loadTarjet();

          //  CarruselDestacados.ItemsSource = Casinito.DestacadosDetalle;

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            GC.Collect();
        }

        void Tab1_Tapped(object sender, System.EventArgs e)
        {
            LabelTab1.TextColor = Color.FromHex("#FDE7EE");
            LabelTab2.TextColor = Color.FromHex("#F282A9");
            LabelTab3.TextColor = Color.FromHex("#F282A9");
            LabelTab4.TextColor = Color.FromHex("#F282A9");

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
            LabelTab1.TextColor = Color.FromHex("#F282A9");
            LabelTab2.TextColor = Color.FromHex("#FDE7EE");
            LabelTab3.TextColor = Color.FromHex("#F282A9");
            LabelTab4.TextColor = Color.FromHex("#F282A9");

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
            LabelTab1.TextColor = Color.FromHex("#F282A9");
            LabelTab2.TextColor = Color.FromHex("#F282A9");
            LabelTab3.TextColor = Color.FromHex("#FDE7EE");
            LabelTab4.TextColor = Color.FromHex("#F282A9");

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
            LabelTab1.TextColor = Color.FromHex("#F282A9");
            LabelTab2.TextColor = Color.FromHex("#F282A9");
            LabelTab3.TextColor = Color.FromHex("#F282A9");
            LabelTab4.TextColor = Color.FromHex("#FDE7EE");

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
            catch (Exception)
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
            catch (Exception)
            {

            }
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
                        image.Source = "Fav";
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
                        image.Source = "Fav";
                    }

                }

            }
            catch (Exception)
            {


            }
        }

        //void Torneo_PositionSelected(object sender, Xamarin.Forms.SelectedPositionChangedEventArgs e)
        //{
        //    int Position = Convert.ToInt32(e.SelectedPosition);

        //    if (Position == VariablesGlobales.RegistrosCasinoTorneo)
        //    {
        //        CarruselTorneos.Position = 1;
        //    }
        //    else if (Position == 0)
        //    {
        //        CarruselTorneos.Position = VariablesGlobales.RegistrosCasinoTorneo - 1;
        //    }
        //}

        //void Destacado_PositionSelected(object sender, Xamarin.Forms.SelectedPositionChangedEventArgs e)
        //{
        //    int Position = Convert.ToInt32(e.SelectedPosition);

        //    if (Position == VariablesGlobales.RegistrosCasinoDestacados)
        //    {
        //        CarruselDestacados.Position = 1;
        //    }
        //    else if (Position == 0)
        //    {
        //        CarruselDestacados.Position = VariablesGlobales.RegistrosCasinoDestacados - 1;
        //    }
        //}

        //void Promociones_PositionSelected(object sender, Xamarin.Forms.SelectedPositionChangedEventArgs e)
        //{
        //    int Position = Convert.ToInt32(e.SelectedPosition);

        //    if (Position == VariablesGlobales.RegistrosCasinoPromociones)
        //    {
        //        CarruselPromociones.Position = 1;
        //    }
        //    else if (Position == 0)
        //    {
        //        CarruselPromociones.Position = VariablesGlobales.RegistrosCasinoPromociones - 1;
        //    }
        //}

        //void Destacado_BatchCommitted(object sender, Xamarin.Forms.Internals.EventArg<Xamarin.Forms.VisualElement> e)
        //{
        //    if (Casinito.DestacadosDetalle != null)
        //    {
        //        CarruselDestacados.ItemsSource = Casinito.DestacadosDetalle;
        //        CarruselDestacados.Position = 1;
               
        //    }
        //}

        //void Promociones_BatchCommitted(object sender, Xamarin.Forms.Internals.EventArg<Xamarin.Forms.VisualElement> e)
        //{
        //    if (Casinito.PromocionesDetalle != null)
        //    {
        //        CarruselPromociones.ItemsSource = Casinito.PromocionesDetalle;
        //        CarruselPromociones.Position = 1;

        //    }
        //}
    }
}
