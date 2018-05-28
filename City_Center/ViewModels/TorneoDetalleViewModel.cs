using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Windows.Input;
using City_Center.Clases;
using City_Center.Models;
using City_Center.Page;
using City_Center.Services;
using GalaSoft.MvvmLight.Command;
using Plugin.Share;
using Xamarin.Forms;
using static City_Center.Models.GuardaFavoritosResultado;
using static City_Center.Models.TorneoResultado;

namespace City_Center.ViewModels
{
	public class TorneoDetalleViewModel:BaseViewModel
    {
        #region Attributes
        private ApiService apiService;

		private string nombre;
		private string correo;
		private string tipoDocumento;
		private string numeroDocumento;
		private string fecha;
		private string nacionalidad;
		private string pais;
		private string provincia;
		private string ciudad;

        InicioViewModel Inicito = new InicioViewModel();

        #endregion

		#region Properties
        public TorneoDetalle td
        {
            get;
            set;
        }
        
		public string Correo
        {
			get { return this.correo; }
			set { SetValue(ref this.correo, value); }
        }

        public string Nombre
        {
            get { return this.nombre; }
            set { SetValue(ref this.nombre, value); }
        }
  
		public string TipoDocumento
        {
			get { return this.tipoDocumento; }
			set { SetValue(ref this.tipoDocumento, value); }
        }

		public string NumeroDocumento
        {
			get { return this.numeroDocumento; }
			set { SetValue(ref this.numeroDocumento, value); }
        }

		public string Nacionalidad
        {
			get { return this.nacionalidad; }
			set { SetValue(ref this.nacionalidad, value); }
        }

        public string Fecha
        {
            get { return this.fecha; }
            set { SetValue(ref this.fecha, value); }
        }

		public string Pais
        {
			get { return this.pais; }
			set { SetValue(ref this.pais, value); }
        }


		public string Provincia
        {
			get { return this.provincia; }
			set { SetValue(ref this.provincia, value); }
        }

		public string Ciudad
        {
			get { return this.ciudad; }
			set { SetValue(ref this.ciudad, value); }
        }

        #endregion

        #region Command
        public ICommand CompartirCommand
        {
            get
            {
                return new RelayCommand(CompartirUrl);
            }
        }

        private async void CompartirUrl()
        {
            Plugin.Share.Abstractions.ShareMessage Compartir = new Plugin.Share.Abstractions.ShareMessage();

            Compartir.Text = td.tor_descripcion;
            Compartir.Title = td.tor_nombre;
            Compartir.Url = "http://cc.comprogapp.com/es/casino/torneo-detail/" + td.tor_id+ "/" + td.tor_nombre;

            await CrossShare.Current.Share(Compartir);

        }

        public ICommand GuardaFavoritoCommand
        {
            get
            {
                return new RelayCommand(GuardaFavorito);
            }
        }


        public ICommand EliminaFavoritosCommand
        {
            get
            {
                return new RelayCommand(EliminaFavoritos);
            }
        }

        private async void EliminaFavoritos()
        {
            try
            {
                bool isLoggedIn = Application.Current.Properties.ContainsKey("IsLoggedIn") ?
                                     (bool)Application.Current.Properties["IsLoggedIn"] : false;

                if (isLoggedIn)
                {

                    if (this.td.tor_guardado == true)
                    {

                        var content = new FormUrlEncodedContent(new[]
                            {
                            new KeyValuePair<string, string>("gua_id",Convert.ToString(this.td.tor_id)),

                        });

                        var response = await this.apiService.Get<GuardadoGenerico>("/guardados", "/destroy", content);

                        if (!response.IsSuccess)
                        {
                            await Mensajes.Alerta("Error al eliminar Guardados");
                            return;
                        }

                        this.td.tor_guardado = false;
                        this.td.oculta = true;

                        var actualiza = MainViewModel.GetInstance().listTorneo.resultado.Where(l => l.tor_id == this.td.tor_id).FirstOrDefault();

                        actualiza.tor_guardado = false;
                        actualiza.oculta = true;

                        Inicito.TorneoDetalle = new ObservableCollection<TorneoItemViewModel>(this.ToTorneosItemViewModel());


                        var list = (GuardadoGenerico)response.Result;

                        await Mensajes.Alerta("Guardado eliminado correctamente");


                    }
                    else
                    {
                        GuardaFavorito();
                    }


                }
                else
                {
                    await Mensajes.Alerta("Inicia Sesion para eliminar Guardados");
                }
            }
            catch (Exception)
            {
                await Mensajes.Alerta("Inicia Sesion para eliminar Guardados");
            }
        }


