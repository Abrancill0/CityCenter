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
using System.Threading.Tasks;
using Acr.UserDialogs;
using static City_Center.Models.ImagenResultado;

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

        private RegistroReturn listRegistro;
        private ObservableCollection<RegistroDetalle> registroDetalle;

        private ImagenReturn ListImagen;

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

        public ObservableCollection<RegistroDetalle> RegistroDetalle
        {
            get { return this.registroDetalle; }
            set { SetValue(ref this.registroDetalle, value); }
        }

        #endregion

        #region Commands

        private async Task<string> GuardaImagen(int UsuarioID)
        {

            var dirotro = "";

            if (string.IsNullOrEmpty(VariablesGlobales.RutaImagene))
            {
               await Mensajes.Error("No se subio ninguna foto");

               return "Error";
            }
            else
            {
                byte[] ImagenSubir = File.ReadAllBytes(VariablesGlobales.RutaImagene);

                dirotro = Convert.ToBase64String(ImagenSubir);    
            }
                
            var content = new FormUrlEncodedContent(new[]
           {
                new KeyValuePair<string, string>("usu_id", Convert.ToString(UsuarioID)),
                new KeyValuePair<string, string>("usu_imagenstr", dirotro)

            });


            var response = await this.apiService.Get<ImagenReturn>("/usuarios", "/carga_foto", content);


            if (!response.IsSuccess)
            {
                await Mensajes.Error("Error al cargar la foto");

                UserDialogs.Instance.HideLoading();

                return "Error";
            }

            ListImagen = (ImagenReturn)response.Result;


            return VariablesGlobales.RutaServidor + ListImagen.resultado;

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

            UserDialogs.Instance.ShowLoading("Procesando...", MaskType.Black);

            if (string.IsNullOrEmpty(this.Ciudad))
            {
               await Mensajes.Error("Ciudad es requerida.");

                return;
            }

         
            if (string.IsNullOrEmpty(this.Email))
            {
                await Mensajes.Error("Correo es requerida.");

                UserDialogs.Instance.HideLoading();

                return;
            }

            if (string.IsNullOrEmpty(this.Password))
            {
                await Mensajes.Error("Contresaña es requerida.");

                UserDialogs.Instance.HideLoading();

                return;
            }

            if (string.IsNullOrEmpty(this.Password2))
            {
                await Mensajes.Error("Contraseña es requerida.");

                return;
            }

            if (this.Password != this.Password2)
            {
                await Mensajes.Error("Las contraseñas no coicien.");

                UserDialogs.Instance.HideLoading();

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


            var response = await this.apiService.Get<RegistroReturn>("/usuarios", "/store", content);

           if (!response.IsSuccess)
            {
               await Mensajes.Error("Error al registra usuario");

                UserDialogs.Instance.HideLoading();

                return;
            }

            listRegistro = (RegistroReturn)response.Result;

            string RutaImagen = await GuardaImagen(listRegistro.resultado.usu_id);

            Application.Current.Properties["IsLoggedIn"] = true;
            Application.Current.Properties["IdUsuario"] = listRegistro.resultado.usu_id;
            Application.Current.Properties["Email"] = this.Email;
            Application.Current.Properties["NombreCompleto"] = this.Nombre;
            Application.Current.Properties["Ciudad"] = this.Ciudad;
            Application.Current.Properties["Pass"] = this.Password;
            Application.Current.Properties["FechaNacimiento"] = this.Fecha;
            Application.Current.Properties["FotoPerfil"] = RutaImagen;

            await Application.Current.SavePropertiesAsync();

           
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

            //await Application.Current.MainPage.Navigation.PushModalAsync(new MasterPage());

            MasterPage fpm = new MasterPage();
            fpm.Master = new DetailPage(); // You have to create a Master ContentPage()
            fpm.Detail = new NavigationPage(new TabPage()); // You have to create a Detail ContenPage()
            Application.Current.MainPage = fpm;

            await Mensajes.success("Bienvenido " + this.Nombre);

            UserDialogs.Instance.HideLoading();

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
                Application.Current.Properties["IsLoggedIn"] = true;
                Application.Current.Properties["IdUsuario"] = 0;
                Application.Current.Properties["Email"] = googleUser.Email;
                Application.Current.Properties["NombreCompleto"] = googleUser.Name;
                Application.Current.Properties["Ciudad"] = "";
                Application.Current.Properties["Pass"] = "";
                Application.Current.Properties["FechaNacimiento"] = "";
                Application.Current.Properties["FotoPerfil"] = googleUser.Picture;
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
                Application.Current.Properties["IsLoggedIn"] = true;
                Application.Current.Properties["IdUsuario"] = 0;
                Application.Current.Properties["Email"] = facebookUser.Email;
                Application.Current.Properties["NombreCompleto"] = facebookUser.FirstName + ' ' + facebookUser.LastName;
                Application.Current.Properties["Ciudad"] = "";
                Application.Current.Properties["Pass"] = "";
                Application.Current.Properties["FechaNacimiento"] = "";
                Application.Current.Properties["FotoPerfil"] = "";

                await Application.Current.SavePropertiesAsync();


                MainViewModel.GetInstance().Master = new MasterViewModel();
                MainViewModel.GetInstance().Inicio = new InicioViewModel();
                MainViewModel.GetInstance().Detail = new DetailViewModel();
                MainViewModel.GetInstance().Casino = new CasinoViewModel();
                MainViewModel.GetInstance().Hotel = new HotelViewModel();

                await Application.Current.MainPage.Navigation.PushModalAsync(new MasterPage());
            }
            else
            {

                await Mensajes.Error("Error al acceder a los servicios de Facebook");

                return;

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
