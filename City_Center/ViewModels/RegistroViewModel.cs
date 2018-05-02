using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Windows.Input;
using City_Center.Models;
using City_Center.Page;
using City_Center.Services;
using City_Center.Services.Contracts;
using GalaSoft.MvvmLight.Command;
using Xamarin.Forms;
using System.IO;
using static City_Center.Models.RegistroUsuario;
using City_Center.Clases;

namespace City_Center.ViewModels
{
    public class RegistroViewModel:BaseViewModel
    {
        #region Services
        private ApiService apiService;
        #endregion

        #region Attributes
        private string email;
        private string ciudad;
        private DateTime fecha;
        private string password;
        private string password2;
        private bool isEnabled;
        private string nombre;
        private ObservableCollection<RegistroResult> registroReturn;
        #endregion

        private IGoogleManager _googleManager;
        private IFacebookManager _facebookManager;

        #region Properties
        public string Ciudad
        {
            get { return this.ciudad; }
            set { SetValue(ref this.ciudad, value); }
        }

        public string Email
        {
            get { return this.email; }
            set { SetValue(ref this.email, value); }
        }

        public DateTime Fecha
        {
            get { return this.fecha; }
            set { SetValue(ref this.fecha, value); }
        }

        public bool IsEnabled
        {
            get { return this.isEnabled; }
            set { SetValue(ref this.isEnabled, value); }
        }

        public string Nombre
        {
            get { return this.nombre; }
            set { SetValue(ref this.nombre, value); }
        }

        public string Password
        {
            get { return this.password; }
            set { SetValue(ref this.password, value); }
        }

        public string Password2
        {
            get { return this.password2; }
            set { SetValue(ref this.password2, value); }
        }

        public ObservableCollection<RegistroResult> RegistroReturn
        {
            get { return this.registroReturn; }
            set { SetValue(ref this.registroReturn, value); }
        }


        #endregion

        #region Commands

        public ICommand PruebaImagenCommand
        {
            get
            {
                return new RelayCommand(PruebaImagen);
            }
        }

        private async void PruebaImagen()
        {

            var dirotro = "";

            if (string.IsNullOrEmpty(VariablesGlobales.RutaImagene))
            {
               await Mensajes.Error("No se subio ninguna foto");
            }
            else
            {
                byte[] ImagenSubir = File.ReadAllBytes(VariablesGlobales.RutaImagene);

                dirotro = Convert.ToBase64String(ImagenSubir);    
            }
                
            var content = new FormUrlEncodedContent(new[]
           {
                new KeyValuePair<string, string>("usu_id", "1"),
                new KeyValuePair<string, string>("usu_imagenstr", dirotro)

            });


            var response = await this.apiService.Get<RegistroResult>("/usuarios", "/carga_foto", content);

        }

        public ICommand RegistroCommand
        {
            get
            {
                return new RelayCommand(Registro);
            }
        }

