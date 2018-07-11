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
        //private EventosReturn list;
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
                if (String.IsNullOrEmpty(VariablesGlobales.FechaShowInicio))
                {
                    EventosDetalle = new ObservableCollection<EventosItemViewModel>(this.ToEventosItemViewModel().Where(l => l.eve_id_locacion == 152 ));
                
                }
                else
                {
                    EventosDetalle = new ObservableCollection<EventosItemViewModel>(this.ToEventosItemViewModel().Where(l => l.eve_id_locacion == 152 && l.eve_fecha_hora_inicio >= Convert.ToDateTime(VariablesGlobales.FechaShowInicio) && l.eve_fecha_hora_inicio <= Convert.ToDateTime(VariablesGlobales.FechaShowFinal)));
                
                }

                                                    
            }
            catch (Exception)
            {
                Mensajes.Alerta("No existen eventos en Jarana");
            }
           
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
                if (String.IsNullOrEmpty(VariablesGlobales.FechaShowInicio))
                {
                    EventosDetalle = new ObservableCollection<EventosItemViewModel>(this.ToEventosItemViewModel().Where(l => l.eve_id_locacion == 1 ));

                }
                else
                {
                    EventosDetalle = new ObservableCollection<EventosItemViewModel>(this.ToEventosItemViewModel().Where(l => l.eve_id_locacion == 1 && l.eve_fecha_hora_inicio >= Convert.ToDateTime(VariablesGlobales.FechaShowInicio) && l.eve_fecha_hora_inicio <= Convert.ToDateTime(VariablesGlobales.FechaShowFinal)));

                }


            }
            catch (Exception)
            {
                Mensajes.Alerta("No existen eventos en Centro de Convenciones");
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
                if (String.IsNullOrEmpty(VariablesGlobales.FechaShowInicio))
                {
                    EventosDetalle = new ObservableCollection<EventosItemViewModel>(this.ToEventosItemViewModel());  
                }
                else
                {
                    EventosDetalle = new ObservableCollection<EventosItemViewModel>(this.ToEventosItemViewModel().Where(l=>l.eve_fecha_hora_inicio >= Convert.ToDateTime(VariablesGlobales.FechaShowInicio)  && l.eve_fecha_hora_inicio<=Convert.ToDateTime(VariablesGlobales.FechaShowFinal)));  
                }
               
            }
            catch (Exception)
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
                await Mensajes.Alerta("Verificá tu conexión a Internet");

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
                await Mensajes.Alerta("Ha habido un error en tu solicitud, por favor volvé a intentarlo");

                return;
            }
            
			if (MainViewModel.GetInstance().listEventos !=null)
			{
				MainViewModel.GetInstance().listEventos.resultado.Clear();
			}
			         
			MainViewModel.GetInstance().listEventos = (EventosReturn)response.Result;

            if (String.IsNullOrEmpty(VariablesGlobales.FechaShowInicio))
            {
                EventosDetalle = new ObservableCollection<EventosItemViewModel>(this.ToEventosItemViewModel());
  
            }
            else
            {
                EventosDetalle = new ObservableCollection<EventosItemViewModel>(this.ToEventosItemViewModel().Where(l=>l.eve_fecha_hora_inicio >= Convert.ToDateTime(VariablesGlobales.FechaShowInicio)  && l.eve_fecha_hora_inicio<=Convert.ToDateTime(VariablesGlobales.FechaShowFinal)));  
  
            }

        }

        private IEnumerable<EventosItemViewModel> ToEventosItemViewModel()
        {
			return MainViewModel.GetInstance().listEventos.resultado.Select(l => new EventosItemViewModel
            {
                eve_imagen =  l.eve_imagen,
                eve_imagen_2 = l.eve_imagen_2,
                eve_descripcion = l.eve_descripcion,
                eve_nombre = l.eve_nombre,
				eve_fecha_hora_inicio = l.eve_fecha_hora_inicio,
                eve_link = l.eve_link,
                eve_id_locacion = l.eve_id_locacion,
                loc_nombre =l.loc_nombre,
                eve_id = l.eve_id,
                eve_guardado = l.eve_guardado,
                eve_id_guardado = l.eve_id_guardado,
                oculta = !(bool)l.eve_guardado,
                eve_fecha_hora_fin = l.eve_fecha_hora_fin,
				eve_id_usuario_creo = l.eve_id_usuario_creo,
				eve_fecha_hora_creo= l.eve_fecha_hora_creo,
				eve_id_usuario_modifico = l.eve_id_usuario_modifico,
				eve_fecha_hora_modifico = l.eve_fecha_hora_modifico,
				eve_num_usuarios_inscritos = l.eve_num_usuarios_inscritos,
				eve_num_compartidos = l.eve_num_compartidos,
				eve_num_favoritos = l.eve_num_favoritos,
				eve_lista = l.eve_lista,
				eve_carrucel = l.eve_carrucel,
				eve_descripcion_locacion = l.eve_descripcion_locacion,
				eve_destacado = l.eve_destacado,
				updated_at = l.updated_at,
				created_at = l.created_at,
				eve_telefono = l.eve_telefono,
				eve_tipo = l.eve_tipo,
				ocultallamada =(string.IsNullOrEmpty(l.eve_telefono) ? false : true),
				ocultaonline =(string.IsNullOrEmpty(l.eve_link) ? false : true)
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
