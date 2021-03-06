﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using City_Center.Services;
using Xamarin.Forms;
using City_Center.Clases;
using static City_Center.Models.HabitacionesResultado;
using System.Collections.ObjectModel;
using City_Center.Page;
using System.Linq;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;

namespace City_Center.ViewModels
{
    public class DetalleHotelViewModel:BaseViewModel
    {
        #region Services
        private ApiService apiService;
        #endregion

        #region Attributes  
        private HabitacionesReturn listHabitaciones;
        private ObservableCollection<HabitacionesDetalle> habitacionesDetalle;

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

        public ObservableCollection<HabitacionesDetalle> HabitacionesDetalle
        {
            get { return this.habitacionesDetalle; }
            set { SetValue(ref this.habitacionesDetalle, value); }
        }

        #endregion

        #region Commands

        //public ICommand CambiaHabitacionCommand
        //{
        //    get
        //    {
        //        return new RelayCommand(CambiaHabitacion);
        //    }
        //}

        //private async void CambiaHabitacion()
        //{

        //    var connection = await this.apiService.CheckConnection();

        //    if (!connection.IsSuccess)
        //    {
        //        await Mensajes.Error(connection.Message);

        //        return;
        //    }

        //}

      

        #endregion

        #region Methods
        private async void LoadDetalleHabitacion()
        {
            var connection = await this.apiService.CheckConnection();

            if (!connection.IsSuccess)
            {
                await Mensajes.Alerta(connection.Message);

                return;
            }

            VariablesGlobales.Img1 = hd.hab_imagen;
            VariablesGlobales.Img2 = "https://citycenter-rosario.com.ar/" + hd.hab_imagen_1;
            VariablesGlobales.Img3 = "https://citycenter-rosario.com.ar/" + hd.hab_imagen_2;
            VariablesGlobales.Img4 = "https://citycenter-rosario.com.ar/" + hd.hab_imagen_3;
            VariablesGlobales.Img5 = "https://citycenter-rosario.com.ar/" + hd.hab_imagen_4;
            VariablesGlobales.Img6 = "https://citycenter-rosario.com.ar/" + hd.hab_imagen_5;
            
        }

        private async void CargaOtrasHabitaciones()
        {
            var connection = await this.apiService.CheckConnection();

            if (!connection.IsSuccess)
            {
                await Mensajes.Alerta(connection.Message);

                return;
            }


            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("", ""),
            });


            var response = await this.apiService.Get<HabitacionesReturn>("/hotel_spa/habitaciones", "/indexApp", content);

            if (!response.IsSuccess)
            {
                await Mensajes.Alerta("Ha habido un error en tu solicitud, por favor volvé a intentarlo");

                return;
            }

            try
            {

            this.listHabitaciones = (HabitacionesReturn)response.Result;

                HabitacionesDetalle = new ObservableCollection<HabitacionesDetalle>();

                foreach (var l in listHabitaciones.resultado)
                {

                    int PosicionEspacio1 = l.hab_nombre.IndexOf(" ");
                    string Arreglo1 = l.hab_nombre.Substring(0, PosicionEspacio1);
                    string Arreglo2 = l.hab_nombre.Substring(PosicionEspacio1 + 1);


                    HabitacionesDetalle.Add(new HotelItemViewModel()
                    {
                        hab_id = l.hab_id,
                        hab_nombre = Arreglo1,
                        hab_nombre2 = Arreglo2,
                        hab_descripcion = l.hab_descripcion,
                        hab_imagen = VariablesGlobales.RutaServidor + l.hab_imagen,
                        hab_titulo_1 = l.hab_titulo_1,
                        hab_descripcion_1 = l.hab_descripcion_1,
                        hab_imagen_1 = l.hab_imagen_1,
                        hab_titulo_2 = l.hab_titulo_2,
                        hab_descripcion_2 = l.hab_descripcion_2,
                        hab_imagen_2 = l.hab_imagen_2,
                        hab_titulo_3 = l.hab_titulo_3,
                        hab_id_usuario_creo = l.hab_id_usuario_creo,
                        hab_fecha_hora_creo = l.hab_fecha_hora_creo,
                        hab_id_usuario_modifico = l.hab_id_usuario_modifico,
                        hab_fecha_hora_modifico = l.hab_fecha_hora_modifico,
                        hab_estatus = l.hab_estatus,
                        hab_descripcion_3 = l.hab_descripcion_3,
                        hab_imagen_3 = l.hab_imagen_3,
                        hab_imagen_4 = l.hab_imagen_4,
                        hab_imagen_5 = l.hab_imagen_5,
                        hab_imagen_6 = l.hab_imagen_6
                    });



                    //            HabitacionesDetalle = new ObservableCollection<HotelItemViewModel>(this.ToHabitacionesItemViewModel());


                }
            }
            catch (Exception ex)
            {

            }


        }

        private IEnumerable<HabitacionesDetalle> ToHabitacionesItemViewModel()
        {
            return this.listHabitaciones.resultado.Select(l => new HabitacionesDetalle
            {
                hab_id = l.hab_id,
                hab_nombre = l.hab_nombre,
                hab_descripcion = l.hab_descripcion,
                hab_imagen = VariablesGlobales.RutaServidor + l.hab_imagen,
                hab_titulo_1 = l.hab_titulo_1,
                hab_descripcion_1 = l.hab_descripcion_1,
                hab_imagen_1 = l.hab_imagen_1,
                hab_titulo_2 = l.hab_titulo_2,
                hab_descripcion_2 = l.hab_descripcion_2,
                hab_imagen_2 = l.hab_imagen_2,
                hab_titulo_3 = l.hab_titulo_3,
                hab_id_usuario_creo = l.hab_id_usuario_creo,
                hab_fecha_hora_creo = l.hab_fecha_hora_creo,
                hab_id_usuario_modifico = l.hab_id_usuario_modifico,
                hab_fecha_hora_modifico = l.hab_fecha_hora_modifico,
                hab_estatus = l.hab_estatus,
                hab_descripcion_3 = l.hab_descripcion_3,
                hab_imagen_3 = l.hab_imagen_3,
                hab_imagen_4 = l.hab_imagen_4,
                hab_imagen_5 = l.hab_imagen_5,
                hab_imagen_6 = l.hab_imagen_6,
            });
        }


        #endregion

        #region Contructors
        public DetalleHotelViewModel(HabitacionesDetalle hd)
        {
            this.apiService = new ApiService();

            this.hd = hd;

            this.LoadDetalleHabitacion();

            this.CargaOtrasHabitaciones();
        }
        #endregion
    }
}