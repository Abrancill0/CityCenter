using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text;
using City_Center.Clases;
using City_Center.Services;
using Xamarin.Forms;
using static City_Center.Models.DestacadosResultado;
using static City_Center.Models.EventosResultado;
using static City_Center.Models.FavoritosResultado;
using static City_Center.Models.PromocionesResultado;
using static City_Center.Models.TorneoResultado;

namespace City_Center.ViewModels
{
    public class MainViewModel
    {
        #region Poperties
		public EventosReturn listEventos { get; set; }
		public TorneoReturn listTorneo { get; set; }
        public FavoritoReturn listFavoritos { get; set; }
        public DestacadosReturn listDestacados { get; set; }
        #endregion

        #region ViewModels
        public LoginViewModel Login { get; set; }
        public InicioViewModel Inicio { get; set; }
      //  public MasterViewModel Master { get; set; }
        public RegistroViewModel Registro { get; set; }
        public ShowsViewModel Shows { get; set; }
        public DetailViewModel Detail { get; set; }
        public PerfilViewModel Perfil { get; set; }
        public EventosItemViewModel EventosItem { get; set; } 
        public CasinoViewModel Casino { get; set; } 
        public FavoritosViewModel Favoritos { get; set; }
        public FavoritoItemViewModel FavoritoItem { get; set; }
        public HotelViewModel Hotel { get; set; }
        public RecompesasWinViewModel RecompesasWin { get; set; }
        public SalaPokerItemViewModel SalaPokerItemViewModel { get; set; }
        public GastronomiaItemViewModel GastronomiaItem { get; set; }
        public GastronomiaViewModel Gastronomia { get; set; }
        public PromocionesViewModel Promociones { get; set; }
        public DetalleHotelViewModel DetalleHotel { get; set; }
        public SalasEventosViewModel SalasEventos { get; set; }
        public DetalleShowViewModel DetalleShow { get; set; }
        public DetalleRestauranteViewModel DetalleRestaurante { get; set; }
        public PromocionesItemViewModel PromocionesItem { get; set; }
        public DetallepromocionesViewModel Detallepromociones { get; set; }
        public TorneoViewModel Torneo { get; set; }
        public TorneoDetalleViewModel TorneoDetalle { get; set; }
        public TorneoItemViewModel TorneoItem { get; set; }
        public DetalleDestacadosViewModel DestacadosDetalle { get; set; }
        public DestacadosItemViewModel DestacadosItem { get; set; }
        public ReiniciaPassViewModel ReiniciaPass { get; set; }
        public ConsultaTarjetaWinViewModel ConsultaTarjetaWin { get; set; }
		public CambiaPassViewModel CambiaContrasena { get; set; }
        public VincularTarjetaViewModel VincularTarjeta { get; set; }
        public TabPageViewModel TabPage { get; set; }
        public ChatviewModel Chat { get; set; }
        #endregion

        #region Contructors
        public MainViewModel()
        {
            
                instance = this;

             //   this.Master = new MasterViewModel();
                this.Inicio = new InicioViewModel();
                this.Casino = new CasinoViewModel();
               
                this.Hotel = new HotelViewModel();
                this.Detail = new DetailViewModel();
                this.Gastronomia = new GastronomiaViewModel();
                this.SalasEventos = new SalasEventosViewModel();
          

        }
        #endregion

        #region Methods
       
        #endregion

        #region Singleton
        private static MainViewModel instance;

        public static MainViewModel GetInstance()
        {
            if (instance  == null)
            {
                return new MainViewModel();
            }

            return instance;
        }
        #endregion

    }
}
