using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Windows.Input;
using City_Center.Clases;
using City_Center.Services;
using GalaSoft.MvvmLight.Command;
using Xamarin.Forms;
using static City_Center.Models.ActualizaUsuarioResultado;
using static City_Center.Models.RegistroUsuario;
using static City_Center.Models.TarjetaUsuarioResultado;
using System.Threading.Tasks;

namespace City_Center.ViewModels
{
    public class PerfilViewModel:BaseViewModel
    {
        #region Services
        private ApiService apiService;
        #endregion
       

        #region Attributes
        private string email;
        private string contraseña;
        private string contraseña2;
        private string ciudad;
        private string fecha;
        private string imagen;
        private string nombre;
        private string tipoDocumento;
        private string numeroDocumento;
        private string numeroSocio;

        private string NoSocio;

        private TarjetaUsuarioReturn listaTarjetausuario;

        private ActualizaUsuarioReturn list;

        #endregion

        #region Properties
        public string Email
        {
            get { return this.email; }
            set { SetValue(ref this.email, value); }
        }

        public string Contraseña
        {
            get { return this.contraseña; }
            set { SetValue(ref this.contraseña, value); }
        }

        public string Contraseña2
        {
            get { return this.contraseña2; }
            set { SetValue(ref this.contraseña2, value); }
        }

        public string Ciudad
        {
            get { return this.ciudad; }
            set { SetValue(ref this.ciudad, value); }
        }

        public string Fecha
        {
            get { return this.fecha; }
            set { SetValue(ref this.fecha, value); }
        }


        public string Imagen
        {
            get { return this.imagen; }
            set { SetValue(ref this.imagen, value); }
        }

        public string Nombre
        {
            get { return this.nombre; }
            set { SetValue(ref this.nombre, value); }
        }

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

        public ICommand ActualizaPerfilCommand
        {
            get
            {
                return new RelayCommand(ActualizaPerfil);
            }
        }

        private async void ActualizaPerfil()
        {
            if (string.IsNullOrEmpty(NumeroSocio))
            {
                await Mensajes.Info("Usuario actual no cuenta con tarjeta Win");
            }
            else
            {
                var MensajeTarjeta = await ValidaTarjetaUsuario(NumeroSocio);


                if (MensajeTarjeta == "OK")
                {
                    await Mensajes.success("Tarjeta vinculada correctamente");  
                }
                else
                {
                  await Mensajes.Info(MensajeTarjeta);

                    NumeroSocio = "";
                }
            }
          
            var content = new FormUrlEncodedContent(new[]
           {
                new KeyValuePair<string, string>("usu_fecha_nacimiento", Fecha),
                new KeyValuePair<string, string>("usu_contrasena",contraseña),
                new KeyValuePair<string, string>("usu_id", Application.Current.Properties["IdUsuario"].ToString()),
                new KeyValuePair<string, string>("usu_usuario", Email),
                new KeyValuePair<string, string>("usu_tipo_contrasena", "1"),
                new KeyValuePair<string, string>("usu_usuario_bloquedado", "0"),
                new KeyValuePair<string, string>("usu_nombre", Nombre),
                new KeyValuePair<string, string>("usu_apellidos", ""),
                new KeyValuePair<string, string>("usu_email", Email),
                new KeyValuePair<string, string>("usu_telefono", ""),
                new KeyValuePair<string, string>("usu_celular", ""),
                new KeyValuePair<string, string>("usu_id_tarjeta_socio", NumeroSocio),
                new KeyValuePair<string, string>("usu_ciudad", ""),
                new KeyValuePair<string, string>("usu_id_rol", "6"),
                new KeyValuePair<string, string>("usu_estatus", "1"),
                new KeyValuePair<string, string>("usu_tipo_documento", TipoDocumento),
                new KeyValuePair<string, string>("usu_no_documento", NumeroDocumento),

            });


            var response = await this.apiService.Get<ActualizaUsuarioReturn>("/usuarios", "/update", content);

            if (!response.IsSuccess)
            {
                await Mensajes.Error(response.Message);

                return;
            }

            list = (ActualizaUsuarioReturn)response.Result;


            //Application.Current.Properties["NombreCompleto"] = Nombre;
            //Ciudad = Application.Current.Properties["Ciudad"].ToString();
            //Contraseña = Application.Current.Properties["Pass"].ToString();
            //Contraseña2 = Application.Current.Properties["Pass"].ToString();
            //Fecha = Application.Current.Properties["FechaNacimiento"].ToString();


            await Mensajes.success("Usuario Actualizado correctamente");
        }

        #endregion

        #region Methods
        private void LoadCampos()
        {
            try
            {
            Email = Application.Current.Properties["Email"].ToString();
            Nombre = Application.Current.Properties["NombreCompleto"].ToString();
            Ciudad = Application.Current.Properties["Ciudad"].ToString();
            Contraseña = Application.Current.Properties["Pass"].ToString();
            Contraseña2 = Application.Current.Properties["Pass"].ToString();
            Fecha = Application.Current.Properties["FechaNacimiento"].ToString();
            Imagen = Application.Current.Properties["FotoPerfil"].ToString();

            TipoDocumento  = Application.Current.Properties["TipoDocumento"].ToString();
            NumeroDocumento  = Application.Current.Properties["NumeroDocumento"].ToString();
            NumeroSocio   = Application.Current.Properties["NumeroSocio"].ToString();
            }
            catch (Exception ex)
            {

            }
        }

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
                    new KeyValuePair<string, string>("usu_id_tarjeta ",NoTarjeta )
                });


                var response = await this.apiService.Get<TarjetaUsuarioReturn>("/tarjetas", "/tarjetaUsuario", content);

                if (!response.IsSuccess)
                {
                    //await Mensajes.Error("Error al cargar Torneos");

                    return "No Existe tarjeta Ingresada";
                }

                //this.listaTarjetausuario = (TarjetaUsuarioReturn)response.Result;


                //NoSocio = listaTarjetausuario.resultado.tar_id;

                //if (NoTarjeta != NoSocio)
                //{
                //await  Mensajes.Info("La tarjeta ingresada es diferente a la que tiene asiganda el usuario");
                //}

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
        public PerfilViewModel()
        {
            this.apiService = new ApiService();
            LoadCampos();
        }
        #endregion
    }

}
