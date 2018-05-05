using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Windows.Input;
using City_Center.Page;
using City_Center.Services;
using GalaSoft.MvvmLight.Command;
using Xamarin.Forms;
using static City_Center.Models.PozosResultado;
using static City_Center.Models.DestacadosResultado;
using static City_Center.Models.SalaPokerResultado;
using static City_Center.Models.GanadoresResultado;
using static City_Center.Models.PromocionesWinResultado;
using static City_Center.Models.EventosResultado;
using static City_Center.Models.TarjetasResultado;
using static City_Center.Models.PromocionesResultado;
using City_Center.Clases;
using static City_Center.Models.TorneoResultado;

namespace City_Center.ViewModels
{
    public class CasinoViewModel : BaseViewModel
    {
        #region Services
        private ApiService apiService;
        #endregion

        #region Attributes
        private DestacadosReturn listDestacados;
        private PozosReturn listPozos;
        private SalaPokerReturn listSalasPoker;
        private GanadoresReturn listGanadores;
        private ObservableCollection<DestacadosItemViewModel> destacadosDetalle;
        private ObservableCollection<pozosDetalle> pozosDetalle;
        private ObservableCollection<SalaPokerDetalle> salaPokerDetalle;
        private ObservableCollection<GanadoresDetalle> ganadoresDetalle;
        private ObservableCollection<PromocionesWinDetalle> promocionesWinDetalle;

        private TorneoReturn listTorneo;
        private ObservableCollection<TorneoItemViewModel> torneoDetalle;

        private EventosReturn list;
        private ObservableCollection<EventosItemViewModel> eventosDetalle;

        private TarjetasReturn listTarjetas;
        private ObservableCollection<TarjetasDetalle> tarjetasDetalle;

        private PromocionesReturn listPromociones;
        private ObservableCollection<PromocionesItemViewModel> promocionesDetalle;

        private string imagen_Selected;

        #endregion

        #region Properties

        public ObservableCollection<DestacadosItemViewModel> DestacadosDetalle
        {
            get { return this.destacadosDetalle; }
            set { SetValue(ref this.destacadosDetalle, value); }
        }

        public ObservableCollection<pozosDetalle> PozosDetalle
        {
            get { return this.pozosDetalle; }
            set { SetValue(ref this.pozosDetalle, value); }
        }

        public ObservableCollection<SalaPokerDetalle> SalaPokerDetalle
        {
            get { return this.salaPokerDetalle; }
            set { SetValue(ref this.salaPokerDetalle, value); }
        }

        public ObservableCollection<GanadoresDetalle> GanadoresDetalle
        {
            get { return this.ganadoresDetalle; }
            set { SetValue(ref this.ganadoresDetalle, value); }
        }

        public ObservableCollection<PromocionesWinDetalle> PromocionesWinDetalle
        {
            get { return this.promocionesWinDetalle; }
            set { SetValue(ref this.promocionesWinDetalle, value); }
        }

        public string Imagen_Selected
        {
            get { return this.imagen_Selected; }
            set { SetValue(ref this.imagen_Selected, value); }
        }

        public ObservableCollection<EventosItemViewModel> EventosDetalle
        {
            get { return this.eventosDetalle; }
            set { SetValue(ref this.eventosDetalle, value); }
        }

        public ObservableCollection<TarjetasDetalle> TarjetasDetalle
        {
            get { return this.tarjetasDetalle; }
            set { SetValue(ref this.tarjetasDetalle, value); }
        }

        public ObservableCollection<PromocionesItemViewModel> PromocionesDetalle
        {
            get { return this.promocionesDetalle; }
            set { SetValue(ref this.promocionesDetalle, value); }
        }

        public ObservableCollection<TorneoItemViewModel> TorneoDetalle
        {
            get { return this.torneoDetalle; }
            set { SetValue(ref this.torneoDetalle, value); }
        }

        #endregion

        #region Commands
        public ICommand VerRecompensasCommand
        {
            get
            {
                return new RelayCommand(VerRecompensas);
            }
        }

        private async void VerRecompensas()
        {
            MainViewModel.GetInstance().RecompesasWin = new RecompesasWinViewModel();

            ((MasterPage)Application.Current.MainPage).IsPresented = false;

            await ((MasterPage)Application.Current.MainPage).Detail.Navigation.PushAsync(new RecompensasWin());

        }

