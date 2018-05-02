using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Windows.Input;
using City_Center.Services;
using GalaSoft.MvvmLight.Command;
using Xamarin.Forms;
using static City_Center.Models.HabitacionesResultado;
using static City_Center.Models.MoaSpaResultado;
using static City_Center.Models.PromocionesResultado;
using City_Center.Clases;

namespace City_Center.ViewModels
{
    public class HotelViewModel : BaseViewModel
    {
        #region Services
        private ApiService apiService;
        #endregion

        #region Attributes
        private HabitacionesReturn listHabitaciones;
        private ObservableCollection<HotelItemViewModel> habitacionesDetalle;

        private MoaSpaReturn listMoaSpa;
        private ObservableCollection<MoaSpaDetalle> moaSpaDetalle;

        private PromocionesReturn listPromociones;
        private ObservableCollection<PromocionesItemViewModel> promocionesDetalle;

        private string imagen_Selected;


        #endregion

        #region Properties

        public ObservableCollection<HotelItemViewModel> HabitacionesDetalle
        {
            get { return this.habitacionesDetalle; }
            set { SetValue(ref this.habitacionesDetalle, value); }
        }

        public ObservableCollection<MoaSpaDetalle> MoaSpaDetalle
        {
            get { return this.moaSpaDetalle; }
            set { SetValue(ref this.moaSpaDetalle, value); }
        }

        public ObservableCollection<PromocionesItemViewModel> PromocionesDetalle
        {
            get { return this.promocionesDetalle; }
            set { SetValue(ref this.promocionesDetalle, value); }
        }

        public string Imagen_Selected
        {
            get { return this.imagen_Selected; }
            set { SetValue(ref this.imagen_Selected, value); }
        }

        #endregion

        #region Commands


        public ICommand MoaSpaCommand
        {
            get
            {
                return new RelayCommand(MoaSpa);
            }
        }

        private void MoaSpa()
        {
            //Poker
            if (this.listMoaSpa == null)
            {
                this.LoadMoaSpa();
            }

        }


        #endregion

        #region Methods
        private async void LoadHabitaciones()
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


            var response = await this.apiService.Get<HabitacionesReturn>("/hotel_spa/habitaciones", "/indexApp", content);

            if (!response.IsSuccess)
            {
                await Mensajes.Error("Error al cargar Habitaciones");

                return;
            }

            this.listHabitaciones = (HabitacionesReturn)response.Result;

            HabitacionesDetalle = new ObservableCollection<HotelItemViewModel>(this.ToHabitacionesItemViewModel());

        }

        private IEnumerable<HotelItemViewModel> ToHabitacionesItemViewModel()
        {
            return this.listHabitaciones.resultado.Select(l => new HotelItemViewModel
            {
                hab_id = l.hab_id,
                hab_nombre = l.hab_nombre,
                hab_descripcion = l.hab_descripcion,
                hab_imagen = VariablesGlobales.RutaServidor + l.hab_imagen,
                hab_titulo_1 = l.hab_titulo_1,
                hab_descripcion_1 = l.hab_descripcion_1,
                hab_imagen_1 = l.hab_imagen_1,
                hab_titulo_2 = l.hab_titulo_2,
                hab_descripcion_2 = l.hab_descripcion_2,
                hab_imagen_2 = l.hab_imagen_2,
                hab_titulo_3 = l.hab_titulo_3,
                hab_id_usuario_creo = l.hab_id_usuario_creo,
                hab_fecha_hora_creo = l.hab_fecha_hora_creo,
                hab_id_usuario_modifico = l.hab_id_usuario_modifico,
                hab_fecha_hora_modifico = l.hab_fecha_hora_modifico,
                hab_estatus = l.hab_estatus,
                hab_descripcion_3 = l.hab_descripcion_3,
                hab_imagen_3 = l.hab_imagen_3,
            });
        }


        private async void LoadMoaSpa()
        {
            var connection = await this.apiService.CheckConnection();

            if (!connection.IsSuccess)
            {
                await Mensajes.Error(connection.Message);

            }


            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("", ""),
            });


            var response = await this.apiService.Get<MoaSpaReturn>("/hotel_spa/galeria_moi_spa", "/indexApp", content);

            if (!response.IsSuccess)
            {
                await Mensajes.Error("Error al cargar Moi Spa");
                return;
            }

            this.listMoaSpa = (MoaSpaReturn)response.Result;


            Imagen_Selected = VariablesGlobales.RutaServidor + this.listMoaSpa.resultado[0].gal_imagen;


            MoaSpaDetalle = new ObservableCollection<MoaSpaDetalle>(this.ToMoaSpaItemViewModel());

        }

        private IEnumerable<MoaSpaDetalle> ToMoaSpaItemViewModel()
        {
            return this.listMoaSpa.resultado.Select(l => new MoaSpaDetalle
            {
                gal_id = l.gal_id,
                gal_descripcion = l.gal_descripcion,
                gal_imagen = VariablesGlobales.RutaServidor + l.gal_imagen,
                gal_galeria = l.gal_galeria,
                gal_id_usuario_creo = l.gal_id_usuario_creo,
                gal_fecha_hora_creo = l.gal_fecha_hora_creo,
                gal_id_usuario_modifico = l.gal_id_usuario_modifico,
                gal_fecha_hora_modifico = l.gal_fecha_hora_modifico,
                gal_estatus = l.gal_estatus,
                gal_eliminado = l.gal_eliminado,
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

            PromocionesDetalle = new ObservableCollection<PromocionesItemViewModel>(this.ToPromocionesItemViewModel().Where(a => a.pro_tipo == "hopa"));

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
        public HotelViewModel()
        {
            this.apiService = new ApiService();
            this.LoadHabitaciones();
            this.LoadPromociones();
           

        }
        #endregion
    }
}
