﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using City_Center.Clases;
using City_Center.Services;
using Xamarin.Forms;
using static City_Center.Models.RestauranteMenuResultado;
using static City_Center.Models.RestaurantResultado;
using System.Linq;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using City_Center.Models;
using Plugin.Share;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using Plugin.Permissions.Abstractions;

namespace City_Center.ViewModels
{
    public class DetalleRestauranteViewModel : BaseViewModel
    {
        #region Services
        private ApiService apiService;
        #endregion

        #region Attributes

        private RestauranteMenuReturn listRestaurantMenu;
        private ObservableCollection<MenuDetalle> restaurantMenuDetalle;

        private ObservableCollection<MenuNombre> restaurantMenuNombre;

        private string NombreViejo;
        private string Nombrenuevo;
        private string NombreMenu;
        private Thickness Margen;
        private bool mM;

        private bool rR;

        string fechaInicio;
        string horaInicio;
        string noPersonas;
        string nombreRestaurante;
        string sillaNiños;
        string nombre;
        string correo;
        string telefono;

        int tamanoListview;
        #endregion

        #region Properties
        public RestaurantDetalle rd
        {
            get;
            set;
        }

        public ObservableCollection<MenuDetalle> RestaurantMenuDetalle
        {
            get { return this.restaurantMenuDetalle; }
            set { SetValue(ref this.restaurantMenuDetalle, value); }
        }

        public ObservableCollection<MenuNombre> RestaurantMenuNombre
        {
            get { return this.restaurantMenuNombre; }
            set { SetValue(ref this.restaurantMenuNombre, value); }
        }

        public bool MM
        {
            get { return this.mM; }
            set { SetValue(ref this.mM, value); }
        }

        public int TamanoListview
        {
            get { return this.tamanoListview; }
            set { SetValue(ref this.tamanoListview, value); }
        }

        public bool RR
        {
            get { return this.rR; }
            set { SetValue(ref this.rR, value); }
        }

        public string FechaInicio
        {
            get { return this.fechaInicio; }
            set { SetValue(ref this.fechaInicio, value); }
        }

        public string HoraInicio
        {
            get { return this.horaInicio; }
            set { SetValue(ref this.horaInicio, value); }
        }

        public string NoPersonas
        {
            get { return this.noPersonas; }
            set { SetValue(ref this.noPersonas, value); }
        }

        public string NombreRestaurante
        {
            get { return this.nombreRestaurante; }
            set { SetValue(ref this.nombreRestaurante, value); }
        }

        public string SillaNiños
        {
            get { return this.sillaNiños; }
            set { SetValue(ref this.sillaNiños, value); }
        }

        public string Nombre
        {
            get { return this.nombre; }
            set { SetValue(ref this.nombre, value); }
        }

        public string Correo
        {
            get { return this.correo; }
            set { SetValue(ref this.correo, value); }
        }

        public string Telefono
        {
            get { return this.telefono; }
            set { SetValue(ref this.telefono, value); }
        }

        #endregion


        #region Command

        public ICommand ReservarCommand
        {
            get
            {
                return new RelayCommand(Reservar);
            }
        }

        private async void Reservar()
        {
            if (string.IsNullOrEmpty(this.Nombre))
            {
                await Mensajes.Alerta("Nombre y Apellido requerido");

                return;
            }

            if (string.IsNullOrEmpty(this.Correo))
            {
                await Mensajes.Alerta("correo electrónico requerido");

                return;
            }

            if (!ValidaEmailMethod.ValidateEMail(this.Correo))
            {
                await Mensajes.Alerta("Correo electronico mal estructurado");
                return;
            }


            if (string.IsNullOrEmpty(this.Telefono))
            {
                await Mensajes.Alerta("Número de teléfono requerido");

                return;
            }


            //string CuerpoMensaje = "Fecha:" + this.FechaInicio + "\n" +
            //"Hora: " + this.HoraInicio + "\n" +
            //"Personas: " + this.NoPersonas + "\n" +
            //"Restaurant: " + this.NombreRestaurante + "\n" +
            //"Silla para niños: " + this.SillaNiños + "\n" +
            //"Nombre y apellido: " + this.Nombre + "\n" +
            //"Correo electrónico: " + this.Correo + "\n" +
            //"Teléfono: " + this.Telefono;

            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("nombre", this.Nombre),
                new KeyValuePair<string, string>("email", this.Correo),
                new KeyValuePair<string, string>("telefono", this.Telefono),
                new KeyValuePair<string, string>("restaurant", this.rd.reb_nombre),
                new KeyValuePair<string, string>("fecha", this.FechaInicio),
                new KeyValuePair<string, string>("hora", this.HoraInicio),
                new KeyValuePair<string, string>("personas", this.SillaNiños),
                new KeyValuePair<string, string>("silla_ninos", this.Telefono),
            });


