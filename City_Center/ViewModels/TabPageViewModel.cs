using System;
using System.Windows.Input;
using City_Center.Services;
using GalaSoft.MvvmLight.Command;

namespace City_Center.ViewModels
{
    public class TabPageViewModel
    {

        #region Services
        private ApiService apiService;
        #endregion

        #region Attributes
      

        #endregion

        #region Properties

       

        #endregion

        #region Commands


        public ICommand CargaHotelCommand
        {
            get
            {
                return new RelayCommand(CargaHotel);
            }
        }

        private  void CargaHotel()
        {
           
          //  MainViewModel.GetInstance().Hotel = new HotelViewModel();
        }


        public ICommand CargaGastronomiaCommand
        {
            get
            {
                return new RelayCommand(CargaGastronomia);
            }
        }

        private  void CargaGastronomia()
        {
          //  MainViewModel.GetInstance().Gastronomia = new GastronomiaViewModel();
        }


        #endregion

        #region Methods


        #endregion

        #region Contructors
        public TabPageViewModel()
        {
            this.apiService = new ApiService();
         
        }
        #endregion
    }
}
