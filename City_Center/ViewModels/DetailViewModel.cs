using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Windows.Input;
using City_Center.Clases;
using City_Center.Page;
using City_Center.Services;
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


        private TarjetaUsuarioReturn listaTarjetausuario;
        private ObservableCollection<TarjetaUsuarioDetalle> tarjetaUsuarioDetalle;
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
                var result = await Application.Current.MainPage.DisplayAlert("CityCenter", "Are you sure you want to exit the application?", "OK", "Cancel");

                string Mensajevalida = string.Format("Result {0}", result);

                if (Mensajevalida == "Result True")
                {

                    Application.Current.Properties["IsLoggedIn"] = false;

                    await Application.Current.SavePropertiesAsync();

                    ((MasterPage)Application.Current.MainPage).IsPresented = false;

                    PerfilVisible = false;
                    OpcionesVisible = true;


                }

            }
            catch (Exception ex)
            {
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

            ListaOpciones = new string[] { "INICIO", "SHOWS", "PROMOCIONES", "GUARDADOS", "MÀS INFORMACIÒN", "TÈRMINOS Y CONDICIONES GENERALES DE USO" };

            bool isLoggedIn = Application.Current.Properties.ContainsKey("IsLoggedIn") ?
                              (bool)Application.Current.Properties["IsLoggedIn"] : false;

            try
            {
                if (isLoggedIn)
                {
                    PerfilVisible = true;
                    OpcionesVisible = false;
                    NombreUsuario = Application.Current.Properties["NombreCompleto"].ToString();
                    Imagen = Application.Current.Properties["FotoPerfil"].ToString();
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
            var connection = await this.apiService.CheckConnection();

            if (!connection.IsSuccess)
            {
                await Mensajes.Error(connection.Message);

                return;
            }


            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("usu_id", Application.Current.Properties["IdUsuario"].ToString()),
            });


            var response = await this.apiService.Get<TarjetaUsuarioReturn>("/tarjetas", "/tarjetaUsuario", content);

            if (!response.IsSuccess)
            {
                await Mensajes.Error("Error al cargar Torneos");

                return;
            }

            this.listaTarjetausuario = (TarjetaUsuarioReturn)response.Result;

            TarjetaUsuarioDetalle = new ObservableCollection<TarjetaUsuarioDetalle>(this.ToTarjetaUsuarioViewModel());

        }

        private IEnumerable<TarjetaUsuarioDetalle> ToTarjetaUsuarioViewModel()
        {
            return this.listaTarjetausuario.resultado.Select(l => new TarjetaUsuarioDetalle
            {
                tar_id = l.tar_id,
                tar_id_tipo = l.tar_id_tipo,
                tar_puntos = l.tar_puntos,
                tar_fecha_hora_creo = l.tar_fecha_hora_creo,
                tar_fecha_hora_modifico = l.tar_fecha_hora_modifico,
                tar_imagen = l.tar_imagen,
            });
        }

        #endregion

        #region Contructors
        public DetailViewModel()
        {
            LoadEventos();
        }
        #endregion
    }

}
