using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Windows.Input;
using City_Center.Services;
using City_Center.Page;
using GalaSoft.MvvmLight.Command;
using Xamarin.Forms;
using static City_Center.Models.EventosResultado;
using System.Text.RegularExpressions;
using City_Center.Clases;
using System;

namespace City_Center.ViewModels
{
    public class ShowsViewModel : BaseViewModel
    {
        #region Services
        private ApiService apiService;
        #endregion

        #region Attributes
        private EventosReturn list;
        private ObservableCollection<EventosItemViewModel> eventosDetalle;
        #endregion

        #region Properties
        public ObservableCollection<EventosItemViewModel> EventosDetalle
        {
            get { return this.eventosDetalle; }
            set { SetValue(ref this.eventosDetalle, value); }
        }


        #endregion

        #region Commands
        public ICommand JaranaCommand
        {
            get
            {
                return new RelayCommand(Jarana);
            }
        }

        private void Jarana()
        {
            try
            {
                EventosDetalle = new ObservableCollection<EventosItemViewModel>(this.ToEventosItemViewModel().Where(l => l.eve_id_locacion == 2));
           
            }
            catch (Exception)
            {
                Mensajes.Info("No existen eventos en Jaraná");
            }
            //l.loc_nombre =="Jaraná"
        }


        public ICommand CentroCommand
        {
            get
            {
                return new RelayCommand(Centro);
            }
        }

        private void Centro()
        {
            try
            {
                EventosDetalle = new ObservableCollection<EventosItemViewModel>(this.ToEventosItemViewModel().Where(l => l.eve_id_locacion == 1));

            }
            catch (Exception)
            {
                Mensajes.Info("No existen eventos en Centro de Convenciones");
            }

        }


        public ICommand TodosCommand
        {
            get
            {
                return new RelayCommand(Todos);
            }
        }

        private  void Todos()
        {
            try
            {
                EventosDetalle = new ObservableCollection<EventosItemViewModel>(this.ToEventosItemViewModel());
            }
            catch (Exception ex)
            {

            }
           

        }

        #endregion

        #region Methods
        private async void LoadEventos()
        {
            var connection = await this.apiService.CheckConnection();

            if (!connection.IsSuccess)
            {
                await Mensajes.Error(connection.Message);

                return;
            }

             string IDUsuario;

            try
            {
                IDUsuario = Application.Current.Properties["IdUsuario"].ToString();
            }
            catch (Exception)
            {
                IDUsuario = "";
            }
             

            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("usu_id", IDUsuario),
            });


            var response = await this.apiService.Get<EventosReturn>("/shows", "/indexApp", content);

            if (!response.IsSuccess)
            {
                await Mensajes.Error("Error al cargar Shows");

                return;
            }

            this.list = (EventosReturn)response.Result;

            EventosDetalle = new ObservableCollection<EventosItemViewModel>(this.ToEventosItemViewModel());

            if (EventosDetalle.Count == 0)
            {
                await Mensajes.Info("No se encontro ningun evento");
            }

        }

        private IEnumerable<EventosItemViewModel> ToEventosItemViewModel()
        {
            return this.list.resultado.Select(l => new EventosItemViewModel
            {
                eve_imagen =  l.eve_imagen,
                eve_descripcion = l.eve_descripcion,
                eve_nombre = l.eve_nombre,
                eve_fecha_hora_inicio = l.eve_fecha_hora_inicio,
                eve_link = l.eve_link,
                eve_id_locacion = l.eve_id_locacion,
                loc_nombre =l.loc_nombre,
                eve_id = l.eve_id,
                eve_guardado = l.eve_guardado,
                eve_id_guardado = l.eve_id_guardado,
                oculta = !(bool)l.eve_guardado
            });
        }

        #endregion

        #region Contructors
        public ShowsViewModel()
        {
            this.apiService = new ApiService();

            this.LoadEventos();

        }
        #endregion
    }
}
