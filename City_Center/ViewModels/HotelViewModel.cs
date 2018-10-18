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
using Plugin.Share;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using Plugin.Permissions.Abstractions;


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

        private ObservableCollection<PromocionesItemViewModel> promocionesDetalle;

        private ObservableCollection<PromocionesItemViewModel> promocionesDetalle2;

        private PromocionesReturn listPromociones;

        private string imagen_Selected;
        private int tamanoHabitacion;

        bool muestraFlechasPromo = false;

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

        public ObservableCollection<PromocionesItemViewModel> PromocionesDetalle2
        {
            get { return this.promocionesDetalle2; }
            set { SetValue(ref this.promocionesDetalle2, value); }
        }

        public string Imagen_Selected
        {
            get { return this.imagen_Selected; }
            set { SetValue(ref this.imagen_Selected, value); }
        }

        public int TamanoHabitacion
        {
            get { return this.tamanoHabitacion; }
            set { SetValue(ref this.tamanoHabitacion, value); }
        }

        public bool MuestraFlechasPromo
        {
            get { return this.muestraFlechasPromo; }
            set { SetValue(ref this.muestraFlechasPromo, value); }
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


        public ICommand UbicacionHotelCommand
        {
            get
            {
                return new RelayCommand(UbicacionHotel);
            }
        }

        private async void UbicacionHotel()
        {
            try
            {
                Plugin.Share.Abstractions.ShareMessage Compartir = new Plugin.Share.Abstractions.ShareMessage();


                var hasPermission = await Utils.CheckPermissions(Permission.Location);
                if (!hasPermission)
                    return;

                var Posicion = await Ubicacion.GetCurrentPosition();

                Compartir.Text = "Ubicacion Actual";
                Compartir.Title = "Tu ubicacion";
                Compartir.Url = "https://www.google.com/maps/@" + Posicion.Latitude + "," + Posicion.Longitude + "," + "16z";

                await CrossShare.Current.Share(Compartir);
            }
            catch (Exception)
            {
                await Mensajes.Alerta("Ubicación denegada, activa el GPS de tu dispositivo");
            }
        }


        #endregion

        #region Methods
        private async void LoadHabitaciones()
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


                var response = await this.apiService.Get<HabitacionesReturn>("/hotel_spa/habitaciones", "/indexApp", content);

                if (!response.IsSuccess)
                {
                    //await Mensajes.Alerta("Error al cargar Habitaciones");

                    return;
                }

                this.listHabitaciones = (HabitacionesReturn)response.Result;

                HabitacionesDetalle = new ObservableCollection<HotelItemViewModel>();

                foreach (var l in listHabitaciones.resultado)
                {

                    int PosicionEspacio1 = l.hab_nombre.IndexOf(" ");
                    string Arreglo1 = l.hab_nombre.Substring(0, PosicionEspacio1);
                    string Arreglo2 = l.hab_nombre.Substring(PosicionEspacio1 + 1);


                    HabitacionesDetalle.Add(new HotelItemViewModel()
                    {
                        hab_id = l.hab_id,
                        hab_nombre = Arreglo1,
                        hab_nombre2 = Arreglo2,
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
                        hab_imagen_4 = l.hab_imagen_4,
                        hab_imagen_5 = l.hab_imagen_5,
                        hab_imagen_6 = l.hab_imagen_6
                    });



                    //            HabitacionesDetalle = new ObservableCollection<HotelItemViewModel>(this.ToHabitacionesItemViewModel());

                    int contador = HabitacionesDetalle.Count;

                    switch (contador)
                    {
                        case 1:
                            TamanoHabitacion = 125;
                            break;
                        case 2:
                            TamanoHabitacion = 125;
                            break;

                        case 3:
                            TamanoHabitacion = 125;
                            break;

                        case 4:
                            TamanoHabitacion = 250;
                            break;

                        case 5:
                            TamanoHabitacion = 250;
                            break;

                        case 6:
                            TamanoHabitacion = 250;
                            break;

                        case 7:
                            TamanoHabitacion = 375;
                            break;

                        case 8:
                            TamanoHabitacion = 375;
                            break;

                        case 9:
                            TamanoHabitacion = 375;
                            break;

                        case 10:
                            TamanoHabitacion = 500;
                            break;

                        case 11:
                            TamanoHabitacion = 500;
                            break;
                        case 12:
                            TamanoHabitacion = 500;
                            break;
                    }

                }

            }
            catch (Exception ex)
            {
                //await Mensajes.Error("Hotel - Habitaciones" + ex.ToString());
            }

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
                hab_imagen_4 = l.hab_imagen_4,
                hab_imagen_5 = l.hab_imagen_5,
                hab_imagen_6 = l.hab_imagen_6,

            });
        }


        private async void LoadMoaSpa()
        {
            try
            {
                var connection = await this.apiService.CheckConnection();

                if (!connection.IsSuccess)
                {
                    await Mensajes.Alerta("Verificá tu conexión a Internet");

                }


                var content = new FormUrlEncodedContent(new[]
                {
                new KeyValuePair<string, string>("", ""),
                });

                var response = await this.apiService.Get<MoaSpaReturn>("/hotel_spa/galeria_moi_spa", "/indexApp", content);

                if (!response.IsSuccess)
                {
                    // await Mensajes.Alerta("Error al cargar Moi Spa");
                    return;
                }

                this.listMoaSpa = (MoaSpaReturn)response.Result;


                Imagen_Selected = VariablesGlobales.RutaServidor + this.listMoaSpa.resultado[0].gal_imagen;


                MoaSpaDetalle = new ObservableCollection<MoaSpaDetalle>(this.ToMoaSpaItemViewModel());

            }
            catch (Exception ex)
            {
                //    await Mensajes.Alerta("Hotel - SpoMoa" + ex.ToString());
            }

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
            try
            {
                var connection = await this.apiService.CheckConnection();

                if (!connection.IsSuccess)
                {
                    await Mensajes.Alerta(connection.Message);

                    return;
                }

                var content = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("pro_tipo", "hopa"),
                });


                var response = await this.apiService.Get<PromocionesReturn>("/promociones", "/indexTipoApp", content);

                if (!response.IsSuccess)
                {
                    // await Mensajes.Alerta("Error al cargar Promociones");

                    return;
                }

                this.listPromociones = (PromocionesReturn)response.Result;
