using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using City_Center.Services;
using Xamarin.Forms;
using static City_Center.Models.PromocionesResultado;
using static City_Center.Models.RestaurantResultado;
using City_Center.Clases;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;

namespace City_Center.ViewModels
{
    public class GastronomiaViewModel : BaseViewModel
    {
        #region Services
        private ApiService apiService;
        #endregion

        #region Attributes
        private RestaurantReturn listRestaruant;
        private ObservableCollection<GastronomiaItemViewModel> restaurantDetalle;

        private PromocionesReturn listPromociones;
        private ObservableCollection<PromocionesItemViewModel> promocionesDetalle;

        #endregion

        #region Properties

        public ObservableCollection<GastronomiaItemViewModel> RestaurantDetalle
        {
            get { return this.restaurantDetalle; }
            set { SetValue(ref this.restaurantDetalle, value); }
        }


        public ObservableCollection<PromocionesItemViewModel> PromocionesDetalle
        {
            get { return this.promocionesDetalle; }
            set { SetValue(ref this.promocionesDetalle, value); }
        }

        #endregion

        #region Commands

        public ICommand TodosCommand
        {
            get
            {
                return new RelayCommand(Todos);
            }
        }

        private void Todos()
        {
            RestaurantDetalle = new ObservableCollection<GastronomiaItemViewModel>(this.ToRestaurantItemViewModel());
        }

        public ICommand RestaurantesCommand
        {
            get
            {
                return new RelayCommand(Restaurantes);
            }
        }

        private void Restaurantes()
        {
           
            RestaurantDetalle = new ObservableCollection<GastronomiaItemViewModel>(this.ToRestaurantItemViewModel().Where(l => l.reb_tipo == "R"));
        }


        public ICommand BarCommand
        {
            get
            {
                return new RelayCommand(Bar);
            }
        }

        private void Bar()
        {
            RestaurantDetalle = new ObservableCollection<GastronomiaItemViewModel>(this.ToRestaurantItemViewModel().Where(l => l.reb_tipo == "B"));

        }

        #endregion

        #region Methods
        private async void LoadRestaurantes()
        {
            var connection = await this.apiService.CheckConnection();

            if (!connection.IsSuccess)
            {
                await Mensajes.Error(connection.Message);

                return;
            }


            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("", ""),
            });


            var response = await this.apiService.GetReal<RestaurantReturn>("/gastronomia", "/obtenerRestaurantBar", content);
           
            if (!response.IsSuccess)
            {
                await Mensajes.Error("Error al cargar Restaurantes/Bar");

                return;
            }

            this.listRestaruant = (RestaurantReturn)response.Result;

            RestaurantDetalle = new ObservableCollection<GastronomiaItemViewModel>(this.ToRestaurantItemViewModel());

        }

        private IEnumerable<GastronomiaItemViewModel> ToRestaurantItemViewModel()
        {
            return this.listRestaruant.resultado.Select(l => new GastronomiaItemViewModel
            {
                reb_id = l.reb_id,
                reb_nombre = l.reb_nombre,
                reb_descripcion = l.reb_descripcion,
                reb_descripcion_horario = l.reb_descripcion_horario,
                reb_ver_hotel_spa = l.reb_ver_hotel_spa,
                reb_reservas = l.reb_reservas,
                reb_tipo = l.reb_tipo,
                reb_imagen_1 =  l.reb_imagen_1,
                reb_imagen_2 = l.reb_imagen_2,
                reb_imagen_3 = l.reb_imagen_3,
                reb_imagen_4 = l.reb_imagen_4,
                reb_id_usuario_creo = l.reb_id_usuario_creo,
                reb_fecha_hora_creo = l.reb_fecha_hora_creo,
                reb_id_usuario_modifico = l.reb_id_usuario_modifico,
                reb_fecha_hora_modifico = l.reb_fecha_hora_modifico,
                reb_estatus = l.reb_estatus,
            });
        }

        private async void LoadPromociones()
        {
            var connection = await this.apiService.CheckConnection();

            if (!connection.IsSuccess)
            {
                await Mensajes.Error(connection.Message);

                return;
            }


            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("", ""),
            });


            var response = await this.apiService.Get<PromocionesReturn>("/promociones", "/indexApp", content);

            if (!response.IsSuccess)
            {
                await Mensajes.Error("Error al cargar Promociones");

                return;
            }

            this.listPromociones = (PromocionesReturn)response.Result;

            PromocionesDetalle = new ObservableCollection<PromocionesItemViewModel>(this.ToPromocionesItemViewModel());

        }

        private IEnumerable<PromocionesItemViewModel> ToPromocionesItemViewModel()
        {
            return this.listPromociones.resultado.Select(l => new PromocionesItemViewModel
            {
                pro_id = l.pro_id,
                pro_id_evento = l.pro_id_evento,
                pro_id_locacion = l.pro_id_locacion,
                pro_nombre = l.pro_nombre,
                pro_descripcion = l.pro_descripcion,
                pro_imagen = l.pro_imagen,
                pro_tipo_promocion = l.pro_tipo_promocion,
                pro_codigo = l.pro_codigo,
                pro_compartidos_codigo = l.pro_compartidos_codigo,
                pro_destacado = l.pro_destacado,
                pro_fecha_duracion_ini = l.pro_fecha_duracion_ini,
                pro_fecha_duracion_fin = l.pro_fecha_duracion_fin,
                pro_importe_decuento = l.pro_importe_decuento,
                pro_porcentaje_decuento = l.pro_porcentaje_decuento,
                pro_id_usuario_creo = l.pro_id_usuario_creo,
                pro_fecha_hora_creo = l.pro_fecha_hora_creo,
                pro_id_usuario_modifico = l.pro_id_usuario_modifico,
                pro_fecha_hora_modifico = l.pro_fecha_hora_modifico,
                pro_tipo = l.pro_tipo,
                pro_estatus = l.pro_estatus,
                loc_nombre = l.loc_nombre
            });
        }

        #endregion

        #region Contructors
        public GastronomiaViewModel()
        {
            this.apiService = new ApiService();
            this.LoadRestaurantes();
            this.LoadPromociones();
        }
        #endregion
    }
}