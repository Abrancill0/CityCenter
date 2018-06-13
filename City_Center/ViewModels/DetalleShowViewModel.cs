using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Windows.Input;
using City_Center.Models;
using City_Center.Services;
using GalaSoft.MvvmLight.Command;
using Plugin.Share;
using Xamarin.Forms;
using static City_Center.Models.EventosResultado;
using City_Center.Clases;
using Plugin.Messaging;
using System.Linq;
using static City_Center.Models.GuardaFavoritosResultado;
using GalaSoft.MvvmLight.Helpers;
using System.Collections.ObjectModel;
using City_Center.PopUp;
using Rg.Plugins.Popup.Extensions;
using City_Center.Page;

namespace City_Center.ViewModels
{
    public class DetalleShowViewModel
    {
        #region Services
        private ApiService apiService;
        #endregion

        #region Attributes
        ShowsViewModel showsito = new ShowsViewModel();
        #endregion

        #region Properties
        public EventosDetalle ds
        {
            get;
            set;
        }

        #endregion

        #region Commands
        public ICommand CompartirCommand
        {
            get
            {
                return new RelayCommand(CompartirUrl);
            }
        }

        private async void CompartirUrl()
        {
            Plugin.Share.Abstractions.ShareMessage Compartir = new Plugin.Share.Abstractions.ShareMessage();

            Compartir.Text = this.ds.eve_nombre;
            Compartir.Title = this.ds.eve_nombre;
            Compartir.Url = "http://wpage.citycenter-rosario.com.ar/es/show-detail/" + this.ds.eve_id + "/" + this.ds.eve_nombre;

            await CrossShare.Current.Share(Compartir);

        }

        public ICommand GuardaFavoritoCommand
        {
            get
            {
                return new RelayCommand(GuardaFavorito);
            }
        }

        private async void GuardaFavorito()
        {
            try
            {
                bool isLoggedIn = Application.Current.Properties.ContainsKey("IsLoggedIn") ?
                                    (bool)Application.Current.Properties["IsLoggedIn"] : false;

                if (isLoggedIn)
                {
                    if (this.ds.eve_guardado == false)
                    {
                        var content = new FormUrlEncodedContent(new[]
                        {
                            new KeyValuePair<string, string>("gua_id_usuario", Application.Current.Properties["IdUsuario"].ToString()),
                            new KeyValuePair<string, string>("gua_id_evento", Convert.ToString(this.ds.eve_id)),
                            new KeyValuePair<string, string>("gua_id_promocion", "0")
                        });

                        var response = await this.apiService.Get<GuardaFavoritosReturn>("/guardados", "/store", content);

                        if (!response.IsSuccess)
                        {
                            await Mensajes.Alerta(response.Message);

                            return;
                        }

                        var list = (GuardaFavoritosReturn)response.Result;

                        this.ds.eve_guardado = true;
                        this.ds.oculta = false;
                         this.ds.eve_id_guardado=list.resultado.gua_id;

                        var actualiza = MainViewModel.GetInstance().listEventos.resultado.Where(l => l.eve_id == this.ds.eve_id).FirstOrDefault();

                        actualiza.eve_guardado = true;
                        actualiza.oculta = false;
                        actualiza.eve_id_guardado = list.resultado.gua_id;


                        showsito.EventosDetalle = new ObservableCollection<EventosItemViewModel>(this.ToEventosItemViewModel());
                        //MainViewModel.GetInstance().listEventos.resultado.ToList();

                        //= new ObservableCollection<EventosItemViewModel>(this.ToEventosItemViewModel())


                        await Mensajes.Alerta("Tu selección fue guardada con éxito");
                    }
                    else
                    {
                        EliminaFavoritos();
                    }   
                }
                else
                {
                    await Mensajes.Alerta("Es necesario que te registres para completar esta acción");
                }

            }
            catch (Exception)
            {

            }

        }


        public ICommand EliminaFavoritosCommand
        {
            get
            {
                return new RelayCommand(EliminaFavoritos);
            }
        }

