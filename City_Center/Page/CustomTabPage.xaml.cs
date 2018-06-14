using System;
using System.Collections.Generic;

using Xamarin.Forms;
using City_Center.Clases;

namespace City_Center.Page
{
    public partial class CustomTabPage : ContentPage
    {
		private InicioContent page1 = new InicioContent();
		private Casino page2 = new Casino();
		private Hotel page3 = new Hotel();
		private Gastronomia page4 = new Gastronomia();
		private MasInfo page5 = new MasInfo();

        protected override void OnAppearing()
        {
            base.OnAppearing();

            VariablesGlobales.FechaShowInicio = string.Empty;
            VariablesGlobales.FechaShowFinal = string.Empty;
        }


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
			App.NavPage.BarBackgroundColor=Color.FromHex("#23144B"); 
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
			App.NavPage.BarBackgroundColor=Color.FromHex("#E91E63"); 

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
            
			Barra.BackgroundColor =Color.FromHex("#8DC7CB"); 
			App.NavPage.BarBackgroundColor=Color.FromHex("#2D97A3"); 
            
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

			App.NavPage.BarBackgroundColor=Color.FromHex("#FF5722"); 
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

			App.NavPage.BarBackgroundColor=Color.FromHex("#3F51B5"); 
        
        }
        
        public CustomTabPage()
        {
            InitializeComponent();
            //NavigationPage.SetTitleIcon(this, "logo_hdpi");

            MainView.Content = page1.Content;
            MainView2.Content = page2.Content;
            MainView3.Content = page3.Content;
            MainView4.Content = page4.Content;
            MainView5.Content = page5.Content;

            Barra.BackgroundColor = Color.FromHex("#877BA1");

        }


        }
    }
