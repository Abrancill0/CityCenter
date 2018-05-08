using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using City_Center.Services;
using Xamarin.Forms;
using static City_Center.Models.RecompensasWinResultado;
using City_Center.Models;
using City_Center.Clases;

namespace City_Center.ViewModels
{
    public class RecompesasWinViewModel : BaseViewModel
    {
        #region Services
        private ApiService apiService;
        #endregion

        #region Attributes

        private RecompesasWinReturn listRecompensasWin;

        private ObservableCollection<RecompesasWinDetalle> recompensasWinDetalle;

        #endregion

        #region Properties
       
        public ObservableCollection<RecompesasWinDetalle> RecompensasWinDetalle
        {
            get { return this.recompensasWinDetalle; }
            set { SetValue(ref this.recompensasWinDetalle, value); }
        }

        #endregion


        #region Methods

        private async void LoadRecompensasWin()
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


            var response = await this.apiService.Get<RecompesasWinReturn>("/casino/win_recompensas", "/indexApp", content);

            if (!response.IsSuccess)
            {
                await Mensajes.Error("Error al cargar Recompensas Win");
                return;
            }

            this.listRecompensasWin = (RecompesasWinReturn)response.Result;

            RecompensasWinDetalle = new ObservableCollection<RecompesasWinDetalle>();

            string Color ="#FFFFFF";

            foreach (var l in listRecompensasWin.resultado)
            {
                
                RecompensasWinDetalle.Add(new RecompesasWinDetalle()
                {
                    wre_id = l.wre_id,
                    wre_nombre = l.wre_nombre,
                    wre_descripcion = l.wre_descripcion,
                    wre_puntos = l.wre_puntos,
                    wre_id_usuario_creo = l.wre_id_usuario_creo,
                    wre_fecha_hora_creo = l.wre_fecha_hora_creo,
                    wre_id_usuario_modifico = l.wre_id_usuario_modifico,
                    wre_fecha_hora_modifico = l.wre_fecha_hora_modifico,
                    wre_estatus = l.wre_estatus,
                    wre_eliminado = l.wre_eliminado,
                    Color = Color
                });

                if (Color =="#FFFFFF")
                {
                    Color = "#FCE8F0"; 
                }
                else
                {
                    Color = "#FFFFFF"; 
                }

            }

            //RecompensasWinDetalle = new ObservableCollection<RecompesasWinDetalle>(this.ToRecompensasWinViewModel());

        }

        private IEnumerable<RecompesasWinDetalle> ToRecompensasWinViewModel()
        {
            return this.listRecompensasWin.resultado.Select(l => new RecompesasWinDetalle
            {
                wre_id = l.wre_id,
                wre_nombre = l.wre_nombre,
                wre_descripcion = l.wre_descripcion,
                wre_puntos = l.wre_puntos,
                wre_id_usuario_creo = l.wre_id_usuario_creo,
                wre_fecha_hora_creo = l.wre_fecha_hora_creo,
                wre_id_usuario_modifico = l.wre_id_usuario_modifico,
                wre_fecha_hora_modifico = l.wre_fecha_hora_modifico,
                wre_estatus = l.wre_estatus,
                wre_eliminado = l.wre_eliminado
            });
        }

        #endregion

        #region Contructors
        public RecompesasWinViewModel()
        {
            this.apiService = new ApiService();

            this.LoadRecompensasWin();
        }
        #endregion
    }
}