        private async void EliminaFavoritos()
        {
            try
            {
                bool isLoggedIn = Application.Current.Properties.ContainsKey("IsLoggedIn") ?
                                     (bool)Application.Current.Properties["IsLoggedIn"] : false;

                if (isLoggedIn)
                {

                    if (this.ds.eve_guardado == true)
                    {

                        var content = new FormUrlEncodedContent(new[]
                       {
                            new KeyValuePair<string, string>("gua_id",Convert.ToString(this.ds.eve_id_guardado)),

                        });

                        var response = await this.apiService.Get<GuardaFavoritosReturn>("/guardados", "/destroy", content);

                        if (!response.IsSuccess)
                        {
                            await Mensajes.Alerta("Ha habido un error en tu solicitud, por favor volvé a intentarlo");
                            return;
                        }

                        this.ds.eve_guardado = false;
                        this.ds.oculta = true;
                        this.ds.eve_id_guardado = 0;

                        var actualiza = MainViewModel.GetInstance().listEventos.resultado.Where(l => l.eve_id == this.ds.eve_id).FirstOrDefault();

                        actualiza.eve_guardado = false;
                        actualiza.oculta = true;
                        actualiza.eve_id_guardado = 0;
                       

                        await Mensajes.Alerta("Tu selección fue guardada con éxito");


                    }
                    else
                    {
                        GuardaFavorito();
                    }


                }
                else
                {
                    await Mensajes.Info("Es necesario que te registres para completar esta acción");
                }
            }
            catch (Exception)
            {
                await Mensajes.Info("Es necesario que te registres para completar esta acción");
            }
        }


        public ICommand LlamarCommand
        {
            get
            {
                return new RelayCommand(Llamar);
            }
        }

        private void Llamar()
        {
            var phoneCallTask = CrossMessaging.Current.PhoneDialer;

            if (phoneCallTask.CanMakePhoneCall)
            {
                phoneCallTask.MakePhoneCall(ds.eve_telefono, ds.eve_nombre);
            }

        }


        public ICommand AgregarCalendarioCommand
        {
            get
            {
                return new RelayCommand(AgregarCalendario);
            }
        }

        private void AgregarCalendario()
        {
            Device.OpenUri(new Uri("http://www.google.com/calendar/event?action=TEMPLATE&text=" + this.ds.eve_nombre +"&dates=20140410T173000/20140412T190000&details=" + this.ds.eve_descripcion + "&location=Aqui+ponemos+la+direcci%C3%B3n+donde+se+celebra+el+evento&trp=false&sprop=www.reviblog.net&sprop=name:ReviBlog"));

        }


       

        public ICommand CompraOnlineCommand
        {
            get
            {
                return new RelayCommand(CompraOnline);
            }
        }

      async private void CompraOnline()
        {
            VariablesGlobales.RutaCompraOnline = ds.eve_link;

           // await app.cur.PushPopupAsync(new WebViewTienda);


        // await  Application.Current.MainPage.Navigation.PushAsync(new WebViewCompraOnline());
            await ((MasterPage)Application.Current.MainPage).Detail.Navigation.PushAsync(new WebViewCompraOnline());
            //Device.OpenUri(new Uri(ds.eve_link));

        }

        #endregion

        #region Methods
        private IEnumerable<EventosItemViewModel> ToEventosItemViewModel()
        {
            return MainViewModel.GetInstance().listEventos.resultado.Select(l => new EventosItemViewModel
            {
                eve_imagen = l.eve_imagen,
                eve_descripcion = l.eve_descripcion,
                eve_nombre = l.eve_nombre,
                eve_fecha_hora_inicio = l.eve_fecha_hora_inicio,
                eve_link = l.eve_link,
                eve_id_locacion = l.eve_id_locacion,
                loc_nombre = l.loc_nombre,
                eve_id = l.eve_id,
                eve_guardado = l.eve_guardado,
                eve_id_guardado = l.eve_id_guardado,
                oculta = !(bool)l.eve_guardado,
                eve_fecha_hora_fin = l.eve_fecha_hora_fin,
                eve_id_usuario_creo = l.eve_id_usuario_creo,
                eve_fecha_hora_creo = l.eve_fecha_hora_creo,
                eve_id_usuario_modifico = l.eve_id_usuario_modifico,
                eve_fecha_hora_modifico = l.eve_fecha_hora_modifico,
                eve_num_usuarios_inscritos = l.eve_num_usuarios_inscritos,
                eve_num_compartidos = l.eve_num_compartidos,
                eve_num_favoritos = l.eve_num_favoritos,
                eve_lista = l.eve_lista,
                eve_carrucel = l.eve_carrucel,
                eve_descripcion_locacion = l.eve_descripcion_locacion,
                eve_destacado = l.eve_destacado,
                updated_at = l.updated_at,
                created_at = l.created_at,
                eve_telefono = l.eve_telefono,
                eve_tipo = l.eve_tipo,
                ocultallamada = (string.IsNullOrEmpty(l.eve_telefono) ? false : true),
                ocultaonline = (string.IsNullOrEmpty(l.eve_link) ? false : true)
            });
        }

        #endregion

        #region Contructors
        public DetalleShowViewModel(EventosDetalle ds)
        {
            this.apiService = new ApiService();

            this.ds = ds;

        }
        #endregion
    }
}