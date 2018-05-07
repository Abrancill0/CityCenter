using System;
using System.Windows.Input;
using City_Center.Services;
using GalaSoft.MvvmLight.Command;
using static City_Center.Models.SalaPokerResultado;

namespace City_Center.ViewModels
{
    public class SalaPokerItemViewModel:SalaPokerDetalle 
    {
        
        #region Services
        private ApiService apiService;
        #endregion


        #region Attributes
        //public string Imagen_Selected
        //{
        //    get { return this.imagen_Selected; }
        //    set { SetValue(ref this.imagen_Selected, value); }
        //}
        #endregion

        #region Commands

        //public ICommand SeleccionaImagenCommand
        //{
        //    get
        //    {
        //        return new RelayCommand(SeleccionaImagen);
        //    }
        //}

        //private async void SeleccionaImagen()
        //{
            

           


        //}

        #endregion


        #region Contructors
        public SalaPokerItemViewModel()
        {
                this.apiService = new ApiService();

        }
        #endregion

    }
}

