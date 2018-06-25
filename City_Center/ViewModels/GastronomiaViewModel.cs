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

        private ObservableCollection<PromocionesItemViewModel> promocionesDetalle;
        private PromocionesReturn listPromociones;

		private int tamanoRestaurant;

        bool muestraFlechasPromo = false;

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

		public int TamanoRestaurant
        {
			get { return this.tamanoRestaurant; }
			set { SetValue(ref this.tamanoRestaurant, value); }
        }

        public bool MuestraFlechasPromo
        {
            get { return this.muestraFlechasPromo; }
            set { SetValue(ref this.muestraFlechasPromo, value); }
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
            try
            {
                RestaurantDetalle = new ObservableCollection<GastronomiaItemViewModel>(this.ToRestaurantItemViewModel());
            
				int Contador = RestaurantDetalle.Count;

				TamanoRestaurant = (Contador * 235) + 220;
			
			}
            catch (Exception)
            {

            }

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
            try
            {
                RestaurantDetalle = new ObservableCollection<GastronomiaItemViewModel>(this.ToRestaurantItemViewModel().Where(l => l.reb_tipo == "R"));
            
				int Contador = RestaurantDetalle.Count;

				TamanoRestaurant = (Contador * 235) + 220;
					
			}
            catch (Exception)
            {

            }

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
            try
            {
                RestaurantDetalle = new ObservableCollection<GastronomiaItemViewModel>(this.ToRestaurantItemViewModel().Where(l => l.reb_tipo == "B"));
            
				int Contador = restaurantDetalle.Count;

				TamanoRestaurant = (Contador * 235) + 250;
			
			
			}
            catch (Exception)
            {

            }

        }

        #endregion

        #region Methods
        private async void LoadRestaurantes()
        {
            try
            {
                var connection = await this.apiService.CheckConnection();

                if (!connection.IsSuccess)
                {
                    await Mensajes.Alerta("Verificá tu conexión a Internet");

                    return;
                }


                var content = new FormUrlEncodedContent(new[]
                {
                new KeyValuePair<string, string>("", ""),
                });


                var response = await this.apiService.GetReal<RestaurantReturn>("/gastronomia", "/obtenerRestaurantBar", content);

                if (!response.IsSuccess)
                {
                    //await Mensajes.Alerta("Error al cargar Restaurantes/Bar");

                    return;
                }

                this.listRestaruant = (RestaurantReturn)response.Result;

                RestaurantDetalle = new ObservableCollection<GastronomiaItemViewModel>(this.ToRestaurantItemViewModel());

				int Contador = RestaurantDetalle.Count;

				TamanoRestaurant = (Contador * 235) + 320;

            }
            catch (Exception ex)
            {
                //await Mensajes.Error("Gastronomia - Restaurantes" + ex.ToString());
            }

        }

        private IEnumerable<GastronomiaItemViewModel> ToRestaurantItemViewModel()
        {
            return this.listRestaruant.resultado.Select(l => new GastronomiaItemViewModel
            {
                reb_id = l.reb_id,
                reb_nombre = l.reb_nombre.ToUpper(),
                reb_descripcion = l.reb_descripcion,
                reb_descripcion_horario = l.reb_descripcion_horario,
                reb_ver_hotel_spa = l.reb_ver_hotel_spa,
                reb_reservas = l.reb_reservas,
                reb_tipo = l.reb_tipo,
                reb_imagen_1 = l.reb_imagen_1,
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

            try
            {
                var connection = await this.apiService.CheckConnection();

                if (!connection.IsSuccess)
                {
                    await Mensajes.Alerta("Verificá tu conexión a Internet");

                    return;
                }

                var content = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("pro_tipo", "gas"),
                });


                var response = await this.apiService.Get<PromocionesReturn>("/promociones", "/indexTipoApp", content);

                if (!response.IsSuccess)
                {
                    //await Mensajes.Alerta("Error al cargar Promociones");

                    return;
                }

                this.listPromociones = (PromocionesReturn)response.Result;

                PromocionesDetalle = new ObservableCollection<PromocionesItemViewModel>(this.ToPromocionesItemViewModel());

                if (PromocionesDetalle.Count > 2)
                {
                    MuestraFlechasPromo = true;

                    VariablesGlobales.RegistrosGastronomiaPromociones = PromocionesDetalle.Count - 2;
                }
                else
                {
                    MuestraFlechasPromo = false;
                }

            }
            catch (Exception)
            {
                MuestraFlechasPromo = false;

                //await Mensajes.Alerta("Gastronomía - Promociones" + ex.ToString());
            }

        }

        private IEnumerable<PromocionesItemViewModel> ToPromocionesItemViewModel()
        {
            return this.listPromociones.resultado.Select(l => new PromocionesItemViewModel
            {
                pro_id = l.pro_id,
                pro_id_evento = l.pro_id_evento,
                pro_id_locacion = l.pro_id_locacion,
				pro_nombre = l.pro_nombre.ToUpper(),
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