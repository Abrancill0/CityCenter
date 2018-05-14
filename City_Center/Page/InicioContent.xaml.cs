using System;
using System.Collections.Generic;
using Xamarin.Forms.Platform;
using Xamarin.Forms;
using City_Center.PopUp;
using Rg.Plugins.Popup.Extensions;
using City_Center.Clases;
using Acr.UserDialogs;
using City_Center.Helper;

namespace City_Center.Page
{
    public partial class InicioContent : ContentPage
    {
        public WebViewHotel _webHotel;
        
        public InicioContent()
        {
            InitializeComponent();
			//FechaInicio.ShowSoftInputOnFocus = false;

            _webHotel = new WebViewHotel();

        }

        protected override void OnDisappearing()
        {         
            base.OnDisappearing();

            GC.Collect();

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
   
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

				if (FechaFinal.Text =="00/00/00")
				{
					await Mensajes.Info("Fecha inicial requerida.");
					return;
				}


				if (Convert.ToDateTime(FechaFinal.Text + " 12:00:00 a.m.") < Convert.ToDateTime(FechaInicio.Text + " 12:00:00 a.m."))
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
            catch (Exception)
            {
                await Mensajes.Info("No se pudo acceder a las reservaciones, intente mas tarde.");
            }
           
        }

        private void Btn1_Clicked(object sender, EventArgs e)
        {
            SL1.IsVisible = true;
            SL2.IsVisible = false;
            SL3.IsVisible = false;
            SL4.IsVisible = false;

            reservarHotel.Source = "RESERVAHOTEL_S";
            tickets.Source = "TICKETSHOWS";
            reservarMesa.Source = "RESERVATUMESA";
            tienda.Source = "TIENDAONLINE";
        }

        private void Btn2_Clicked(object sender, System.EventArgs e)
        {

            SL1.IsVisible = false;
            SL2.IsVisible = true;
            SL3.IsVisible = false;
            SL4.IsVisible = false;

            reservarHotel.Source = "RESERVAHOTEL";
            tickets.Source = "TICKETSHOWS_S";
            reservarMesa.Source = "RESERVATUMESA";
            tienda.Source = "TIENDAONLINE";
        }

        private void Btn3_Clicked(object sender, System.EventArgs e)
        {

            SL1.IsVisible = false;
            SL2.IsVisible = false;
            SL3.IsVisible = true;
            SL4.IsVisible = false;

            reservarHotel.Source = "RESERVAHOTEL";
            tickets.Source = "TICKETSHOWS";
            reservarMesa.Source = "RESERVATUMESA_S";
            tienda.Source = "TIENDAONLINE";
        }

        private void Btn4_Clicked(object sender, System.EventArgs e)
        {

            SL1.IsVisible = false;
            SL2.IsVisible = false;
            SL3.IsVisible = false;
            SL4.IsVisible = true;

            reservarHotel.Source = "RESERVAHOTEL";
            tickets.Source = "TICKETSHOWS";
            reservarMesa.Source = "RESERVATUMESA";
            tienda.Source = "TIENDAONLINE_S";
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

		async void FechaR1_Focused(object sender, Xamarin.Forms.FocusEventArgs e)
        {
            var result = await UserDialogs.Instance.DatePromptAsync(new DatePromptConfig
            {
                IsCancellable = true,
                MinimumDate = DateTime.Now.AddDays(0)
            });


            if (result.Ok)
            {
                FechaR1.Text = String.Format("{0:dd/MM/yyyy}", result.SelectedDate);
				FechaR1.Unfocus();
                DependencyService.Get<IForceKeyboardDismissalService>().DismissKeyboard();
            }
            else
            {
				FechaR1.Unfocus();
                DependencyService.Get<IForceKeyboardDismissalService>().DismissKeyboard();
            }

            //String.Format("{0:dd MMMM yyyy}"

        }

		async void FechaRango1_Focused(object sender, Xamarin.Forms.FocusEventArgs e)
        {
            var result = await UserDialogs.Instance.DatePromptAsync(new DatePromptConfig
            {
                IsCancellable = true,
                MinimumDate = DateTime.Now.AddDays(0)
            });


            if (result.Ok)
            {
                FechaRango1.Text = String.Format("{0:dd/MM/yyyy}", result.SelectedDate);
				FechaRango1.Unfocus();
                DependencyService.Get<IForceKeyboardDismissalService>().DismissKeyboard();
            }
            else
            {
				FechaRango1.Unfocus();
                DependencyService.Get<IForceKeyboardDismissalService>().DismissKeyboard();
            }

            //String.Format("{0:dd MMMM yyyy}"

        }

		async void FechaRango2_Focused(object sender, Xamarin.Forms.FocusEventArgs e)
        {
            var result = await UserDialogs.Instance.DatePromptAsync(new DatePromptConfig
            {
                IsCancellable = true,
                MinimumDate = DateTime.Now.AddDays(0)
            });


            if (result.Ok)
            {
                FechaRango2.Text = String.Format("{0:dd/MM/yyyy}", result.SelectedDate);
				FechaRango2.Unfocus();
                DependencyService.Get<IForceKeyboardDismissalService>().DismissKeyboard();
            }
            else
            {
				FechaRango2.Unfocus();
                DependencyService.Get<IForceKeyboardDismissalService>().DismissKeyboard();
            }

            //String.Format("{0:dd MMMM yyyy}"

        }

		async void HoraR1_Focused(object sender, Xamarin.Forms.FocusEventArgs e)
		{
			var result = await UserDialogs.Instance.TimePromptAsync(new TimePromptConfig
            {
                 IsCancellable=true
            });
            

			if (result.Ok)
            {
				HoraR1.Text = Convert.ToString(result.SelectedTime);
				HoraR1.Unfocus();
                DependencyService.Get<IForceKeyboardDismissalService>().DismissKeyboard();
            }
            else
            {
				HoraR1.Unfocus();
                DependencyService.Get<IForceKeyboardDismissalService>().DismissKeyboard();
            }
            
		}

        async void Restaurant_Focused(object sender, Xamarin.Forms.FocusEventArgs e)
		{
			var result = await UserDialogs.Instance.ActionSheetAsync("Restaurant", "Cancel", null, null, "JARANÁ", "LE GULA", "PIU");
            
			if (result !="Cancel")
			{
				Restaurante.Text = result.ToString();

				Restaurante.Unfocus();
                DependencyService.Get<IForceKeyboardDismissalService>().DismissKeyboard();
			}
			else
			{
				Restaurante.Unfocus();	
                DependencyService.Get<IForceKeyboardDismissalService>().DismissKeyboard();
			}

		}
        
		async void SillaNinos_Focused(object sender, Xamarin.Forms.FocusEventArgs e)
        {
            var result = await UserDialogs.Instance.ActionSheetAsync("Sillas niños", "Cancel", null, null, "No", "Si");

            if (result != "Cancel")
            {
                SillaNiño.Text = result.ToString();

				SillaNiño.Unfocus();
                DependencyService.Get<IForceKeyboardDismissalService>().DismissKeyboard();
            }
            else
            {
				SillaNiño.Unfocus();
                DependencyService.Get<IForceKeyboardDismissalService>().DismissKeyboard();
            }

        }

	}

}
