using System;
using System.Collections.Generic;
using City_Center.Clases;
using Xamarin.Forms;
using City_Center.PopUp;
using Rg.Plugins.Popup.Extensions;
using static City_Center.Models.HabitacionesResultado;
using City_Center.ViewModels;
using System.Linq;
using City_Center.Models;
using System.Collections.ObjectModel;


namespace City_Center.Page
{
    public partial class DetalleHotel : ContentPage
    {
        string[] ListaOpciones;

        public WebViewHotel _webHotel;

        private ObservableCollection<HabitacionesDetalle> HabitacionesDetalle;

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
                    //ListviewOtrasHabitaciones.ItemsSource = DTVM.HabitacionesDetalle.Where(l => l.hab_id != Convert.ToInt32(selection.hab_id));

                }

            }
            catch (Exception ex)
            {

            }
        }

        void Handle_ItemAppearing(object sender, Xamarin.Forms.ItemVisibilityEventArgs e)
        {
            var Item = e;

            HabitacionesDetalle.Add(new HabitacionesDetalle()
            {
                 = l.mde_id,
                mde_id_menu = l.mde_id_menu,
                mde_id_restaurant = l.mde_id_restaurant,
                mde_nombre = (Convert.ToString(l.mde_nombre)).ToUpper(),
                mde_descripcion = l.mde_descripcion,
                mde_imagen = l.mde_imagen,
                mde_precio = l.mde_precio,
                mde_id_usuario_creo = l.mde_id_usuario_creo,
                mde_fecha_hora_creo = l.mde_fecha_hora_creo,
                mde_id_usuario_modifico = l.mde_id_usuario_modifico,
                mde_fecha_hora_modifico = l.mde_fecha_hora_modifico,
                mde_estatus = l.mde_estatus,
                NombreMenu = NombreMenu,
                Margen = Margen

            });



        }
    }
}
