using System;
using System.Collections.Generic;
using Xamarin.Forms.Platform;
using Xamarin.Forms;
using City_Center.PopUp;
using Rg.Plugins.Popup.Extensions;

namespace City_Center.Page
{
    public partial class InicioContent : ContentPage
    {
        public WebViewHotel _webHotel;

        private string[] ListaOpciones;
        private string[] ListaOpciones2;
        private string[] ListaOpciones3;

        public InicioContent()
        {
            InitializeComponent();
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

            ListaOpciones = new string[] { "1", "2", "3", "4"};

            ListaOpciones2 = new string[] { "Restaurant 1", "Restaurant 2", "Restaurant 3", "Restaurant 4" };

            ListaOpciones3 = new string[] { "No", "Si"};

            NoPersona.ItemsSource = ListaOpciones;

            NoPersona1.ItemsSource = ListaOpciones;

            CR.ItemsSource = ListaOpciones2;

            Combosillaniños.ItemsSource = ListaOpciones3;

            NoPersona.SelectedIndex = 0;
            NoPersona1.SelectedIndex = 0;
            CR.SelectedIndex = 0;
            Combosillaniños.SelectedIndex = 0;
           
        }

        async void Handle_Clicked(object sender, System.EventArgs e)
        {
           await Navigation.PushPopupAsync(_webHotel);
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


    }

}
