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
using static City_Center.Models.TarjetaValidaResultado;

namespace City_Center.ViewModels
{
    public class PerfilViewModel : BaseViewModel
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
        private bool hC;

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

        public bool HC
        {
            get { return this.hC; }
            set { SetValue(ref this.hC, value); }
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

            if (Contraseña != Contraseña2)
            {
                await Mensajes.Alerta("Las contraseñas no coinciden, verificar los campos");  
            }


            if (string.IsNullOrEmpty(NumeroSocio))
            {
                await Mensajes.Alerta("Usuario actual no cuenta con tarjeta Win");
            }
            else
            {
                var MensajeTarjeta = await ValidaTarjetaUsuario(NumeroSocio);


                if (MensajeTarjeta == "OK")
                {
                    await Mensajes.Alerta("Tarjeta vinculada correctamente");
                }
                else
                {
                    await Mensajes.Alerta(MensajeTarjeta);

                    NumeroSocio = "";
                }
            }

            string Dia = this.Fecha.Substring(0, 2);
            string Mes = this.Fecha.Substring(3, 2);
            string Año = this.Fecha.Substring(6, 4);

            var content = new FormUrlEncodedContent(new[]
           {
                new KeyValuePair<string, string>("usu_fecha_nacimiento", Convert.ToString(Año + "-" + Mes + "-" + Dia)),
                new KeyValuePair<string, string>("usu_contrasena",this.contraseña),
                new KeyValuePair<string, string>("usu_id", Application.Current.Properties["IdUsuario"].ToString()),
                new KeyValuePair<string, string>("usu_usuario", this.Email),
                new KeyValuePair<string, string>("usu_tipo_contrasena", "1"),
                new KeyValuePair<string, string>("usu_usuario_bloquedado", "0"),
                new KeyValuePair<string, string>("usu_nombre", Nombre),
                new KeyValuePair<string, string>("usu_apellidos", ""),
                new KeyValuePair<string, string>("usu_email", this.Email),
                new KeyValuePair<string, string>("usu_telefono", ""),
                new KeyValuePair<string, string>("usu_celular", ""),
                new KeyValuePair<string, string>("usu_id_tarjeta_socio", this.NumeroSocio),
                new KeyValuePair<string, string>("usu_ciudad", ""),
                new KeyValuePair<string, string>("usu_id_rol", "6"),
                new KeyValuePair<string, string>("usu_estatus", "1"),
                new KeyValuePair<string, string>("usu_tipo_documento", this.TipoDocumento),
                new KeyValuePair<string, string>("usu_no_documento", this.NumeroDocumento),
                new KeyValuePair<string, string>("passUpdate","1" ),
            });


            var response = await this.apiService.Get<ActualizaUsuarioReturn>("/usuarios", "/update", content);

            if (!response.IsSuccess)
            {
                await Mensajes.Alerta("Ocurrió un error al actualizar el usuario, intente de nuevo");

                return;
            }

            list = (ActualizaUsuarioReturn)response.Result;

           // string RutaImagen = await GuardaImagen(list.resultado.);

            Application.Current.Properties["Email"] = Email;
            Application.Current.Properties["NombreCompleto"]= Nombre ;
            Application.Current.Properties["Ciudad"] = Ciudad;
            Application.Current.Properties["Pass"] = Contraseña ;
            Application.Current.Properties["Pass"] = Contraseña2;
            Application.Current.Properties["FechaNacimiento"] = Fecha;
            Application.Current.Properties["FotoPerfil"] = Imagen;

            Application.Current.Properties["TipoDocumento"] =TipoDocumento;
            Application.Current.Properties["NumeroDocumento"] =NumeroDocumento;
            Application.Current.Properties["NumeroSocio"] = NumeroSocio;

			await Application.Current.SavePropertiesAsync();

            await Mensajes.Alerta("Usuario actualizadó con éxito");
        }


        //private async Task<string> GuardaImagen(int UsuarioID)
        //{

        //    var dirotro = "";

        //    if (string.IsNullOrEmpty(VariablesGlobales.RutaImagene))
        //    {
        //        await Mensajes.Error("No se subio ninguna foto");

        //        return "Error";
        //    }
        //    else
        //    {
        //        byte[] ImagenSubir = File.ReadAllBytes(VariablesGlobales.RutaImagene);

        //        dirotro = Convert.ToBase64String(ImagenSubir);
        //    }

        //    var content = new FormUrlEncodedContent(new[]
        //   {
        //        new KeyValuePair<string, string>("usu_id", Convert.ToString(UsuarioID)),
        //        new KeyValuePair<string, string>("usu_imagenstr", dirotro)

        //    });


        //    var response = await this.apiService.Get<ImagenReturn>("/usuarios", "/carga_foto", content);


        //    if (!response.IsSuccess)
        //    {
        //        await Mensajes.Error("Error al cargar la foto");

        //        UserDialogs.Instance.HideLoading();

        //        return "Error";
        //    }

        //    ListImagen = (ImagenReturn)response.Result;


        //    return VariablesGlobales.RutaServidor + ListImagen.resultado;

        //}

        #endregion

        #region Methods
        private void LoadCampos()
        {
            try
            {
                Email = Application.Current.Properties["Email"].ToString();
                Nombre = Application.Current.Properties["NombreCompleto"].ToString().ToUpper();
                Ciudad = Application.Current.Properties["Ciudad"].ToString();
                Contraseña = Application.Current.Properties["Pass"].ToString();
                Contraseña2 = Application.Current.Properties["Pass"].ToString();

                string fotoPerfil = Application.Current.Properties["FotoPerfil"].ToString();

                if (fotoPerfil=="http://cc.comprogapp.com/")
                {
                    Imagen = "user"; 
                }
                else
                {
                    Imagen = Application.Current.Properties["FotoPerfil"].ToString();   
                }

               

                TipoDocumento = Application.Current.Properties["TipoDocumento"].ToString();
                NumeroDocumento = Application.Current.Properties["NumeroDocumento"].ToString();
                NumeroSocio = Application.Current.Properties["NumeroSocio"].ToString();

                if (Application.Current.Properties["TipoCuenta"].ToString() != "CityCenter")
                {
                    HC = false;
                }
                else
                {
                    HC = true;
                }
				DateTime fecha1 = Convert.ToDateTime(Application.Current.Properties["FechaNacimiento"].ToString());

				Fecha = String.Format("{0:dd/MM/yyyy}",fecha1);

            }
            catch (Exception)
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
                    await Mensajes.Alerta("Parece que no tenés conexión a internet, intentalo mas tarde");

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

               
                //if (NoTarjeta != NoSocio)
                //{
                //await  Mensajes.Info("La tarjeta ingresada es diferente a la que tiene asiganda el usuario");
                //}

                return "OK";
            }
            catch (Exception ex)
            {
                //await Mensajes.Error(ex.ToString());
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
