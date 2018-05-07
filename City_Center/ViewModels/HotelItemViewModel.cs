using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Windows.Input;
using City_Center.Services;
using GalaSoft.MvvmLight.Command;
using static City_Center.Models.HabitacionesResultado;
using City_Center.Page;
using Xamarin.Forms;

namespace City_Center.ViewModels
{
    public class HotelItemViewModel : HabitacionesDetalle
    {


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
            MainViewModel.GetInstance().DetalleHotel = new DetalleHotelViewModel(this);

            await ((MasterPage)Application.Current.MainPage).Detail.Navigation.PushAsync(new DetalleHotel());

        }

        #endregion


        #region Contructors
        public HotelItemViewModel()
        {
            
        }
        #endregion

    }
}
