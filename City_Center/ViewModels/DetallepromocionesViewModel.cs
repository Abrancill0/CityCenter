using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Windows.Input;
using City_Center.Clases;
using City_Center.Models;
using City_Center.Services;
using GalaSoft.MvvmLight.Command;
using Plugin.Share;
using static City_Center.Models.PromocionesResultado;

namespace City_Center.ViewModels
{
    public class DetallepromocionesViewModel:BaseViewModel
    {
        #region Services
        private ApiService apiService;
        #endregion

        #region Attributes
        private string nombre;
        private string correo;
        private string telefono; 
        #endregion

        #region Properties
        public PromocionesDetalle pd
        {
            get;
            set;
        }

        public string Correo
        {
            get { return this.correo; }
            set { SetValue(ref this.correo, value); }
        }

        public string Nombre
        {
            get { return this.nombre; }
            set { SetValue(ref this.nombre, value); }
        }

        public string Telefono
        {
            get { return this.telefono; }
            set { SetValue(ref this.telefono, value); }
        }
        #endregion

        #region Commands

        public ICommand CompartirCommand
        {
            get
            {
                return new RelayCommand(Compartir);
            }
        }

        private async void Compartir()
        {

            Plugin.Share.Abstractions.ShareMessage Shared = new Plugin.Share.Abstractions.ShareMessage();

            Shared.Text = this.pd.pro_descripcion;
            Shared.Title = this.pd.pro_nombre;
            Shared.Url = "http://cc.comprogapp.com/es/promocion-detail/" + this.pd.pro_id + "/" + this.pd.pro_nombre;

            await CrossShare.Current.Share(Shared);

        }


        public ICommand EnviaCorreoCommand
        {
            get
            {
                return new RelayCommand(EnviaCorreo);
            }
        }

        private async void EnviaCorreo()
        {
            if (string.IsNullOrEmpty(this.Nombre))
            {
                await Mensajes.Error("Nombre y apellido requeridos");

                return;
            }

            if (string.IsNullOrEmpty(this.Telefono))
            {
                await Mensajes.Error("Correo electrónico requerido");

                return;
            }


            if (string.IsNullOrEmpty(this.Telefono))
            {
                await Mensajes.Error("Teléfono requerido");

                return;
            }


            //string CuerpoMensaje = "Nombre y apellido: " + this.Nombre + "\n" +
                                   //"Correo electrónico: " + this.Correo + "\n" +
                                   //"Teléfono: " + this.Telefono;

                  
            var content = new FormUrlEncodedContent(new[]
            {
				new KeyValuePair<string, string>("pro_id", Convert.ToString(this.pd.pro_id)),
				new KeyValuePair<string, string>("nombre", this.Nombre),
				new KeyValuePair<string, string>("email", this.Correo),
				new KeyValuePair<string, string>("telefono", this.Telefono),
            });


			var response = await this.apiService.Get<GuardadoGenerico>("/es/promocion-reserva", "/correo_reserva", content);

            if (!response.IsSuccess)
            {
                await Mensajes.Error(response.Message);
            }

            await Mensajes.Alerta("Promoción reservada con éxito");

        
            this.Nombre = string.Empty;
            this.Correo = string.Empty;
            this.Telefono = string.Empty;
 
        }

        #endregion

        public DetallepromocionesViewModel(PromocionesDetalle pd)
        {
            this.apiService = new ApiService();
            this.pd = pd;

        }
    }
}
