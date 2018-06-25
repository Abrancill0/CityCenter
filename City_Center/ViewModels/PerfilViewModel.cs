using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Windows.Input;
using City_Center.Clases;
using City_Center.Services;
using GalaSoft.MvvmLight.Command;
using Xamarin.Forms;
using static City_Center.Models.ActualizaUsuarioResultado;
using System.Threading.Tasks;
using static City_Center.Models.TarjetaValidaResultado;
using Acr.UserDialogs;
using System.IO;
using static City_Center.Models.ImagenResultado;
using static City_Center.Models.NotificacionesRecibidasResultado;
using static City_Center.Models.NotificacionesResultado;
using System.Collections.ObjectModel;
using System.Linq;
using City_Center.Page.SlideMenu;

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

        private ImagenReturn ListImagen;

        private ActualizaUsuarioReturn list;

        private NotificacionesRecibidasReturn listNotificaciones;
        private ObservableCollection<NotificacionesRecibidasDetalle> notificacionesDetalle;

        private NotificacionesReturn listConfigNotificaciones;
        private ObservableCollection<NotificacionesDetalle> configNotificacionesDetalle;

        private bool promociones;
        private bool show;
        private bool reservaciones;
        private bool chat;
        private bool eventos;
        private bool avisos;
        private bool geolocalizacion;

        private int PromocionesID;
        private int ShowID;
        private int ReservacionesID;
        private int ChatID;
        private int EventosID;
        private int AvisosID;

        #endregion

        #region Properties

        public bool Promociones
        {
            get { return this.promociones; }
            set { SetValue(ref this.promociones, value); }
        }

        public bool Show
        {
            get { return this.show; }
            set { SetValue(ref this.show, value); }
        }

        public bool Reservaciones
        {
            get { return this.reservaciones; }
            set { SetValue(ref this.reservaciones, value); }
        }

        public bool Chat
        {
            get { return this.chat; }
            set { SetValue(ref this.chat, value); }
        }

        public bool Eventos
        {
            get { return this.eventos; }
            set { SetValue(ref this.eventos, value); }
        }

        public bool Avisos
        {
            get { return this.avisos; }
            set { SetValue(ref this.avisos, value); }
        }

        public bool Geolocalizacion
        {
            get { return this.geolocalizacion; }
            set { SetValue(ref this.geolocalizacion, value); }
        }

        public ObservableCollection<NotificacionesRecibidasDetalle> NotificacionesDetalle
        {
            get { return this.notificacionesDetalle; }
            set { SetValue(ref this.notificacionesDetalle, value); }
        }

        public ObservableCollection<NotificacionesDetalle> ConfigNotificacionesDetalle
        {
            get { return this.configNotificacionesDetalle; }
            set { SetValue(ref this.configNotificacionesDetalle, value); }
        }

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

            try
            {
                
            UserDialogs.Instance.ShowLoading("Procesando...", MaskType.Black);

            if (string.IsNullOrEmpty(this.Nombre))
            {
                await Mensajes.Alerta("Nombre y Apellido es requerida");

                UserDialogs.Instance.HideLoading();

                return;
            }

            if (string.IsNullOrEmpty(this.Email))
            {
                await Mensajes.Alerta("Correo electrónico es requerido");

                UserDialogs.Instance.HideLoading();

                return;
            }

            if (Application.Current.Properties["TipoCuenta"].ToString() == "CityCenter")
            {
                if (string.IsNullOrEmpty(this.Contraseña))
                {
                    await Mensajes.Alerta("Contraseña es requerida");

                    UserDialogs.Instance.HideLoading();

                    return;
                }

                if (string.IsNullOrEmpty(this.Contraseña2))
                {
                    await Mensajes.Alerta("Contraseña es requerida");

                    UserDialogs.Instance.HideLoading();

                    return;
                } 


                if (Contraseña != Contraseña2)
                {
                    await Mensajes.Alerta("Las contraseñas no coinciden, verificar los campos");
                    UserDialogs.Instance.HideLoading();
                    return;
                }

            }
           
            if (string.IsNullOrEmpty(this.Ciudad))
            {
                await Mensajes.Alerta("Ciudad es requerida");

                UserDialogs.Instance.HideLoading();

                return;
            }

            if (this.Fecha == "00/00/0000" || string.IsNullOrEmpty(this.fecha))
            {
                await Mensajes.Alerta("La fecha de nacimiento es obligatoria");

                UserDialogs.Instance.HideLoading();

                return;
            }

           
            if (string.IsNullOrEmpty(NumeroSocio))
            {
              //  await Mensajes.Alerta("Usuario actual no cuenta con tarjeta Win");
            }
            else
            {
                var MensajeTarjeta = await ValidaTarjetaUsuario(NumeroSocio);


                if (MensajeTarjeta == "OK")
                {
                 //   await Mensajes.Alerta("Tarjeta vinculada correctamente");
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
                new KeyValuePair<string, string>("usu_ciudad", this.Ciudad),
                new KeyValuePair<string, string>("usu_id_rol", "6"),
                new KeyValuePair<string, string>("usu_estatus", "1"),
                new KeyValuePair<string, string>("usu_tipo_documento", this.TipoDocumento),
                new KeyValuePair<string, string>("usu_no_documento", this.NumeroDocumento),
                new KeyValuePair<string, string>("passUpdate","1" ),
            });


            var response = await this.apiService.Get<ActualizaUsuarioReturn>("/usuarios", "/update", content);

            if (!response.IsSuccess)
            {
                await Mensajes.Alerta("Ha habido un error en tu solicitud, por favor volvé a intentarlo");

                return;
            }

            list = (ActualizaUsuarioReturn)response.Result;


            string RutaImagen;

            if (string.IsNullOrEmpty(VariablesGlobales.RutaImagene))
            {
                RutaImagen = ""; 
            }
            else
            {
                RutaImagen  = await GuardaImagen(Convert.ToInt32(Application.Current.Properties["IdUsuario"].ToString())); 

                    if (RutaImagen =="Error")
                    {
                        try
                        {
                            RutaImagen = Application.Current.Properties["FotoPerfil"].ToString();
                        }
                        catch (Exception ex)
                        {
                            RutaImagen = "";
                        }
                         
                    }
                    else
                    {
                        Application.Current.Properties["FotoPerfil"] = RutaImagen;   
                    }
               
            } 


            //string RutaImagen = await GuardaImagen(list.resultado.);

            Application.Current.Properties["Email"] = Email;
            Application.Current.Properties["NombreCompleto"]= Nombre ;
            Application.Current.Properties["Ciudad"] = Ciudad;
            Application.Current.Properties["Pass"] = Contraseña ;
          //  Application.Current.Properties["Pass"] = Contraseña2;
            Application.Current.Properties["FechaNacimiento"] = Fecha;


            Application.Current.Properties["TipoDocumento"] =TipoDocumento;
            Application.Current.Properties["NumeroDocumento"] =NumeroDocumento;
            Application.Current.Properties["NumeroSocio"] = NumeroSocio;

			await Application.Current.SavePropertiesAsync();

            await Mensajes.Alerta("Perfil actualizado con éxito");
                VariablesGlobales.ActualizaDatos = true;
            UserDialogs.Instance.HideLoading();

            }
            catch (Exception ex)
            {
                await Mensajes.Alerta("Ocurrio un error al actualizar el perfil,favor de volve a intentar mas tarde");

                UserDialogs.Instance.HideLoading();
            }

        }

        private async Task<string> GuardaImagen(int IDusuario)
        {

            var dirotro = "";

            if (string.IsNullOrEmpty(VariablesGlobales.RutaImagene))
            {
              //  await Mensajes.Alerta("Ninguna foto subida");

                return "Error";
            }
            else
            {
                byte[] ImagenSubir = File.ReadAllBytes(VariablesGlobales.RutaImagene);

                dirotro = Convert.ToBase64String(ImagenSubir);
            }

            var content = new FormUrlEncodedContent(new[]
           {
                new KeyValuePair<string, string>("usu_id", Convert.ToString(IDusuario)),
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

            //await Mensajes.Alerta("Imagen actualizada correctamente");

           // Application.Current.Properties["FotoPerfil"] = VariablesGlobales.RutaServidor + ListImagen.resultado;

           //  await Application.Current.SavePropertiesAsync();

            return VariablesGlobales.RutaServidor + ListImagen.resultado;
           
        }


        public ICommand GuardaConfiguracionCommand
        {
            get
            {
                return new RelayCommand(GuardaConfiguracion);
            }
        }

        private async void GuardaConfiguracion()
        {

            UserDialogs.Instance.ShowLoading("Guardando...", MaskType.Black);

            if (Promociones == true)
            {
                var content2 = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("nus_id_usuario", Application.Current.Properties["IdUsuario"].ToString()),
                    new KeyValuePair<string, string>("nus_id_notificacion",Convert.ToString(PromocionesID)),

                });


                var response2 = await this.apiService.Get<ActualizaUsuarioReturn>("/notificaciones", "/activar_notificacion", content2);

                if (!response2.IsSuccess)
                {
                    await Mensajes.Alerta("Ha habido un error en tu solicitud, por favor volvé a intentarlo");
                    UserDialogs.Instance.HideLoading();
                    return;
                } 
            }
            else
            {
                var content = new FormUrlEncodedContent(new[]
                 {
                    new KeyValuePair<string, string>("nus_id_usuario", Application.Current.Properties["IdUsuario"].ToString()),
                    new KeyValuePair<string, string>("nus_id_notificacion",Convert.ToString(PromocionesID)),

                });

                var response = await this.apiService.Get<ActualizaUsuarioReturn>("/notificaciones", "/cancelar_notificacion", content);

                if (!response.IsSuccess)
                {
                    await Mensajes.Alerta("Ha habido un error en tu solicitud, por favor volvé a intentarlo");
                    UserDialogs.Instance.HideLoading();
                    return;
                }     
            }

            if (Show == true)
            {
                var content2 = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("nus_id_usuario", Application.Current.Properties["IdUsuario"].ToString()),
                    new KeyValuePair<string, string>("nus_id_notificacion",Convert.ToString(ShowID)),

                });


                var response2 = await this.apiService.Get<ActualizaUsuarioReturn>("/notificaciones", "/activar_notificacion", content2);

                if (!response2.IsSuccess)
                {
                    await Mensajes.Alerta("Ha habido un error en tu solicitud, por favor volvé a intentarlo");
                    UserDialogs.Instance.HideLoading();
                    return;
                }   
            }
            else
            {
                var content = new FormUrlEncodedContent(new[]
                 {
                    new KeyValuePair<string, string>("nus_id_usuario", Application.Current.Properties["IdUsuario"].ToString()),
                    new KeyValuePair<string, string>("nus_id_notificacion",Convert.ToString(ShowID)),

                });

                var response = await this.apiService.Get<ActualizaUsuarioReturn>("/notificaciones", "/cancelar_notificacion", content);

                if (!response.IsSuccess)
                {
                    await Mensajes.Alerta("Ha habido un error en tu solicitud, por favor volvé a intentarlo");
                    UserDialogs.Instance.HideLoading();
                    return;
                }     
            }


            if (Reservaciones == true)
            {
                var content2 = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("nus_id_usuario", Application.Current.Properties["IdUsuario"].ToString()),
                    new KeyValuePair<string, string>("nus_id_notificacion",Convert.ToString(ReservacionesID)),

                });


                var response2 = await this.apiService.Get<ActualizaUsuarioReturn>("/notificaciones", "/activar_notificacion", content2);

                if (!response2.IsSuccess)
                {
                    await Mensajes.Alerta("Ha habido un error en tu solicitud, por favor volvé a intentarlo");
                    UserDialogs.Instance.HideLoading();
                    return;
                }   
            }
            else
            {
                var content = new FormUrlEncodedContent(new[]
                 {
                    new KeyValuePair<string, string>("nus_id_usuario", Application.Current.Properties["IdUsuario"].ToString()),
                    new KeyValuePair<string, string>("nus_id_notificacion",Convert.ToString(ReservacionesID)),

                });

                var response = await this.apiService.Get<ActualizaUsuarioReturn>("/notificaciones", "/cancelar_notificacion", content);

                if (!response.IsSuccess)
                {
                    await Mensajes.Alerta("Ha habido un error en tu solicitud, por favor volvé a intentarlo");
                    UserDialogs.Instance.HideLoading();
                    return;
                }     
            }
       

            if (Chat == true)
            {
                var content2 = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("nus_id_usuario", Application.Current.Properties["IdUsuario"].ToString()),
                    new KeyValuePair<string, string>("nus_id_notificacion",Convert.ToString(ChatID)),

                });


                var response2 = await this.apiService.Get<ActualizaUsuarioReturn>("/notificaciones", "/activar_notificacion", content2);

                if (!response2.IsSuccess)
                {
                    await Mensajes.Alerta("Ha habido un error en tu solicitud, por favor volvé a intentarlo");
                    UserDialogs.Instance.HideLoading();
                    return;
                }   
            }
            else
            {
                var content = new FormUrlEncodedContent(new[]
                 {
                    new KeyValuePair<string, string>("nus_id_usuario", Application.Current.Properties["IdUsuario"].ToString()),
                    new KeyValuePair<string, string>("nus_id_notificacion",Convert.ToString(ChatID)),

                });

                var response = await this.apiService.Get<ActualizaUsuarioReturn>("/notificaciones", "/cancelar_notificacion", content);

                if (!response.IsSuccess)
                {
                    await Mensajes.Alerta("Ha habido un error en tu solicitud, por favor volvé a intentarlo");
                    UserDialogs.Instance.HideLoading();
                    return;
                } 
            }

            if (Eventos == true)
            {
                var content2 = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("nus_id_usuario", Application.Current.Properties["IdUsuario"].ToString()),
                    new KeyValuePair<string, string>("nus_id_notificacion",Convert.ToString(EventosID)),

                });


                var response2 = await this.apiService.Get<ActualizaUsuarioReturn>("/notificaciones", "/activar_notificacion", content2);

                if (!response2.IsSuccess)
                {
                    await Mensajes.Alerta("Ha habido un error en tu solicitud, por favor volvé a intentarlo");
                    UserDialogs.Instance.HideLoading();
                    return;
                }   
            }
            else
            {
                var content = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("nus_id_usuario", Application.Current.Properties["IdUsuario"].ToString()),
                    new KeyValuePair<string, string>("nus_id_notificacion",Convert.ToString(EventosID)),

                });

                var response = await this.apiService.Get<ActualizaUsuarioReturn>("/notificaciones", "/cancelar_notificacion", content);

                if (!response.IsSuccess)
                {
                    await Mensajes.Alerta("Ha habido un error en tu solicitud, por favor volvé a intentarlo");
                    UserDialogs.Instance.HideLoading();
                    return;
                }
            }

      
            if (Avisos == true)
            {
                var content2 = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("nus_id_usuario", Application.Current.Properties["IdUsuario"].ToString()),
                    new KeyValuePair<string, string>("nus_id_notificacion",Convert.ToString(AvisosID)),

                });


                var response2 = await this.apiService.Get<ActualizaUsuarioReturn>("/notificaciones", "/activar_notificacion", content2);

                if (!response2.IsSuccess)
                {
                    await Mensajes.Alerta("Ha habido un error en tu solicitud, por favor volvé a intentarlo");
                    UserDialogs.Instance.HideLoading();
                    return;
                }   
            }
            else
            {
                var content = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("nus_id_usuario", Application.Current.Properties["IdUsuario"].ToString()),
                    new KeyValuePair<string, string>("nus_id_notificacion",Convert.ToString(AvisosID)),

                });

                var response = await this.apiService.Get<ActualizaUsuarioReturn>("/notificaciones", "/cancelar_notificacion", content);

                if (!response.IsSuccess)
                {
                    await Mensajes.Alerta("Ha habido un error en tu solicitud, por favor volvé a intentarlo");
                    UserDialogs.Instance.HideLoading();
                    return;
                }
            }

            UserDialogs.Instance.HideLoading();

            Mensajes.Alerta("Configuracion guardada correctamente");
            return;
        }


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

                if (string.IsNullOrEmpty(fotoPerfil))
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
                    await Mensajes.Alerta("Verificá tu conexión a Internet");

                    return "No se tiene conexion a internet";
                }

                //string idusuario = Application.Current.Properties["IdUsuario"].ToString();

                var content = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("usu",Application.Current.Properties["IdUsuario"].ToString()),
                    new KeyValuePair<string, string>("tarjeta",NoTarjeta )
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

        private async void LoadNotificaciones()
        {
            var content = new FormUrlEncodedContent(new[]
               {
                new KeyValuePair<string, string>("nus_id_usuario",Application.Current.Properties["IdUsuario"].ToString())
               });


            var response = await this.apiService.Get<NotificacionesRecibidasReturn>("/notificaciones/", "NotificacionesRecibidas", content);

            if (!response.IsSuccess)
            {

                return;
            }
            this.listNotificaciones = (NotificacionesRecibidasReturn)response.Result;

            NotificacionesDetalle = new ObservableCollection<NotificacionesRecibidasDetalle>(this.ToPromocionesItemViewModel());

        }

        private IEnumerable<NotificacionesRecibidasDetalle> ToPromocionesItemViewModel()
        {
            return this.listNotificaciones.respuesta.Select(l => new NotificacionesRecibidasDetalle
            {
                nen_id = l.nen_id,
                nen_equipo = l.nen_equipo,
                nen_id_usuario = l.nen_id_usuario,
                nen_titulo = l.nen_titulo,
                nen_mensaje = l.nen_mensaje,

            });
        }

      
        private async void LoadConfiguracionNotificaciones()
        {
            try
            {
                var content = new FormUrlEncodedContent(new[]
              {
                new KeyValuePair<string, string>("nus_id_usuario",Application.Current.Properties["IdUsuario"].ToString())
               });


                var response = await this.apiService.Get<NotificacionesReturn>("/notificaciones/", "usuarioNotificaciones", content);

                if (!response.IsSuccess)
                {

                }
                this.listConfigNotificaciones = (NotificacionesReturn)response.Result;

                this.ConfigNotificacionesDetalle = new ObservableCollection<NotificacionesDetalle>(this.ToNotificacionesItemViewModel());


                foreach (var item in ConfigNotificacionesDetalle)
                {


                    switch (item.not_nombre)
                    {
                        case "Avisos":
                            this.Avisos = Convert.ToBoolean(item.nus_activa);
                            AvisosID = item.nus_id_notificacion;
                            break;
                        case "Chat":
                            this.Chat = Convert.ToBoolean(item.nus_activa);
                            ChatID = item.nus_id_notificacion;
                            break;
                        case "Eventos":
                            this.Eventos = Convert.ToBoolean(item.nus_activa);
                            EventosID = item.nus_id_notificacion;
                            break;
                        case "Promociones":
                            this.Promociones = Convert.ToBoolean(item.nus_activa);

                            PromocionesID = item.nus_id_notificacion;
                            break;
                        case "Rservaciones":
                            this.Reservaciones = Convert.ToBoolean(item.nus_activa);

                            ReservacionesID = item.nus_id_notificacion;
                            break;
                        case "Shows":
                            this.Show = Convert.ToBoolean(item.nus_activa);
                            ShowID = item.nus_id_notificacion;
                            break;

                    }
                }


                }
            catch (Exception ex)
            {

            }
           
        }

        private IEnumerable<NotificacionesDetalle> ToNotificacionesItemViewModel()
        {
            
                return this.listConfigNotificaciones.respuesta.Select(l => new NotificacionesDetalle
                {
                    nus_id = l.nus_id,
                    nus_id_usuario = l.nus_id_usuario,
                    nus_id_notificacion = l.nus_id_notificacion,
                    nus_fecha_hora_creo = l.nus_fecha_hora_creo,
                    nus_fecha_hora_modifico = l.nus_fecha_hora_modifico,
                    nus_activa = l.nus_activa,
                    not_nombre = l.not_nombre,
                    not_decripcion = l.not_decripcion,
                });
           
        }

        #endregion

        #region Contructors
        public PerfilViewModel()
        {
            this.apiService = new ApiService();
            LoadCampos();
            LoadNotificaciones();
            LoadConfiguracionNotificaciones();

        }
        #endregion
    }

}
