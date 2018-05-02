using System;
using System.Collections.Generic;
using City_Center.Clases;
using Xamarin.Forms;

namespace City_Center.Page
{
    public partial class DetalleRestaurante : ContentPage
    {
        string[] ListaOpciones;

        public DetalleRestaurante()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            ListaOpciones = new string[] { VariablesGlobales.Img1, VariablesGlobales.Img2, VariablesGlobales.Img3 ,VariablesGlobales.Img4};

            listaDetalleRestaurante.ItemsSource = ListaOpciones;

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

        void Handle_Tapped(object sender, System.EventArgs e)
        {

            if (SLM.IsVisible==false)
            {
                SLM.IsVisible = true;
            }
            else
            {
                SLM.IsVisible = false;
            }


        }
    }
}
