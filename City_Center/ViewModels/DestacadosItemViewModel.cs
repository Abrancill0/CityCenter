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
using static City_Center.Models.DestacadosResultado;

namespace City_Center.ViewModels
{
    public class DestacadosItemViewModel: DestacadosDetalle
    {

        #region Services
        private ApiService apiService;
        #endregion


        #region Commands

        public ICommand VerDetalleCommand
        {
            get
            {
                return new RelayCommand(VerDetalle);
            }
        }

        private async void VerDetalle()
        {
            MainViewModel.GetInstance().DestacadosDetalle = new DetalleDestacadosViewModel(this);

            await ((MasterPage)Application.Current.MainPage).Detail.Navigation.PushAsync(new DetalleDestacados());

        }


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

            Compartir.Text = this.des_descripcion;
            Compartir.Title = this.des_nombre;
            Compartir.Url = "http://cc.comprogapp.com/es/show-detail/" + this.des_id + "/" + this.des_nombre;
         
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
                        new KeyValuePair<string, string>("gua_id_evento", Convert.ToString(this.des_id)),
                        new KeyValuePair<string, string>("gua_id_promocion", "0")
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
            }
            catch (Exception)
            {
                await Mensajes.Info("Inicia Sesion para guardar este torneo");
            }

        }

        #endregion



        #region Contructors
        public DestacadosItemViewModel()
        {
            this.apiService = new ApiService();
        }
        #endregion

    }
}
