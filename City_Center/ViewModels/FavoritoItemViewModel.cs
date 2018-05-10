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
    public class FavoritoItemViewModel:FavoritoDetalle
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

            Compartir.Text = this.descripcion;
            Compartir.Title = this.nombre;
         
            if (this.gua_id_evento>0)
            {
                Compartir.Url = "http://cc.comprogapp.com/es/es/show-detail/" + this.gua_id_evento + "/" + this.nombre;
            }

            if (this.gua_id_promocion > 0)
            {
                Compartir.Url = "http://cc.comprogapp.com/es/promocion-detail/" + this.gua_id_promocion + "/" + this.nombre;
            }

            if (this.gua_id_torneo > 0)
            {
                Compartir.Url = "http://cc.comprogapp.com/es/casino/torneo-detail/" + this.gua_id_torneo + "/" + this.nombre;
            }

            if (this.gua_id_destacado > 0)
            {
                Compartir.Url = "http://cc.comprogapp.com/es/promocion-detail/" + this.gua_id_destacado + "/" + this.nombre;
            }


            await CrossShare.Current.Share(Compartir);

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
                    var content = new FormUrlEncodedContent(new[]
                    {
                        new KeyValuePair<string, string>("gua_id",Convert.ToString(this.gua_id)),
                    
                    });

                    var response = await this.apiService.Get<GuardadoGenerico>("/guardados", "/destroy", content);

                    if (!response.IsSuccess)
                    {
                        await Mensajes.Error("Error al eliminar Guardados");
                        return;
                    }

                    var list = (GuardadoGenerico)response.Result;

                    await Mensajes.success(list.mensaje);

                }
                else
                {
                    await Mensajes.Info("Inicia Sesion para eliminar Guardados");
                }
            }
            catch (Exception)
            {
                await Mensajes.Info("Inicia Sesion para eliminar Guardados");
            }
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