        public ICommand PokerCommand
        {
            get
            {
                return new RelayCommand(Poker);
            }
        }

        private void Poker()
        {
            if (this.listGanadores == null)
            {
                this.LoadGanadores();
            }

            if (this.listSalasPoker == null)
            {
                this.LoadSalaPoker();
            }

            if (this.list == null)
            {
                this.LoadTorneo();
            }

        }

        public ICommand WinCommand
        {
            get
            {
                return new RelayCommand(Win);
            }
        }

        private void Win()
        {
            if (this.listTarjetas == null)
            {
                this.LoadTarjetas();
            }

        }


        public ICommand ConsultaPuntosCommand
        {
            get
            {
                return new RelayCommand(ConsultaPuntos);
            }
        }

        public async void ConsultaPuntos()
        {
            MainViewModel.GetInstance().ConsultaTarjetaWin = new ConsultaTarjetaWinViewModel();


            await ((MasterPage)Application.Current.MainPage).Detail.Navigation.PushAsync(new ConsultaTarjetaWin());

        }


        #endregion

        #region Methods
        private async void LoadDestacados()
        {
            try
            {
                var connection = await this.apiService.CheckConnection();

                if (!connection.IsSuccess)
                {
                    await Mensajes.Error(connection.Message);

                }


                var content = new FormUrlEncodedContent(new[]
                {
                new KeyValuePair<string, string>("", ""),
            });


                var response = await this.apiService.Get<DestacadosReturn>("/casino/destacados", "/indexApp", content);

                if (!response.IsSuccess)
                {
                    await Mensajes.Error("Error al cargar Destacados");

                    return;
                }

                this.listDestacados = (DestacadosReturn)response.Result;

                DestacadosDetalle = new ObservableCollection<DestacadosItemViewModel>(this.ToDestacadosItemViewModel());

            }
            catch (Exception ex)
            {
                await Mensajes.Error(ex.ToString());
            }


        }

        private IEnumerable<DestacadosItemViewModel> ToDestacadosItemViewModel()
        {
            return this.listDestacados.resultado.Select(l => new DestacadosItemViewModel
            {
                des_imagen = VariablesGlobales.RutaServidor + l.des_imagen,
                des_descripcion = l.des_descripcion,
                des_fecha_hora_inicio = l.des_fecha_hora_inicio,
                des_nombre = l.des_nombre,
                des_id = l.des_id
            });
        }

        private async void LoadPozos()
        {
            try
            {
                var connection = await this.apiService.CheckConnection();

                if (!connection.IsSuccess)
                {
                    await Mensajes.Error(connection.Message);

                    return;
                }


                var content = new FormUrlEncodedContent(new[]
                {
                new KeyValuePair<string, string>("", ""),
            });


                var response = await this.apiService.Get<PozosReturn>("/casino/pozos", "/indexApp", content);

                if (!response.IsSuccess)
                {
                    await Mensajes.Error("Error al cargar Pozos");

                    return;
                }

                this.listPozos = (PozosReturn)response.Result;

                PozosDetalle = new ObservableCollection<pozosDetalle>(this.ToPozosItemViewModel());

            }
            catch (Exception ex)
            {
                await Mensajes.Error(ex.ToString());
            }

        }

        private IEnumerable<pozosDetalle> ToPozosItemViewModel()
        {
            return this.listPozos.resultado.Select(l => new pozosDetalle
            {
                poz_imagen = VariablesGlobales.RutaServidor + l.poz_imagen,
                poz_monto = l.poz_monto,
                poz_descripcion = l.poz_descripcion,
                poz_fecha_entrega = l.poz_fecha_entrega

            });
        }

        private async void LoadSalaPoker()
        {
            try
            {
                var connection = await this.apiService.CheckConnection();

                if (!connection.IsSuccess)
                {

                    await Mensajes.Error(connection.Message);

                    return;
                }


                var content = new FormUrlEncodedContent(new[]
                {
                new KeyValuePair<string, string>("", ""),
            });


                var response = await this.apiService.Get<SalaPokerReturn>("/casino/sala_poker", "/indexApp", content);

                if (!response.IsSuccess)
                {

                    await Mensajes.Error("Error al cargar Sala de Poker");
                    return;
                }

                this.listSalasPoker = (SalaPokerReturn)response.Result;


                Imagen_Selected = VariablesGlobales.RutaServidor + this.listSalasPoker.resultado[0].spo_imagen;


                SalaPokerDetalle = new ObservableCollection<SalaPokerDetalle>(this.ToSalaPokerItemViewModel());

            }
            catch (Exception ex)
            {
                await Mensajes.Error(ex.ToString());
            }



        }

