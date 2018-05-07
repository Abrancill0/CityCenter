using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace City_Center.Page
{
    public partial class ConsultaTarjetaWin : ContentPage
    {
        private string[] ListaOpciones;

        public ConsultaTarjetaWin()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            ListaOpciones = new string[] { "DNI", "LE", "LC", "CI" };

            TipoDocumento.ItemsSource = ListaOpciones;

            TipoDocumento.SelectedIndex = 0;

            TarjetaPrueba.Source = "DummyCard";

        }

    }
}
