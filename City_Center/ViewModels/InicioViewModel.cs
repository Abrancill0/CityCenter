using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Windows.Input;
using City_Center.Models;
using City_Center.Page;
using City_Center.Page.SlideMenu;
using City_Center.Services;
using GalaSoft.MvvmLight.Command;
using Xamarin.Forms;
using static City_Center.Models.EventosResultado;
using static City_Center.Models.TorneoResultado;
using City_Center.Clases;
using static City_Center.Models.TarjetaUsuarioResultado;

namespace City_Center.ViewModels
{
    public class InicioViewModel : BaseViewModel
    {
        #region Services
        private ApiService apiService;
        #endregion

        #region Attributes
        private EventosReturn list;
        private ObservableCollection<EventosItemViewModel> eventosDetalle;

        private TorneoReturn listTorneo;
        private ObservableCollection<TorneoItemViewModel> torneoDetalle;

        private TarjetaUsuarioReturn listaTarjetausuario;
        private ObservableCollection<TarjetaUsuarioDetalle> tarjetaUsuarioDetalle;

        private string imagenTarjeta;
        private int puntosWin;
        private string noSocio;

        DateTime fechaInicio;
        DateTime horaInicio;
        string noPersonas;
        string nombreRestaurante;
        string sillaNiños;
        string nombre;
        string correo;
        string telefono;
        bool verTarjeta;
        #endregion

        #region Properties

        public ObservableCollection<EventosItemViewModel> EventosDetalle
        {
            get { return this.eventosDetalle; }
            set { SetValue(ref this.eventosDetalle, value); }
        }

        public ObservableCollection<TorneoItemViewModel> TorneoDetalle
        {
            get { return this.torneoDetalle; }
            set { SetValue(ref this.torneoDetalle, value); }
        }

        public ObservableCollection<TarjetaUsuarioDetalle> TarjetaUsuarioDetalle
        {
            get { return this.tarjetaUsuarioDetalle; }
            set { SetValue(ref this.tarjetaUsuarioDetalle, value); }
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

        public string ImagenTarjeta
        {
            get { return this.imagenTarjeta; }
            set { SetValue(ref this.imagenTarjeta, value); }
        }

        public int PuntosWin
        {
            get { return this.puntosWin; }
            set { SetValue(ref this.puntosWin, value); }
        }

        public string NoSocio
        {
            get { return this.noSocio; }
            set { SetValue(ref this.noSocio, value); }
        }


        public bool VerTarjeta
        {
            get { return this.verTarjeta; }
            set { SetValue(ref this.verTarjeta, value); }
        }

        #endregion

        #region Commands
        public ICommand ShowCommand
        {
            get
            {
                return new RelayCommand(TodoShows);
            }
        }

        private async void TodoShows()
        {
            MainViewModel.GetInstance().Torneo = new TorneoViewModel();

            ((MasterPage)Application.Current.MainPage).IsPresented = false;

            await ((MasterPage)Application.Current.MainPage).Detail.Navigation.PushAsync(new Torneos());

        }

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

        #endregion

        #region Methods
        private async void LoadEventos()
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


            var response = await this.apiService.Get<EventosReturn>("/shows", "/indexApp", content);

            if (!response.IsSuccess)
            {
                await Mensajes.Error("Error al cargar Shows");

                return;
            }

            this.list = (EventosReturn)response.Result;

            EventosDetalle = new ObservableCollection<EventosItemViewModel>(this.ToEventosItemViewModel());

        }

        private IEnumerable<EventosItemViewModel> ToEventosItemViewModel()
        {
            return this.list.resultado.Select(l => new EventosItemViewModel
            {
                eve_imagen = l.eve_imagen,
                eve_descripcion = l.eve_descripcion,
                eve_nombre = l.eve_nombre,
                eve_fecha_hora_inicio = l.eve_fecha_hora_inicio,
                eve_link = l.eve_link,
                eve_id = l.eve_id
            });
        }

        private async void LoadTorneo()
        {
            try
            {
                var connection = await this.apiService.CheckConnection();

                if (!connection.IsSuccess)
                {
                    await Mensajes.Error(connection.Message);

                    return;
                }

                string IDUsuario;

                try
                {
                    IDUsuario = Application.Current.Properties["IdUsuario"].ToString();
                }
                catch (Exception)
                {
                    IDUsuario = "";
                }


                var content = new FormUrlEncodedContent(new[]
                {
                new KeyValuePair<string, string>("usu_id", IDUsuario),
                });

                var response = await this.apiService.Get<TorneoReturn>("/casino/torneos", "/indexApp", content);

                if (!response.IsSuccess)
                {
                    await Mensajes.Error("Error al cargar Torneos");

                    return;
                }

                this.listTorneo = (TorneoReturn)response.Result;

                TorneoDetalle = new ObservableCollection<TorneoItemViewModel>(this.ToTorneosItemViewModel());

            }
            catch (Exception ex)
            {

            }

           
        }

        private IEnumerable<TorneoItemViewModel> ToTorneosItemViewModel()
        {
            return this.listTorneo.resultado.Select(l => new TorneoItemViewModel
            {
                tor_id = l.tor_id,
                tor_nombre = l.tor_nombre,
                tor_descripcion = l.tor_descripcion,
                tor_imagen = l.tor_imagen,
                tor_fecha_hora_inicio = l.tor_fecha_hora_inicio,
                tor_fecha_hora_fin = l.tor_fecha_hora_fin,
                tor_destacado = l.tor_destacado,
                tor_id_usuario_creo = l.tor_id_usuario_creo,
                tor_fecha_hora_creo = l.tor_fecha_hora_creo,
                tor_id_usuario_modifico = l.tor_id_usuario_modifico,
                tor_fecha_hora_modifico = l.tor_fecha_hora_modifico,
                tor_estatus = l.tor_estatus,
                tor_guardado = l.tor_guardado,
                tor_id_guardado = l.tor_id_guardado,
                oculta = !(bool)l.tor_guardado
            });
        }
       
        private async void LoadTarjetaUsuario()
        {
            try
            {

                var connection = await this.apiService.CheckConnection();

                if (!connection.IsSuccess)
                {
                    await Mensajes.Error(connection.Message);

                    return;
                }

                string idusuario = Application.Current.Properties["IdUsuario"].ToString();

                var content = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("usu_id",idusuario )
                });


                var response = await this.apiService.Get<TarjetaUsuarioReturn>("/tarjetas", "/tarjetaUsuario", content);

                if (!response.IsSuccess)
                {
                    await Mensajes.Error("Error al cargar Tarjeta");

                    ImagenTarjeta = "";

                    PuntosWin = 0;
                    NoSocio = "";

                    ImagenTarjeta = "";
                    VerTarjeta = false;
                    PuntosWin = 0;
                    NoSocio = "";

                    return;
                }

                this.listaTarjetausuario = (TarjetaUsuarioReturn)response.Result;

                ImagenTarjeta = VariablesGlobales.RutaServidor + listaTarjetausuario.resultado.tar_imagen;

                PuntosWin = listaTarjetausuario.resultado.tar_puntos;
                NoSocio = listaTarjetausuario.resultado.tar_id;

                VerTarjeta = true;

            }
            catch (Exception)
            {
                //await Mensajes.Error(ex.ToString());

                ImagenTarjeta = "";
                VerTarjeta = false;
                PuntosWin = 0;
                NoSocio = "";
            }

        }

        #endregion

        #region Contructors
        public InicioViewModel()
        {
            this.apiService = new ApiService();
            this.LoadTarjetaUsuario();
            this.LoadTorneo();

            FechaInicio = DateTime.Today;
        }
        #endregion
    }
}
