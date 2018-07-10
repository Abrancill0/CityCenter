using System;
using System.Windows.Input;
using City_Center.Clases;
using City_Center.Models;
using City_Center.Page;
using City_Center.PopUp;
using City_Center.Services;
using GalaSoft.MvvmLight.Command;
using Xamarin.Forms;
using static City_Center.Models.PromocionesResultado;

namespace City_Center.ViewModels
{
    public class PromocionesItemViewModel:PromocionesDetalle
    {

        #region Services
        private ApiService apiService;
        #endregion


        #region Commands

        public ICommand VerDetalleCommand
        {
            get
            {
                return new RelayCommand(VerDetalle);
            }
        }

        private async void VerDetalle()
        {

            //pro_ejecutar_url
            if (this.pro_ejecutar_url == "1")
            {
                VariablesGlobales.RutaCompraOnline = this.pro_url;

                await ((MasterPage)Application.Current.MainPage).Detail.Navigation.PushAsync(new WebViewCompraOnline());
  
            }
            else
            {
                MainViewModel.GetInstance().Detallepromociones = new DetallepromocionesViewModel(this);

                await ((MasterPage)Application.Current.MainPage).Detail.Navigation.PushAsync(new DetallePromocion()); 
            }
             

        }

      
        #endregion


        #region Contructors
        public PromocionesItemViewModel()
        {
            this.apiService = new ApiService();
        }
        #endregion

    }
}
