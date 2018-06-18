using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using City_Center.Services;
using Xamarin.Forms;
using static City_Center.Models.TorneoResultado;
using City_Center.Clases;

namespace City_Center.ViewModels
{
    public class TorneoViewModel : BaseViewModel
    {
        #region Services
        private ApiService apiService;
        #endregion

        #region Attributes
        private TorneoReturn listTorneo;
        private ObservableCollection<TorneoItemViewModel> torneoDetalle;
        #endregion

        #region Properties
        public ObservableCollection<TorneoItemViewModel> TorneoDetalle
        {
            get { return this.torneoDetalle; }
            set { SetValue(ref this.torneoDetalle, value); }
        }

        #endregion

        #region Methods
        private async void LoadTorneo()
        {
            var connection = await this.apiService.CheckConnection();

            if (!connection.IsSuccess)
            {
                await Mensajes.Alerta("Parece que no tenés conexión a internet, intentalo mas tarde");

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
                //await Mensajes.Error("Error al cargar Torneos");

                return;
            }

            this.listTorneo = (TorneoReturn)response.Result;

            TorneoDetalle = new ObservableCollection<TorneoItemViewModel>(this.ToTorneosItemViewModel());

        }

        private IEnumerable<TorneoItemViewModel> ToTorneosItemViewModel()
        {
            return this.listTorneo.resultado.Where(l => l.tor_id>0).Select(l => new TorneoItemViewModel
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
                tor_guardado=l.tor_guardado,
                tor_id_guardado= l.tor_id_guardado,
                oculta =  !(bool)l.tor_guardado
            });
        }

        #endregion

        #region Contructors
        public TorneoViewModel()
        {
            this.apiService = new ApiService();

            this.LoadTorneo();

        }
        #endregion
    }

}