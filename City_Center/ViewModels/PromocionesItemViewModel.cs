using System;
using System.Windows.Input;
using City_Center.Models;
using City_Center.Page;
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
            MainViewModel.GetInstance().Detallepromociones = new DetallepromocionesViewModel(this);

            await ((MasterPage)Application.Current.MainPage).Detail.Navigation.PushAsync(new DetallePromocion());  

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
