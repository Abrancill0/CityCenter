using System;
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
        private bool mM;


        DateTime fechaInicio;
        DateTime horaInicio;
        string noPersonas;
        string nombreRestaurante;
        string sillaNiños;
        string nombre;
        string correo;
        string telefono;
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

        public DateTime FechaInicio
        {
            get { return this.fechaInicio; }
            set { SetValue(ref this.fechaInicio, value); }
        }

        public DateTime HoraInicio
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
                await Mensajes.Error("Nombre requerido");

                return;
            }

            if (string.IsNullOrEmpty(this.Correo))
            {
                await Mensajes.Error("Correo requerido");

                return;
            }

            if (string.IsNullOrEmpty(this.Telefono))
            {
                await Mensajes.Error("Telefono requerido");

                return;
            }


            string CuerpoMensaje = "Fecha:" + this.FechaInicio.ToString("dd/MM/yyyy") + "\n" +
                                   "Hora: " + this.HoraInicio + "\n" +
                                   "Personas: " + this.NoPersonas + "\n" +
                                   "Restaurant: " + this.NombreRestaurante + "\n" +
                                   "Silla para niños: " + this.SillaNiños + "\n" +
                                   "Nombre y apellido: " + this.Nombre + "\n" +
                                   "Correo electrónico: " + this.Correo + "\n" +
                                   "Teléfono: " + this.Telefono;


            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("nombre", "Reservacion de Mesa"),
                new KeyValuePair<string, string>("email", "abrx23@gmail.com"),
                new KeyValuePair<string, string>("mensaje", CuerpoMensaje),
            });


            var response = await this.apiService.Get<GuardadoGenerico>("/correo", "/envioemail", content);

            if (!response.IsSuccess)
            {
                await Mensajes.Error(response.Message);
            }

            await Mensajes.success("Correo enviado exitosamente");

            //this.FechaInicio
            //this.HoraInicio 
            this.NoPersonas = string.Empty;
            this.NombreRestaurante = string.Empty;
            this.SillaNiños = string.Empty;
            this.Correo = string.Empty;
            this.Telefono = string.Empty;

        }
        #region Command

        #endregion


        #region Method
        private async void LoadDetalleRestaurante()
        {
            var connection = await this.apiService.CheckConnection();

            if (!connection.IsSuccess)
            {
                await Mensajes.Error(connection.Message);

                return;
            }

            VariablesGlobales.Img1 = "http://cc.comprogapp.com/" + rd.reb_imagen_1;
            VariablesGlobales.Img2 = "http://cc.comprogapp.com/" + rd.reb_imagen_2;
            VariablesGlobales.Img3 = "http://cc.comprogapp.com/" + rd.reb_imagen_2;
            VariablesGlobales.Img4 = "http://cc.comprogapp.com/" + rd.reb_imagen_3;

            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("", ""),
            });

            var response = await this.apiService.GetReal<RestauranteMenuReturn>("/gastronomia/restaurant_bar", "/ver/" + rd.reb_id, content);

            if (!response.IsSuccess)
            {
                await Mensajes.Error("Error al cargar Restaurantes/Bar");

                return;
            }


            this.listRestaurantMenu = (RestauranteMenuReturn)response.Result;

            RestaurantMenuNombre = new ObservableCollection<MenuNombre>(this.ToRestaurantMenuNombreItemViewModel());

            RestaurantMenuDetalle = new ObservableCollection<MenuDetalle>();

            foreach (var l in listRestaurantMenu.resultado.menu_detalle)
            {
                NombreViejo = this.ToRestaurantMenuNombreItemViewModel().Where(p => p.men_id == l.mde_id_menu).Select(s => s.men_nombre).SingleOrDefault();

                if (NombreViejo == Nombrenuevo)
                {
                    NombreMenu = ""; 
                }
                else
                {
                    NombreMenu =  NombreViejo; 
                }

                RestaurantMenuDetalle.Add(new MenuDetalle()
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
                    NombreMenu = NombreMenu
                               
                });

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

           // RestaurantMenuDetalle = new ObservableCollection<MenuDetalle>(this.ToRestaurantMenuDetalleItemViewModel());

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

            LoadDetalleRestaurante();
        }
        #endregion
    }
}
