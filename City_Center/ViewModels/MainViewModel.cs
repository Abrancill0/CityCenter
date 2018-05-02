using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace City_Center.ViewModels
{
    public class MainViewModel
    {
        #region Poperties
     
        #endregion

        #region ViewModels
        public LoginViewModel Login { get; set; }
        public InicioViewModel Inicio { get; set; }
        public MasterViewModel Master { get; set; }
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
        #endregion

        #region Contructors
        public MainViewModel()
        {
            //bool isLoggedIn = Application.Current.Properties.ContainsKey("IsLoggedIn") ?
            //                  (bool)Application.Current.Properties["IsLoggedIn"] : false;

            //if (isLoggedIn)
            //{
                instance = this;
                this.Master = new MasterViewModel();
                this.Inicio = new InicioViewModel();
                this.Casino = new CasinoViewModel();
                this.Hotel = new HotelViewModel();
                this.Detail = new DetailViewModel();
                this.Gastronomia = new GastronomiaViewModel();
                this.SalasEventos = new SalasEventosViewModel();
            //}
            //else
            //{
            //    instance = this;
            //    this.Login = new LoginViewModel();
            //}

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
