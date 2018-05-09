using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Windows.Input;
using City_Center.Clases;
using City_Center.Services;
using GalaSoft.MvvmLight.Command;
using Xamarin.Forms;
using static City_Center.Models.SalasEventosResultado;
using System.Net.Http.Headers;
using City_Center.Models;

namespace City_Center.ViewModels
{
    public class SalasEventosViewModel : BaseViewModel
    {
        
        #region Services
        private ApiService apiService;
        #endregion

        #region Attributes
        private SalasEventosReturn listSalasEventos;
        private ObservableCollection<SalasEventosDetalle> salasEventosDetalle;

        private string imagen_Selected;

		private string nombre;
		private string correo;
		private string empresa;
		private string celular;
		private string tipoEvento;
		private string asistentes;
		private DateTime fecha;
		private string comentarios;

        #endregion

        #region Properties

        public ObservableCollection<SalasEventosDetalle> SalasEventosDetalle
        {
            get { return this.salasEventosDetalle; }
            set { SetValue(ref this.salasEventosDetalle, value); }
        }
        
        public string Imagen_Selected
        {
            get { return this.imagen_Selected; }
            set { SetValue(ref this.imagen_Selected, value); }
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

		public string Empresa
        {
			get { return this.empresa; }
			set { SetValue(ref this.empresa, value); }
        }

		public string Celular
        {
			get { return this.celular; }
			set { SetValue(ref this.celular, value); }
        }

		public string TipoEvento
        {
			get { return this.tipoEvento; }
			set { SetValue(ref this.tipoEvento, value); }
        }

		public string Asistentes
        {
			get { return this.asistentes; }
			set { SetValue(ref this.asistentes, value); }
        }

		public DateTime Fecha
        {
			get { return this.fecha; }
			set { SetValue(ref this.fecha, value); }
        }

		public string Comentarios
        {
			get { return this.comentarios; }
			set { SetValue(ref this.comentarios, value); }
        }

        #endregion

        #region Commands

		public ICommand EnviaCorreoCommand
        {
            get
            {
				return new RelayCommand(EnviaCorreo);
            }
        }

		private async void EnviaCorreo()
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

			if (string.IsNullOrEmpty(this.Empresa))
            {
                await Mensajes.Error("Empresa requerido");

                return;
            }

			if (string.IsNullOrEmpty(this.Celular))
            {
				await Mensajes.Error("Celular requerido");

                return;
            }

			if (string.IsNullOrEmpty(this.Asistentes))
            {
				await Mensajes.Error("Asistentes requerido");

                return;
            }
   
   
            var content = new FormUrlEncodedContent(new[]
            {            
				new KeyValuePair<string, string>("fecha_aproximada", Convert.ToString(Fecha)),
				new KeyValuePair<string, string>("nombre", Nombre),
				new KeyValuePair<string, string>("email", Correo),
				new KeyValuePair<string, string>("empresa", Empresa),
				new KeyValuePair<string, string>("telefono", Celular),
				new KeyValuePair<string, string>("tipo_evento", TipoEvento),
				new KeyValuePair<string, string>("cantidad_asistentes", Asistentes),
				new KeyValuePair<string, string>("comentarios", Comentarios),
                
            });


			var response = await this.apiService.Get<GuardadoGenerico>("/es/convenciones-salas/tarifas-salas", "/correo", content);

            if (!response.IsSuccess)
            {
                await Mensajes.Error(response.Message);
            }

            await Mensajes.success("Correo enviado exitosamente");

            //this.FechaInicio
			this.Celular = string.Empty; 
            this.Nombre = string.Empty;
            this.Correo = string.Empty;
            this.Empresa = string.Empty;
            this.TipoEvento = string.Empty;
            this.Asistentes = string.Empty;
			this.Comentarios = string.Empty;

        }


		//
        #endregion

        #region Methods
        private async void LoadSalasEventos()
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


            var response = await this.apiService.Get<SalasEventosReturn>("/convenciones_salas/galeria_ecs", "/indexApp", content);

            if (!response.IsSuccess)
            {
                await Mensajes.Error("Error al cargar Galerias");

                return;
            }

            this.listSalasEventos = (SalasEventosReturn)response.Result;

            Imagen_Selected = VariablesGlobales.RutaServidor + this.listSalasEventos.resultado[0].gal_imagen;

            SalasEventosDetalle = new ObservableCollection<SalasEventosDetalle>(this.ToSalasEventosItemViewModel());

        }

        private IEnumerable<SalasEventosDetalle> ToSalasEventosItemViewModel()
        {
            return this.listSalasEventos.resultado.Select(l => new SalasEventosDetalle
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
                gal_eliminado = l.gal_eliminado
            });
        }
        
        #endregion

        #region Contructors
        public SalasEventosViewModel()
        {
            this.apiService = new ApiService();
			Fecha = DateTime.Today;
            this.LoadSalasEventos();
           
        }
        #endregion
    }
}