        private IEnumerable<SalaPokerDetalle> ToSalaPokerItemViewModel()
        {
            return this.listSalasPoker.resultado.Select(l => new SalaPokerDetalle
            {
                spo_id = l.spo_id,
                spo_descripcion = l.spo_descripcion,
                spo_imagen = VariablesGlobales.RutaServidor + l.spo_imagen,
                spo_id_usuario_creo = l.spo_id_usuario_creo,
                spo_fecha_hora_creo = l.spo_fecha_hora_creo,
                spo_id_usuario_modifico = l.spo_id_usuario_modifico,
                spo_fecha_hora_modifico = l.spo_fecha_hora_modifico,
                spo_estatus = l.spo_estatus,
                spo_eliminado = l.spo_eliminado
            });
        }

        private async void LoadGanadores()
        {

            try
            {
                var connection = await this.apiService.CheckConnection();

                if (!connection.IsSuccess)
                {
                    await Mensajes.Error(connection.Message);

                    return;
                }

                var content = new FormUrlEncodedContent(new[]
                {
                new KeyValuePair<string, string>("", ""),
            });


                var response = await this.apiService.Get<GanadoresReturn>("/casino/ganadores", "/indexApp", content);

                if (!response.IsSuccess)
                {
                    await Mensajes.Error("Error al cargar Ganadores");

                    return;
                }

                this.listGanadores = (GanadoresReturn)response.Result;

                GanadoresDetalle = new ObservableCollection<GanadoresDetalle>(this.ToGanadoresViewModel());
            }
            catch (Exception ex)
            {
                await Mensajes.Error(ex.ToString());
            }


        }

        private IEnumerable<GanadoresDetalle> ToGanadoresViewModel()
        {
            return this.listGanadores.resultado.Select(l => new GanadoresDetalle
            {
                gan_id = l.gan_id,
                gan_nombre = l.gan_nombre,
                gan_premio = l.gan_premio,
                gan_imagen = VariablesGlobales.RutaServidor + l.gan_imagen,
                gan_id_usuario_creo = l.gan_id_usuario_creo,
                gan_fecha_hora_creo = l.gan_fecha_hora_creo,
                gan_id_usuario_modifico = l.gan_id_usuario_modifico,
                gan_fecha_hora_modifico = l.gan_fecha_hora_modifico,
                gan_estatus = l.gan_estatus,
                gan_eliminado = l.gan_eliminado

            });
        }

        private async void LoadTarjetas()
        {

            try
            {
                var connection = await this.apiService.CheckConnection();

                if (!connection.IsSuccess)
                {
                    await Mensajes.Error(connection.Message);

                    return;
                }


                var content = new FormUrlEncodedContent(new[]
                {
                new KeyValuePair<string, string>("", ""),
            });


                var response = await this.apiService.Get<TarjetasReturn>("/tarjetas", "/indexApp", content);

                if (!response.IsSuccess)
                {

                    await Mensajes.Error("Error al cargar Tarjetas");

                    return;
                }

                this.listTarjetas = (TarjetasReturn)response.Result;

                TarjetasDetalle = new ObservableCollection<TarjetasDetalle>(this.ToTarjetasViewModel());

            }
            catch (Exception ex)
            {
                await Mensajes.Error(ex.ToString());
            }

        }

        private IEnumerable<TarjetasDetalle> ToTarjetasViewModel()
        {
            return this.listTarjetas.resultado.Select(l => new TarjetasDetalle
            {
                tar_id = l.tar_id,
                tar_nombre = l.tar_nombre,
                tar_descripcion = l.tar_descripcion,
                tar_imagen = VariablesGlobales.RutaServidor + l.tar_imagen,
                tar_id_usuario_creo = l.tar_id_usuario_creo,
                tar_fecha_hora_creo = l.tar_fecha_hora_creo,
                tar_id_usuario_modifico = l.tar_id_usuario_modifico,
                tar_fecha_hora_modifico = l.tar_fecha_hora_modifico,
                tar_estatus = l.tar_estatus,
            });
        }