        private async void GuardaFavorito()
        {
            try
            {
                bool isLoggedIn = Application.Current.Properties.ContainsKey("IsLoggedIn") ?
                                     (bool)Application.Current.Properties["IsLoggedIn"] : false;

                if (isLoggedIn)
                {
                    if (this.td.tor_guardado == false)
                    {
                        var content = new FormUrlEncodedContent(new[]
                        {
                            new KeyValuePair<string, string>("gua_id_usuario", Application.Current.Properties["IdUsuario"].ToString()),
                            new KeyValuePair<string, string>("gua_id_torneo", Convert.ToString(td.tor_id)),
                        });

                        var response = await this.apiService.Get<GuardaFavoritosReturn>("/guardados", "/store", content);

                        if (!response.IsSuccess)
                        {
                            await Mensajes.Alerta("Error al guardar Guardados");
                            return;
                        }

                        var list = (GuardaFavoritosReturn)response.Result;

                        this.td.tor_guardado = true;
                        this.td.oculta = false;
                        this.td.tor_id_guardado = list.resultado.gua_id;

                        var actualiza = MainViewModel.GetInstance().listTorneo.resultado.Where(l => l.tor_id == this.td.tor_id).FirstOrDefault();

                        actualiza.tor_guardado = true;
                        actualiza.oculta = false;
                        actualiza.tor_id_guardado = list.resultado.gua_id;

                        Inicito.TorneoDetalle = new ObservableCollection<TorneoItemViewModel>(this.ToTorneosItemViewModel());

                        await Mensajes.Alerta("Guardado Correctamente");  
                    }
                    else
                    {
                        EliminaFavoritos();  
                    }

                }
                else
                {
                    await Mensajes.Alerta("Inicia Sesion para guardar este torneo");
                }
            }
            catch (Exception)
            {
                await Mensajes.Alerta("Inicia Sesion para guardar este torneo");
            }

        }
        
        public ICommand EnviaCorreoCommand
		{
			get
			{
				return new RelayCommand(EnviaCorreo);	
			}
		}

        private async void EnviaCorreo()
		{
         
			if (string.IsNullOrEmpty(Nombre))
			{
			await Mensajes.Info("Nombre Requerido");
				return;
			}

			if (string.IsNullOrEmpty(Correo))
            {
                await Mensajes.Info("Correo Requerido");
                return;
            }

			if (string.IsNullOrEmpty(NumeroDocumento))
            {
				await    Mensajes.Info("Numero de documento Requerido");
                return;
            }

			if (string.IsNullOrEmpty(Nacionalidad))
            {
				await  Mensajes.Info("Nacionalidad Requerido");
                return;
            }

			if (string.IsNullOrEmpty(Provincia))
            {
				await  Mensajes.Info("Provincia Requerido");
                return;
            }

			if (string.IsNullOrEmpty(TipoDocumento))
            {
				await  Mensajes.Info("Tipo de documento Requerido");
                return;
            }

			if (string.IsNullOrEmpty(Pais))
            {
				await  Mensajes.Info("Pais Requerido");
                return;
            }

			if (string.IsNullOrEmpty(Ciudad))
            {
				await  Mensajes.Info("Ciudad Requerido");
                return;
            }

			var content = new FormUrlEncodedContent(new[]
            {
				new KeyValuePair<string, string>("email", Correo), 
				new KeyValuePair<string, string>("tor_nombre", td.tor_nombre),            
				new KeyValuePair<string, string>("nombre", Convert.ToString(Nombre)),            
				new KeyValuePair<string, string>("numero_documento", NumeroDocumento),
				new KeyValuePair<string, string>("nacionalidad", Nacionalidad),
				new KeyValuePair<string, string>("provincia", Provincia),
				new KeyValuePair<string, string>("tipo_de_documento", TipoDocumento),
                new KeyValuePair<string, string>("fecha_nac", Fecha),
				new KeyValuePair<string, string>("pais", Pais),
				new KeyValuePair<string, string>("ciudad", Ciudad)

            });


			var response = await this.apiService.Get<GuardadoGenerico>("/casino/torneos", "/registro_torneo", content);

            if (!response.IsSuccess)
            {
                await Mensajes.Alerta("Parece que no tenés conexión a internet, intentalo mas tarde");
				return;
            }

            await Mensajes.Alerta("Correo enviado exitosamente");

			Correo = string.Empty;
            Nombre=string.Empty;         
			NumeroDocumento=string.Empty;
			Nacionalidad=string.Empty;
			Provincia = string.Empty;
			TipoDocumento =string.Empty;
			pais =string.Empty;
			Ciudad =string.Empty;
            Fecha = "00/00/0000";
            pais = string.Empty;
		}
           

        private IEnumerable<TorneoItemViewModel> ToTorneosItemViewModel()
        {
            return MainViewModel.GetInstance().listTorneo.resultado.Select(l => new TorneoItemViewModel
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


        #endregion
      
        public TorneoDetalleViewModel(TorneoDetalle td)
        {
            this.apiService = new ApiService();
            this.td = td;

			this.Fecha = "00/00/0000";

        }
    }
}
