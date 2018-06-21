using System;
using System.Collections.Generic;
using City_Center.Clases;
using Xamarin.Forms;
using City_Center.PopUp;
using Rg.Plugins.Popup.Extensions;
using static City_Center.Models.HabitacionesResultado;

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

            NavigationPage.SetTitleIcon(this, "logo@2x.png");
              
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
                await ((MasterPage)Application.Current.MainPage).Detail.Navigation.PushAsync(new SeleccionTipoChat());
               
            }
            else
            {
                await Mensajes.Alerta("Es necesario que te registres para completar esta acción");
            }
        }

        void Handle_ItemSelected_1(object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
        {
            try
            {
                var selection = e.SelectedItem as HabitacionesDetalle;


                if (selection != null)
                {
                   
                    NombreHabitacion.Text = selection.hab_nombre;
                    DescripcionHabitacion.Text = selection.hab_descripcion;
                    Img1provisional.Source = selection.hab_imagen;
                    VariablesGlobales.Img1 = VariablesGlobales.RutaServidor + selection.hab_imagen_1;
                    VariablesGlobales.Img2 = VariablesGlobales.RutaServidor + selection.hab_imagen_2;
                    VariablesGlobales.Img3 = VariablesGlobales.RutaServidor + selection.hab_imagen_3;
                    VariablesGlobales.Img4 = VariablesGlobales.RutaServidor + selection.hab_imagen_4;
                    VariablesGlobales.Img5 = VariablesGlobales.RutaServidor + selection.hab_imagen_5;
                    VariablesGlobales.Img6 = VariablesGlobales.RutaServidor + selection.hab_imagen_6;

                    ListaOpciones = new string[] { VariablesGlobales.Img1, VariablesGlobales.Img2, VariablesGlobales.Img3, VariablesGlobales.Img4, VariablesGlobales.Img5, VariablesGlobales.Img6 };

                    listaDetalleHabitacion.ItemsSource = ListaOpciones;
                   
                }

            }
            catch (Exception ex)
            {

            }
        }
    }
}
