using City_Center.Models;
using City_Center.Page;
using City_Center.Services;
using City_Center.Services.Contracts;
using GalaSoft
    .MvvmLight.Command;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Input;
using City_Center.Clases;
using Xamarin.Forms;
using static City_Center.Models.CambiaContrasenaResultado;
using Acr.UserDialogs;

namespace City_Center.ViewModels
{
    public class CambiaPassViewModel:BaseViewModel
    {
        #region Services
        private ApiService apiService;
        #endregion

        #region Attributes
        
        private string contrasenatemp;
        private string contrasena1;
        private string contrasena2;

        #endregion

        #region Properties

        public string Contrasenatemp
        {
            get { return this.contrasenatemp; }
            set { SetValue(ref this.contrasenatemp, value); }
        }

        public string Contrasena1
        {
            get { return this.contrasena1; }
            set { SetValue(ref this.contrasena1, value); }
        }

        public string Contrasena2
        {
            get { return this.contrasena2; }
            set { SetValue(ref this.contrasena2, value); }
        }

        #endregion

        #region Commands


        public ICommand CambiaContrasenaCommand
        {
            get
            {
                return new RelayCommand(CambiaContrasena);
            }
        }

        private async void CambiaContrasena()
        {
            var connection = await this.apiService.CheckConnection();

            if (!connection.IsSuccess)
            {
                await Mensajes.Error(connection.Message);

                return;
            }
            
            if (string.IsNullOrEmpty(this.contrasenatemp))
            {
                await Mensajes.Error("Contraseña temporal requrida");
    
                return;
            }

            if (string.IsNullOrEmpty(this.Contrasena1))
            {
                await Mensajes.Error("Nueva Contraseña requrida");
            
                return;
            }

            if (string.IsNullOrEmpty(this.Contrasena2))
            {
                await Mensajes.Error("Confirma Contraseña requrida");
            
                return;
            }


            if (this.Contrasena1 != this.Contrasena2)
            {
                await Mensajes.Info("Contraseñas no coiciden");

                return;
            }


            UserDialogs.Instance.ShowLoading("Iniciando sesion...", MaskType.Black);

            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("use_id", VariablesGlobales.IDUsuario),
                new KeyValuePair<string, string>("usu_contrasena_temp", this.contrasenatemp),
                new KeyValuePair<string, string>("usu_contrasena", this.Contrasena1),
            });


            var response = await this.apiService.Get<CambiaContrasenaReturn>("/usuarios", "/modifica_contrasena", content);

            if (!response.IsSuccess)
            {
                await Mensajes.Error("Error al cambiar contraseña");

                UserDialogs.Instance.HideLoading();

                return;
            }
   
            CambiaContrasenaReturn list = (CambiaContrasenaReturn)response.Result;

            if (list.mensaje=="Contraseña No Actualizada!!!")
            {
                await Mensajes.Error("Error al cambiar contraseña");

                UserDialogs.Instance.HideLoading();

                return;
            }
         
            Application.Current.Properties["IsLoggedIn"] = true;
			Application.Current.Properties["IdUsuario"] = list.resultado.usu_id;
			Application.Current.Properties["Email"] = list.resultado.usu_email;
			Application.Current.Properties["NombreCompleto"] = list.resultado.usu_nombre + ' ' + list.resultado.usu_apellidos;
			Application.Current.Properties["Ciudad"] = list.resultado.usu_ciudad;
			Application.Current.Properties["Pass"] = this.Contrasena1;
			Application.Current.Properties["FechaNacimiento"] = list.resultado.usu_fecha_nacimiento;
			Application.Current.Properties["FotoPerfil"] = VariablesGlobales.RutaServidor + list.resultado.usu_imagen;
			Application.Current.Properties["TipoCuenta"] = "CityCenter";

			Application.Current.Properties["TipoDocumento"] = list.resultado.usu_tipo_documento;
			Application.Current.Properties["NumeroDocumento"] = list.resultado.usu_no_documento;
			Application.Current.Properties["NumeroSocio"] = list.resultado.usu_id_tarjeta_socio;

            await Application.Current.SavePropertiesAsync();

            
            MasterPage fpm = new MasterPage();
			fpm.Master = new DetailPage(); // You have to create a Master ContentPage()
			fpm.Detail = new NavigationPage(new CustomTabPage()) { BarBackgroundColor = Color.FromHex("#23144B") }; // You have to create a Detail ContenPage()
			Application.Current.MainPage = fpm;

			await Mensajes.success("Bienvenido " + list.resultado.usu_nombre + ' ' + list.resultado.usu_apellidos);

			UserDialogs.Instance.HideLoading();
         
        }


        #endregion

        #region Methods

        #endregion

        #region Contructors
		public CambiaPassViewModel()
        {
            this.apiService = new ApiService();


        }
        #endregion

    }
}
