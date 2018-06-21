using System;
using System.Collections.Generic;
using Xamarin.Forms;
using City_Center.Clases;
using Acr.UserDialogs;
using City_Center.ViewModels;
using City_Center.Page.SlideMenu;

namespace City_Center.Page
{
    public partial class MasterPage : MasterDetailPage
    {
        private InicioContent page1 = new InicioContent();
        private Casino page2 = new Casino();
        private Hotel page3 = new Hotel();
        private Gastronomia page4 = new Gastronomia();
        private MasInfo page5 = new MasInfo();

        public MasterPage()
        {
            InitializeComponent();

            App.NavPage = Pruebita;

            App.NavPage.BarBackgroundColor = Color.FromHex("#23144B");

            MainView.Content = page1.Content;
            MainView2.Content = page2.Content;
            MainView3.Content = page3.Content;
            MainView4.Content = page4.Content;
            MainView5.Content = page5.Content;

            Barra.BackgroundColor = Color.FromHex("#877BA1");

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            VariablesGlobales.FechaShowInicio = string.Empty;
            VariablesGlobales.FechaShowFinal = string.Empty;

            try
            {
                Image1.Source = Application.Current.Properties["FotoPerfil"].ToString();
            }
            catch (Exception ex)
            {

            }
           
        }

        //protected override void OnPropertyChanged(string cosa)
        //{
        //    if (VariablesGlobales.ActualizaDatos == true)
        //   {
        //        VariablesGlobales.ActualizaDatos = false;

        //       this.OnAppearing();
        //    }
        //}

        #region Botones
        public void ClickTap1(object sender, EventArgs e)
        {
            BV1.IsVisible = true;
            BV2.IsVisible = false;
            BV3.IsVisible = false;
            BV4.IsVisible = false;
            BV5.IsVisible = false;

            MainView.IsVisible = true;
            MainView2.IsVisible = false;
            MainView3.IsVisible = false;
            MainView4.IsVisible = false;
            MainView5.IsVisible = false;

            Barra.BackgroundColor = Color.FromHex("#877BA1");

            App.NavPage.BarBackgroundColor = Color.FromHex("#23144B");
        }

        public void ClickTap2(object sender, EventArgs e)
        {
            BV1.IsVisible = false;
            BV2.IsVisible = true;
            BV3.IsVisible = false;
            BV4.IsVisible = false;
            BV5.IsVisible = false;

            MainView.IsVisible = false;
            MainView2.IsVisible = true;
            MainView3.IsVisible = false;
            MainView4.IsVisible = false;
            MainView5.IsVisible = false;

            Barra.BackgroundColor = Color.FromHex("#F783A8");
            App.NavPage.BarBackgroundColor = Color.FromHex("#E91E63");

        }

        public void ClickTap3(object sender, EventArgs e)
        {
            BV1.IsVisible = false;
            BV2.IsVisible = false;
            BV3.IsVisible = true;
            BV4.IsVisible = false;
            BV5.IsVisible = false;

            MainView.IsVisible = false;
            MainView2.IsVisible = false;
            MainView3.IsVisible = true;
            MainView4.IsVisible = false;
            MainView5.IsVisible = false;

            Barra.BackgroundColor = Color.FromHex("#8DC7CB");
            App.NavPage.BarBackgroundColor = Color.FromHex("#2D97A3");

        }

        public void ClickTap4(object sender, EventArgs e)
        {
            BV1.IsVisible = false;
            BV2.IsVisible = false;
            BV3.IsVisible = false;
            BV4.IsVisible = true;
            BV5.IsVisible = false;

            MainView.IsVisible = false;
            MainView2.IsVisible = false;
            MainView3.IsVisible = false;
            MainView4.IsVisible = true;
            MainView5.IsVisible = false;


            Barra.BackgroundColor = Color.FromHex("#F9A786");

            App.NavPage.BarBackgroundColor = Color.FromHex("#FF5722");
            // MainView.ControlTemplate = page.ControlTemplate;
        }

