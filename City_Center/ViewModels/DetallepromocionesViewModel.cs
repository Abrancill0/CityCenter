using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Windows.Input;
using City_Center.Clases;
using City_Center.Models;
using City_Center.Page;
using City_Center.PopUp;
using City_Center.Services;
using GalaSoft.MvvmLight.Command;
using Plugin.Messaging;
using Plugin.Share;
using Xamarin.Forms;
using static City_Center.Models.PromocionesResultado;

namespace City_Center.ViewModels
{
    public class DetallepromocionesViewModel:BaseViewModel
    {
        #region Services
        private ApiService apiService;
        #endregion

        #region Attributes
        private string nombreBoton;

        private bool formularioCasino;
        private bool formularioHotel;
        private bool formularioGastronomia;
        private bool formularioShow;

        private bool llevaTelefono;

        private bool ocultaBoton;
        private bool ocultaBoton2;

        //Campos Shows
        private string nombres;
        private string telefonos;
        private string correos;

        //Campos Casino
        private string nombrec;
        private string dni;
        private string celularc;
        private string correoc;
        private string fechac;

        //Campos Hotel
        private string nombreh;
        private string correoh;
        private string telefonoh;
        private string fechah;
        private string cantidadNoches;
        private string cantidadAdulto;
        private string cantidadNiños;

        //Campos Gastronomia
        private string nombreg;
        private string correog;
        private string telefonog;
        private string fechag;

        private bool landing;

        #endregion

        #region Properties
        public PromocionesDetalle pd
        {
            get;
            set;
        }

        public bool Landing
        {
            get { return this.landing; }
            set { SetValue(ref this.landing, value); }
        }

        public string NombreBoton
        {
            get { return this.nombreBoton; }
            set { SetValue(ref this.nombreBoton, value); }
        }

        public bool FormularioCasino
        {
            get { return this.formularioCasino; }
            set { SetValue(ref this.formularioCasino, value); }
        }

        public bool FormularioGastronomia
        {
            get { return this.formularioGastronomia; }
            set { SetValue(ref this.formularioGastronomia, value); }
        }

        public bool FormularioHotel
        {
            get { return this.formularioHotel; }
            set { SetValue(ref this.formularioHotel, value); }
        }

        public bool FormularioShow
        {
            get { return this.formularioShow; }
            set { SetValue(ref this.formularioShow, value); }
        }


        public bool LlevaTelefono
        {
            get { return this.llevaTelefono; }
            set { SetValue(ref this.llevaTelefono, value); }
        }

        //Campos Casino
        public string Nombrec
        {
            get { return this.nombrec; }
            set { SetValue(ref this.nombrec, value); }
        }

        public string Dni
        {
            get { return this.dni; }
            set { SetValue(ref this.dni, value); }
        }

        public string Celularc
        {
            get { return this.celularc; }
            set { SetValue(ref this.celularc, value); }
        }

        public string Correoc
        {
            get { return this.correoc; }
            set { SetValue(ref this.correoc, value); }
        }
       
        public string Fechac
        {
            get { return this.fechac; }
            set { SetValue(ref this.fechac, value); }
        }
      
        //Campos Hotel
        public string Nombreh
        {
            get { return this.nombreh; }
            set { SetValue(ref this.nombreh, value); }
        }

        public string Correoh
        {
            get { return this.correoh; }
            set { SetValue(ref this.correoh, value); }
        }

        public string Telefonoh
        {
            get { return this.telefonoh; }
            set { SetValue(ref this.telefonoh, value); }
        }

        public string Fechah
        {
            get { return this.fechah; }
            set { SetValue(ref this.fechah, value); }
        }
     
        public string CantidadNoches
        {
            get { return this.cantidadNoches; }
            set { SetValue(ref this.cantidadNoches, value); }
        }

        public string CantidadAdulto
        {
            get { return this.cantidadAdulto; }
            set { SetValue(ref this.cantidadAdulto, value); }
        }
       
        public string CantidadNiños
        {
            get { return this.cantidadNiños; }
            set { SetValue(ref this.cantidadNiños, value); }
        }


        //Campos Gastronomia
        public string Nombreg
        {
            get { return this.nombreg; }
            set { SetValue(ref this.nombreg, value); }
        }

