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
using City_Center.Database;
using Rg.Plugins.Popup.Extensions;
using Plugin.DeviceInfo;

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
                    await Mensajes.Alerta("Correo electrónico/Usuario requerido");

                    UserDialogs.Instance.HideLoading();

                    return;
                }

                if (!ValidaEmailMethod.ValidateEMail(this.Email))
                {
                    await Mensajes.Alerta("Correo electronico mal estructurado");

                    UserDialogs.Instance.HideLoading();

                    return;
                }

                if (string.IsNullOrEmpty(this.Password))
                {
                    await Mensajes.Alerta("Contraseña requerida");

                    UserDialogs.Instance.HideLoading();

                    return;

                }

                var content = new FormUrlEncodedContent(new[]
               {
                new KeyValuePair<string, string>("usu_usuario", this.Email),
                new KeyValuePair<string, string>("usu_contrasena", this.Password)

                });


                var response = await this.apiService.Get<LoginReturn>("/usuarios", "/loginApp", content);

                if (!response.IsSuccess)
                {
                    await Mensajes.Alerta("Usuario o Contraseña incorrectos");

                    UserDialogs.Instance.HideLoading();

                    return;
                }

                LoginReturn list = (LoginReturn)response.Result;


                if  (list.estatus==1)
                {
                    
					if (string.IsNullOrEmpty(list.resultado.usu_contrasena_temp))
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

                        Application.Current.Properties["TipoDocumento"] = list.resultado.usu_tipo_documento;
                        Application.Current.Properties["NumeroDocumento"] = list.resultado.usu_no_documento;
                        Application.Current.Properties["NumeroSocio"] = list.resultado.usu_id_tarjeta_socio;

                        Application.Current.Properties["RutaChatCasino"] = "";
                        Application.Current.Properties["VariableChatHotel"] = "";
                        Application.Current.Properties["VariableChatCasino"] = "";
                        Application.Current.Properties["RutaChatHotel"] = "";
                        Application.Current.Properties["Casino"] = 1;
                        Application.Current.Properties["Hotel"] = 1;

                        await Application.Current.SavePropertiesAsync();

                        try
                        {
                            var Contenido = new FormUrlEncodedContent(new[]
                       {

                            new KeyValuePair<string, string>("neq_equipo", Application.Current.Properties["Token"].ToString()),
                            new KeyValuePair<string, string>("neq_id_usuario", Convert.ToString(list.resultado.usu_id)),
                            new KeyValuePair<string, string>("neq_dispositivo", CrossDeviceInfo.Current.Platform.ToString()),
                            new KeyValuePair<string, string>("neq_app_id", CrossDeviceInfo.Current.Id)
                        });


                            var response2 = await this.apiService.Get<GuardadoGenerico>("/notificaciones", "/guardar_equipo", Contenido);

                            if (!response2.IsSuccess)
                            {

                            }
                        }
                        catch (Exception ex)
                        {

                        }
                       


                            //try
                            //{
                            //    Restcliente Cliente = new Restcliente();

                            //    var contentenido = new FormUrlEncodedContent(new[]
                            //    {
                            //        new KeyValuePair<string, string>("neq_equipo", Application.Current.Properties["Token"].ToString()),
                            //        new KeyValuePair<string, string>("neq_id_usuario", Convert.ToString(list.resultado.usu_id)),
                            //        new KeyValuePair<string, string>("neq_dispositivo", CrossDeviceInfo.Current.Platform.ToString()),
                            //        new KeyValuePair<string, string>("neq_app_id", CrossDeviceInfo.Current.Id)
                            //    });


                            //    var LoginReturn = await Cliente.Get<GuardadoGenerico>("/notificaciones/guardar_equipo", contentenido);

                            //    if (LoginReturn != null)
                            //    {
                            //        //await Mensajes.success("OK");
                            //    }
                            //    // Mensajes.Alerta(token);
                            //    // Mensajes.success(token);
                            //}
                            //catch (System.Exception)
                            //{


                            //}

                        this.Email = string.Empty;
                        this.Password = string.Empty;

                       // MainViewModel.GetInstance().Master = new MasterViewModel();
                        MainViewModel.GetInstance().Inicio = new InicioViewModel();
                        MainViewModel.GetInstance().Detail = new DetailViewModel();
                        MainViewModel.GetInstance().Casino = new CasinoViewModel();
                        //MainViewModel.GetInstance().Hotel = new HotelViewModel();
                        //MainViewModel.GetInstance().SalasEventos = new SalasEventosViewModel();
                        //MainViewModel.GetInstance().Gastronomia = new GastronomiaViewModel();

                        //await Application.Current.MainPage.Navigation.PushModalAsync(new MasterPage());

                        MasterPage fpm = new MasterPage();
                       // fpm.Master = new DetailPage(); // You have to create a Master ContentPage()

					   //App.NavPage = new NavigationPage(new CustomTabPage()) { BarBackgroundColor = Color.FromHex("#23144B") };

                       // fpm.Detail = App.NavPage; // You have to create a Detail ContenPage() 

                        Application.Current.MainPage = fpm;

                        await Mensajes.Alerta("Bienvenido " + list.resultado.usu_nombre + ' ' + list.resultado.usu_apellidos);


                        // --- aqui se genera el Room del chat con su correo
                        //var db = new DBFire();
                        //await db.saveRoom(new Room() { Name = this.Email });
                        //await Navigation.PopAsync();
                        // ---

                        UserDialogs.Instance.HideLoading();
	
					}

					else
					{

                        if (list.resultado.usu_contrasena_temp == this.Password)
                        {
                            MainViewModel.GetInstance().CambiaContrasena = new CambiaPassViewModel();

                            VariablesGlobales.IDUsuario = Convert.ToString(list.resultado.usu_id);

                            ((MasterPage)Application.Current.MainPage).IsPresented = false;

                            await ((MasterPage)Application.Current.MainPage).Detail.Navigation.PushAsync(new CambiaContraseña());

                            UserDialogs.Instance.HideLoading();   
                        }
                        else
                        {
                            await Mensajes.Alerta("Usuario o Contraseña incorrectos");

                            UserDialogs.Instance.HideLoading();

                            return; 
                        }

					}
                     
                }
                else
                {
                  //  await Mensajes.Error(list.mensaje);

                    UserDialogs.Instance.HideLoading();

                    return;
                }

             

            }
            catch (Exception ex)
            {
               // await Application.Current.MainPage.DisplayAlert(
               //           "Error",
               //            ex.ToString(),
               //           "Ok");

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
            try
            {
                _googleManager = DependencyService.Get<IGoogleManager>();

                UserDialogs.Instance.ShowLoading("Iniciando sesion...", MaskType.Black);

                _googleManager.Login(OnLoginComplete);
#if __IOS__
                UserDialogs.Instance.HideLoading();
#endif
            }
			catch (Exception ex)
			{
				
			}
		

        }

        private async void OnLoginComplete(GoogleUser googleUser, string message)
        {

            try
			{
				if (googleUser != null)
                {
                    ValidaUsuarioReturn ValUsu = await ValidaUsuario(googleUser.Email);

                    string IDUsuario;
                    string TipoDocumento = "";
                    string NumeroDocumento = "";
                    string NumeroSocio = "";
                    string Ciudad = "";
                    string FechaNacimiento = "";

                    if (ValUsu == null)
                    {
                        IDUsuario = await GuardaUsuarioGF(googleUser.Name, googleUser.Email);

                        await Mensajes.Alerta("Usuario creado con éxito");
                    }
                    else
                    {
                        IDUsuario = Convert.ToString(ValUsu.usu_id);
                        TipoDocumento = ValUsu.usu_tipo_documento;
                        NumeroDocumento = ValUsu.usu_no_documento;
                        NumeroSocio = ValUsu.usu_id_tarjeta_socio;
                        Ciudad = ValUsu.usu_ciudad;
                        FechaNacimiento = ValUsu.usu_fecha_nacimiento;
                    }

                    Application.Current.Properties["IsLoggedIn"] = true;
                    Application.Current.Properties["IdUsuario"] = IDUsuario;
                    Application.Current.Properties["Email"] = googleUser.Email;
                    Application.Current.Properties["NombreCompleto"] = googleUser.Name;
                    Application.Current.Properties["Ciudad"] = Ciudad;
                    Application.Current.Properties["Pass"] = "";
                    Application.Current.Properties["FechaNacimiento"] = FechaNacimiento;
                    Application.Current.Properties["FotoPerfil"] = googleUser.Picture;
                    Application.Current.Properties["TipoCuenta"] = "Google";
                    Application.Current.Properties["TipoDocumento"] = TipoDocumento;
                    Application.Current.Properties["NumeroDocumento"] = NumeroDocumento;
                    Application.Current.Properties["NumeroSocio"] = NumeroSocio;

                    Application.Current.Properties["RutaChatCasino"] = "";
                    Application.Current.Properties["VariableChatHotel"] = "";
                    Application.Current.Properties["VariableChatCasino"] = "";
                    Application.Current.Properties["RutaChatHotel"] = "";
                    Application.Current.Properties["Casino"] = 1;
                    Application.Current.Properties["Hotel"] = 1;


                    await Application.Current.SavePropertiesAsync();

                    try
                    {
                        var Contenido = new FormUrlEncodedContent(new[]
                       {

                            new KeyValuePair<string, string>("neq_equipo", Application.Current.Properties["Token"].ToString()),
                            new KeyValuePair<string, string>("neq_id_usuario", Convert.ToString(IDUsuario)),
                            new KeyValuePair<string, string>("neq_dispositivo", CrossDeviceInfo.Current.Platform.ToString()),
                            new KeyValuePair<string, string>("neq_app_id", CrossDeviceInfo.Current.Id)
                        });


                        var response2 = await this.apiService.Get<GuardadoGenerico>("/notificaciones", "/guardar_equipo", Contenido);

                        if (!response2.IsSuccess)
                        {

                        }
                    }
                    catch (Exception ex)
                    {

                    }
                   



                   // MainViewModel.GetInstance().Master = new MasterViewModel();
                    MainViewModel.GetInstance().Inicio = new InicioViewModel();
                    MainViewModel.GetInstance().Detail = new DetailViewModel();
                    MainViewModel.GetInstance().Casino = new CasinoViewModel();

                    //await Application.Current.MainPage.Navigation.PushModalAsync(new MasterPage());
                    MasterPage fpm = new MasterPage();
                    // fpm.Master = new DetailPage(); // You have to create a Master ContentPage()

                    //App.NavPage = new NavigationPage(new CustomTabPage()) { BarBackgroundColor = Color.FromHex("#23144B") };

                    // fpm.Detail = App.NavPage; // You have to create a Detail ContenPage() 

                    Application.Current.MainPage = fpm;

                    await Mensajes.Alerta("Bienvenido " + googleUser.Name);

                    UserDialogs.Instance.HideLoading();


                }
                else
                {
                    await Mensajes.Alerta("Error al acceder a los servicios de google, intente de nuevo");
                    UserDialogs.Instance.HideLoading();

                    return;
                }
			}
			catch (Exception ex)
			{
				await Mensajes.Alerta("Error al acceder a los servicios de google, intente de nuevo");
                UserDialogs.Instance.HideLoading();
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
			try
			{
				_facebookManager = DependencyService.Get<IFacebookManager>();

                UserDialogs.Instance.ShowLoading("Iniciando sesion...", MaskType.Black);

                _facebookManager.Login(OnLoginComplete);

                //_facebookManager.Logout();

                UserDialogs.Instance.HideLoading();
			}
			catch (Exception ex)
			{
                //Mensajes.Alerta(ex.ToString()); 
			}

        }

        private async void OnLoginComplete(FacebookUser facebookUser, string message)
        {
            try
			{
				if (facebookUser != null)
                {

                    ValidaUsuarioReturn ValUsu = await ValidaUsuario(facebookUser.Email);

                    string IDUsuario;
                    string TipoDocumento = "";
                    string NumeroDocumento = "";
                    string NumeroSocio = "";
                    string Ciudad = "";
                    string FechaNacimiento = "";

                    if (ValUsu == null)
                    {
                        IDUsuario = await GuardaUsuarioGF(facebookUser.FirstName + ' ' + facebookUser.LastName, facebookUser.Email);
                    }
                    else
                    {
                        IDUsuario = Convert.ToString(ValUsu.usu_id);
                        TipoDocumento = ValUsu.usu_tipo_documento;
                        NumeroDocumento = ValUsu.usu_no_documento;
                        NumeroSocio = ValUsu.usu_id_tarjeta_socio;
                        Ciudad = ValUsu.usu_ciudad;
                        FechaNacimiento = ValUsu.usu_fecha_nacimiento;
                    }

                    Application.Current.Properties["IsLoggedIn"] = true;
                    Application.Current.Properties["IdUsuario"] = IDUsuario;
                    Application.Current.Properties["Email"] = facebookUser.Email;
                    Application.Current.Properties["NombreCompleto"] = facebookUser.FirstName + ' ' + facebookUser.LastName;
                    Application.Current.Properties["Ciudad"] = "";
                    Application.Current.Properties["Pass"] = "";
                    Application.Current.Properties["FechaNacimiento"] = "";
                    Application.Current.Properties["FotoPerfil"] = facebookUser.Picture;
                    Application.Current.Properties["TipoCuenta"] = "Facebook";
                    Application.Current.Properties["TipoDocumento"] = TipoDocumento;
                    Application.Current.Properties["NumeroDocumento"] = NumeroDocumento;
                    Application.Current.Properties["NumeroSocio"] = NumeroSocio;

                    Application.Current.Properties["RutaChatCasino"] = "";
                    Application.Current.Properties["VariableChatHotel"] = "";
                    Application.Current.Properties["VariableChatCasino"] = "";
                    Application.Current.Properties["RutaChatHotel"] = "";
                    Application.Current.Properties["Casino"] = 1;
                    Application.Current.Properties["Hotel"] = 1;


                    await Application.Current.SavePropertiesAsync();


                    var Contenido = new FormUrlEncodedContent(new[]
                        {

                            new KeyValuePair<string, string>("neq_equipo", Application.Current.Properties["Token"].ToString()),
                            new KeyValuePair<string, string>("neq_id_usuario", Convert.ToString(IDUsuario)),
                            new KeyValuePair<string, string>("neq_dispositivo", CrossDeviceInfo.Current.Platform.ToString()),
                            new KeyValuePair<string, string>("neq_app_id", CrossDeviceInfo.Current.Id)
                        });


                    var response2 = await this.apiService.Get<GuardadoGenerico>("/notificaciones", "/guardar_equipo", Contenido);

                    if (!response2.IsSuccess)
                    {

                    }

                   // MainViewModel.GetInstance().Master = new MasterViewModel();
                    MainViewModel.GetInstance().Inicio = new InicioViewModel();
                    MainViewModel.GetInstance().Detail = new DetailViewModel();
                    MainViewModel.GetInstance().Casino = new CasinoViewModel();

                    //await Application.Current.MainPage.Navigation.PushModalAsync(new MasterPage());

                    MasterPage fpm = new MasterPage();
                    // fpm.Master = new DetailPage(); // You have to create a Master ContentPage()

                    //App.NavPage = new NavigationPage(new CustomTabPage()) { BarBackgroundColor = Color.FromHex("#23144B") };

                    // fpm.Detail = App.NavPage; // You have to create a Detail ContenPage() 

                    Application.Current.MainPage = fpm;

                    await Mensajes.Alerta("Bienvenido " + facebookUser.FirstName + ' ' + facebookUser.LastName);

                    UserDialogs.Instance.HideLoading();
                }
                else
                {

                    await Mensajes.Alerta("Error al acceder a los servicios de Facebook, intente de nuevo");
                    UserDialogs.Instance.HideLoading();
                    return;

                }
			}
			catch (Exception)
			{
				await Mensajes.Alerta("Error al acceder a los servicios de Facebook, intente de nuevo");
                UserDialogs.Instance.HideLoading();
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

            await ((MasterPage)Application.Current.MainPage).Detail.Navigation.PushPopupAsync(new RestableceContraseña());  
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
                await Mensajes.Alerta(response.Message);

                return "Error";
            }

            listRegistro = (RegistroReturn)response.Result;

            return Convert.ToString(listRegistro.resultado.usu_id);
        }

        private async Task<ValidaUsuarioReturn> ValidaUsuario(string CorreoElectronico)
        {
            var content = new FormUrlEncodedContent(new[]
           {
                new KeyValuePair<string, string>("usu_usuario", CorreoElectronico)
            });

            var response = await this.apiService.Get<ValidaUsuarioReturn>("/usuarios", "/usuario_valido", content);

            if (!response.IsSuccess)
            {
                await Mensajes.Alerta(response.Message);

                return null;
            }

            listValidaUsuario = (ValidaUsuarioReturn)response.Result;


            if (listValidaUsuario.resultado == "Usuario disponible.")
            {
                return null;

            }
            else
            {
                return listValidaUsuario;

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

