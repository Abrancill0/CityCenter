using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Windows.Input;
using City_Center.Models;
using City_Center.Services;
using GalaSoft.MvvmLight.Command;
using Plugin.Share;
using Xamarin.Forms;
using static City_Center.Models.FavoritosResultado;
using City_Center.Clases;

namespace City_Center.ViewModels
{
    public class FavoritoItemViewModel:FavoritosDetalle
    {

        #region Services
        private ApiService apiService;
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

            Compartir.Text = this.eve_descripcion;
            Compartir.Title = this.eve_nombre;
            Compartir.Url = this.eve_link;

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
                new KeyValuePair<string, string>("gua_id",Convert.ToString(this.gua_id) ),
              
             });


            var response = await this.apiService.Get<GuardadoGenerico>("/guardados", "/destroy", content);

            if (!response.IsSuccess)
            {
                await Mensajes.Error("Error al guardar Guardados");
                return;
            }

            var list = (GuardadoGenerico)response.Result;

            await Mensajes.success(list.mensaje);

        }

        #endregion


        #region Contructors
        public FavoritoItemViewModel()
        {
            this.apiService = new ApiService();
        }
        #endregion

    }
}
