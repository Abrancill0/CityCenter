using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Windows.Input;
using City_Center.Clases;
using City_Center.Services;
using GalaSoft.MvvmLight.Command;
using static City_Center.Models.ReiniciaPassResultado;

namespace City_Center.ViewModels
{
    public class ReiniciaPassViewModel : BaseViewModel
    {
        #region Services
        private ApiService apiService;
        #endregion

        #region Attributes


        private string correoElectronico;


        #endregion

        #region Properties

        public string CorreoElectronico
        {
            get { return this.correoElectronico; }
            set { SetValue(ref this.correoElectronico, value); }
        }

        #endregion

        #region Commands


        public ICommand RestableceCommand
        {
            get
            {
                return new RelayCommand(Restablece);
            }
        }

        private async void Restablece()
        {
            var connection = await this.apiService.CheckConnection();

            if (!connection.IsSuccess)
            {
                await Mensajes.Alerta("Parece que no tenés conexión a internet, intentalo mas tarde");

                return;
            }


            if (string.IsNullOrEmpty(this.CorreoElectronico))
            {
                await Mensajes.Alerta("Correo electrónico requerido");
                return;
            }


            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("usu_email", this.CorreoElectronico),
            });


            var response = await this.apiService.Get<ReiniciaPassReturn>("/usuarios", "/reinicia_contrasena", content);

            if (!response.IsSuccess)
            {
                await Mensajes.Alerta("Error al restablecer contraseña, intente de nuevo");

                return;
            }

            this.CorreoElectronico = string.Empty;

            await Mensajes.Alerta("Contraseña temporal generada, se ha enviado a tu correo electrónico");


        }


        #endregion

        #region Methods

        #endregion

        #region Contructors
        public ReiniciaPassViewModel()
        {
            this.apiService = new ApiService();


        }
        #endregion
    }
}
