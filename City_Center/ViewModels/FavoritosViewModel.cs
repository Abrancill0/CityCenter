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
        private FavoritoReturn list;
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
                await Mensajes.Error(connection.Message);

                return;
            }


            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("gua_id_usuario", Application.Current.Properties["IdUsuario"].ToString()),
            });


            var response = await this.apiService.Get<FavoritoReturn>("/guardados", "/index", content);

            if (!response.IsSuccess)
            {
                await Mensajes.Error("Error al cargar Guardados");

                return;
            }

            this.list = (FavoritoReturn)response.Result;

           FavoritoDetalle = new ObservableCollection<FavoritoItemViewModel>(this.ToEventosItemViewModel());


            if (this.list.resultado.Count == 0)
            {
                await Mensajes.Info("No se tiene ningun Guardado");
            }

        }

        private IEnumerable<FavoritoItemViewModel> ToEventosItemViewModel()
        {
            return this.list.resultado.Select(l => new FavoritoItemViewModel
            {
                eve_imagen = "http://cc.comprogapp.com/" + l.eve_imagen,
                eve_descripcion = l.eve_descripcion,
                eve_nombre = l.eve_nombre,
                eve_link = l.eve_link,
                eve_fecha_hora_inicio =l.eve_fecha_hora_inicio,
                eve_id = l.eve_id,
                gua_id = l.gua_id
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
