using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Windows.Input;
using City_Center.Models;
using City_Center.Services;
using GalaSoft.MvvmLight.Command;
using Xamarin.Forms;
using static City_Center.Models.NotificacionesRecibidasResultado;
using static City_Center.Models.NotificacionesResultado;

namespace City_Center.ViewModels
{
    public class NotificacionesViewModel:BaseViewModel
    {
        #region Services
        private ApiService apiService;
        #endregion

        #region Attributes
        private NotificacionesRecibidasReturn list;
        private ObservableCollection<NotificacionesRecibidasDetalle> NotificacionesDetalle;

        #endregion

        #region Methods
        private async void LoadNotificaciones()
        {
            var content = new FormUrlEncodedContent(new[]
               {
                new KeyValuePair<string, string>("nus_id_usuario",Application.Current.Properties["IdUsuario"].ToString())
               });


            var response = await this.apiService.Get<NotificacionesRecibidasReturn>("/notificaciones/", "NotificacionesRecibidas", content);

            if (!response.IsSuccess)
            {

                return;
            }
            this.list = (NotificacionesRecibidasReturn)response.Result;

            NotificacionesDetalle = new ObservableCollection<NotificacionesRecibidasDetalle>(this.ToPromocionesItemViewModel());

        }

        private IEnumerable<NotificacionesRecibidasDetalle> ToPromocionesItemViewModel()
        {
            return this.list.respuesta.Select(l => new NotificacionesRecibidasDetalle
            {
                nen_id = l.nen_id,
                nen_equipo =l.nen_equipo,
                nen_id_usuario = l.nen_id_usuario, 
                nen_titulo = l.nen_titulo,
                nen_mensaje =l.nen_mensaje,

            });
        }

        //nen_fecha_hora_creo =l.nen_fecha_hora_creo,
        //nen_fecha_hora_modifico =l.nen_fecha_hora_creo,
        //        nen_resultado =l.nen_resultado
       
        #endregion

        #region Contructors
        public NotificacionesViewModel()
        {
            this.apiService = new ApiService();
           // LoadNotificaciones();
        
        }
        #endregion

    }
}
