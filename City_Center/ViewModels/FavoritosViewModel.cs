using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using City_Center.Services;
using Xamarin.Forms;
using static City_Center.Models.FavoritosResultado;
using City_Center.Clases;

namespace City_Center.ViewModels
{
    public class FavoritosViewModel:BaseViewModel
    {
        #region Services
        private ApiService apiService;
        #endregion

        #region Attributes
       
        private ObservableCollection<FavoritoItemViewModel> favoritoDetalle;
        #endregion

        #region Properties
        public ObservableCollection<FavoritoItemViewModel> FavoritoDetalle
        {
            get { return this.favoritoDetalle; }
            set { SetValue(ref this.favoritoDetalle, value); }
        }


        #endregion

        #region Commands
        
        #endregion

        #region Methods
        private async void LoadEventos()
        {
            var connection = await this.apiService.CheckConnection();

            if (!connection.IsSuccess)
            {
                await Mensajes.Alerta("Verificá tu conexión a Internet");

                return;
            }


            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("gua_id_usuario", Application.Current.Properties["IdUsuario"].ToString()),
            });


            var response = await this.apiService.Get<FavoritoReturn>("/guardados", "/index", content);

            if (!response.IsSuccess)
            {
                await Mensajes.Alerta("Ha habido un error en tu solicitud, por favor volvé a intentarlo");

                return;
            }

            MainViewModel.GetInstance().listFavoritos = (FavoritoReturn)response.Result;

           FavoritoDetalle = new ObservableCollection<FavoritoItemViewModel>(this.ToEventosItemViewModel());


            if (MainViewModel.GetInstance().listFavoritos.resultado.Count == 0)
            {
                await Mensajes.Alerta("Parece que no tenés Guardados");
            }

        }

        private IEnumerable<FavoritoItemViewModel> ToEventosItemViewModel()
        {
            return MainViewModel.GetInstance().listFavoritos.resultado.Select(l => new FavoritoItemViewModel
            {
                  gua_id = l.gua_id,
                  gua_id_usuario  = l.gua_id_usuario,
                  gua_id_evento = l.gua_id_evento,
                  gua_id_promocion = l.gua_id_promocion,
                  gua_id_torneo = l.gua_id_torneo,
                  gua_id_destacado = l.gua_id_destacado,
                  gua_id_usuario_creo = l.gua_id_usuario_creo,
                  gua_fecha_hora_creo = l.gua_fecha_hora_creo,
                  gua_id_usuario_modifico = l.gua_id_usuario_modifico,
                  gua_fecha_hora_modifico = l.gua_fecha_hora_modifico,
                  nombre = l.nombre,
                  descripcion = l.descripcion,
                  imagen = "http://cc.comprogapp.com/" + l.imagen,
                  imagen_2 = l.imagen_2,
                  link = l.link,
                  fecha = l.fecha,
				  ocultallamada = (string.IsNullOrEmpty(l.telefono) ? false : true),
				  ocultaonline = (string.IsNullOrEmpty(l.link) ? false : true),
				  ocultatorneo = l.gua_id_torneo == 0 ? false : true,
            });
        }

        #endregion

        #region Contructors
        public FavoritosViewModel()
        {
            this.apiService = new ApiService();

            this.LoadEventos();

        }
        #endregion
    }

}
