using System;
using System.Collections.Generic;
using City_Center.Clases;
using City_Center.ViewModels;
using Xamarin.Forms;

namespace City_Center.Page
{
    public partial class Gastronomia : ContentPage
    {
        public Gastronomia()
        {
            InitializeComponent();
            //   MainViewModel.GetInstance().Gastronomia = new GastronomiaViewModel();

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
            LabelTab1.TextColor = Color.FromHex("#FFE8E1");
            LabelTab2.TextColor = Color.FromHex("#FFA081");
            LabelTab3.TextColor = Color.FromHex("#FFA081");

            BV1.IsVisible = true;
            BV2.IsVisible = false;
            BV3.IsVisible = false;

        }

        void Tab2_Tapped(object sender, System.EventArgs e)
        {
            LabelTab1.TextColor = Color.FromHex("#FFA081");
            LabelTab2.TextColor = Color.FromHex("#FFE8E1");
            LabelTab3.TextColor = Color.FromHex("#FFA081");

            BV1.IsVisible = false;
            BV2.IsVisible = true;
            BV3.IsVisible = false;

        }

        void Tab3_Tapped(object sender, System.EventArgs e)
        {
            LabelTab1.TextColor = Color.FromHex("#FFA081");
            LabelTab2.TextColor = Color.FromHex("#FFA081");
            LabelTab3.TextColor = Color.FromHex("#FFE8E1");
            BV1.IsVisible = false;
            BV2.IsVisible = false;
            BV3.IsVisible = true;


        }


        void PositionSelected_GP(object sender, CarouselView.FormsPlugin.Abstractions.PositionSelectedEventArgs e)
        {
           
                VariablesGlobales.IndiceGastronomiaPromociones = e.NewValue;
           
            #if __IOS__
            try
            {
                if (VariablesGlobales.validacionIORestaurante == 1)
                {
                    if (e.NewValue != 0)
                    {
                        e.NewValue = 0;
                        CarruselPromociones.Position = 0;
                    }
                    VariablesGlobales.IndiceGastronomiaPromociones = 1;
                }
                else if (VariablesGlobales.validacionIORestaurante == 2)
                {

                    CarruselPromociones.Position = VariablesGlobales.RegistrosGastronomiaPromociones + 1;
                    VariablesGlobales.IndiceGastronomiaPromociones = VariablesGlobales.RegistrosGastronomiaPromociones;
                }

            }
            catch (Exception)
            {

            }
#endif

        }

        void Scrolled_GP(object sender, CarouselView.FormsPlugin.Abstractions.ScrolledEventArgs e)
        {
#if __IOS__
            try
            {
                string Direccion = Convert.ToString(e.Direction);

                if (VariablesGlobales.IndiceGastronomiaPromociones == VariablesGlobales.RegistrosGastronomiaPromociones && Direccion == "Right" && e.NewValue == 100)
                {
                    VariablesGlobales.validacionIORestaurante = 1;
                    CarruselPromociones.Position = 0;
                    CarruselPromociones.AnimateTransition = false;

                }
                else if (VariablesGlobales.IndiceGastronomiaPromociones == 1 && Direccion == "Left" && e.NewValue == 100)
                {
                    CarruselPromociones.AnimateTransition = false;
                    VariablesGlobales.validacionIORestaurante = 2;
                    CarruselPromociones.Position = VariablesGlobales.RegistrosGastronomiaPromociones + 1;
                }
                else if (e.NewValue != 100)
                {
                    VariablesGlobales.validacionIORestaurante = 0;
                }
            }
            catch (Exception ex)
            {
                DisplayAlert("Error", ex.ToString(), "OK");
            }
#endif


#if __ANDROID__
            try
            {
                string Direccion = Convert.ToString(e.Direction);

                if (VariablesGlobales.IndiceGastronomiaPromociones >= VariablesGlobales.RegistrosGastronomiaPromociones && Direccion == "Right")
                {
                    CarruselPromociones.AnimateTransition = false;
                    CarruselPromociones.Position = 0;

                }
                else if (VariablesGlobales.IndiceGastronomiaPromociones <= 1 && Direccion == "Left")
                {
                    CarruselPromociones.AnimateTransition = false;
                    CarruselPromociones.Position = VariablesGlobales.RegistrosGastronomiaPromociones + 1;
                }
            }
            catch (Exception ex)
            {
                DisplayAlert("Error", ex.ToString(), "OK");
            }
#endif
        }

    }
}
