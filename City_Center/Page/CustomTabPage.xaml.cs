using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace City_Center.Page
{
    public partial class CustomTabPage : ContentPage
    {
		private InicioContent page1 = new InicioContent();
		private Casino page2 = new Casino();
		private Hotel page3 = new Hotel();
		private Gastronomia page4 = new Gastronomia();
		private MasInfo page5 = new MasInfo();

		public void ClickTap1(object sender, EventArgs e)
        {
            //var page = new InicioContent();
            //MainView.Content = page1.Content;

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
            //var page = new Casino();
            //MainView.Content = page2.Content;

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
            //var page = new Hotel();
            //MainView.Content = page3.Content;

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
            //var page = new Gastronomia();
			//MainView.Content = page4.Content;

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
			//var page = new MasInfo();
			//MainView.Content = page5.Content;

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

			NavigationPage.SetTitleIcon(this, "logo.png");

			//var page = new InicioContent();
            MainView.Content = page1.Content;
			MainView2.Content = page2.Content;
			MainView3.Content = page3.Content;
			MainView4.Content = page4.Content;
			MainView5.Content = page5.Content;


			Barra.BackgroundColor = Color.FromHex("#877BA1");

            //App.NavPage.BarBackgroundColor=Color.FromHex("#23144B"); 
        }
    }
}