        private async void Registro()
        {
            if (string.IsNullOrEmpty(this.Ciudad))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Erorr",
                    "You Must an City",
                    "Accept");
                return;
            }

         
            if (string.IsNullOrEmpty(this.Email))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Erorr",
                    "You Must an email",
                    "Accept");
                return;
            }

            if (string.IsNullOrEmpty(this.Password))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Erorr",
                    "You Must an password",
                    "Accept");
                return;
            }

            if (string.IsNullOrEmpty(this.Password2))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Erorr",
                    "You Must an password2",
                    "Accept");
                return;
            }

            if (this.Password != this.Password2)
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Erorr",
                    "Pass1 different Pass2",
                    "Accept");
                return;
            }


            var content = new FormUrlEncodedContent(new[]
           {
                new KeyValuePair<string, string>("usu_usuario", this.Email),
                new KeyValuePair<string, string>("usu_contrasena", this.Password),
                new KeyValuePair<string, string>("usu_tipo_contrasena", "1"),
                new KeyValuePair<string, string>("usu_usuario_bloquedado", "0"),
                new KeyValuePair<string, string>("usu_nombre", this.Nombre),
                new KeyValuePair<string, string>("usu_apellidos", ""),
                new KeyValuePair<string, string>("usu_email", this.Email),
                new KeyValuePair<string, string>("usu_celular", ""),
                new KeyValuePair<string, string>("usu_telefono", ""),
                new KeyValuePair<string, string>("usu_ciudad", this.Ciudad),
                new KeyValuePair<string, string>("usu_id_rol", "6"),
                new KeyValuePair<string, string>("usu_id_tarjeta_socio", "1"),
                new KeyValuePair<string, string>("usu_estatus", "1"),
                new KeyValuePair<string, string>("usu_id_tarjeta_socio", ""),
                new KeyValuePair<string, string>("usu_fecha_nacimiento", this.Fecha.ToString("yyyy/MM/dd")),
            });


            var response = await this.apiService.Get<RegistroResult>("/usuarios", "/store", content);

           if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert(
                      "Error",
                      response.Message,
                      "Ok");
                return;
            }

            //LoginReturn list = (LoginReturn)response.Result;

            Application.Current.Properties["IsLoggedIn"] = true;
            Application.Current.Properties["Email"] = this.Email;
            Application.Current.Properties["NombreCompleto"] = this.Nombre;
            Application.Current.Properties["Ciudad"] = this.Ciudad;
            Application.Current.Properties["Pass"] = this.Password;
            Application.Current.Properties["FechaNacimiento"] = this.Fecha;

            await Application.Current.SavePropertiesAsync();

            await Application.Current.MainPage.DisplayAlert(
                      "Login",
                      "Welcome " + this.Nombre,
                      "Ok");


            this.Email = string.Empty;
            this.Nombre = string.Empty;
            this.Ciudad = string.Empty;
            this.Password = string.Empty;
            this.Ciudad = string.Empty;

            MainViewModel.GetInstance().Master = new MasterViewModel();
            MainViewModel.GetInstance().Inicio = new InicioViewModel();
            MainViewModel.GetInstance().Detail = new DetailViewModel();
            //MainViewModel.GetInstance().Casino = new CasinoViewModel();
            //MainViewModel.GetInstance().Hotel = new HotelViewModel();
            //MainViewModel.GetInstance().SalasEventos = new SalasEventosViewModel();
            //MainViewModel.GetInstance().Gastronomia = new GastronomiaViewModel();

            await Application.Current.MainPage.Navigation.PushModalAsync(new MasterPage());

        }

        public ICommand GoogleCommand
        {
            get
            {
                return new RelayCommand(GoogleLogin);
            }
        }

       
        private void GoogleLogin()
        {
            _googleManager = DependencyService.Get<IGoogleManager>();

            _googleManager.Login(OnLoginComplete);
        }

        private async void OnLoginComplete(GoogleUser googleUser, string message)
        {
            if (googleUser != null)
            {
                Application.Current.Properties["IsLoggedIn"] = true;
                Application.Current.Properties["Email"] = googleUser.Email;
                Application.Current.Properties["NombreCompleto"] = googleUser.Name;
                await Application.Current.SavePropertiesAsync();

                MainViewModel.GetInstance().Master = new MasterViewModel();

                await Application.Current.MainPage.Navigation.PushModalAsync(new MasterPage());

            }
            else
            {
                await Application.Current.MainPage.DisplayAlert(
                      "Error",
                      "You can not access Google services",
                      "Ok");
            }
        }

        public ICommand FacebookCommand
        {
            get
            {
                return new RelayCommand(FacebookLogin);
            }
        }

        private void FacebookLogin()
        {
            _facebookManager = DependencyService.Get<IFacebookManager>();

            _facebookManager.Login(OnLoginComplete);
        }

        private async void OnLoginComplete(FacebookUser facebookUser, string message)
        {
            if (facebookUser != null)
            {
                Application.Current.Properties["IsLoggedIn"] = true;
                Application.Current.Properties["Email"] = facebookUser.Email;
                Application.Current.Properties["NombreCompleto"] = facebookUser.FirstName + ' ' + facebookUser.LastName;
                await Application.Current.SavePropertiesAsync();

                MainViewModel.GetInstance().Master = new MasterViewModel();

                await Application.Current.MainPage.Navigation.PushModalAsync(new MasterPage());
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert(
                      "Error",
                      "You can not access Facebook services",
                      "Ok");
            }
        }

        #endregion

        #region Contructors
        public RegistroViewModel()
        {
            this.apiService = new ApiService();
            this.isEnabled = true;
           
        }
        #endregion

    }
}
