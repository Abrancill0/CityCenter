using System;
using System.Collections.Generic;
using System.Net.Http;
using City_Center.Services;
using Xamarin.Forms;
using City_Center.Clases;
using static City_Center.Models.HabitacionesResultado;

namespace City_Center.ViewModels
{
    public class DetalleHotelViewModel:BaseViewModel
    {
        #region Services
        private ApiService apiService;
        #endregion

        #region Attributes  
        private string imagen_1;
        private string imagen_2;
        private string imagen_3;
        #endregion

        #region Properties
        public HabitacionesDetalle hd
        {
            get;
            set;
        }

        public string Imagen_1
        {
            get { return this.imagen_1; }
            set { SetValue(ref this.imagen_1, value); }
        }

        public string Imagen_2
        {
            get { return this.imagen_2; }
            set { SetValue(ref this.imagen_2, value); }
        }

        public string Imagen_3
        {
            get { return this.imagen_3; }
            set { SetValue(ref this.imagen_3, value); }
        }

      
        #endregion

        #region Commands


        #endregion

        #region Methods
        private async void LoadDetalleHabitacion()
        {
            var connection = await this.apiService.CheckConnection();

            if (!connection.IsSuccess)
            {
                await Mensajes.Error(connection.Message);

                return;
            }

            VariablesGlobales.Img1 = hd.hab_imagen;
            VariablesGlobales.Img2 = "http://cc.comprogapp.com/" + hd.hab_imagen_1;
            VariablesGlobales.Img3 = "http://cc.comprogapp.com/" + hd.hab_imagen_2;
          
        }

        #endregion

        #region Contructors
        public DetalleHotelViewModel(HabitacionesDetalle hd)
        {
            this.apiService = new ApiService();

            this.hd = hd;

            this.LoadDetalleHabitacion();
        }
        #endregion
    }
}