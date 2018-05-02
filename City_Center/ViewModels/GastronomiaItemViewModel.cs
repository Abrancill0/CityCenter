using System;
using System.Windows.Input;
using City_Center.Services;
using GalaSoft.MvvmLight.Command;
using City_Center.Page;
using static City_Center.Models.RestaurantResultado;
using Xamarin.Forms;

namespace City_Center.ViewModels
{
    public class GastronomiaItemViewModel:RestaurantDetalle
    {

        #region Services
        private ApiService apiService;
        #endregion
       
        #region Commands

        public ICommand DetalleCommand
        {
            get
            {
                return new RelayCommand(Detalle);
            }
        }

        private async void Detalle()
        {
            MainViewModel.GetInstance().DetalleRestaurante = new DetalleRestauranteViewModel(this);

            await((MasterPage)Application.Current.MainPage).Detail.Navigation.PushAsync(new DetalleRestaurante());  
        }

        #endregion


        #region Contructors
        public GastronomiaItemViewModel()
        {
           
        }
        #endregion

    }
}
