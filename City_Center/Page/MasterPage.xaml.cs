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

            App.NavPage =  Pruebita;//() { BarBackgroundColor = Color.FromHex("#23144B") };; //new NavigationPage(new Prueba()) { BarBackgroundColor = Color.FromHex("#23144B") };

            //Detail = App.NavPage; //new NavigationPage (new TabPage()){ BarBackgroundColor = Color.FromHex("#23144B")};
            App.NavPage.BarBackgroundColor = Color.FromHex("#23144B");
            //App.NavPage.Icon = "logo";
            //NavigationPage.SetTitleIcon(this, "logowhite.png");
            //NavigationPage.SetTitleIcon(this, "logo_hdpi");

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
        }

        #region Botones
        public void ClickTap1(object sender, EventArgs e)
        {
            //tab_home_icon_selected.Source = "home";
            //tab_Casino_icon_selected.Source = "Casinogray";
            //tab_hotel_icon_selected.Source = "Hotelgray";
            //tab_food_icon_selected.Source = "foodgray";
            //tab_info_icon_selected.Source = "InfoGray";

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
            //tab_home_icon_selected.Source = "homegray";
            //tab_Casino_icon_selected.Source = "casino";
            //tab_hotel_icon_selected.Source = "Hotelgray";
            //tab_food_icon_selected.Source = "foodgray";
            //tab_info_icon_selected.Source = "InfoGray";

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
            //tab_home_icon_selected.Source = "homegray";
            //tab_Casino_icon_selected.Source = "Casinogray";
            //tab_hotel_icon_selected.Source = "hotel";
            //tab_food_icon_selected.Source = "foodgray";
            //tab_info_icon_selected.Source = "InfoGray";

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
            //tab_home_icon_selected.Source = "homegray";
            //tab_Casino_icon_selected.Source = "Casinogray";
            //tab_hotel_icon_selected.Source = "Hotelgray";
            //tab_food_icon_selected.Source = "food";
            //tab_info_icon_selected.Source = "InfoGray";

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
            //tab_home_icon_selected.Source = "homegray";
            //tab_Casino_icon_selected.Source = "Casinogray";
            //tab_hotel_icon_selected.Source = "Hotelgray";
            //tab_food_icon_selected.Source = "foodgray";
            //tab_info_icon_selected.Source = "info2";

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

        async void Handle_ItemSelected(object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
        {
            var Seleccion = e.SelectedItem;

            if (Seleccion != null)
            {
                var nameItem = e.SelectedItem.ToString();

                switch (nameItem)
                {
                    case "INICIO":
                        //Application.Current.MainPage = new MasterPage();
                        listviewMenu.SelectedItem = null;

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

                        break;
                    case "SHOWS":

                        //Application.Current.MainPage = new MasterPage();
                        listviewMenu.SelectedItem = null;

                        App.NavPage.BarBackgroundColor = Color.FromHex("#23144B");

                        ((MasterPage)Application.Current.MainPage).IsPresented = false;

                        //if (MainViewModel.GetInstance().Shows == null)
                        //{
                        MainViewModel.GetInstance().Shows = new ShowsViewModel();
                        MainViewModel.GetInstance().EventosItem = new EventosItemViewModel();

                        await ((MasterPage)Application.Current.MainPage).Detail.Navigation.PushAsync(new Show());


                        //}
                        //else
                        //{

                        //    await((MasterPage)Application.Current.MainPage).Detail.Navigation.PushAsync(new Show());
                        //}

                        break;
                    case "PROMOCIONES":
                        //Application.Current.MainPage = new MasterPage();

                        listviewMenu.SelectedItem = null;

                        App.NavPage.BarBackgroundColor = Color.FromHex("#23144B");

                        ((MasterPage)Application.Current.MainPage).IsPresented = false;

                        //if (MainViewModel.GetInstance().Favoritos == null)
                        //{
                        MainViewModel.GetInstance().Promociones = new PromocionesViewModel();

                        await ((MasterPage)Application.Current.MainPage).Detail.Navigation.PushAsync(new Promociones());

                        //}
                        //else
                        //{
                        //    await ((MasterPage)Application.Current.MainPage).Detail.Navigation.PushAsync(new Favoritos());
                        //}



                        break;
                    case "GUARDADOS":

                        bool isLoggedIn = Application.Current.Properties.ContainsKey("IsLoggedIn") ?
                             (bool)Application.Current.Properties["IsLoggedIn"] : false;

                        ((MasterPage)Application.Current.MainPage).IsPresented = false;

                        listviewMenu.SelectedItem = null;

                        if (isLoggedIn)
                        {
                            App.NavPage.BarBackgroundColor = Color.FromHex("#23144B");

                            MainViewModel.GetInstance().Favoritos = new FavoritosViewModel();
                            MainViewModel.GetInstance().FavoritoItem = new FavoritoItemViewModel();

                            await ((MasterPage)Application.Current.MainPage).Detail.Navigation.PushAsync(new Favoritos());
                        }
                        else
                        {
                            listviewMenu.SelectedItem = null;
                            await Mensajes.Alerta("Debes de estar logeado para acceder a esta opcion");
                        }


                        break;
                    case "MÁS INFORMACIÓN":

                        //Application.Current.MainPage = new MasterPage();
                        listviewMenu.SelectedItem = null;

                        App.NavPage.BarBackgroundColor = Color.FromHex("#23144B");

                        ((MasterPage)Application.Current.MainPage).IsPresented = false;

                        await ((MasterPage)Application.Current.MainPage).Detail.Navigation.PushAsync(new InfoPage());


                        break;
                    case "TÉRMINOS Y CONDICIONES GENERALES DE USO":

                        listviewMenu.SelectedItem = null;

                        ((MasterPage)Application.Current.MainPage).IsPresented = false;

                        App.NavPage.BarBackgroundColor = Color.FromHex("#23144B");

                        await ((MasterPage)Application.Current.MainPage).Detail.Navigation.PushAsync(new TerminosCondiciones());


                        break;
                    default:
                        Console.WriteLine("Default case");
                        break;
                }
                return;
            }

        }

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
    }
}