#if __IOS__
                PromocionesDetalle = new ObservableCollection<PromocionesItemViewModel>(this.ToPromocionesItemViewModel());

                PromocionesDetalle2 = new ObservableCollection<PromocionesItemViewModel>(this.ToPromocionesItemViewModel());
#endif

#if __ANDROID__
                PromocionesDetalle = new ObservableCollection<PromocionesItemViewModel>(this.ToPromocionesItemViewModel2());

                PromocionesDetalle2 = new ObservableCollection<PromocionesItemViewModel>(this.ToPromocionesItemViewModel2());
#endif

                if (PromocionesDetalle.Count > 0)
                {
                    MuestraFlechasPromo = true;

                    VariablesGlobales.RegistrosHotelPromociones = promocionesDetalle.Count - 1;
                    VariablesGlobales.RegistrosHotelPromociones2 = promocionesDetalle.Count - 1;
                }
                else
                {
                    MuestraFlechasPromo = false;
                }

            }
            catch (Exception)
            {
                MuestraFlechasPromo = false;
                // await Mensajes.Al("Hotel - Promociones" + ex.ToString());
            }
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
                pro_imagen_2 = l.pro_imagen_2,
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
                loc_nombre = l.loc_nombre,
                pro_vinculo = l.pro_vinculo,
                pro_telefono = l.pro_telefono,
                pro_url = l.pro_url,
                pro_nombre_btn = l.pro_nombre_btn,
                pro_ejecutar_url = l.pro_ejecutar_url
            });
        }

        private IEnumerable<PromocionesItemViewModel> ToPromocionesItemViewModel2()
        {
            return this.listPromociones.resultado.Where(l => l.pro_id > 0).Select(l => new PromocionesItemViewModel
            {
                pro_id = l.pro_id,
                pro_id_evento = l.pro_id_evento,
                pro_id_locacion = l.pro_id_locacion,
                pro_nombre = l.pro_nombre,
                pro_descripcion = l.pro_descripcion,
                pro_imagen = l.pro_imagen,
                pro_imagen_2 = l.pro_imagen_2,
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
                loc_nombre = l.loc_nombre,
                pro_vinculo = l.pro_vinculo,
                pro_telefono = l.pro_telefono,
                pro_url = l.pro_url,
                pro_nombre_btn = l.pro_nombre_btn,
                pro_ejecutar_url = l.pro_ejecutar_url
            });
        }


        //return this.listPromociones.resultado.Where(l => l.pro_id > 0).Select(l => new PromocionesItemViewModel


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
