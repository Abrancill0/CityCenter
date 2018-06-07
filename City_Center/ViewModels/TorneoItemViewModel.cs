using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        //InicioViewModel Inicito = new InicioViewModel();
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
            Compartir.Url = "http://wpage.citycenter-rosario.com.ar/es/casino/torneo-detail/"+ this.tor_id +"/" + this.tor_nombre;

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
                            await Mensajes.Alerta("Ha habido un error en tu solicitud, por favor volvé a intentarlo");
                            return;
                        }

                        var list = (GuardaFavoritosReturn)response.Result;

                        this.tor_guardado = true;
                        this.oculta = false;
                        this.tor_id_guardado=list.resultado.gua_id;

                        var actualiza = MainViewModel.GetInstance().listTorneo.resultado.Where(l => l.tor_id == this.tor_id).FirstOrDefault();

                        actualiza.tor_guardado = true;
                        actualiza.oculta = false;
                        actualiza.tor_id_guardado = list.resultado.gua_id;

                      //  Inicito.TorneoDetalle = new ObservableCollection<TorneoItemViewModel>(this.ToTorneosItemViewModel());

                        await Mensajes.Alerta("Tu selección fue guardada con éxito");   
                    }
                    else
                    {
                        EliminaFavoritos();  
                    }

                }
                else
                {
                    await Mensajes.Alerta("Es necesario que te registres para completar esta acción");  
                }
            }
            catch (Exception)
            {
                await Mensajes.Alerta("Es necesario que te registres para completar esta acción");
            }

        }

        private IEnumerable<TorneoItemViewModel> ToTorneosItemViewModel()
        {
            return MainViewModel.GetInstance().listTorneo.resultado.Select(l => new TorneoItemViewModel
            {
                tor_id = l.tor_id,
                tor_nombre = l.tor_nombre,
                tor_descripcion = l.tor_descripcion,
                tor_imagen = l.tor_imagen,
                tor_fecha_hora_inicio = l.tor_fecha_hora_inicio,
                tor_fecha_hora_fin = l.tor_fecha_hora_fin,
                tor_destacado = l.tor_destacado,
                tor_id_usuario_creo = l.tor_id_usuario_creo,
                tor_fecha_hora_creo = l.tor_fecha_hora_creo,
                tor_id_usuario_modifico = l.tor_id_usuario_modifico,
                tor_fecha_hora_modifico = l.tor_fecha_hora_modifico,
                tor_estatus = l.tor_estatus,
                tor_guardado = l.tor_guardado,
                tor_id_guardado = l.tor_id_guardado,
                oculta = !(bool)l.tor_guardado
            });
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
                            await Mensajes.Alerta("Ha habido un error en tu solicitud, por favor volvé a intentarlo");
                            return;
                        }

                        this.tor_guardado= false;
                        this.oculta = true;

                        var actualiza = MainViewModel.GetInstance().listTorneo.resultado.Where(l => l.tor_id == this.tor_id).FirstOrDefault();

                        actualiza.tor_guardado = false;
                        actualiza.oculta = true;

                        var list = (GuardadoGenerico)response.Result;

                        await Mensajes.Alerta("Guardado eliminado con éxito");

                    }
                    else
                    {
                        GuardaFavorito();
                    }


                }
                else
                {
                    await Mensajes.Alerta("Es necesario que te registres para completar esta acción");
                }
            }
            catch (Exception)
            {
                await Mensajes.Alerta("Es necesario que te registres para completar esta acción");
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
