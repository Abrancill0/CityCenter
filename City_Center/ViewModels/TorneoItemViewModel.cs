using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Windows.Input;
using City_Center.Clases;
using City_Center.Models;
using City_Center.Page;
using City_Center.Services;
using GalaSoft.MvvmLight.Command;
using Plugin.Share;
using Xamarin.Forms;
using static City_Center.Models.GuardaFavoritosResultado;
using static City_Center.Models.TorneoResultado;

namespace City_Center.ViewModels
{
    public class TorneoItemViewModel:TorneoDetalle
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
            MainViewModel.GetInstance().TorneoDetalle = new TorneoDetalleViewModel(this);

            await ((MasterPage)Application.Current.MainPage).Detail.Navigation.PushAsync(new DetalleTorneo());

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

            Compartir.Text = this.tor_descripcion;
            Compartir.Title = this.tor_nombre;
            Compartir.Url = "http://cc.comprogapp.com/es/casino/torneo-detail/"+ this.tor_id +"/" + this.tor_nombre;

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

                    if (this.tor_guardado == false)
                    {
                        var content = new FormUrlEncodedContent(new[]
                        {
                        new KeyValuePair<string, string>("gua_id_usuario", Application.Current.Properties["IdUsuario"].ToString()),
                        new KeyValuePair<string, string>("gua_id_torneo", Convert.ToString(this.tor_id)),
                        });

                        var response = await this.apiService.Get<GuardaFavoritosReturn>("/guardados", "/store", content);

                        if (!response.IsSuccess)
                        {
                            await Mensajes.Error("Error al guardar Guardados");
                            return;
                        }

                        var list = (GuardaFavoritosReturn)response.Result;

                        this.tor_guardado = false;
                        this.oculta = true;

                        var actualiza = MainViewModel.GetInstance().listTorneo.resultado.Where(l => l.tor_id == this.tor_id).FirstOrDefault();

                        actualiza.tor_guardado = true;
                        actualiza.oculta = false;
                        actualiza.tor_id_guardado = list.resultado.gua_id;

                        await Mensajes.Alerta("Guardado Correctamente");   
                    }
                    else
                    {
                        EliminaFavoritos();  
                    }

                }
                else
                {
                    await Mensajes.Alerta("Inicia Sesion para guardar este torneo");  
                }
            }
            catch (Exception)
            {
                await Mensajes.Alerta("Inicia Sesion para guardar este torneo");
            }

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

                    if (this.tor_guardado == true)
                    {

                        var content = new FormUrlEncodedContent(new[]
                            {
                            new KeyValuePair<string, string>("gua_id",Convert.ToString(this.tor_id_guardado)),

                        });

                        var response = await this.apiService.Get<GuardadoGenerico>("/guardados", "/destroy", content);

                        if (!response.IsSuccess)
                        {
                            await Mensajes.Error("Error al eliminar Guardados");
                            return;
                        }

                        this.tor_guardado= false;
                        this.oculta = true;

                        var actualiza = MainViewModel.GetInstance().listTorneo.resultado.Where(l => l.tor_id == this.tor_id).FirstOrDefault();

                        actualiza.tor_guardado = false;
                        actualiza.oculta = true;

                        var list = (GuardadoGenerico)response.Result;

                        await Mensajes.Alerta("Guardado eliminado correctamente");

                    }
                    else
                    {
                        GuardaFavorito();
                    }


                }
                else
                {
                    await Mensajes.Alerta("Inicia Sesion para eliminar Guardados");
                }
            }
            catch (Exception)
            {
                await Mensajes.Alerta("Inicia Sesion para eliminar Guardados");
            }
        }



        #endregion

        #region Contructors
        public TorneoItemViewModel()
        {
            this.apiService = new ApiService();
        }
        #endregion

    }
}
