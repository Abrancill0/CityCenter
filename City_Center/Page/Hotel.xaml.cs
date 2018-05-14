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
       

        public Hotel()
        {
            //Resources = new App().Resources;
            InitializeComponent();

            //var images = new List<String>
            //{
            //    "Hab_1",
            //    "Hab_2",
            //    "Hab_3",
            //    "Hab_4",
            //    "Hab_5",
            //    "Hab_6"
            //};
            _webHotel = new WebViewHotel();
            NavigationPage.SetTitleIcon(this, "logo.png");



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
                if (FechaInicio.Text == "00/00/00")
                {
                    await Mensajes.Info("Fecha inicial requerida.");
                    return;
                }

                if (FechaFinal.Text == "00/00/00")
                {
                    await Mensajes.Info("Fecha inicial requerida.");
                    return;
                }

                DateTime Fecha1 = DateTime.Parse(FechaInicio.Text);
                DateTime Fecha2 = DateTime.Parse(FechaFinal.Text);

                if (Fecha1.Date < Fecha2.Date)
                {
                    await Mensajes.Info("La fecha final no puede ser menor a la fecha inicial");
                }
                else
                {
                    VariablesGlobales.FechaInicio = Convert.ToDateTime(FechaInicio.Text);
                    VariablesGlobales.FechaFin = Convert.ToDateTime(FechaFinal.Text);
                    VariablesGlobales.NumeroHuespedes = Convert.ToInt32(NoPersona.Text);

                    await Navigation.PushPopupAsync(_webHotel);
                }
            }
            catch (Exception ex)
            {
                await Mensajes.Info("No se pudo acceder a las reservaciones, intente mas tarde.");
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
            catch (Exception ex)
            {

            }


        }
    
		async void FechaInicio_Focused(object sender, Xamarin.Forms.FocusEventArgs e)
        {

            var result = await UserDialogs.Instance.DatePromptAsync(new DatePromptConfig
            {
                IsCancellable = true,
                MinimumDate = DateTime.Now.AddDays(0)
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
            var result = await UserDialogs.Instance.DatePromptAsync(new DatePromptConfig
            {
                IsCancellable = true,
                MinimumDate = DateTime.Now.AddDays(0)
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

            //String.Format("{0:dd MMMM yyyy}"

        }
	
	
	}
}
