using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using City_Center.Services;
using Xamarin.Forms;
using static City_Center.Models.PromocionesResultado;
using City_Center.Models;
using System.Linq;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using City_Center.Clases;

namespace City_Center.ViewModels
{
    public class PromocionesViewModel : BaseViewModel
    {
        #region Services
        private ApiService apiService;
        #endregion

        #region Attributes
        private PromocionesReturn listPromociones;
        private ObservableCollection<PromocionesItemViewModel> promocionesDetalle;

        #endregion

        #region Properties

        public ObservableCollection<PromocionesItemViewModel> PromocionesDetalle
        {
            get { return this.promocionesDetalle; }
            set { SetValue(ref this.promocionesDetalle, value); }
        }

        #endregion

        #region Commands

        public ICommand TodosCommand
        {
            get
            {
                return new RelayCommand(Todos);
            }
        }

        private void Todos()
        {
            try
            {
                PromocionesDetalle = new ObservableCollection<PromocionesItemViewModel>(this.ToPromocionesItemViewModel());
            }
            catch (Exception)
            {

            }
           
        }

        public ICommand CasinoCommand
        {
            get
            {
                return new RelayCommand(Casino);
            }
        }

        private void Casino()
        {
            try
            {
                PromocionesDetalle = new ObservableCollection<PromocionesItemViewModel>(this.ToPromocionesItemViewModel().Where(l => l.pro_tipo == "cas"));
            }
            catch (Exception)
            {

            }

        }

        public ICommand HotelCommand
        {
            get
            {
                return new RelayCommand(Hotel);
            }
        }

        private void Hotel()
        {
            try
            {
                PromocionesDetalle = new ObservableCollection<PromocionesItemViewModel>(this.ToPromocionesItemViewModel().Where(l => l.pro_tipo == "hopa"));
 
            }
            catch (Exception )
            {

            }
                  }


        public ICommand ShowCommand
        {
            get
            {
                return new RelayCommand(Show);
            }
        }

        private void Show()
        {
            try
            {
                PromocionesDetalle = new ObservableCollection<PromocionesItemViewModel>(this.ToPromocionesItemViewModel().Where(l => l.pro_tipo == "show"));
            }
            catch (Exception)
            {

            }


       
        }


        public ICommand GastronomiaCommand
        {
            get
            {
                return new RelayCommand(Gastronomia);
            }
        }

        private void Gastronomia()
        {
            try
            {
                PromocionesDetalle = new ObservableCollection<PromocionesItemViewModel>(this.ToPromocionesItemViewModel().Where(l => l.pro_tipo == "gas"));

            }
            catch (Exception)
            {

            }

         }



        #endregion

        #region Methods
        private async void LoadPromociones()
        {
            var connection = await this.apiService.CheckConnection();

            if (!connection.IsSuccess)
            {
                await Mensajes.Alerta("Parece que no tenés conexión a internet, intentalo mas tarde");

                return;
            }


            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("", ""),
            });


            var response = await this.apiService.Get<PromocionesReturn>("/promociones", "/indexApp", content);

            if (!response.IsSuccess)
            {
                await Mensajes.Alerta("Error al cargar Promociones, intenta de nuevo");
                return;
            }

            this.listPromociones = (PromocionesReturn)response.Result;

            PromocionesDetalle = new ObservableCollection<PromocionesItemViewModel>(this.ToPromocionesItemViewModel());

        }

        private IEnumerable<PromocionesItemViewModel> ToPromocionesItemViewModel()
        {
            return this.listPromociones.resultado.Select(l => new PromocionesItemViewModel
            {
                pro_id = l.pro_id,
                pro_id_evento = l.pro_id_evento,
                pro_id_locacion = l.pro_id_locacion,
                pro_nombre = l.pro_nombre,
                pro_descripcion = l.pro_descripcion,
                pro_imagen = l.pro_imagen,
                pro_tipo_promocion = l.pro_tipo_promocion,
                pro_codigo = l.pro_codigo,
                pro_compartidos_codigo = l.pro_compartidos_codigo,
                pro_destacado = l.pro_destacado,
                pro_fecha_duracion_ini = l.pro_fecha_duracion_ini,
                pro_fecha_duracion_fin = l.pro_fecha_duracion_fin,
                pro_importe_decuento = l.pro_importe_decuento,
                pro_porcentaje_decuento = l.pro_porcentaje_decuento,
                pro_id_usuario_creo = l.pro_id_usuario_creo,
                pro_fecha_hora_creo = l.pro_fecha_hora_creo,
                pro_id_usuario_modifico = l.pro_id_usuario_modifico,
                pro_fecha_hora_modifico = l.pro_fecha_hora_modifico,
                pro_tipo = l.pro_tipo,
                pro_estatus = l.pro_estatus,
                loc_nombre = l.loc_nombre
            });
        }

        #endregion

        #region Contructors
        public PromocionesViewModel()
        {
            this.apiService = new ApiService();
            this.LoadPromociones();

        }
        #endregion
    }
}