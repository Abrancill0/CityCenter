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
            //MainViewModel.GetInstance().Casino = new CasinoViewModel();
         
            ListaOpciones = new string[] { "vip1", "vip2", "svip1", "svip2" };

            NavigationPage.SetTitleIcon(this, "logo@2x.png");
            Img2provisional.Source = "vip1";
            listaCasino.ItemsSource = ListaOpciones;

          //  MainViewModel.GetInstance().Casino = new CasinoViewModel();
           // loadTarjet();

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
    
        void PositionSelected_CT(object sender, CarouselView.FormsPlugin.Abstractions.PositionSelectedEventArgs e)
        {
            try
            {
                VariablesGlobales.IndiceCasinoTorneo = e.NewValue;
            }
            catch (Exception)
            {

            }

        }

        void Scrolled_CT(object sender, CarouselView.FormsPlugin.Abstractions.ScrolledEventArgs e)
        {
            try
            {
                string Direccion = Convert.ToString(e.Direction);

                if (VariablesGlobales.IndiceCasinoTorneo >= VariablesGlobales.RegistrosCasinoTorneo && Direccion == "Right")
                {
                    CarruselTorneos.AnimateTransition = false;
                    CarruselTorneos.Position = 0;

                }
                else if (VariablesGlobales.IndiceCasinoTorneo <= 1 && Direccion == "Left")
                {
                    CarruselTorneos.AnimateTransition = false;
                    CarruselTorneos.Position = VariablesGlobales.RegistrosCasinoTorneo + 1;
                }
            }
            catch (Exception ex)
            {
                DisplayAlert("Error", ex.ToString(), "OK");
            }


        }

        void PositionSelected_DT(object sender, CarouselView.FormsPlugin.Abstractions.PositionSelectedEventArgs e)
        {
            try
            {
                VariablesGlobales.IndiceCasinoDestacados = e.NewValue;
            }
            catch (Exception)
            {

            }

        }

        void Scrolled_DT(object sender, CarouselView.FormsPlugin.Abstractions.ScrolledEventArgs e)
        {
            try
            {
                string Direccion = Convert.ToString(e.Direction);

                if (VariablesGlobales.IndiceCasinoDestacados >= VariablesGlobales.RegistrosCasinoDestacados && Direccion == "Right")
                {
                    CarruselDestacados.AnimateTransition = false;
                    CarruselDestacados.Position = 0;

                }
                else if (VariablesGlobales.IndiceCasinoDestacados <= 1 && Direccion == "Left")
                {
                    CarruselDestacados.AnimateTransition = false;
                    CarruselDestacados.Position = VariablesGlobales.RegistrosCasinoDestacados + 1;
                }
            }
            catch (Exception)
            {
                
            }

        }

        void PositionSelected_CP(object sender, CarouselView.FormsPlugin.Abstractions.PositionSelectedEventArgs e)
        {
            try
            {
                VariablesGlobales.IndiceCasinoPromociones = e.NewValue;
            }
            catch (Exception)
            {

            } 
        }

        void Scrolled_CP(object sender, CarouselView.FormsPlugin.Abstractions.ScrolledEventArgs e)
        {
            try
            {
                string Direccion = Convert.ToString(e.Direction);

                if (VariablesGlobales.IndiceCasinoPromociones >= VariablesGlobales.RegistrosCasinoPromociones && Direccion == "Right")
                {
                    CarruselPromociones.AnimateTransition = false;
                    CarruselPromociones.Position = 0;

                }
                else if (VariablesGlobales.IndiceCasinoPromociones <= 1 && Direccion == "Left")
                {
                    CarruselPromociones.AnimateTransition = false;
                    CarruselPromociones.Position = VariablesGlobales.RegistrosCasinoPromociones + 1;
                }
            }
            catch (Exception ex)
            {
                DisplayAlert("Error", ex.ToString(), "OK");
            }
        }
    }
}
