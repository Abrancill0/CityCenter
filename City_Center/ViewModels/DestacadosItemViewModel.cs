﻿using System;
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
using static City_Center.Models.DestacadosResultado;
using static City_Center.Models.GuardaFavoritosResultado;

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
            Compartir.Url = "https://citycenter-rosario.com.ar/es/show-detail/" + this.des_id + "/" + this.des_nombre;
         
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
                    if (this.des_guardado == false)
                    {
                        var content = new FormUrlEncodedContent(new[]
                     {
                        new KeyValuePair<string, string>("gua_id_usuario", Application.Current.Properties["IdUsuario"].ToString()),
                        new KeyValuePair<string, string>("gua_id_destacado", Convert.ToString(this.des_id)),

                    });

                        var response = await this.apiService.Get<GuardaFavoritosReturn>("/guardados", "/store", content);

                        if (!response.IsSuccess)
                        {
                            await Mensajes.Alerta("Ha habido un error en tu solicitud, por favor volvé a intentarlo");
                            return;
                        }

                        var list = (GuardaFavoritosReturn)response.Result;


                        this.des_guardado = true;
                        this.oculta = false;
                        this.des_id_guardado = list.resultado.gua_id;

                        var actualiza = MainViewModel.GetInstance().listDestacados.resultado.Where(l => l.des_id == this.des_id).FirstOrDefault();

                        actualiza.des_guardado = true;
                        actualiza.oculta = false;
                        actualiza.des_id_guardado = list.resultado.gua_id;

                        await Mensajes.Alerta("Guardado con éxito");

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

                    if (this.des_guardado == true)
                    {

                        var content = new FormUrlEncodedContent(new[]
                            {
                            new KeyValuePair<string, string>("gua_id",Convert.ToString(this.des_id_guardado)),

                        });

                        var response = await this.apiService.Get<GuardadoGenerico>("/guardados", "/destroy", content);

                        if (!response.IsSuccess)
                        {
                            await Mensajes.Alerta("Ha habido un error en tu solicitud, por favor volvé a intentarlo");
                            return;
                        }

                        this.des_guardado = false;
                        this.oculta = true;

                        var actualiza = MainViewModel.GetInstance().listDestacados.resultado.Where(l => l.des_id == this.des_id).FirstOrDefault();

                        actualiza.des_guardado = false;
                        actualiza.oculta = true;

                       // var list = (GuardadoGenerico)response.Result;

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
        public DestacadosItemViewModel()
        {
            this.apiService = new ApiService();
        }
        #endregion

    }
}
