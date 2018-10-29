using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using static City_Center.Models.MoaSpaResultado;
using City_Center.PopUp;
using Rg.Plugins.Popup.Extensions;
using City_Center.ViewModels;
using City_Center.Clases;
using City_Center.Helper;
using Acr.UserDialogs;

namespace City_Center.Page
{
    public partial class Hotel : ContentPage
    {
        public WebViewHotel _webHotel;

        HotelViewModel Hotelito = new HotelViewModel();


        public Hotel()
        {
            InitializeComponent();

            MainViewModel.GetInstance().Hotel = new HotelViewModel();

            FechaInicio.Text = String.Format("{0:dd/MM/yyyy}", DateTime.Today);
            FechaFinal.Text = String.Format("{0:dd/MM/yyyy}", DateTime.Today.AddDays(1));

            _webHotel = new WebViewHotel();

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

        async void Handle_Clicked(object sender, System.EventArgs e)
        {
            try
            {
                if (FechaInicio.Text == "00/00/0000")
                {
                    await Mensajes.Alerta("Fecha inicial requerida.");
                    return;
                }

                if (FechaFinal.Text == "00/00/0000")
                {
                    await Mensajes.Alerta("Fecha inicial requerida.");
                    return;
                }

                //DateTime fecha1 = Convert.ToDateTime(Application.Current.Properties["FechaNacimiento"].ToString());

                string Dia = FechaInicio.Text.Substring(0, 2);
                string Mes = FechaInicio.Text.Substring(3, 2);
                string Año = FechaInicio.Text.Substring(6, 4);


                string Dia2 = FechaFinal.Text.Substring(0, 2);
                string Mes2 = FechaFinal.Text.Substring(3, 2);
                string Año2 = FechaFinal.Text.Substring(6, 4);


                DateTime Fecha1 = Convert.ToDateTime(Año + "-" + Mes + "-" + Dia);
                DateTime Fecha2 = Convert.ToDateTime(Año2 + "-" + Mes2 + "-" + Dia2);

                if (Fecha2.Date < Fecha1.Date)
                {
                    await Mensajes.Alerta("La fecha final no puede ser menor a la fecha inicial");
                }
                else
                {
                    VariablesGlobales.FechaInicio = Fecha1.Date;
                    VariablesGlobales.FechaFin = Fecha2.Date;
                    VariablesGlobales.NumeroHuespedes = Convert.ToInt32(NoPersona.Text);

                    await ((MasterPage)Application.Current.MainPage).Detail.Navigation.PushAsync(_webHotel);

                    //await Navigation.PushPopupAsync(_webHotel);
                }
            }
            catch (Exception ex)
            {
                //await DisplayAlert("oj", ex.ToString(), "ok");
                await Mensajes.Alerta("No se pudo acceder a las reservaciones, intente mas tarde.");
            }

        }

        void Tab1_Tapped(object sender, System.EventArgs e)
        {
            LabelTab1.TextColor = Color.FromHex("#E8F3F4");
            LabelTab2.TextColor = Color.FromHex("#6FB7C0");
            LabelTab3.TextColor = Color.FromHex("#6FB7C0");

            BV1.IsVisible = true;
            BV2.IsVisible = false;
            BV3.IsVisible = false;

            SL1.IsVisible = true;
            SL2.IsVisible = false;
            SL3.IsVisible = false;
        }

        void Tab2_Tapped(object sender, System.EventArgs e)
        {
            LabelTab1.TextColor = Color.FromHex("#6FB7C0");
            LabelTab2.TextColor = Color.FromHex("#E8F3F4");
            LabelTab3.TextColor = Color.FromHex("#6FB7C0");

            BV1.IsVisible = false;
            BV2.IsVisible = true;
            BV3.IsVisible = false;

            SL1.IsVisible = false;
            SL2.IsVisible = true;
            SL3.IsVisible = false;
        }

        void Tab3_Tapped(object sender, System.EventArgs e)
        {
            LabelTab1.TextColor = Color.FromHex("#6FB7C0");
            LabelTab2.TextColor = Color.FromHex("#6FB7C0");
            LabelTab3.TextColor = Color.FromHex("#E8F3F4");

            BV1.IsVisible = false;
            BV2.IsVisible = false;
            BV3.IsVisible = true;

            SL1.IsVisible = false;
            SL2.IsVisible = false;
            SL3.IsVisible = true;
        }

        void Handle_ItemSelected(object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
        {
            try
            {
                var Seleccion = e.SelectedItem;

                if (Seleccion != null)
                {
                    var selection = e.SelectedItem as MoaSpaDetalle;

                    Img1provisional.Source = selection.gal_imagen;


                }
            }
            catch (Exception)
            {

            }


        }

        async void FechaInicio_Focused(object sender, Xamarin.Forms.FocusEventArgs e)
        {
#if __IOS__
            DependencyService.Get<IForceKeyboardDismissalService>().DismissKeyboard();
#endif
            var result = await UserDialogs.Instance.DatePromptAsync(new DatePromptConfig
            {
                IsCancellable = true,
                MinimumDate = DateTime.Now.AddDays(0),
                CancelText = "CANCELAR",
                Title = "Llegada"
            });


            if (result.Ok)
            {
                FechaInicio.Text = String.Format("{0:dd/MM/yyyy}", result.SelectedDate);
                FechaInicio.Unfocus();
                DependencyService.Get<IForceKeyboardDismissalService>().DismissKeyboard();

            }
            else
            {
                FechaInicio.Unfocus();
                DependencyService.Get<IForceKeyboardDismissalService>().DismissKeyboard();
            }

            //String.Format("{0:dd MMMM yyyy}"

        }

        async void FechaFin_Focused(object sender, Xamarin.Forms.FocusEventArgs e)
        {
#if __IOS__
            DependencyService.Get<IForceKeyboardDismissalService>().DismissKeyboard();
#endif
            var result = await UserDialogs.Instance.DatePromptAsync(new DatePromptConfig
            {
                IsCancellable = true,
                MinimumDate = DateTime.Now.AddDays(0),
                CancelText = "CANCELAR",
                Title = "Salida"
            });


            if (result.Ok)
            {
                FechaFinal.Text = String.Format("{0:dd/MM/yyyy}", result.SelectedDate);
                FechaFinal.Unfocus();
                DependencyService.Get<IForceKeyboardDismissalService>().DismissKeyboard();

            }
            else
            {
                FechaFinal.Unfocus();
                DependencyService.Get<IForceKeyboardDismissalService>().DismissKeyboard();

            }

           

        }


        void Promociones1_PositionSelected(object sender, Xamarin.Forms.SelectedPositionChangedEventArgs e)
        {
            int Position = Convert.ToInt32(e.SelectedPosition);

            if (Position == VariablesGlobales.RegistrosHotelPromociones)
            {
                CarruselPromociones.Position = 1;
            }
            else if (Position == 0)
            {
                CarruselPromociones.Position = VariablesGlobales.RegistrosHotelPromociones-1;
            }     
        }

        void Promociones2_PositionSelected(object sender, Xamarin.Forms.SelectedPositionChangedEventArgs e)
        {
            int Position = Convert.ToInt32(e.SelectedPosition);

            if (Position == VariablesGlobales.RegistrosHotelPromociones2)
            {
                CarruselPromociones2.Position = 1;
            }
            else if (Position == 0)
            {
                CarruselPromociones2.Position = VariablesGlobales.RegistrosHotelPromociones2-1;
            }   
        }



        void Promociones1_BatchCommitted(object sender, Xamarin.Forms.Internals.EventArg<Xamarin.Forms.VisualElement> e)
        {
            if (Hotelito.PromocionesDetalle != null)
            {
                CarruselPromociones.ItemsSource = Hotelito.PromocionesDetalle;
                CarruselPromociones.Position = 1;

            }
        }

        void Promociones2_BatchCommitted(object sender, Xamarin.Forms.Internals.EventArg<Xamarin.Forms.VisualElement> e)
        {
            if (Hotelito.PromocionesDetalle2 != null)
            {
                CarruselPromociones2.ItemsSource = Hotelito.PromocionesDetalle2;
                CarruselPromociones2.Position = 1;

            }
        }

	}

}