            var response = await this.apiService.Get<GuardadoGenerico>("/es/gastronomia/reserva", "/correo", content);

            if (!response.IsSuccess)
            {
                await Mensajes.Alerta(response.Message);
                return;
            }

            await Mensajes.Alerta("La información ha sido enviada correctamente");

            //this.FechaInicio
            //this.HoraInicio 
            this.NoPersonas = "2";
            this.NombreRestaurante = string.Empty;
            this.SillaNiños = "No";
            this.Correo = string.Empty;
            this.Telefono = string.Empty;
            this.Nombre = string.Empty;

            this.FechaInicio = "00/00/0000";


            if (rd.reb_nombre == "PIÚ")
            {

                this.HoraInicio = "12:30";

            }
            else if (rd.reb_nombre == "LE GULÁ")
            {

                this.HoraInicio = "21:00";

            }
            else
            {

                this.HoraInicio = "00:00";

            }


        }


        public ICommand CheckInReservaCommand
        {
            get
            {
                return new RelayCommand(CheckInReserva);
            }
        }

        private async void CheckInReserva()
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
            catch (Exception ex)
            {
                await Mensajes.Alerta("Ubicación denegada, activa el GPS de tu dispositivo");
            }

        }

        #endregion


        #region Method
        private async void LoadDetalleRestaurante()
        {
            var connection = await this.apiService.CheckConnection();

            if (!connection.IsSuccess)
            {
                await Mensajes.Alerta("Parece que no tenés conexión a internet, intentalo mas tarde");

                return;
            }

            VariablesGlobales.Img1 = "https://citycenter-rosario.com.ar/" + rd.reb_imagen_1;
            VariablesGlobales.Img2 = "https://citycenter-rosario.com.ar/" + rd.reb_imagen_2;
            VariablesGlobales.Img3 = "https://citycenter-rosario.com.ar/" + rd.reb_imagen_2;
            VariablesGlobales.Img4 = "https://citycenter-rosario.com.ar/" + rd.reb_imagen_3;

            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("", ""),
            });

            var response = await this.apiService.GetReal<RestauranteMenuReturn>("/gastronomia/restaurant_bar", "/ver/" + rd.reb_id, content);

            if (!response.IsSuccess)
            {
                await Mensajes.Alerta("Ha habido un error en tu solicitud, por favor volvé a intentarlo");

                return;
            }


            this.listRestaurantMenu = (RestauranteMenuReturn)response.Result;

            RestaurantMenuNombre = new ObservableCollection<MenuNombre>(this.ToRestaurantMenuNombreItemViewModel());

            RestaurantMenuDetalle = new ObservableCollection<MenuDetalle>();

            double TamanoRegistro;
            double NumeroRenglones;
            int TamanoFinal = 0;

            foreach (var l in listRestaurantMenu.resultado.menu_detalle)
            {
                NombreViejo = this.ToRestaurantMenuNombreItemViewModel().Where(p => p.men_id == l.mde_id_menu).Select(s => s.men_nombre).SingleOrDefault();

                if (NombreViejo == Nombrenuevo)
                {
                    NombreMenu = "";

                    if (Device.OS == TargetPlatform.iOS)
                    {
                        Margen = new Thickness(0, 6, 0, 10);
                    }
                    else if (Device.OS == TargetPlatform.Android)
                    {
                        Margen = new Thickness(0, -5, 0, -15);
                    }

                }
                else
                {
                    NombreMenu = NombreViejo;
                    //Margen = new Thickness(0, 5, 0, 23);

                    if (Device.OS == TargetPlatform.iOS)
                    {
                        Margen = new Thickness(0, 18, 0, 18);
                    }
                    else if (Device.OS == TargetPlatform.Android)
                    {
                        Margen = new Thickness(0, 5, 0, 23);
                    }

                }

                RestaurantMenuDetalle.Add(new MenuDetalle()
                {
                    mde_id = l.mde_id,
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

                //tamano del renglon
                TamanoRegistro = (l.mde_descripcion.Length);
                try
                {
                  
                    if (TamanoRegistro > 45)
                    {
                        double NR = Convert.ToDouble(TamanoRegistro / Convert.ToDouble(45));
                        NumeroRenglones = Convert.ToDouble(NR.ToString("#.0"));

                        string[] arr = Convert.ToString(NumeroRenglones).Split('.');

                        int entero = Convert.ToInt32(arr[0]);
                        decimal dec = Convert.ToDecimal(arr[1]);


                        if (dec > 0)
                        {
                            NumeroRenglones = entero + 1;

                        }
                        else
                        {

                            NumeroRenglones = entero;
                        }
                    }
                    else
                    {
                        NumeroRenglones = 1;
                    }
                }
                catch (Exception ex)
                {
                    NumeroRenglones = 1;
                }
              

                TamanoFinal = TamanoFinal + ((Convert.ToInt32(NumeroRenglones)) + 2);

                Nombrenuevo = this.ToRestaurantMenuNombreItemViewModel().Where(p => p.men_id == l.mde_id_menu).Select(s => s.men_nombre).SingleOrDefault();

            }

            if (listRestaurantMenu.resultado.menu_detalle.Count > 0)
            {
                MM = true;
            }
            else
            {
                MM = false;
            }

            //Piu
            if (this.rd.reb_nombre=="LE GULÁ")
            {
                TamanoListview = (TamanoFinal * 16);
            }
            else
            {
                TamanoListview = (TamanoFinal * 17)+30;   
            }
           

        }

        private IEnumerable<MenuNombre> ToRestaurantMenuNombreItemViewModel()
        {
            return this.listRestaurantMenu.resultado.menu.Select(l => new MenuNombre
            {
                men_id = l.men_id,
                men_nombre = l.men_nombre,
                men_descripcion = l.men_descripcion,
                men_id_restaurant_bar = l.men_id_restaurant_bar,
                men_id_usuario_creo = l.men_id_usuario_creo,
                men_fecha_hora_creo = l.men_fecha_hora_creo,
                men_id_usuario_modifico = l.men_id_usuario_modifico,
                men_fecha_hora_modifico = l.men_fecha_hora_modifico,
                men_estatus = l.men_estatus
            });
        }

        private IEnumerable<MenuDetalle> ToRestaurantMenuDetalleItemViewModel()
        {
            return this.listRestaurantMenu.resultado.menu_detalle.Select(l => new MenuDetalle
            {
                mde_id = l.mde_id,
                mde_id_menu = l.mde_id_menu,
                mde_id_restaurant = l.mde_id_restaurant,
                mde_nombre = l.mde_nombre,
                mde_descripcion = l.mde_descripcion,
                mde_imagen = l.mde_imagen,
                mde_precio = l.mde_precio,
                mde_id_usuario_creo = l.mde_id_usuario_creo,
                mde_fecha_hora_creo = l.mde_fecha_hora_creo,
                mde_id_usuario_modifico = l.mde_id_usuario_modifico,
                mde_fecha_hora_modifico = l.mde_fecha_hora_modifico,
                mde_estatus = l.mde_estatus,
                NombreMenu = this.ToRestaurantMenuNombreItemViewModel().Where(p => p.men_id == l.mde_id_menu).Select(s => s.men_nombre).SingleOrDefault(),

            });
        }

        #endregion

        #region Contructor
        public DetalleRestauranteViewModel(RestaurantDetalle rd)
        {
            this.apiService = new ApiService();

            this.rd = rd;

            this.FechaInicio = "00/00/0000";

            this.SillaNiños = "No";

            if (rd.reb_nombre.Contains("PIÚ"))
            {
                this.HoraInicio = "12:30";
                VariablesGlobales.HorarioPIU = true;
                VariablesGlobales.HorarioLEGULA = false;
                VariablesGlobales.HorarioCityCenter = false;
            }
            else if (rd.reb_nombre.Contains("LE GULÁ"))
            {
                this.HoraInicio = "21:00";
                VariablesGlobales.HorarioPIU = false;
                VariablesGlobales.HorarioLEGULA = true;
                VariablesGlobales.HorarioCityCenter = false;
            }
            else if (rd.reb_nombre.Contains("CITY ROCK"))
            {
                this.HoraInicio = "20:30";
                VariablesGlobales.HorarioPIU = false;
                VariablesGlobales.HorarioLEGULA = false;
                VariablesGlobales.HorarioCityCenter = true;
               
            }
            else
            {
                this.HoraInicio = "00:00";
                VariablesGlobales.HorarioPIU = false;
                VariablesGlobales.HorarioLEGULA = false;
                VariablesGlobales.HorarioCityCenter = false;
            }


            if (this.rd.reb_reservas==1)
            {
                RR = true;
            }
            else
            {
                RR = false;
            }

            LoadDetalleRestaurante();
        }
        #endregion
    }
}
