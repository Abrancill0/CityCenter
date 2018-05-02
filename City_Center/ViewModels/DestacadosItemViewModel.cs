using System;
using System.Windows.Input;
using City_Center.Page;
using City_Center.Services;
using GalaSoft.MvvmLight.Command;
using Xamarin.Forms;
using static City_Center.Models.DestacadosResultado;

namespace City_Center.ViewModels
{
    public class DestacadosItemViewModel: DestacadosDetalle
    {

        #region Services
        private ApiService apiService;
        #endregion


        #region Commands

        //public ICommand CompartirCommand
        //{
        //    get
        //    {
        //        return new RelayCommand(CompartirUrl);
        //    }
        //}

        //private async void CompartirUrl()
        //{
        //    Plugin.Share.Abstractions.ShareMessage Compartir = new Plugin.Share.Abstractions.ShareMessage();

        //    Compartir.Text = this.eve_descripcion;
        //    Compartir.Title = this.eve_nombre;
        //    Compartir.Url = this.eve_link;

        //    await CrossShare.Current.Share(Compartir);

        //}

        //public ICommand GuardaFavoritoCommand
        //{
        //    get
        //    {
        //        return new RelayCommand(GuardaFavorito);
        //    }
        //}

        //private async void GuardaFavorito()
        //{
        //    var content = new FormUrlEncodedContent(new[]
        //     {
        //        new KeyValuePair<string, string>("gua_id_usuario", Application.Current.Properties["IdUsuario"].ToString()),
        //        new KeyValuePair<string, string>("gua_id_evento", Convert.ToString(this.eve_id)),
        //        new KeyValuePair<string, string>("gua_id_promocion", "0")

        //     });

        //    var response = await this.apiService.Get<GuardadoGenerico>("/guardados", "/store", content);

        //    if (!response.IsSuccess)
        //    {
        //        await Application.Current.MainPage.DisplayAlert(
        //              "Error",
        //              response.Message,
        //              "Ok");
        //        return;
        //    }

        //    var list = (GuardadoGenerico)response.Result;

        //    await Application.Current.MainPage.DisplayAlert(
        //             "City Center",
        //              list.mensaje,
        //             "Ok");
            
        //}


        public ICommand VerDetalleCommand
        {
          get
           {
                return new RelayCommand(VerDetalle);
            }
        }


        private async void VerDetalle()
        {

            MainViewModel.GetInstance().DestacadosDetalle = new DetalleDestacadosViewModel(this);

            await ((MasterPage)Application.Current.MainPage).Detail.Navigation.PushAsync(new DetalleDestacados());

            //await Application.Current.MainPage.Navigation.PushModalAsync(new DetalleShow());
        }


        #endregion


        #region Contructors
        public DestacadosItemViewModel()
        {
            this.apiService = new ApiService();
        }
        #endregion

    }
}
