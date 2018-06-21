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
using static City_Center.Models.ValidaUsuarioResultado;
using City_Center.Database;
using Org.BouncyCastle.Crypto.Tls;
using Plugin.DeviceInfo;

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
        private string fecha;
        private string password;
        private string password2;
        private bool isEnabled;
        private string nombre;

        private RegistroReturn listRegistro;
        private ObservableCollection<RegistroDetalle> registroDetalle;
		private ValidaUsuarioReturn listValidaUsuario;

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

        public string Fecha
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
               await Mensajes.Alerta("Ninguna foto subida");

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
                await Mensajes.Alerta("Error al cargar la foto, intenta de nuevo");

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

            if (string.IsNullOrEmpty(this.Nombre))
            {
                await Mensajes.Alerta("Nombre y Apellido son requeridos");

                UserDialogs.Instance.HideLoading();

                return;
            }


            if (string.IsNullOrEmpty(this.Email))
            {
                await Mensajes.Alerta("Correo electrónico es requerido");

                UserDialogs.Instance.HideLoading();

                return;
            }


            if (string.IsNullOrEmpty(this.Password))
            {
                await Mensajes.Alerta("Contraseña es requerida");

                UserDialogs.Instance.HideLoading();

                return;
            }

            if (string.IsNullOrEmpty(this.Password2))
            {
                await Mensajes.Alerta("Contraseña es requerida");

                UserDialogs.Instance.HideLoading();

                return;
            }


            if (string.IsNullOrEmpty(this.Ciudad))
            {
               await Mensajes.Alerta("Ciudad es requerida");

                UserDialogs.Instance.HideLoading();

                return;
            }


            if (this.Fecha == "00/00/0000")
            {
                await Mensajes.Alerta("Fecha de nacimiento es requerida");

                UserDialogs.Instance.HideLoading();

                return;
            }

            if (this.Password != this.Password2)
            {
                await Mensajes.Alerta("Las contraseñas no coiciden, verificar de nuevo");

                UserDialogs.Instance.HideLoading();

                return;
            }

            string Dia = this.Fecha.Substring(0, 2);
            string Mes = this.Fecha.Substring(3, 2);
            string Año = this.Fecha.Substring(6, 4);

            //DateTime FechaConvertida = Convert.ToDateTime(Año + "-" + Mes + "-" + Dia);

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
                new KeyValuePair<string, string>("usu_fecha_nacimiento", Convert.ToString(Año + "-" + Mes + "-" + Dia)),
            });


            var response = await this.apiService.Get<RegistroReturn>("/usuarios", "/store", content);

           if (!response.IsSuccess)
            {
               await Mensajes.Alerta("Error al registra usuario, intenta de nuevo");

                UserDialogs.Instance.HideLoading();

                return;
            }

            listRegistro = (RegistroReturn)response.Result;

            try
            {
                var Contenido = new FormUrlEncodedContent(new[]
                     {
                            new KeyValuePair<string, string>("neq_equipo", Application.Current.Properties["Token"].ToString()),
                            new KeyValuePair<string, string>("neq_id_usuario", Convert.ToString(listRegistro.resultado.usu_id)),
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
          

            string RutaImagen;

            if (string.IsNullOrEmpty(VariablesGlobales.RutaImagene))
            {
                RutaImagen = ""; 
            }
            else
            {
                RutaImagen   = await GuardaImagen(listRegistro.resultado.usu_id);   
            } 


            Application.Current.Properties["IsLoggedIn"] = true;
            Application.Current.Properties["IdUsuario"] = listRegistro.resultado.usu_id;
            Application.Current.Properties["Email"] = this.Email;
            Application.Current.Properties["NombreCompleto"] = this.Nombre;
            Application.Current.Properties["Ciudad"] = this.Ciudad;
            Application.Current.Properties["Pass"] = this.Password;
            Application.Current.Properties["FechaNacimiento"] = Convert.ToString(Año + "-" + Mes + "-" + Dia);
            Application.Current.Properties["FotoPerfil"] = RutaImagen;
			Application.Current.Properties["TipoCuenta"] = "CityCenter";

            Application.Current.Properties["RutaChatCasino"] = "";
            Application.Current.Properties["VariableChatHotel"] = "";
            Application.Current.Properties["VariableChatCasino"] = "";
            Application.Current.Properties["RutaChatHotel"] = "";
            Application.Current.Properties["Casino"] = 1;
            Application.Current.Properties["Hotel"] = 1;

            Application.Current.Properties["TipoDocumento"] = "";
            Application.Current.Properties["NumeroDocumento"] = "";
            Application.Current.Properties["NumeroSocio"] = "";

            await Application.Current.SavePropertiesAsync();

            //var db = new DBFire();
            //await db.saveRoom(new Room() { Email = this.Email, Name = this.Nombre });

            this.Email = string.Empty;
            this.Nombre = string.Empty;
            this.Ciudad = string.Empty;
            this.Password = string.Empty;
            this.Ciudad = string.Empty;

            MainViewModel.GetInstance().VincularTarjeta = new VincularTarjetaViewModel();

            await ((MasterPage)Application.Current.MainPage).Detail.Navigation.PushModalAsync(new VincularTarjetaWin());

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
            try
			{
				_googleManager = DependencyService.Get<IGoogleManager>();

                UserDialogs.Instance.ShowLoading("Iniciando sesion...", MaskType.Black);

                _googleManager.Login(OnLoginComplete);

                //UserDialogs.Instance.HideLoading();
			}
			catch (Exception)
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

                    this.Email = string.Empty;
                    this.Nombre = string.Empty;
                    this.Ciudad = string.Empty;
                    this.Password = string.Empty;
                    this.Ciudad = string.Empty;

                    MainViewModel.GetInstance().VincularTarjeta = new VincularTarjetaViewModel();

                    await ((MasterPage)Application.Current.MainPage).Detail.Navigation.PushAsync(new VincularTarjetaWin());

                    UserDialogs.Instance.HideLoading();

                }
                else
                {
                    await Mensajes.Alerta("Error al acceder a los servicios de Google");
                    UserDialogs.Instance.HideLoading();

                    return;
                }
			}
			catch (Exception ex)
			{
				await Mensajes.Alerta("Error al acceder a los servicios de Google");
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

                _facebookManager.Logout();
			}
			catch (Exception)
			{

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

                    //MainViewModel.GetInstance().Master = new MasterViewModel();
                    MainViewModel.GetInstance().Inicio = new InicioViewModel();
                    MainViewModel.GetInstance().Detail = new DetailViewModel();
                    MainViewModel.GetInstance().Casino = new CasinoViewModel();

                   // await Application.Current.MainPage.Navigation.PushModalAsync(new MasterPage());
                    this.Email = string.Empty;
                    this.Nombre = string.Empty;
                    this.Ciudad = string.Empty;
                    this.Password = string.Empty;
                    this.Ciudad = string.Empty;

                    MainViewModel.GetInstance().VincularTarjeta = new VincularTarjetaViewModel();

                    await ((MasterPage)Application.Current.MainPage).Detail.Navigation.PushAsync(new VincularTarjetaWin());

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
                await Mensajes.Alerta("Parece que no tenés conexión a internet, intentalo mas tarde");

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
                await Mensajes.Alerta("Parece que no tenés conexión a internet, intentalo mas tarde");

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

        #region Contructors
        public RegistroViewModel()
        {
            this.apiService = new ApiService();
            this.isEnabled = true;
			this.Fecha = "00/00/0000";
        }
        #endregion

    }
}
