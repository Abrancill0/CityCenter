using System;
using System.Windows.Input;
using City_Center.Page;
using GalaSoft.MvvmLight.Command;
using Xamarin.Forms;

namespace City_Center.ViewModels
{
    public class DetailViewModel : BaseViewModel
    {
        #region Services

        #endregion

        #region Attributes
        private string nombreUsuario;
        private string[] listaOpciones;
        private bool perfilVisible;
        private bool opcionesVisible;
        private string imagen;
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
        #endregion

        #region Contructors
        public DetailViewModel()
        {
            LoadEventos();
        }
        #endregion
    }

}
