using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Windows.Input;
using City_Center.Clases;
using City_Center.Services;
using GalaSoft.MvvmLight.Command;
using Xamarin.Forms;
using static City_Center.Models.TarjetaUsuarioResultado;

namespace City_Center.ViewModels
{
    public class ConsultaTarjetaWinViewModel:BaseViewModel
    {
        #region Services
        private ApiService apiService;
        #endregion

        #region Attributes
        private string imagenTarjeta;

        private int puntosWin;
        private string noSocio;

        private TarjetaUsuarioReturn listaTarjetausuario;

        #endregion

        #region Properties

        public string ImagenTarjeta
        {
            get { return this.imagenTarjeta; }
            set { SetValue(ref this.imagenTarjeta, value); }
        }

        public int PuntosWin
        {
            get { return this.puntosWin; }
            set { SetValue(ref this.puntosWin, value); }
        }

        public string NoSocio
        {
            get { return this.noSocio; }
            set { SetValue(ref this.noSocio, value); }
        }

        #endregion

        #region Commands
        public ICommand ConsultaPuntosCommand
        {
            get
            {
                return new RelayCommand(ConsultaPuntos);
            }
        }

        private async void ConsultaPuntos()
        {
            try
            {
                
                 LoadTarjetaUsuario();

               
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert(
                          "Error",
                           ex.ToString(),
                          "Ok");
            }

        }

       
        #endregion

        #region Methods

        private async void LoadTarjetaUsuario()
        {
            try
            {

                var connection = await this.apiService.CheckConnection();

                if (!connection.IsSuccess)
                {
                    await Mensajes.Error(connection.Message);

                    return;
                }


                var content = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("usu_id_tarjeta",NoSocio )
            });


                var response = await this.apiService.Get<TarjetaUsuarioReturn>("/tarjetas", "/tarjetaUsuario", content);

                if (!response.IsSuccess)
                {
                    await Mensajes.Error("Error al cargar Tarjeta WIn");

                    return;
                }

                this.listaTarjetausuario = (TarjetaUsuarioReturn)response.Result;

                ImagenTarjeta = VariablesGlobales.RutaServidor + listaTarjetausuario.resultado.tar_imagen;

                PuntosWin = listaTarjetausuario.resultado.tar_puntos;
                //NoSocio = listaTarjetausuario.resultado.tar_id;


            }
            catch (Exception ex)
            {
                await Mensajes.Error(ex.ToString());
            }

        }


        #endregion

        #region Contructors
        public ConsultaTarjetaWinViewModel()
        {
            this.apiService = new ApiService();
           
            //LoadTarjetaUsuario();
        }
        #endregion
    }
}
