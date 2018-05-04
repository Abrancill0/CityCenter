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
using Xamarin.Forms;
using static City_Center.Models.LoginResultado;
using static City_Center.Models.RegistroUsuario;
using City_Center.Clases;
using Acr.UserDialogs;
using static City_Center.Models.ValidaUsuarioResultado;

namespace City_Center.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        #region Services
        private ApiService apiService;
        #endregion

        #region Attributes
        private string email;
        private string password;
        private bool isEnabled;
        private ObservableCollection<LoginReturn> loginReturn;
        private RegistroReturn listRegistro;
        private ValidaUsuarioReturn listValidaUsuario;

        #endregion

        private IGoogleManager _googleManager;
        private IFacebookManager _facebookManager;

        #region Properties
        public string Email
        {
            get { return this.email; }
            set { SetValue(ref this.email, value); }
        }

        public bool IsEnabled
        {
            get { return this.isEnabled; }
            set { SetValue(ref this.isEnabled, value); }
        }

        public ObservableCollection<LoginReturn> LoginReturn
        {
            get { return this.loginReturn; }
            set { SetValue(ref this.loginReturn, value); }
        }

        public string Password
        {
            get { return this.password; }
            set { SetValue(ref this.password, value); }
        }


        #endregion

        #region Commands
        public ICommand LoginCommand
        {
            get
            {
                return new RelayCommand(Login);
            }
        }

        private async void Login()
        {
            try
            {
                UserDialogs.Instance.ShowLoading("Iniciando sesion...", MaskType.Black);

                if (string.IsNullOrEmpty(this.Email))
                {
                    await Mensajes.Error("Correo/Usuario Requerido");

                    UserDialogs.Instance.HideLoading();

                    return;
                }

                if (string.IsNullOrEmpty(this.Password))
                {
                    await Mensajes.Error("Contraseña Requerido");

                    UserDialogs.Instance.HideLoading();

                    return;

                }

                var content = new FormUrlEncodedContent(new[]
               {
                new KeyValuePair<string, string>("usu_usuario", this.Email),
                new KeyValuePair<string, string>("usu_contrasena", this.Password),
                new KeyValuePair<string, string>("usu_token", "")

                });


                var response = await this.apiService.Get<LoginReturn>("/usuarios", "/login", content);

                if (!response.IsSuccess)
                {
                    await Mensajes.Error("Usuario o contraseña incorrecto.");

                    UserDialogs.Instance.HideLoading();

                    return;
                }

                LoginReturn list = (LoginReturn)response.Result;


                if  (list.estatus==1)
                {
                    
                    Application.Current.Properties["IsLoggedIn"] = true;
                    Application.Current.Properties["IdUsuario"] = list.resultado.usu_id;
                    Application.Current.Properties["Email"] = this.Email;
                    Application.Current.Properties["NombreCompleto"] = list.resultado.usu_nombre + ' ' + list.resultado.usu_apellidos;
                    Application.Current.Properties["Ciudad"] = list.resultado.usu_ciudad;
                    Application.Current.Properties["Pass"] = this.Password;
                    Application.Current.Properties["FechaNacimiento"] = list.resultado.usu_fecha_nacimiento;
                    Application.Current.Properties["FotoPerfil"] = VariablesGlobales.RutaServidor + list.resultado.usu_imagen;
                    Application.Current.Properties["TipoCuenta"] = "CityCenter";

                    await Application.Current.SavePropertiesAsync();

                    this.Email = string.Empty;
                    this.Password = string.Empty;

                    MainViewModel.GetInstance().Master = new MasterViewModel();
                    MainViewModel.GetInstance().Inicio = new InicioViewModel();
                    MainViewModel.GetInstance().Detail = new DetailViewModel();
                    //MainViewModel.GetInstance().Casino = new CasinoViewModel();
                    //MainViewModel.GetInstance().Hotel = new HotelViewModel();
                    //MainViewModel.GetInstance().SalasEventos = new SalasEventosViewModel();
                    //MainViewModel.GetInstance().Gastronomia = new GastronomiaViewModel();

               
                    MasterPage fpm = new MasterPage();
                    fpm.Master = new DetailPage(); // You have to create a Master ContentPage()
                    fpm.Detail = new NavigationPage(new TabPage()); // You have to create a Detail ContenPage()
                    Application.Current.MainPage = fpm;

                    await Mensajes.success("Bienvenido " + list.resultado.usu_nombre + ' ' + list.resultado.usu_apellidos);

                    UserDialogs.Instance.HideLoading();

                   
                }
                else
                {
                    await Mensajes.Error(list.mensaje);

                    UserDialogs.Instance.HideLoading();

                    return;
                }

             

            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert(
                          "Error",
                           ex.ToString(),
                          "Ok");

                UserDialogs.Instance.HideLoading();
            }

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
            MainViewModel.GetInstance().Registro = new RegistroViewModel();

            ((MasterPage)Application.Current.MainPage).IsPresented = false;

           // await Application.Current.MainPage.Navigation.PushAsync(new Registro());
            await ((MasterPage)Application.Current.MainPage).Detail.Navigation.PushAsync(new Registro());
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

            _googleManager.Logout();
        }

        private async void OnLoginComplete(GoogleUser googleUser, string message)
        {
            if (googleUser != null)
            {

                int ValUsu = await ValidaUsuario(googleUser.Email);

                string IDUsuario;

                if (ValUsu == 0)
                {
                    IDUsuario = await GuardaUsuarioGF(googleUser.Name, googleUser.Email); 
                }
                else
                {
                    IDUsuario = Convert.ToString(ValUsu);
                }

                Application.Current.Properties["IsLoggedIn"] = true;
                Application.Current.Properties["IdUsuario"] = IDUsuario;
                Application.Current.Properties["Email"] = googleUser.Email;
                Application.Current.Properties["NombreCompleto"] = googleUser.Name;
                Application.Current.Properties["Ciudad"] = "";
                Application.Current.Properties["Pass"] = "";
                Application.Current.Properties["FechaNacimiento"] = "";
                Application.Current.Properties["FotoPerfil"] = googleUser.Picture;
                Application.Current.Properties["TipoCuenta"] = "Google";
                await Application.Current.SavePropertiesAsync();


                MainViewModel.GetInstance().Master = new MasterViewModel();
                MainViewModel.GetInstance().Inicio = new InicioViewModel();
                MainViewModel.GetInstance().Detail = new DetailViewModel();
                MainViewModel.GetInstance().Casino = new CasinoViewModel();
                MainViewModel.GetInstance().Hotel = new HotelViewModel();
                MainViewModel.GetInstance().Gastronomia = new GastronomiaViewModel();

                await Application.Current.MainPage.Navigation.PushModalAsync(new MasterPage());

            }
            else
            {
                await Mensajes.Error(message);

                return;
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
                
                int ValUsu = await ValidaUsuario(facebookUser.Email);

                string IDUsuario;

                if (ValUsu == 0)
                {
                    IDUsuario = await GuardaUsuarioGF(facebookUser.FirstName + ' ' + facebookUser.LastName, facebookUser.Email);

                }
                else
                {
                    IDUsuario = Convert.ToString(ValUsu);
                }


                Application.Current.Properties["IsLoggedIn"] = true;
                Application.Current.Properties["IdUsuario"] = IDUsuario;
                Application.Current.Properties["Email"] = facebookUser.Email;
                Application.Current.Properties["NombreCompleto"] = facebookUser.FirstName + ' ' + facebookUser.LastName;
                Application.Current.Properties["Ciudad"] = "";
                Application.Current.Properties["Pass"] = "";
                Application.Current.Properties["FechaNacimiento"] = "";
                Application.Current.Properties["FotoPerfil"] =facebookUser.Picture;
                Application.Current.Properties["TipoCuenta"] = "Facebook";
                Application.Current.Properties["TipoDocumento"] = "";

                Application.Current.Properties["NumeroDocumento"] = "";
                Application.Current.Properties["NumeroSocio"] = "";

                await Application.Current.SavePropertiesAsync();

                MainViewModel.GetInstance().Master = new MasterViewModel();
                MainViewModel.GetInstance().Inicio = new InicioViewModel();
                MainViewModel.GetInstance().Detail = new DetailViewModel();
                MainViewModel.GetInstance().Casino = new CasinoViewModel();
                MainViewModel.GetInstance().Hotel = new HotelViewModel();
                MainViewModel.GetInstance().Gastronomia = new GastronomiaViewModel();

                await Application.Current.MainPage.Navigation.PushModalAsync(new MasterPage());
            }
            else
            {

                await Mensajes.Error("Error al acceder a los servicios de Facebook");

                return;

            }
        }


        public ICommand RestablecePassCommand
        {
            get
            {
                return new RelayCommand(Restablece);
            }
        }

        private async void Restablece()
        {
            MainViewModel.GetInstance().ReiniciaPass = new ReiniciaPassViewModel();

            ((MasterPage)Application.Current.MainPage).IsPresented = false;

            await ((MasterPage)Application.Current.MainPage).Detail.Navigation.PushAsync(new RestableceContraseña());  
        }

        private async Task<string> GuardaUsuarioGF(string UserNameGF, string EmailGF)
        {
            var content = new FormUrlEncodedContent(new[]
           {
                new KeyValuePair<string, string>("usu_usuario", EmailGF),
                new KeyValuePair<string, string>("usu_contrasena", ""),
                new KeyValuePair<string, string>("usu_tipo_contrasena", "1"),
                new KeyValuePair<string, string>("usu_usuario_bloquedado", "0"),
                new KeyValuePair<string, string>("usu_nombre", UserNameGF),
                new KeyValuePair<string, string>("usu_apellidos", ""),
                new KeyValuePair<string, string>("usu_email", EmailGF),
                new KeyValuePair<string, string>("usu_celular", ""),
                new KeyValuePair<string, string>("usu_telefono", ""),
                new KeyValuePair<string, string>("usu_ciudad", ""),
                new KeyValuePair<string, string>("usu_id_rol", "6"),
                new KeyValuePair<string, string>("usu_id_tarjeta_socio", "1"),
                new KeyValuePair<string, string>("usu_estatus", "1"),
                new KeyValuePair<string, string>("usu_id_tarjeta_socio", ""),
                new KeyValuePair<string, string>("usu_fecha_nacimiento", ""),
            });


            var response = await this.apiService.Get<RegistroReturn>("/usuarios", "/store", content);

            if (!response.IsSuccess)
            {
                await Mensajes.Error(response.Message);

                return "Error";
            }

            listRegistro = (RegistroReturn)response.Result;

            return Convert.ToString(listRegistro.resultado.usu_id);
        }

        private async Task<int> ValidaUsuario(string CorreoElectronico)
        {
            var content = new FormUrlEncodedContent(new[]
           {
                new KeyValuePair<string, string>("usu_usuario", CorreoElectronico)
            });

            ///usuario_valido
            var response = await this.apiService.Get<ValidaUsuarioReturn>("/usuarios", "/usuario_valido", content);

            if (!response.IsSuccess)
            {
                await Mensajes.Error(response.Message);

                return 0;
            }


            listValidaUsuario = (ValidaUsuarioReturn)response.Result;


            if (listValidaUsuario.resultado == "Usuario disponible.")
            {
                return 0;
            }
            else
            {
                return listValidaUsuario.usu_id;
            }


        }


        #endregion


        #region Methods

       
        #endregion


        #region Contructors
        public LoginViewModel()
        {
            this.apiService = new ApiService();
            this.isEnabled = true;

        }
        #endregion


    }
}

