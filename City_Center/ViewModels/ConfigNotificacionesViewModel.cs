using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using City_Center.Services;
using Xamarin.Forms;
using static City_Center.Models.NotificacionesResultado;

namespace City_Center.ViewModels
{
    public class ConfigNotificacionesViewModel:BaseViewModel
    {
        #region Services
        private ApiService apiService;
        #endregion

        #region Attributes
        private NotificacionesReturn list;
        private ObservableCollection<NotificacionesDetalle> NotificacionesDetalle;

        #endregion

        #region Methods
        private async void LoadNotificaciones()
        {
            var content = new FormUrlEncodedContent(new[]
               {
                new KeyValuePair<string, string>("nus_id_usuario",Application.Current.Properties["IdUsuario"].ToString())
               });


            var response = await this.apiService.Get<NotificacionesReturn>("/notificaciones/", "usuarioNotificaciones", content);

            if (!response.IsSuccess)
            {

            }
            this.list = (NotificacionesReturn)response.Result;

            NotificacionesDetalle = new ObservableCollection<NotificacionesDetalle>(this.ToPromocionesItemViewModel());

        }

        private IEnumerable<NotificacionesDetalle> ToPromocionesItemViewModel()
        {
            return this.list.respuesta.Select(l => new NotificacionesDetalle
            {
                nus_id =l.nus_id,
                nus_id_usuario = l.nus_id_usuario,
                nus_id_notificacion = l.nus_id_notificacion,
                nus_fecha_hora_creo =l.nus_fecha_hora_creo,
                nus_fecha_hora_modifico =l.nus_fecha_hora_modifico,
                nus_activa = l.nus_activa,
                not_nombre = l.not_nombre,
                not_decripcion = l.not_decripcion,
            });
        }



        #endregion

        #region Contructors
        public ConfigNotificacionesViewModel()
        {
            this.apiService = new ApiService();
            LoadNotificaciones();

        }
        #endregion
    }
}
