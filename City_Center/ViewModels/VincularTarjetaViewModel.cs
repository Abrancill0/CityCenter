using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Input;
using City_Center.Clases;
using City_Center.Page;
using City_Center.Services;
using GalaSoft.MvvmLight.Command;
using Xamarin.Forms;
using static City_Center.Models.ActualizaUsuarioResultado;
using static City_Center.Models.TarjetaValidaResultado;
using Acr.UserDialogs;

namespace City_Center.ViewModels
{
    public class VincularTarjetaViewModel:BaseViewModel
    {
        #region Services
        private ApiService apiService;
        #endregion


        #region Attributes
        private string tipoDocumento;
        private string numeroDocumento;
        private string numeroSocio;
        private ActualizaUsuarioReturn list;

        #endregion

        #region Properties

        public string TipoDocumento
        {
            get { return this.tipoDocumento; }
            set { SetValue(ref this.tipoDocumento, value); }
        }

        public string NumeroDocumento
        {
            get { return this.numeroDocumento; }
            set { SetValue(ref this.numeroDocumento, value); }
        }

        public string NumeroSocio
        {
            get { return this.numeroSocio; }
            set { SetValue(ref this.numeroSocio, value); }
        }

        #endregion

        #region Commands

        public ICommand VincularTarjetaCommand
        {
            get
            {
                return new RelayCommand(VincularTarjeta);
            }
        }

        private async void VincularTarjeta()
        {
            UserDialogs.Instance.ShowLoading("Iniciando Sesion...", MaskType.Black);


            if (string.IsNullOrEmpty(NumeroSocio))
            {
                UserDialogs.Instance.HideLoading();
                await Mensajes.Alerta("El numero de socio es obligatorio para poder vincular la tarjeta win");
                return;
            }
            else
            {
                var MensajeTarjeta = await ValidaTarjetaUsuario(NumeroSocio);

                if (MensajeTarjeta == "OK")
                {
                    //await Mensajes.Alerta("Tarjeta vinculada correctamente");
                }
                else
                {
                    UserDialogs.Instance.HideLoading();

                    await Mensajes.Alerta("La tarjeta colocada no existe, favor de verificar");

                    NumeroSocio = "";
                    return;
                }
            }

            var content = new FormUrlEncodedContent(new[]
           {
                new KeyValuePair<string, string>("usu_fecha_nacimiento", Application.Current.Properties["FechaNacimiento"].ToString()),
                new KeyValuePair<string, string>("usu_contrasena",Application.Current.Properties["Pass"].ToString()),
                new KeyValuePair<string, string>("usu_id", Application.Current.Properties["IdUsuario"].ToString()),
                new KeyValuePair<string, string>("usu_usuario", Application.Current.Properties["Email"].ToString()),
                new KeyValuePair<string, string>("usu_tipo_contrasena", "1"),
                new KeyValuePair<string, string>("usu_usuario_bloquedado", "0"),
                new KeyValuePair<string, string>("usu_nombre", Application.Current.Properties["NombreCompleto"].ToString()),
                new KeyValuePair<string, string>("usu_apellidos", ""),
                new KeyValuePair<string, string>("usu_email", Application.Current.Properties["Email"].ToString()),
                new KeyValuePair<string, string>("usu_telefono", ""),
                new KeyValuePair<string, string>("usu_celular", ""),
                new KeyValuePair<string, string>("usu_id_tarjeta_socio", this.NumeroSocio),
                new KeyValuePair<string, string>("usu_ciudad", Application.Current.Properties["Ciudad"].ToString()),
                new KeyValuePair<string, string>("usu_id_rol", "6"),
                new KeyValuePair<string, string>("usu_estatus", "1"),
                new KeyValuePair<string, string>("usu_tipo_documento", this.TipoDocumento),
                new KeyValuePair<string, string>("usu_no_documento", this.NumeroDocumento),
                new KeyValuePair<string, string>("passUpdate","1" ),
            });


            var response = await this.apiService.Get<ActualizaUsuarioReturn>("/usuarios", "/update", content);

            if (!response.IsSuccess)
            {
                await Mensajes.Error(response.Message);

                return;
            }

            list = (ActualizaUsuarioReturn)response.Result;

            Application.Current.Properties["TipoDocumento"] = TipoDocumento;
            Application.Current.Properties["NumeroDocumento"] = NumeroDocumento;
            Application.Current.Properties["NumeroSocio"] = NumeroSocio;

            await Application.Current.SavePropertiesAsync();


            MainViewModel.GetInstance().Master = new MasterViewModel();
            MainViewModel.GetInstance().Inicio = new InicioViewModel();
            MainViewModel.GetInstance().Detail = new DetailViewModel();
            //MainViewModel.GetInstance().Casino = new CasinoViewModel();
            //MainViewModel.GetInstance().Hotel = new HotelViewModel();
            //MainViewModel.GetInstance().SalasEventos = new SalasEventosViewModel();
            //MainViewModel.GetInstance().Gastronomia = new GastronomiaViewModel();

           
            MasterPage fpm = new MasterPage();
            fpm.Master = new DetailPage(); // You have to create a Master ContentPage()
            App.NavPage = new NavigationPage(new CustomTabPage()) { BarBackgroundColor = Color.FromHex("#23144B") };

            fpm.Detail = App.NavPage; // You have to create a Detail ContenPage()
            Application.Current.MainPage = fpm;

            UserDialogs.Instance.HideLoading();

            await Mensajes.Alerta("Bienvenido " + Application.Current.Properties["NombreCompleto"].ToString());

        }


