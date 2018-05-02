using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using City_Center.Clases;
using City_Center.Services;
using Xamarin.Forms;
using static City_Center.Models.SalasEventosResultado;

namespace City_Center.ViewModels
{
    public class SalasEventosViewModel : BaseViewModel
    {
        
        #region Services
        private ApiService apiService;
        #endregion

        #region Attributes
        private SalasEventosReturn listSalasEventos;
        private ObservableCollection<SalasEventosDetalle> salasEventosDetalle;

        private string imagen_Selected;


        #endregion

        #region Properties

        public ObservableCollection<SalasEventosDetalle> SalasEventosDetalle
        {
            get { return this.salasEventosDetalle; }
            set { SetValue(ref this.salasEventosDetalle, value); }
        }


        public string Imagen_Selected
        {
            get { return this.imagen_Selected; }
            set { SetValue(ref this.imagen_Selected, value); }
        }

        #endregion

        #region Commands


        #endregion

        #region Methods
        private async void LoadSalasEventos()
        {
            var connection = await this.apiService.CheckConnection();

            if (!connection.IsSuccess)
            {
                await Mensajes.Error(connection.Message);

                return;
            }


            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("", ""),
            });


            var response = await this.apiService.Get<SalasEventosReturn>("/convenciones_salas/galeria_ecs", "/indexApp", content);

            if (!response.IsSuccess)
            {
                await Mensajes.Error("Error al cargar Galerias");

                return;
            }

            this.listSalasEventos = (SalasEventosReturn)response.Result;

            Imagen_Selected = VariablesGlobales.RutaServidor + this.listSalasEventos.resultado[0].gal_imagen;

            SalasEventosDetalle = new ObservableCollection<SalasEventosDetalle>(this.ToSalasEventosItemViewModel());

        }

        private IEnumerable<SalasEventosDetalle> ToSalasEventosItemViewModel()
        {
            return this.listSalasEventos.resultado.Select(l => new SalasEventosDetalle
            {
                gal_id = l.gal_id,
                gal_descripcion = l.gal_descripcion,
                gal_imagen = VariablesGlobales.RutaServidor + l.gal_imagen,
                gal_galeria = l.gal_galeria,
                gal_id_usuario_creo = l.gal_id_usuario_creo,
                gal_fecha_hora_creo = l.gal_fecha_hora_creo,
                gal_id_usuario_modifico = l.gal_id_usuario_modifico,
                gal_fecha_hora_modifico = l.gal_fecha_hora_modifico,
                gal_estatus = l.gal_estatus,
                gal_eliminado = l.gal_eliminado
            });
        }


       

        #endregion

        #region Contructors
        public SalasEventosViewModel()
        {
            this.apiService = new ApiService();
                this.LoadSalasEventos();
           
        }
        #endregion
    }
}

