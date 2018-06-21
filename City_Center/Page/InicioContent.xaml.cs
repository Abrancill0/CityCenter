using System;
using System.Collections.Generic;
using Xamarin.Forms.Platform;
using Xamarin.Forms;
using City_Center.PopUp;
using Rg.Plugins.Popup.Extensions;
using City_Center.Clases;
using Acr.UserDialogs;
using City_Center.Helper;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using City_Center.ViewModels;
using CarouselView.FormsPlugin.Abstractions;
using System.Collections.ObjectModel;
using System.Linq;

namespace City_Center.Page
{
    public partial class InicioContent : ContentPage
    {
        public WebViewHotel _webHotel;
        public AlertaConfirmacion _AlertaConfirmacion;

        InicioViewModel Inicito = new InicioViewModel();

        public InicioContent()
        {
            InitializeComponent();
           
            _webHotel = new WebViewHotel();

            _AlertaConfirmacion = new AlertaConfirmacion();

            FechaInicio.Text = String.Format("{0:dd/MM/yyyy}", DateTime.Today);
            FechaFinal.Text = String.Format("{0:dd/MM/yyyy}", DateTime.Today.AddDays(1));

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

                    //await Navigation.PushPopupAsync(_webHotel);
                    // #if __ANDROID__
                    await ((MasterPage)Application.Current.MainPage).Detail.Navigation.PushAsync(_webHotel);
                    // #endif
                }
            }
            catch (Exception)
            {
                //await DisplayAlert("oj", ex.ToString(), "ok");
                await Mensajes.Alerta("No se pudo acceder a las reservaciones, intente mas tarde.");
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

            //await((MasterPage)Application.Current.MainPage).Detail.Navigation.PushAsync(new WebViewTienda());
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

        async void FechaInicio_Focused(object sender, Xamarin.Forms.FocusEventArgs e)
        {
#if __IOS__
            DependencyService.Get<IForceKeyboardDismissalService>().DismissKeyboard();
#endif

            var result = await UserDialogs.Instance.DatePromptAsync(new DatePromptConfig
            {
                Title = "Llegada",
                CancelText = "CANCELAR",
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

#if __IOS__
            DependencyService.Get<IForceKeyboardDismissalService>().DismissKeyboard();
#endif

            var result = await UserDialogs.Instance.DatePromptAsync(new DatePromptConfig
            {
                IsCancellable = true,
                CancelText = "CANCELAR",
                MinimumDate = DateTime.Now.AddDays(0),
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

            //String.Format("{0:dd MMMM yyyy}"

        }

        async void FechaR1_Focused(object sender, Xamarin.Forms.FocusEventArgs e)
        {
#if __IOS__
            DependencyService.Get<IForceKeyboardDismissalService>().DismissKeyboard();
#endif

            var result = await UserDialogs.Instance.DatePromptAsync(new DatePromptConfig
            {
                IsCancellable = true,
                CancelText = "CANCELAR",
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
#if __IOS__
            DependencyService.Get<IForceKeyboardDismissalService>().DismissKeyboard();
#endif

            var result = await UserDialogs.Instance.DatePromptAsync(new DatePromptConfig
            {
                IsCancellable = true,
                CancelText = "CANCELAR",
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
#if __IOS__
            DependencyService.Get<IForceKeyboardDismissalService>().DismissKeyboard();
#endif

            var result = await UserDialogs.Instance.DatePromptAsync(new DatePromptConfig
            {
                IsCancellable = true,
                CancelText = "CANCELAR",
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
#if __IOS__
            DependencyService.Get<IForceKeyboardDismissalService>().DismissKeyboard();
#endif

            if (Restaurante.Text.Contains("PIÚ"))
            {
                var result = await UserDialogs.Instance.ActionSheetAsync("Horario", "CANCELAR", null, null, "12:30", "20:30", "21:00", "23:00");

                if (result != "CANCELAR")
                {
                    HoraR1.Text = result.ToString();

                    HoraR1.Unfocus();
                    DependencyService.Get<IForceKeyboardDismissalService>().DismissKeyboard();
                }
                else
                {
                    HoraR1.Text = "12:30";
                    HoraR1.Unfocus();
                    DependencyService.Get<IForceKeyboardDismissalService>().DismissKeyboard();
                }
            }
            else if (Restaurante.Text.Contains("LE GULÁ"))
            {
                var result = await UserDialogs.Instance.ActionSheetAsync("Horario", "CANCELAR", null, null, "21:00", "23:00");

                if (result != "CANCELAR")
                {
                    HoraR1.Text = result.ToString();

                    HoraR1.Unfocus();
                    DependencyService.Get<IForceKeyboardDismissalService>().DismissKeyboard();
                }
                else
                {
                    HoraR1.Text = "21:00";
                    HoraR1.Unfocus();
                    DependencyService.Get<IForceKeyboardDismissalService>().DismissKeyboard();
                }
            }
            else
            {
                var result = await UserDialogs.Instance.TimePromptAsync(new TimePromptConfig
                {
                    IsCancellable = true
                });


                if (result.Ok)
                {
                    HoraR1.Text = FechaRango1.Text = (Convert.ToString(result.SelectedTime).Substring(0, 5));

                    HoraR1.Unfocus();
                    DependencyService.Get<IForceKeyboardDismissalService>().DismissKeyboard();
                }
                else
                {
                    HoraR1.Unfocus();
                    DependencyService.Get<IForceKeyboardDismissalService>().DismissKeyboard();
                }
            }


            //var result = await UserDialogs.Instance.TimePromptAsync(new TimePromptConfig
            //         {
            //              IsCancellable=true
            //         });


            //if (result.Ok)
            //        {
            //            string hora = Convert.ToString(result.SelectedTime);
            //            HoraR1.Text = hora.Substring(0,5);
            //HoraR1.Unfocus();
            //            DependencyService.Get<IForceKeyboardDismissalService>().DismissKeyboard();
            //        }
            //        else
            //        {
            //HoraR1.Unfocus();
            //    DependencyService.Get<IForceKeyboardDismissalService>().DismissKeyboard();
            //}

        }

        async void Restaurant_Focused(object sender, Xamarin.Forms.FocusEventArgs e)
        {
#if __IOS__
            DependencyService.Get<IForceKeyboardDismissalService>().DismissKeyboard();
#endif
            var result = await UserDialogs.Instance.ActionSheetAsync("Restaurant", "CANCELAR", null, null, "LE GULÁ", "PIÚ! EXPRESS");

            if (result != "CANCELAR")
            {
                Restaurante.Text = result.ToString();
                if (Restaurante.Text.Contains("PIÚ"))
                {
                    HoraR1.Text = "12:30";
                }
                else if (Restaurante.Text.Contains("LE GULÁ"))
                {
                    HoraR1.Text = "21:00";
                }

                Restaurante.Unfocus();
                DependencyService.Get<IForceKeyboardDismissalService>().DismissKeyboard();
            }
            else
            {
                HoraR1.Text = "00:00";
                Restaurante.Unfocus();
                DependencyService.Get<IForceKeyboardDismissalService>().DismissKeyboard();
            }

        }

        async void SillaNinos_Focused(object sender, Xamarin.Forms.FocusEventArgs e)
        {
#if __IOS__
            DependencyService.Get<IForceKeyboardDismissalService>().DismissKeyboard();
#endif

            var result = await UserDialogs.Instance.ActionSheetAsync("Sillas niños", "CANCELAR", null, null, "No", "Si");

            if (result != "CANCELAR")
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

        void Handle_TextChanged(object sender, Xamarin.Forms.TextChangedEventArgs e)
        {
            try
            {
                Entry entry = sender as Entry;
                String val = entry.Text; //Get Current Text


                if (val.Length > 0)//If it is more than your character restriction
                {

                    Match match = Regex.Match(val, @"([0-9\-]+)$",
                                    RegexOptions.IgnoreCase);

                    if (!match.Success)
                    {
                        val = val.Remove(val.Length - 1);
                        entry.Text = val;
                        return;
                    }

                    //Set the Old value
                }
            }
            catch (Exception)
            {

            }

        }

        async void Handle_Clicked_1(object sender, System.EventArgs e)
        {
            await ((MasterPage)Application.Current.MainPage).Detail.Navigation.PushAsync(new WebViewTienda());
        }

        void PositionSelected_CT(object sender, CarouselView.FormsPlugin.Abstractions.PositionSelectedEventArgs e)
        {

            VariablesGlobales.indice = e.NewValue;

            #if __IOS__
            try
            {
                
                if (VariablesGlobales.validacionIOS == 1)
                {
                    if (e.NewValue != 0)
                    {
                        CarruselTorneos.Position = 0;
                        VariablesGlobales.indice = 1;
                    }
                }
                else if (VariablesGlobales.validacionIOS == 2)
                {
                    //if (e.NewValue == 0)
                    //{
                    CarruselTorneos.Position = VariablesGlobales.RegistrosTorneo + 1;
                    VariablesGlobales.indice = VariablesGlobales.RegistrosTorneo;
                    //}
                }


            }
            catch (Exception)
            {

            }
        #endif
        }

        void Scrolled_CT(object sender, CarouselView.FormsPlugin.Abstractions.ScrolledEventArgs e)
        {
        #if __IOS__
            try
            {
                string Direccion = Convert.ToString(e.Direction);

                if (VariablesGlobales.indice == VariablesGlobales.RegistrosTorneo && Direccion == "Right")
                {
                    //CarruselTorneos.ItemsSource = Inicito.TorneoDetalle;
                    VariablesGlobales.validacionIOS = 1;
                    CarruselTorneos.Position = 0;
                    CarruselTorneos.AnimateTransition = false;

                }
                else if (VariablesGlobales.indice == 1 && Direccion == "Left")
                {
                    CarruselTorneos.AnimateTransition = false;
                    VariablesGlobales.validacionIOS = 2;
                    CarruselTorneos.Position = VariablesGlobales.RegistrosTorneo + 1;
                }
                else
                {
                    VariablesGlobales.validacionIOS = 0;
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

                if (VariablesGlobales.indice == VariablesGlobales.RegistrosTorneo && Direccion == "Right")
                {
                    CarruselTorneos.AnimateTransition = false;
                    CarruselTorneos.Position = 1;


                }
                else if (VariablesGlobales.indice == 1 && Direccion == "Left")
                {
                    CarruselTorneos.AnimateTransition = false;
                    CarruselTorneos.Position = VariablesGlobales.RegistrosTorneo + 1;
                }
            }
            catch (Exception ex)
            {
                DisplayAlert("Error", ex.ToString(), "OK");
            }

#endif
        }

        void Scrolled_HP(object sender, CarouselView.FormsPlugin.Abstractions.ScrolledEventArgs e)
        {

        #if __IOS__
            try
            {
                string Direccion = Convert.ToString(e.Direction);

                if (VariablesGlobales.IndicePromociones == VariablesGlobales.RegistrosPromociones && Direccion == "Right")
                {
                    //CarruselTorneos.ItemsSource = Inicito.TorneoDetalle;
                    VariablesGlobales.validacionIOSPromociones = 1;
                    CarruselPromociones.Position = 0;
                    CarruselPromociones.AnimateTransition = false;

                }
                else if (VariablesGlobales.IndicePromociones == 1 && Direccion == "Left")
                {
                    CarruselPromociones.AnimateTransition = false;
                    VariablesGlobales.validacionIOSPromociones = 2;
                    CarruselPromociones.Position = VariablesGlobales.RegistrosPromociones + 1;
                }
                else
                {
                    VariablesGlobales.validacionIOSPromociones = 0;
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

                if (VariablesGlobales.IndicePromociones >= VariablesGlobales.RegistrosPromociones && Direccion == "Right")
                        {
                            CarruselPromociones.AnimateTransition = false;
                            CarruselPromociones.Position = 0;

                        }
                else if (VariablesGlobales.IndicePromociones <= 1 && Direccion == "Left")
                        {
                            CarruselPromociones.AnimateTransition = false;
                            CarruselPromociones.Position = VariablesGlobales.RegistrosPromociones + 1;
                        }
                    }
                    catch (Exception ex)
                    {
                       
                    }  
                 
        #endif

        }

        void PositionSelected_HP(object sender, CarouselView.FormsPlugin.Abstractions.PositionSelectedEventArgs e)
        {
            VariablesGlobales.IndicePromociones = e.NewValue;

            #if __IOS__
                try
                {
                    
                    if (VariablesGlobales.validacionIOSPromociones == 1)
                    {
                        if (e.NewValue != 0)
                        {
                            CarruselPromociones.Position = 0;
                            VariablesGlobales.IndicePromociones = 1;
                        }
                    }
                    else if (VariablesGlobales.validacionIOSPromociones == 2)
                    {
                        //if (e.NewValue == 0)
                        //{
                        CarruselPromociones.Position = VariablesGlobales.RegistrosPromociones + 1;
                        VariablesGlobales.IndicePromociones = VariablesGlobales.RegistrosPromociones;
                        //}
                    }

                }
                catch (Exception)
                {

                }
            #endif

        }
    }

}
