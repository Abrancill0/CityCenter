using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Windows.Input;
using City_Center.Clases;
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

        private async void CompartirUrl()
        {

            Plugin.Share.Abstractions.ShareMessage Compartir = new Plugin.Share.Abstractions.ShareMessage();

            Compartir.Text = dd.des_descripcion;
            Compartir.Title = dd.des_nombre;
            Compartir.Url = "http://cc.comprogapp.com/es/show-detail/" + dd.des_id + "/" + this.dd;

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
                        new KeyValuePair<string, string>("gua_id_destacado", Convert.ToString(dd.des_id)),

                    });

                    var response = await this.apiService.Get<GuardadoGenerico>("/guardados", "/store", content);

                    if (!response.IsSuccess)
                    {
                        await Mensajes.Error("Error al guardar Guardados");
                        return;
                    }

                    var list = (GuardadoGenerico)response.Result;

					await Mensajes.success("Guardado Correctamente");

                }
                else
                {
                    await Mensajes.Info("Inicia Sesion para guardar este Destacado");
                }
            }
            catch (Exception)
            {
                await Mensajes.Info("Inicia Sesion para guardar este Destacado");
            }         
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