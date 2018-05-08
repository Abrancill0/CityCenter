using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Windows.Input;
using City_Center.Clases;
using City_Center.Models;
using City_Center.Page;
using City_Center.Services;
using GalaSoft.MvvmLight.Command;
using Plugin.Share;
using Xamarin.Forms;
using static City_Center.Models.TorneoResultado;

namespace City_Center.ViewModels
{
    public class TorneoDetalleViewModel
    {
        #region Attributes
        private ApiService apiService;
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

        private async void GuardaFavorito()
        {
            try
            {

                bool isLoggedIn = Application.Current.Properties.ContainsKey("IsLoggedIn") ?
                                     (bool)Application.Current.Properties["IsLoggedIn"] : false;

                if (isLoggedIn)
                {
                    var content = new FormUrlEncodedContent(new[]
                    {
                        new KeyValuePair<string, string>("gua_id_usuario", Application.Current.Properties["IdUsuario"].ToString()),
                        new KeyValuePair<string, string>("gua_id_torneo", Convert.ToString(td.tor_id)),
                    });

                    var response = await this.apiService.Get<GuardadoGenerico>("/guardados", "/store", content);

                    if (!response.IsSuccess)
                    {
                        await Mensajes.Error("Error al guardar Guardados");
                        return;
                    }

                    var list = (GuardadoGenerico)response.Result;

                    await Mensajes.success(list.mensaje);

                }
                else
                {
                    await Mensajes.Info("Inicia Sesion para guardar este torneo");
                }
            }
            catch (Exception)
            {
                await Mensajes.Info("Inicia Sesion para guardar este torneo");
            }

        }

        #endregion

        #region Properties
        public TorneoDetalle td
        {
            get;
            set;
        }
        #endregion

        public TorneoDetalleViewModel(TorneoDetalle td)
        {
            this.apiService = new ApiService();
            this.td = td;

        }
    }
}