        public string Correog
        {
            get { return this.correog; }
            set { SetValue(ref this.correog, value); }
        }

        public string Telefonog
        {
            get { return this.telefonog; }
            set { SetValue(ref this.telefonog, value); }
        }

        public string Fechag
        {
            get { return this.fechag; }
            set { SetValue(ref this.fechag, value); }
        }
       
        public bool OcultaBoton
        {
            get { return this.ocultaBoton; }
            set { SetValue(ref this.ocultaBoton, value); }
        }

        public bool OcultaBoton2
        {
            get { return this.ocultaBoton2; }
            set { SetValue(ref this.ocultaBoton2, value); }
        }




        //Campos Shows
        public string Nombres
        {
            get { return this.nombres; }
            set { SetValue(ref this.nombres, value); }
        }

        public string Correos
        {
            get { return this.correos; }
            set { SetValue(ref this.correos, value); }
        }

        public string Telefonos
        {
            get { return this.telefonos; }
            set { SetValue(ref this.telefonos, value); }
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
            Shared.Url = "https://citycenter-rosario.com.ar/es/promocion-detail/" + this.pd.pro_id + "/" + this.pd.pro_nombre;

            await CrossShare.Current.Share(Shared);

        }

        public ICommand EnviaCorreoCasinoCommand
        {
            get
            {
                return new RelayCommand(EnviaCorreoCasino);
            }
        }

        private async void EnviaCorreoCasino()
        {
            if (string.IsNullOrEmpty(this.Nombrec))
            {
                await Mensajes.Alerta("Nombre y Apellido requeridos");

                return;
            }

            if (string.IsNullOrEmpty(this.Dni))
            {
                await Mensajes.Alerta("Dni requerido");

                return;
            }

            if (string.IsNullOrEmpty(this.Celularc))
            {
                await Mensajes.Alerta("Celular requerido");

                return;
            }

            if (string.IsNullOrEmpty(this.Correoc))
            {
                await Mensajes.Alerta("Correo electrónico requerido");

                return;
            }

            if (!ValidaEmailMethod.ValidateEMail(this.Correoc))
            {
                await Mensajes.Alerta("Correo electrónico mal estructurado");
                return;
            }


            if (string.IsNullOrEmpty(this.Fechac))
            {
                await Mensajes.Alerta("Fecha de nacimiento requerido");

                return;
            }

            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("pro_id", Convert.ToString(this.pd.pro_id)),
                new KeyValuePair<string, string>("nombre", Convert.ToString(this.Nombrec)),
                new KeyValuePair<string, string>("dni", Convert.ToString(this.Dni)),
                new KeyValuePair<string, string>("celular", Convert.ToString(this.Celularc)),
                new KeyValuePair<string, string>("fecha_nacimiento", Convert.ToString(this.Fechac))
            });

            //* Casino
            //nombre
            //dni
            //celular
            //email
            //fecha_nacimiento
            //* en el caso de los show vamos a dejar el formulario que ya tenemos

			var response = await this.apiService.Get<GuardadoGenerico>("/es/promocion-reserva", "/correo_reserva", content);

            if (!response.IsSuccess)
            {
                await Mensajes.Alerta(response.Message);
            }

            await Mensajes.Alerta("La información ha sido enviada correctamente");

