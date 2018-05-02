using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Windows.Input;
using City_Center.Models;
using City_Center.Services;
using GalaSoft.MvvmLight.Command;
using Plugin.Share;
using Xamarin.Forms;
using static City_Center.Models.EventosResultado;
using City_Center.Clases;

namespace City_Center.ViewModels
{
    public class DetalleShowViewModel
    {
        #region Services
        private ApiService apiService;
        #endregion

        #region Attributes

        #endregion

        #region Properties
        public EventosDetalle ds
        {
            get;
            set;
        }

        #endregion

        #region Commands
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

            Compartir.Text = this.ds.eve_nombre;
            Compartir.Title = this.ds.eve_nombre;
            Compartir.Url = this.ds.eve_link;

            await CrossShare.Current.Share(Compartir);

        }

        public ICommand GuardaFavoritoCommand
        {
            get
            {
                return new RelayCommand(GuardaFavorito);
            }
        }

        private async void GuardaFavorito()
        {
            var content = new FormUrlEncodedContent(new[]
             {
                new KeyValuePair<string, string>("gua_id_usuario", Application.Current.Properties["IdUsuario"].ToString()),
                new KeyValuePair<string, string>("gua_id_evento", Convert.ToString(this.ds.eve_id)),
                new KeyValuePair<string, string>("gua_id_promocion", "0")

             });

            var response = await this.apiService.Get<GuardadoGenerico>("/guardados", "/store", content);

            if (!response.IsSuccess)
            {
                await Mensajes.Error(response.Message);

                return;
            }

            var list = (GuardadoGenerico)response.Result;

            await Mensajes.success(list.mensaje);

        }

        #endregion

        #region Methods
       
        #endregion

        #region Contructors
        public DetalleShowViewModel(EventosDetalle ds)
        {
            this.apiService = new ApiService();

            this.ds = ds;

        }
        #endregion
    }
}