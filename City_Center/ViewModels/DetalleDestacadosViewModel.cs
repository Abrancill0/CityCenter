using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Windows.Input;
using City_Center.Models;
using City_Center.Services;
using GalaSoft.MvvmLight.Command;
using Plugin.Share;
using Xamarin.Forms;
using static City_Center.Models.DestacadosResultado;

namespace City_Center.ViewModels
{
    public class DetalleDestacadosViewModel
    {
        #region Services
        private ApiService apiService;
        #endregion

        #region Attributes

        #endregion

        #region Properties
        public DestacadosDetalle dd
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

        private void CompartirUrl()
        {
            //Plugin.Share.Abstractions.ShareMessage Compartir = new Plugin.Share.Abstractions.ShareMessage();

            //Compartir.Text = this.ds.eve_nombre;
            //Compartir.Title = this.ds.eve_nombre;
            //Compartir.Url = this.ds.eve_link;

            //await CrossShare.Current.Share(Compartir);

        }

        public ICommand GuardaFavoritoCommand
        {
            get
            {
                return new RelayCommand(GuardaFavorito);
            }
        }

        private void GuardaFavorito()
        {
            //var content = new FormUrlEncodedContent(new[]
            // {
            //    new KeyValuePair<string, string>("gua_id_usuario", Application.Current.Properties["IdUsuario"].ToString()),
            //    new KeyValuePair<string, string>("gua_id_evento", Convert.ToString(this.ds.eve_id)),
            //    new KeyValuePair<string, string>("gua_id_promocion", "0")

            // });

            //var response = await this.apiService.Get<GuardadoGenerico>("/guardados", "/store", content);

            //if (!response.IsSuccess)
            //{
            //    await Application.Current.MainPage.DisplayAlert(
            //          "Error",
            //          response.Message,
            //          "Ok");
            //    return;
            //}

            //var list = (GuardadoGenerico)response.Result;

            //await Application.Current.MainPage.DisplayAlert(
                     //"City Center",
                     // list.mensaje,
                     //"Ok");

        }

        #endregion

        #region Methods

        #endregion

        #region Contructors
            public DetalleDestacadosViewModel(DestacadosDetalle dd)
        {
            this.apiService = new ApiService();

            this.dd = dd;

        }
        #endregion
    }
}