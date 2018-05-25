﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Windows.Input;
using Acr.UserDialogs;
using City_Center.Clases;
using City_Center.Page;
using City_Center.Services;
using City_Center.Services.Contracts;
using GalaSoft.MvvmLight.Command;
using Xamarin.Forms;
using static City_Center.Models.TarjetaUsuarioResultado;

namespace City_Center.ViewModels
{
    public class DetailViewModel : BaseViewModel
    {
        #region Services
        private ApiService apiService;
        #endregion

        #region Attributes
        private string nombreUsuario;
        private string[] listaOpciones;
        private bool perfilVisible;
        private bool opcionesVisible;
        private string imagen;
        private string imagenTarjeta;

        private int puntosWin;
        private string noSocio;

        private bool verTarjeta;

        private TarjetaUsuarioReturn listaTarjetausuario;
        private ObservableCollection<TarjetaUsuarioDetalle> tarjetaUsuarioDetalle;


        private IGoogleManager _googleManager;
        #endregion

        #region Properties

        public string NombreUsuario
        {
            get { return this.nombreUsuario; }
            set { SetValue(ref this.nombreUsuario, value); }
        }

        public string[] ListaOpciones
        {
            get { return this.listaOpciones; }
            set { SetValue(ref this.listaOpciones, value); }
        }

        public bool PerfilVisible
        {
            get { return this.perfilVisible; }
            set { SetValue(ref this.perfilVisible, value); }
        }

        public bool OpcionesVisible
        {
            get { return this.opcionesVisible; }
            set { SetValue(ref this.opcionesVisible, value); }
        }

        public string Imagen
        {
            get { return this.imagen; }
            set { SetValue(ref this.imagen, value); }
        }

        public ObservableCollection<TarjetaUsuarioDetalle> TarjetaUsuarioDetalle
        {
            get { return this.tarjetaUsuarioDetalle; }
            set { SetValue(ref this.tarjetaUsuarioDetalle, value); }
        }

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

        public bool VerTarjeta
        {
            get { return this.verTarjeta; }
            set { SetValue(ref this.verTarjeta, value); }
        }

        #endregion

        #region Commands
        public ICommand CierraSesionCommand
        {
            get
            {
                return new RelayCommand(CierraSesion);
            }
        }

        private async void CierraSesion()
        {
            try
            {
                var result = await Application.Current.MainPage.DisplayAlert("CityCenter", "¿ESTÁ SEGURO QUE DESEA SALIR DE LA APLICACACIÓN?", "OK", "Cancelar");

                string Mensajevalida = string.Format("Result {0}", result);

                if (Mensajevalida == "Result True")
                {
                    UserDialogs.Instance.ShowLoading("Cerrando sesion...", MaskType.Black);

                    Application.Current.Properties["IsLoggedIn"] = false;
                    Application.Current.Properties["IdUsuario"] = 0;
                    Application.Current.Properties["Email"] = "";
                    Application.Current.Properties["NombreCompleto"] = "";
                    Application.Current.Properties["Ciudad"] = "";
                    Application.Current.Properties["Pass"] = "";
                    Application.Current.Properties["FechaNacimiento"] = "";
                    Application.Current.Properties["FotoPerfil"] = "";
                    Application.Current.Properties["TipoCuenta"] = "";

                    await Application.Current.SavePropertiesAsync();

                    MainViewModel.GetInstance().Master = new MasterViewModel();
                    MainViewModel.GetInstance().Inicio = new InicioViewModel();
                    MainViewModel.GetInstance().Detail = new DetailViewModel();
                    MainViewModel.GetInstance().Casino = new CasinoViewModel();

                    PerfilVisible = false;
                    OpcionesVisible = true;

                    ((MasterPage)Application.Current.MainPage).IsPresented = false;

                    _googleManager = DependencyService.Get<IGoogleManager>();
                    _googleManager.Logout();
                  
                    MasterPage fpm = new MasterPage();
                    fpm.Master = new DetailPage(); // You have to create a Master ContentPage()

					App.NavPage = new NavigationPage(new CustomTabPage()) { BarBackgroundColor = Color.FromHex("#23144B") };

					fpm.Detail = App.NavPage; // You have to create a Detail ContenPage()
                    Application.Current.MainPage = fpm;

                    UserDialogs.Instance.HideLoading();
                }

            }
            catch (Exception ex)
            {
                UserDialogs.Instance.HideLoading();

                await Application.Current.MainPage.DisplayAlert(
                          "Error",
                           ex.ToString(),
                          "Ok");
            }

        }

