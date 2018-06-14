using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using static City_Center.Models.EventosResultado;
using GalaSoft.MvvmLight.Helpers;
using City_Center.ViewModels;
using City_Center.Clases;
using System.Linq;

namespace City_Center.Page.SlideMenu
{
    public partial class Show : ContentPage
    {
        ShowsViewModel showsito = new ShowsViewModel();

        public Show()
        {
            InitializeComponent();

            NavigationPage.SetTitleIcon(this, "logo_hdpi");
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (showsito.EventosDetalle !=null)
            {
                
                ListaShow.ItemsSource = null;
               
                ListaShow.ItemsSource = showsito.EventosDetalle;

                if (showsito.EventosDetalle.Count == 0)
                {
                     Mensajes.Alerta("No se encontro ningún evento");
                }

            }
  
        }


        protected override void OnDisappearing()
        {
            
            base.OnDisappearing();

            GC.Collect();

        }

        void Tab1_Tapped(object sender, System.EventArgs e)
        {
            LabelTab1.TextColor = Color.FromHex("#EAE8ED");
            LabelTab2.TextColor = Color.FromHex("#71628A");
            LabelTab3.TextColor = Color.FromHex("#71628A");

            BV1.IsVisible = true;
            BV2.IsVisible = false;
            BV3.IsVisible = false;


            if (String.IsNullOrEmpty(VariablesGlobales.FechaShowInicio))
            {
                showsito.EventosDetalle = new ObservableCollection<EventosItemViewModel>(this.ToEventosItemViewModel());

            }
            else
            {
                showsito.EventosDetalle = new ObservableCollection<EventosItemViewModel>(this.ToEventosItemViewModel().Where(l => l.eve_fecha_hora_inicio >= Convert.ToDateTime(VariablesGlobales.FechaShowInicio) && l.eve_fecha_hora_inicio <= Convert.ToDateTime(VariablesGlobales.FechaShowFinal)));

            }

            ListaShow.ItemsSource = showsito.EventosDetalle;

            //SL1.IsVisible = true;
            //SL2.IsVisible = false;
            //SL3.IsVisible = false;
        }

        void Tab2_Tapped(object sender, System.EventArgs e)
        {
            LabelTab1.TextColor = Color.FromHex("#71628A");
            LabelTab2.TextColor = Color.FromHex("#EAE8ED");
            LabelTab3.TextColor = Color.FromHex("#71628A");

            BV1.IsVisible = false;
            BV2.IsVisible = true;
            BV3.IsVisible = false;


            if (String.IsNullOrEmpty(VariablesGlobales.FechaShowInicio))
            {
                showsito.EventosDetalle = new ObservableCollection<EventosItemViewModel>(this.ToEventosItemViewModel().Where(l => l.eve_id_locacion == 2));

            }
            else
            {
                showsito.EventosDetalle = new ObservableCollection<EventosItemViewModel>(this.ToEventosItemViewModel().Where(l => l.eve_id_locacion == 2 && l.eve_fecha_hora_inicio >= Convert.ToDateTime(VariablesGlobales.FechaShowInicio) && l.eve_fecha_hora_inicio <= Convert.ToDateTime(VariablesGlobales.FechaShowFinal)));

            }

            ListaShow.ItemsSource = showsito.EventosDetalle;
         
        }

        void Tab3_Tapped(object sender, System.EventArgs e)
        {
            LabelTab1.TextColor = Color.FromHex("#71628A");
            LabelTab2.TextColor = Color.FromHex("#71628A");
            LabelTab3.TextColor = Color.FromHex("#EAE8ED");

            BV1.IsVisible = false;
            BV2.IsVisible = false;
            BV3.IsVisible = true;


            if (String.IsNullOrEmpty(VariablesGlobales.FechaShowInicio))
            {
                showsito.EventosDetalle = new ObservableCollection<EventosItemViewModel>(this.ToEventosItemViewModel().Where(l => l.eve_id_locacion == 1));

            }
            else
            {
                showsito.EventosDetalle = new ObservableCollection<EventosItemViewModel>(this.ToEventosItemViewModel().Where(l => l.eve_id_locacion == 1 && l.eve_fecha_hora_inicio >= Convert.ToDateTime(VariablesGlobales.FechaShowInicio) && l.eve_fecha_hora_inicio <= Convert.ToDateTime(VariablesGlobales.FechaShowFinal)));

            }

            ListaShow.ItemsSource = showsito.EventosDetalle;

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
                        image.Source = "FavoritoOK";                        
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
                        image.Source = "FavoritoOK";
                    }

                }

            }
            catch (Exception)
            {


            }
        }

        private IEnumerable<EventosItemViewModel> ToEventosItemViewModel()
        {
            return MainViewModel.GetInstance().listEventos.resultado.Select(l => new EventosItemViewModel
            {
                eve_imagen = l.eve_imagen,
                eve_descripcion = l.eve_descripcion,
                eve_nombre = l.eve_nombre,
                eve_fecha_hora_inicio = l.eve_fecha_hora_inicio,
                eve_link = l.eve_link,
                eve_id_locacion = l.eve_id_locacion,
                loc_nombre = l.loc_nombre,
                eve_id = l.eve_id,
                eve_guardado = l.eve_guardado,
                eve_id_guardado = l.eve_id_guardado,
                oculta = !(bool)l.eve_guardado,
                eve_fecha_hora_fin = l.eve_fecha_hora_fin,
                eve_id_usuario_creo = l.eve_id_usuario_creo,
                eve_fecha_hora_creo = l.eve_fecha_hora_creo,
                eve_id_usuario_modifico = l.eve_id_usuario_modifico,
                eve_fecha_hora_modifico = l.eve_fecha_hora_modifico,
                eve_num_usuarios_inscritos = l.eve_num_usuarios_inscritos,
                eve_num_compartidos = l.eve_num_compartidos,
                eve_num_favoritos = l.eve_num_favoritos,
                eve_lista = l.eve_lista,
                eve_carrucel = l.eve_carrucel,
                eve_descripcion_locacion = l.eve_descripcion_locacion,
                eve_destacado = l.eve_destacado,
                updated_at = l.updated_at,
                created_at = l.created_at,
                eve_telefono = l.eve_telefono,
                eve_tipo = l.eve_tipo,
                ocultallamada = (string.IsNullOrEmpty(l.eve_telefono) ? false : true),
                ocultaonline = (string.IsNullOrEmpty(l.eve_link) ? false : true)
            });
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
