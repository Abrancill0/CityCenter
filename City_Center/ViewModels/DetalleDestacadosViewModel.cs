using System;
using System.Collections.Generic;
using System.Linq;
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
using static City_Center.Models.DestacadosResultado;
using static City_Center.Models.GuardaFavoritosResultado;

namespace City_Center.ViewModels
{
    public class DetalleDestacadosViewModel:BaseViewModel
    {
        #region Services
        private ApiService apiService;
        #endregion

        #region Attributes
        bool ocultaLlamada;
        bool ocultaLink;
        #endregion

        #region Properties
        public bool OcultaLlamada
        {
            get { return this.ocultaLlamada; }
            set { SetValue(ref this.ocultaLlamada, value); }
        }

        public bool OcultaLink
        {
            get { return this.ocultaLink; }
            set { SetValue(ref this.ocultaLink, value); }
        }

        public DestacadosDetalle dd
        {
            get;
            set;
        }

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

            Compartir.Text = dd.des_descripcion;
            Compartir.Title = dd.des_nombre;
            Compartir.Url = "https://citycenter-rosario.com.ar/es/show-detail/" + dd.des_id + "/" + this.dd;

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

                    if (this.dd.des_guardado == false)
                    { 
                        
                    var content = new FormUrlEncodedContent(new[]
                    {
                        new KeyValuePair<string, string>("gua_id_usuario", Application.Current.Properties["IdUsuario"].ToString()),
                        new KeyValuePair<string, string>("gua_id_destacado", Convert.ToString(dd.des_id)),

                    });

                        var response = await this.apiService.Get<GuardaFavoritosReturn>("/guardados", "/store", content);

                        if (!response.IsSuccess)
                        {
                            await Mensajes.Alerta("Ha habido un error en tu solicitud, por favor volvé a intentarlo");
                            return;
                        }

                        var list = (GuardaFavoritosReturn)response.Result;


                        this.dd.des_guardado = true;
                        this.dd.oculta = false;

                        var actualiza = MainViewModel.GetInstance().listDestacados.resultado.Where(l => l.des_id == this.dd.des_id).FirstOrDefault();

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

                    if (this.dd.des_guardado == true)
                    {

                        var content = new FormUrlEncodedContent(new[]
                            {
                            new KeyValuePair<string, string>("gua_id",Convert.ToString(this.dd.des_id_guardado)),

                        });

                        var response = await this.apiService.Get<GuardadoGenerico>("/guardados", "/destroy", content);

                        if (!response.IsSuccess)
                        {
                            await Mensajes.Alerta("Ha habido un error en tu solicitud, por favor volvé a intentarlo");
                            return;
                        }

                        this.dd.des_guardado = false;
                        this.dd.oculta = true;

                        var actualiza = MainViewModel.GetInstance().listDestacados.resultado.Where(l => l.des_id == this.dd.des_id).FirstOrDefault();

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
                phoneCallTask.MakePhoneCall(dd.des_telefono, dd.des_nombre);
            }

        }

        public ICommand CompraOnlineCommand
        {
            get
            {
                return new RelayCommand(CompraOnline);
            }
        }

        async private void CompraOnline()
        {
            VariablesGlobales.RutaCompraOnline = this.dd.des_link;

            // await app.cur.PushPopupAsync(new WebViewTienda);


            // await  Application.Current.MainPage.Navigation.PushAsync(new WebViewCompraOnline());


            await ((MasterPage)Application.Current.MainPage).Detail.Navigation.PushAsync(new WebViewCompraOnline());
            //Device.OpenUri(new Uri(ds.eve_link));

        }


        #endregion

        #region Methods

        #endregion

        #region Contructors
            public DetalleDestacadosViewModel(DestacadosDetalle dd)
        {
            this.apiService = new ApiService();

            this.dd = dd;

           this.OcultaLlamada = this.dd.ocultallamada;
           this.OcultaLink = this.dd.ocultaonline;

         

        }
        #endregion
    }
}