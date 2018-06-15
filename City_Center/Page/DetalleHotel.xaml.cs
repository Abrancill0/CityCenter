using System;
using System.Collections.Generic;
using City_Center.Clases;
using Xamarin.Forms;
using City_Center.PopUp;
using Rg.Plugins.Popup.Extensions;

namespace City_Center.Page
{
    public partial class DetalleHotel : ContentPage
    {
        string[] ListaOpciones;

        public WebViewHotel _webHotel;
        
        public DetalleHotel()
        {
            InitializeComponent();
            _webHotel = new WebViewHotel();

<<<<<<< HEAD
            NavigationPage.SetTitleIcon(this, "logo@2x.png");
=======
            NavigationPage.SetTitleIcon(this, "logo_hdpi.png");
>>>>>>> 66a2e5f3c284a595e654f62d503db44111e45e87
              
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

          
                ListaOpciones = new string[] { VariablesGlobales.Img1, VariablesGlobales.Img2, VariablesGlobales.Img3, VariablesGlobales.Img4, VariablesGlobales.Img5, VariablesGlobales.Img6 };

                listaDetalleHabitacion.ItemsSource = ListaOpciones;


        }

        void Handle_ItemSelected(object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
        {
            try
            {
                var Seleccion = e.SelectedItem;


                if (Seleccion != null)
                {
                    var selection = e.SelectedItem;

                    Img1provisional.Source = selection.ToString();
                }

            }
            catch (Exception ex)
            {

            }


        }

        async void BtnReservaOnline(object sender, System.EventArgs e)
        {
            try
            {
                VariablesGlobales.FechaInicio = DateTime.Today;
                VariablesGlobales.FechaFin = DateTime.Today.AddDays(2);
                VariablesGlobales.NumeroHuespedes = Convert.ToInt32(2);

                await ((MasterPage)Application.Current.MainPage).Detail.Navigation.PushAsync(_webHotel);


               // await Navigation.PushPopupAsync(_webHotel);
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", "E: " + ex.ToString(), "ok");
            }

        }

        async void Chat_click(object sender, System.EventArgs e)
        {
            bool isLoggedIn = Application.Current.Properties.ContainsKey("IsLoggedIn") ?
                                    (bool)Application.Current.Properties["IsLoggedIn"] : false;

            if (isLoggedIn)
            {
                #if __ANDROID__
                await ((MasterPage)Application.Current.MainPage).Detail.Navigation.PushAsync(new SeleccionTipoChat());
                #endif
            }
            else
            {
                await Mensajes.Alerta("Es necesario que te registres para completar esta acción");
            }
        }
    }
}
