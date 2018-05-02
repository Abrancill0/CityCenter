using System;
using System.Windows.Input;
using City_Center.Page;
using City_Center.Services;
using GalaSoft.MvvmLight.Command;
using Xamarin.Forms;
using static City_Center.Models.TorneoResultado;

namespace City_Center.ViewModels
{
    public class TorneoItemViewModel:TorneoDetalle
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
            MainViewModel.GetInstance().TorneoDetalle = new TorneoDetalleViewModel(this);

            await ((MasterPage)Application.Current.MainPage).Detail.Navigation.PushAsync(new DetalleTorneo());

        }

        #endregion

        #region Contructors
        public TorneoItemViewModel()
        {
            this.apiService = new ApiService();
        }
        #endregion

    }
}
