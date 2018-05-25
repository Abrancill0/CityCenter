using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Windows.Input;
using City_Center.Models;
using City_Center.Page;
using City_Center.Services;
using GalaSoft.MvvmLight.Command;
using Plugin.Share;
using Xamarin.Forms;
using static City_Center.Models.EventosResultado;
using City_Center.Clases;
using System.Linq;
using System.Threading.Tasks;
using static City_Center.Models.GuardaFavoritosResultado;

namespace City_Center.ViewModels
{
    public class EventosItemViewModel : EventosDetalle
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
            Compartir.Url = "http://cc.comprogapp.com/es/show-detail/" + this.eve_id + "/" + this.eve_nombre;

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
                    if (this.eve_guardado!=true)
                    {
                        var content = new FormUrlEncodedContent(new[]
                        {
                            new KeyValuePair<string, string>("gua_id_usuario", Application.Current.Properties["IdUsuario"].ToString()),
                            new KeyValuePair<string, string>("gua_id_evento", Convert.ToString(this.eve_id)),
                            new KeyValuePair<string, string>("gua_id_promocion", "0")
                        });

                        var response = await this.apiService.Get<GuardaFavoritosReturn>("/guardados", "/store", content);

                        if (!response.IsSuccess)
                        {
                            await Mensajes.Error("Error al guardar Guardados");
                            return;
                        }

                        var list = (GuardaFavoritosReturn)response.Result;

                        this.eve_guardado = true;
                        this.oculta = false;
                        this.eve_id_guardado = list.resultado.gua_id;

                        var actualiza = MainViewModel.GetInstance().listEventos.resultado.Where(l => l.eve_id == this.eve_id).FirstOrDefault();

                        actualiza.eve_guardado = true;
                        actualiza.oculta = false;
                        actualiza.eve_id_guardado = list.resultado.gua_id;
                       

                        await Mensajes.Alerta("Guardado Correctamente");  
                    }
                    else
                    {
                       EliminaFavoritos();    
                    }

                }
                else
                {
                    await Mensajes.Alerta("Inicia Sesion para guardar este Show");
                }
            }
            catch (Exception)
            {
                await Mensajes.Alerta("Inicia Sesion para guardar este Show");
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

                    if (this.eve_guardado == true)
                    {

                        var content = new FormUrlEncodedContent(new[]
                            {
                            new KeyValuePair<string, string>("gua_id",Convert.ToString(this.eve_id_guardado)),

                        });

                        var response = await this.apiService.Get<GuardadoGenerico>("/guardados", "/destroy", content);

                        if (!response.IsSuccess)
                        {
                            await Mensajes.Error("Error al eliminar Guardados");
                            return;
                        }

                        this.eve_guardado = false;
                        this.oculta = true;

                        var actualiza = MainViewModel.GetInstance().listEventos.resultado.Where(l => l.eve_id == this.eve_id).FirstOrDefault();

                        actualiza.eve_guardado = false;
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


        public ICommand VerDetalleShowCommand
        {
            get
            {
                return new RelayCommand(VerDetalleShow);
            }
        }
        
        private async void VerDetalleShow()
        {
            
            MainViewModel.GetInstance().DetalleShow = new DetalleShowViewModel(this);

            await ((MasterPage)Application.Current.MainPage).Detail.Navigation.PushAsync(new DetalleShow());
            
            //await Application.Current.MainPage.Navigation.PushModalAsync(new DetalleShow());
        }


        #endregion


        #region Contructors
        public EventosItemViewModel()
        {
            this.apiService = new ApiService();
        }
        #endregion

    }
}