            this.Nombrec = string.Empty;
            this.Dni = string.Empty;
            this.Celularc = string.Empty;
            this.Correoc = string.Empty;
            this.Fechac = string.Empty;

        }


        public ICommand EnviaCorreShowCommand
        {
            get
            {
                return new RelayCommand(EnviaCorreShow);
            }
        }

        private async void EnviaCorreShow()
        {
            if (string.IsNullOrEmpty(this.Nombres))
            {
                await Mensajes.Alerta("Nombre y Apellido requeridos");

                return;
            }

            if (string.IsNullOrEmpty(this.Correos))
            {
                await Mensajes.Alerta("Correo electrónico requerido");

                return;
            }

            if (!ValidaEmailMethod.ValidateEMail(this.Correos))
            {
                await Mensajes.Alerta("Correo electrónico mal estructurado");
                return;
            }

            if (string.IsNullOrEmpty(this.Telefonos))
            {
                await Mensajes.Alerta("Teléfono requerido");

                return;
            }


            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("pro_id", Convert.ToString(this.pd.pro_id)),
                new KeyValuePair<string, string>("nombre", Convert.ToString(this.Nombres)),
                new KeyValuePair<string, string>("email", Convert.ToString(this.correos)),
                new KeyValuePair<string, string>("telefono", Convert.ToString(this.telefonos))
            });


            var response = await this.apiService.Get<GuardadoGenerico>("/es/promocion-reserva", "/correo_reserva", content);

            if (!response.IsSuccess)
            {
                await Mensajes.Alerta(response.Message);
            }


            this.Nombres = string.Empty;
            this.Correos = string.Empty;
            this.Telefonos = string.Empty;
           
            await Mensajes.Alerta("La información ha sido enviada correctamente");

        }


        public ICommand EnviaCorreoHotelCommand
        {
            get
            {
                return new RelayCommand(EnviaCorreoHotel);
            }
        }

        private async void EnviaCorreoHotel()
        {
            if (string.IsNullOrEmpty(this.Nombreh))
            {
                await Mensajes.Alerta("Nombre y Apellido requeridos");

                return;
            }

            if (string.IsNullOrEmpty(this.Correoh))
            {
                await Mensajes.Alerta("Correo electrónico requerido");

                return;
            }

            if (!ValidaEmailMethod.ValidateEMail(this.Correoh))
            {
                await Mensajes.Alerta("Correo electrónico mal estructurado");
                return;
            }

            if (string.IsNullOrEmpty(this.Telefonoh))
            {
                await Mensajes.Alerta("Teléfono requerido");

                return;
            }

            if (string.IsNullOrEmpty(this.Fechah))
            {
                await Mensajes.Alerta("Fecha de nacimiento requerido");

                return;
            }


            if (string.IsNullOrEmpty(Convert.ToString(this.CantidadNoches)))
            {
                await Mensajes.Alerta("Cantidad de noches requerido");

                return;
            }

            if (string.IsNullOrEmpty(Convert.ToString(this.CantidadAdulto)))
            {
                await Mensajes.Alerta("Cantidad de adultos requerido");

                return;
            }

            if (string.IsNullOrEmpty(Convert.ToString(this.CantidadNiños)))
            {
                await Mensajes.Alerta("Cantidad de niños requerido");

                return;
            }
             
            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("pro_id", Convert.ToString(this.pd.pro_id)),
                new KeyValuePair<string, string>("nombre", Convert.ToString(this.Nombreh)),
                new KeyValuePair<string, string>("email", Convert.ToString(this.Correoh)),
                new KeyValuePair<string, string>("telefono", Convert.ToString(this.Telefonoh)),
                new KeyValuePair<string, string>("fecha_check_in", Convert.ToString(this.Fechah)),
                new KeyValuePair<string, string>("cantidad_noches", Convert.ToString(this.CantidadNoches)),
                new KeyValuePair<string, string>("cantidad_adultos", Convert.ToString(this.CantidadAdulto)),
                new KeyValuePair<string, string>("cantidad_ninos", Convert.ToString(this.CantidadNiños)),
            });


            var response = await this.apiService.Get<GuardadoGenerico>("/es/promocion-reserva", "/correo_reserva", content);

            if (!response.IsSuccess)
            {
                await Mensajes.Alerta(response.Message);
            }

            this.Nombreh = string.Empty;
            this.Correoh = string.Empty;
            this.Telefonoh = string.Empty;
            this.Fechah = string.Empty;
            this.CantidadNoches = string.Empty;
            this.CantidadAdulto = string.Empty;
            this.CantidadNiños = string.Empty;

            await Mensajes.Alerta("La información ha sido enviada correctamente");

        }


        public ICommand EnviaCorreoGastronomiaCommand
        {
            get
            {
                return new RelayCommand(EnviaCorreoGastronomia);
            }
        }

        private async void EnviaCorreoGastronomia()
        {
            if (string.IsNullOrEmpty(this.Nombreg))
            {
                await Mensajes.Alerta("Nombre y Apellido requeridos");

                return;
            }

            if (string.IsNullOrEmpty(this.Correog))
            {
                await Mensajes.Alerta("Correo electrónico requerido");

                return;
            }

            if (!ValidaEmailMethod.ValidateEMail(this.Correog))
            {
                await Mensajes.Alerta("Correo electrónico mal estructurado");
                return;
            }

            if (string.IsNullOrEmpty(this.Telefonog))
            {
                await Mensajes.Alerta("Teléfono requerido");

                return;
            }

            if (string.IsNullOrEmpty(this.Fechag))
            {
                await Mensajes.Alerta("Fecha solicitada requerido");

                return;
            }


            //*Gastronomia
            //nombre
            //email
            //telefono
            //fecha_solicitada



            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("pro_id", Convert.ToString(this.pd.pro_id)),
                new KeyValuePair<string, string>("nombre", Convert.ToString(this.Nombreg)),
                new KeyValuePair<string, string>("email", Convert.ToString(this.Correog)),
                new KeyValuePair<string, string>("telefono", Convert.ToString(this.Telefonog)),
                new KeyValuePair<string, string>("fecha_solicitada", Convert.ToString(this.Fechag)),
            });


            var response = await this.apiService.Get<GuardadoGenerico>("/es/promocion-reserva", "/correo_reserva", content);

            if (!response.IsSuccess)
            {
                await Mensajes.Alerta(response.Message);
            }

            await Mensajes.Alerta("La información ha sido enviada correctamente");

            this.Nombreg = string.Empty;
            this.Correog = string.Empty;
            this.Telefonog = string.Empty;
            this.Fechag = string.Empty;


        }


        public ICommand FormulariosCommand
        {
            get
            {
                return new RelayCommand(Formularios);
            }
        }

        private async void Formularios()
        {
            if (this.pd.pro_vinculo == "formulario")
            {

                FormularioCasino = false;
                FormularioHotel = false;
                FormularioGastronomia = false;
                FormularioShow = false;

                Landing = false;

                switch (this.pd.pro_tipo)
                {
                    case "cas":
                        FormularioCasino = true;
                        break;
                    case "hopa":
                        FormularioHotel = true;
                        break;
                    case "gas":
                        FormularioGastronomia = true; ;
                        break;
                        case "show":
                        FormularioShow = true; ;
                        break;
                }

                OcultaBoton = false;
                OcultaBoton2 = true;

            }
            else
            {
                VariablesGlobales.RutaCompraOnline = this.pd.pro_url;

                await((MasterPage)Application.Current.MainPage).Detail.Navigation.PushAsync(new WebViewCompraOnline());

            }

        }

        public ICommand RegresarCommand
        {
            get
            {
                return new RelayCommand(Regresar);
            }
        }

        private void Regresar()
        {
            FormularioCasino = false;
            FormularioHotel = false;
            FormularioGastronomia = false;

            OcultaBoton = true;
            OcultaBoton2 = false;

            Landing = true;
        }

        public ICommand LlamarCommand
        {
            get
            {
                return new RelayCommand(Llamar);
            }
        }

        private void Llamar()
        {
            var phoneCallTask = CrossMessaging.Current.PhoneDialer;

            if (phoneCallTask.CanMakePhoneCall)
            {
                phoneCallTask.MakePhoneCall(this.pd.pro_telefono, this.pd.pro_nombre);
            }

        }

        #endregion

        public DetallepromocionesViewModel(PromocionesDetalle pd)
        {
            this.apiService = new ApiService();
            this.pd = pd;

            if (this.pd.pro_vinculo == "formulario")
             {
                

                switch (this.pd.pro_tipo)
                {
                    case "cas":
                        NombreBoton = "PARTICIPAR";    
                        break;
                    case "hopa":
                        NombreBoton = "CONSULTAR";    
                        break;
                    case "gas":
                        NombreBoton = "PARTICIPAR";    
                        break;
                    case "show":
                        NombreBoton = "PARTICIPAR";    
                        break;
                }

             }
            else if (this.pd.pro_vinculo == "url")
            {
                NombreBoton = "Comprar";  
            }
           
            OcultaBoton = true;
            Landing = true;

            if (this.pd.pro_vinculo == "telefono")
            {
                OcultaBoton2 = false;
                OcultaBoton = false;
            }


            if (string.IsNullOrEmpty(this.pd.pro_telefono))
            {
                LlevaTelefono = false;
            }
            else
            {
                LlevaTelefono = true;
            }


        }
    }
}