        public void ClickTap5(object sender, EventArgs e)
        {
            BV1.IsVisible = false;
            BV2.IsVisible = false;
            BV3.IsVisible = false;
            BV4.IsVisible = false;
            BV5.IsVisible = true;

            MainView.IsVisible = false;
            MainView2.IsVisible = false;
            MainView3.IsVisible = false;
            MainView4.IsVisible = false;
            MainView5.IsVisible = true;

            Barra.BackgroundColor = Color.FromHex("#97A1D2");

            App.NavPage.BarBackgroundColor = Color.FromHex("#3F51B5");

        }
        #endregion

        async void Chat_click(object sender, System.EventArgs e)
        {
            bool isLoggedIn = Application.Current.Properties.ContainsKey("IsLoggedIn") ?
                                    (bool)Application.Current.Properties["IsLoggedIn"] : false;

            if (isLoggedIn)
            {

                await ((MasterPage)Application.Current.MainPage).Detail.Navigation.PushAsync(new SeleccionTipoChat());
            }
            else
            {
                await Mensajes.Alerta("Es necesario que te registres para completar esta acción");
            }
        }

        void Inicio_Clicked(object sender, System.EventArgs e)
        {

            BV1.IsVisible = true;
            BV2.IsVisible = false;
            BV3.IsVisible = false;
            BV4.IsVisible = false;
            BV5.IsVisible = false;

            MainView.IsVisible = true;
            MainView2.IsVisible = false;
            MainView3.IsVisible = false;
            MainView4.IsVisible = false;
            MainView5.IsVisible = false;

            Barra.BackgroundColor = Color.FromHex("#877BA1");

            App.NavPage.BarBackgroundColor = Color.FromHex("#23144B");

            ((MasterPage)Application.Current.MainPage).IsPresented = false;

        }

        async void Shows_Clicked(object sender, System.EventArgs e)
        {
            App.NavPage.BarBackgroundColor = Color.FromHex("#23144B");

           // ((MasterPage)Application.Current.MainPage).IsPresented = false;
            this.IsPresented = false;

            MainViewModel.GetInstance().Shows = new ShowsViewModel();
            MainViewModel.GetInstance().EventosItem = new EventosItemViewModel();

            await ((MasterPage)Application.Current.MainPage).Detail.Navigation.PushAsync(new Show());

        }

        async void Promociones_Clicked(object sender, System.EventArgs e)
        {
            App.NavPage.BarBackgroundColor = Color.FromHex("#23144B");

            ((MasterPage)Application.Current.MainPage).IsPresented = false;

            MainViewModel.GetInstance().Promociones = new PromocionesViewModel();

            await ((MasterPage)Application.Current.MainPage).Detail.Navigation.PushAsync(new Promociones());


        }

        async void Guardados_Clicked(object sender, System.EventArgs e)
        {
            bool isLoggedIn = Application.Current.Properties.ContainsKey("IsLoggedIn") ?
                           (bool)Application.Current.Properties["IsLoggedIn"] : false;

            ((MasterPage)Application.Current.MainPage).IsPresented = false;

            if (isLoggedIn)
            {
                App.NavPage.BarBackgroundColor = Color.FromHex("#23144B");

                MainViewModel.GetInstance().Favoritos = new FavoritosViewModel();
                MainViewModel.GetInstance().FavoritoItem = new FavoritoItemViewModel();

                await ((MasterPage)Application.Current.MainPage).Detail.Navigation.PushAsync(new Favoritos());
            }
            else
            {
                await Mensajes.Alerta("Debes de estar registrado para acceder a esta opción");
            }
        }

        async void MasInfo_Clicked(object sender, System.EventArgs e)
        {

            App.NavPage.BarBackgroundColor = Color.FromHex("#23144B");

            ((MasterPage)Application.Current.MainPage).IsPresented = false;

            await ((MasterPage)Application.Current.MainPage).Detail.Navigation.PushAsync(new InfoPage());


        }

        async void Terminos_Clicked(object sender, System.EventArgs e)
        {
            ((MasterPage)Application.Current.MainPage).IsPresented = false;

            App.NavPage.BarBackgroundColor = Color.FromHex("#23144B");

            await ((MasterPage)Application.Current.MainPage).Detail.Navigation.PushAsync(new TerminosCondiciones());

        }
    }
}