        private async void LoadTorneo()
        {

            try
            {
                var connection = await this.apiService.CheckConnection();

                if (!connection.IsSuccess)
                {
                    await Mensajes.Error(connection.Message);

                    return;
                }


                var content = new FormUrlEncodedContent(new[]
                {
                new KeyValuePair<string, string>("", ""),
            });


                var response = await this.apiService.Get<TorneoReturn>("/casino/torneos", "/indexApp", content);

                if (!response.IsSuccess)
                {
                    await Mensajes.Error("Error al cargar Torneos");

                    return;
                }

                this.listTorneo = (TorneoReturn)response.Result;

                TorneoDetalle = new ObservableCollection<TorneoItemViewModel>(this.ToTorneosItemViewModel());

            }
            catch (Exception ex)
            {
                await Mensajes.Error(ex.ToString());
            }

        }

        private IEnumerable<TorneoItemViewModel> ToTorneosItemViewModel()
        {
            return this.listTorneo.resultado.Select(l => new TorneoItemViewModel
            {
                tor_id = l.tor_id,
                tor_nombre = l.tor_nombre,
                tor_descripcion = l.tor_descripcion,
                tor_imagen = l.tor_imagen,
                tor_fecha_hora_inicio = l.tor_fecha_hora_inicio,
                tor_fecha_hora_fin = l.tor_fecha_hora_fin,
                tor_destacado = l.tor_destacado,
                tor_id_usuario_creo = l.tor_id_usuario_creo,
                tor_fecha_hora_creo = l.tor_fecha_hora_creo,
                tor_id_usuario_modifico = l.tor_id_usuario_modifico,
                tor_fecha_hora_modifico = l.tor_fecha_hora_modifico,
                tor_estatus = l.tor_estatus,
            });
        }

        private async void LoadPromociones()
        {

            try
            {
                var connection = await this.apiService.CheckConnection();

                if (!connection.IsSuccess)
                {
                    await Mensajes.Error(connection.Message);

                    return;
                }

                var content = new FormUrlEncodedContent(new[]
                {
                new KeyValuePair<string, string>("", ""),
            });


                var response = await this.apiService.Get<PromocionesReturn>("/promociones", "/indexApp", content);

                if (!response.IsSuccess)
                {
                    await Mensajes.Error("Error al cargar Promociones");

                    return;
                }

                this.listPromociones = (PromocionesReturn)response.Result;

                PromocionesDetalle = new ObservableCollection<PromocionesItemViewModel>(this.ToPromocionesItemViewModel().Where(a => a.pro_tipo == "cas"));

            }
            catch (Exception ex)
            {
                await Mensajes.Error(ex.ToString());
            }

        }

        private IEnumerable<PromocionesItemViewModel> ToPromocionesItemViewModel()
        {
            return this.listPromociones.resultado.Select(l => new PromocionesItemViewModel
            {
                pro_id = l.pro_id,
                pro_id_evento = l.pro_id_evento,
                pro_id_locacion = l.pro_id_locacion,
                pro_nombre = l.pro_nombre,
                pro_descripcion = l.pro_descripcion,
                pro_imagen = l.pro_imagen,
                pro_tipo_promocion = l.pro_tipo_promocion,
                pro_codigo = l.pro_codigo,
                pro_compartidos_codigo = l.pro_compartidos_codigo,
                pro_destacado = l.pro_destacado,
                pro_fecha_duracion_ini = l.pro_fecha_duracion_ini,
                pro_fecha_duracion_fin = l.pro_fecha_duracion_fin,
                pro_importe_decuento = l.pro_importe_decuento,
                pro_porcentaje_decuento = l.pro_porcentaje_decuento,
                pro_id_usuario_creo = l.pro_id_usuario_creo,
                pro_fecha_hora_creo = l.pro_fecha_hora_creo,
                pro_id_usuario_modifico = l.pro_id_usuario_modifico,
                pro_fecha_hora_modifico = l.pro_fecha_hora_modifico,
                pro_tipo = l.pro_tipo,
                pro_estatus = l.pro_estatus,
                loc_nombre = l.loc_nombre
            });
        }

        #endregion

        #region Contructors
        public CasinoViewModel()
        {
            this.apiService = new ApiService();

            this.LoadDestacados();
            this.LoadPromociones();
            this.LoadPozos();
            this.LoadSalaPoker();
            this.LoadTarjetas();

        }


        #endregion
    }
}

