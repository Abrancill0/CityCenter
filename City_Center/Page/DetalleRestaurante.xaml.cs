using System;
using System.Collections.Generic;
using City_Center.Clases;
using Xamarin.Forms;

namespace City_Center.Page
{
    public partial class DetalleRestaurante : ContentPage
    {
        string[] ListaOpciones;

        private string[] ListaOpciones1;
        private string[] ListaOpciones2;
        private string[] ListaOpciones3;

        public DetalleRestaurante()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            ListaOpciones = new string[] { VariablesGlobales.Img1, VariablesGlobales.Img2, VariablesGlobales.Img3, VariablesGlobales.Img4 };

            listaDetalleRestaurante.ItemsSource = ListaOpciones;

            ListaOpciones = new string[] { "1", "2", "3", "4", "5", "6", "7", "8" };

            ListaOpciones2 = new string[] { "JARANÁ", "LE GULA", "PIU" };

            ListaOpciones3 = new string[] { "No", "Si" };

            NoPersona.ItemsSource = ListaOpciones1;
            CR.ItemsSource = ListaOpciones2;
            Combosillaniños.ItemsSource = ListaOpciones3;

            NoPersona.SelectedIndex = 0;
            CR.SelectedIndex = 0;
            Combosillaniños.SelectedIndex = 0;

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
            catch (Exception)
            {

            }


        }

        void Handle_Tapped(object sender, System.EventArgs e)
        {

            if (SLM.IsVisible == false)
            {
                SLM.IsVisible = true;
                SLR.IsVisible = false;
            }
            else
            {
                SLM.IsVisible = false;
            }


        }

        void Handle_Tapped_1(object sender, System.EventArgs e)
        {
            if (SLR.IsVisible == false)
            {
                SLR.IsVisible = true;
                SLM.IsVisible = false;
            }
            else
            {
                SLR.IsVisible = false;
            }
        }
    }
}