        public ICommand NoVincularCommand
        {
            get
            {
                return new RelayCommand(NoVincular);
            }
        }

        private async void NoVincular()
        {
            UserDialogs.Instance.ShowLoading("Iniciando Sesion...", MaskType.Black);

            MainViewModel.GetInstance().Master = new MasterViewModel();
            MainViewModel.GetInstance().Inicio = new InicioViewModel();
            MainViewModel.GetInstance().Detail = new DetailViewModel();
            //MainViewModel.GetInstance().Casino = new CasinoViewModel();
            //MainViewModel.GetInstance().Hotel = new HotelViewModel();
            //MainViewModel.GetInstance().SalasEventos = new SalasEventosViewModel();
            //MainViewModel.GetInstance().Gastronomia = new GastronomiaViewModel();


            MasterPage fpm = new MasterPage();
            fpm.Master = new DetailPage(); // You have to create a Master ContentPage()
            App.NavPage = new NavigationPage(new CustomTabPage()) { BarBackgroundColor = Color.FromHex("#23144B") };

            fpm.Detail = App.NavPage; // You have to create a Detail ContenPage()
            Application.Current.MainPage = fpm;

            UserDialogs.Instance.HideLoading();

            await Mensajes.Alerta("Bienvenido " + Application.Current.Properties["NombreCompleto"].ToString());

        }

        #endregion

        #region Methods
       
        private async Task<string> ValidaTarjetaUsuario(string NoTarjeta)
        {
            try
            {

                var connection = await this.apiService.CheckConnection();

                if (!connection.IsSuccess)
                {
                    await Mensajes.Error(connection.Message);

                    return "No se tiene conexion a internet";
                }

                //string idusuario = Application.Current.Properties["IdUsuario"].ToString();

                var content = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("tarjeta",Application.Current.Properties["IdUsuario"].ToString()),
                    new KeyValuePair<string, string>("usu",NoTarjeta )
                });


                var response = await this.apiService.Get<TarjetaValidaReturn>("/es/register", "/valida_tarjeta_socio", content);

                if (!response.IsSuccess)
                {
                    //await Mensajes.Error("Error al cargar Torneos");

                    return "No Existe tarjeta Ingresada";
                }

                return "OK";
            }
            catch (Exception ex)
            {
                await Mensajes.Error(ex.ToString());
                return "Error";
            }

        }

        #endregion

        #region Contructors
        public VincularTarjetaViewModel()
        {
            this.apiService = new ApiService();

        }
        #endregion
    }
}