        public ICommand PerfilCommand
        {
            get
            {
                return new RelayCommand(VerPerfil);
            }
        }

        private async void VerPerfil()
        {
            MainViewModel.GetInstance().Perfil = new PerfilViewModel();

            ((MasterPage)Application.Current.MainPage).IsPresented = false;

			App.NavPage.BarBackgroundColor=Color.FromHex("#23144B"); 

            await ((MasterPage)Application.Current.MainPage).Detail.Navigation.PushAsync(new Perfil());

        }

        public ICommand IniciarSesionCommand
        {
            get
            {
                return new RelayCommand(IniciarSesion);
            }
        }

        private async void IniciarSesion()
        {
            MainViewModel.GetInstance().Login = new LoginViewModel();

            ((MasterPage)Application.Current.MainPage).IsPresented = false;

			App.NavPage.BarBackgroundColor=Color.FromHex("#23144B"); 

            await ((MasterPage)Application.Current.MainPage).Detail.Navigation.PushAsync(new Login());

        }


        public ICommand RegistrarseCommand
        {
            get
            {
                return new RelayCommand(Registrarse);
            }
        }

        private async void Registrarse()
        {
            MainViewModel.GetInstance().Registro = new RegistroViewModel();

            ((MasterPage)Application.Current.MainPage).IsPresented = false;

            await ((MasterPage)Application.Current.MainPage).Detail.Navigation.PushAsync(new Registro());

        }

        #endregion

        #region Methods
        private void LoadEventos()
        {

            ListaOpciones = new string[] { "INICIO", "SHOWS", "PROMOCIONES", "GUARDADOS", "MÁS INFORMACIÓN", "TÉRMINOS Y CONDICIONES GENERALES DE USO" };

            bool isLoggedIn = Application.Current.Properties.ContainsKey("IsLoggedIn") ?
                              (bool)Application.Current.Properties["IsLoggedIn"] : false;

            try
            {
                if (isLoggedIn)
                {
                    PerfilVisible = true;
                    OpcionesVisible = false;

                    NombreUsuario = Application.Current.Properties["NombreCompleto"].ToString();

                    string fotoPerfil = Application.Current.Properties["FotoPerfil"].ToString();

                    if (fotoPerfil == "http://cc.comprogapp.com/")
                    {
                        Imagen = "user";
                    }
                    else
                    {
                        Imagen = Application.Current.Properties["FotoPerfil"].ToString();
                    }

                   // Imagen = Application.Current.Properties["FotoPerfil"].ToString();
                   
                }
                else
                {
                    PerfilVisible = false;
                    OpcionesVisible = true;
                }

            }
            catch (Exception)
            {
                PerfilVisible = false;
                OpcionesVisible = true;
            }
        }

        private async void LoadTarjetaUsuario()
        {
            try
            {
                
            var connection = await this.apiService.CheckConnection();

            if (!connection.IsSuccess)
            {
                await Mensajes.Error(connection.Message);
                   
                VerTarjeta = false;

                return;
            }

                string idusuario = Application.Current.Properties["IdUsuario"].ToString();

            var content = new FormUrlEncodedContent(new[]
            {
                    new KeyValuePair<string, string>("usu_id",idusuario )
            });


            var response = await this.apiService.Get<TarjetaUsuarioReturn>("/tarjetas", "/tarjetaUsuario", content);

            if (!response.IsSuccess)
            {
                await Mensajes.Error("Error al cargar Tarjeta WIn");

                    VerTarjeta = false;

                return;
            }

            this.listaTarjetausuario = (TarjetaUsuarioReturn)response.Result;

                ImagenTarjeta = VariablesGlobales.RutaServidor + listaTarjetausuario.resultado.tar_imagen;

                PuntosWin = listaTarjetausuario.resultado.tar_puntos;
                NoSocio = listaTarjetausuario.resultado.tar_id;

                VerTarjeta = true;
            }
            catch (Exception)
            {
                VerTarjeta = false;
            }

        }


        #endregion

        #region Contructors
        public DetailViewModel()
        {
            this.apiService = new ApiService();
            LoadEventos();
            LoadTarjetaUsuario();
        }
        #endregion
    }

}
