using System;
using System.Collections.Generic;
using City_Center.ViewModels;
using Xamarin.Forms;
using City_Center.Models;
using static City_Center.Models.SalaPokerResultado;
using City_Center.Clases;
using System.Net.Http;
using static City_Center.Models.TarjetasResultado;

namespace City_Center.Page
{
    public partial class Casino : ContentPage
    {
        string[] ListaOpciones;
       
        public Casino()
        {   
         
			InitializeComponent();
            //MainViewModel.GetInstance().Casino = new CasinoViewModel();
         
            ListaOpciones = new string[] { "vip1", "vip2", "svip1", "svip2" };

            NavigationPage.SetTitleIcon(this, "logo.png");
            Img2provisional.Source = "vip1";
            listaCasino.ItemsSource = ListaOpciones;

            MainViewModel.GetInstance().Casino = new CasinoViewModel();
           // loadTarjet();

        }

        protected override void OnAppearing()
		{         
            base.OnAppearing(); 	         
        }

        //private async void loadTarjet()
        //{
        //    var content = new FormUrlEncodedContent(new[]
        //        {
        //        new KeyValuePair<string, string>("", ""),
        //    });
        //    Restcliente DatosUsuarioRequest = new Restcliente();

        //    var response = await DatosUsuarioRequest.Get<TarjetasReturn>("/tarjetas/indexApp", content);

        //    if (response.estatus != 0)
        //    {
        //       foreach(TarjetasDetalle it in response.resultado)
        //        {
        //            Grid grid = new Grid{
                        
        //            };
        //            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(.5f, GridUnitType.Auto) });
        //            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1.5f, GridUnitType.Auto) });
        //            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1f, GridUnitType.Star) });
        //            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1f, GridUnitType.Star) });

        //            Image image = new Image
        //            {
        //                Source = "http://cc.comprogapp.com/" + it.tar_imagen+"?1",

        //                Aspect=Aspect.AspectFill
        //           };
                    
        //            Label name = new Label
        //            {
        //                Text = it.tar_nombre,
        //                TextColor = Color.FromHex("4A3697"),
        //                FontSize = 18f,
        //                HorizontalOptions = LayoutOptions.Start,
        //                VerticalOptions = LayoutOptions.End
        //            };

        //            Label desc = new Label
        //            {
        //                Text = it.tar_descripcion,
        //                TextColor = Color.Black,
        //                FontSize = 12f,
        //                VerticalOptions = LayoutOptions.Center,
        //                HorizontalTextAlignment = TextAlignment.Start
        //            };

        //            grid.Children.Add(name, 1, 0);
        //            grid.Children.Add(desc, 1, 1);
        //            grid.Children.Add(image, 0, 0);
        //            Grid.SetRowSpan(image, 2);
        //            slTarjetas.Children.Add(grid);
        //        }

        //    }

        //}

        protected override void OnDisappearing()
        {         
            base.OnDisappearing();

            GC.Collect();
        }
       
        void Tab1_Tapped(object sender, System.EventArgs e)
        {
            LabelTab1.TextColor = Color.FromHex("#FDE7EE");
            LabelTab2.TextColor = Color.FromHex("#F282A9");
            LabelTab3.TextColor = Color.FromHex("#F282A9");
            LabelTab4.TextColor = Color.FromHex("#F282A9");

            BV1.IsVisible = true;
            BV2.IsVisible = false;
            BV3.IsVisible = false;
            BV4.IsVisible = false;

            SL1.IsVisible = true;
            SL2.IsVisible = false;
            SL3.IsVisible = false;
            SL4.IsVisible = false;
        }

        void Tab2_Tapped(object sender, System.EventArgs e)
        {
            LabelTab1.TextColor = Color.FromHex("#F282A9");
            LabelTab2.TextColor = Color.FromHex("#FDE7EE");
            LabelTab3.TextColor = Color.FromHex("#F282A9");
            LabelTab4.TextColor = Color.FromHex("#F282A9");

            BV1.IsVisible = false;
            BV2.IsVisible = true;
            BV3.IsVisible = false;
            BV4.IsVisible = false;

            SL1.IsVisible = false;
            SL2.IsVisible = true;
            SL3.IsVisible = false;
            SL4.IsVisible = false;
        }

        void Tab3_Tapped(object sender, System.EventArgs e)
        {
            LabelTab1.TextColor = Color.FromHex("#F282A9");
            LabelTab2.TextColor = Color.FromHex("#F282A9");
            LabelTab3.TextColor = Color.FromHex("#FDE7EE");
            LabelTab4.TextColor = Color.FromHex("#F282A9");

            BV1.IsVisible = false;
            BV2.IsVisible = false;
            BV3.IsVisible = true;
            BV4.IsVisible = false;

            SL1.IsVisible = false;
            SL2.IsVisible = false;
            SL3.IsVisible = true;
            SL4.IsVisible = false;
        }

        void Tab4_Tapped(object sender, System.EventArgs e)
        {
            LabelTab1.TextColor = Color.FromHex("#F282A9");
            LabelTab2.TextColor = Color.FromHex("#F282A9");
            LabelTab3.TextColor = Color.FromHex("#F282A9");
            LabelTab4.TextColor = Color.FromHex("#FDE7EE");

            BV1.IsVisible = false;
            BV2.IsVisible = false;
            BV3.IsVisible = false;
            BV4.IsVisible = true;

            SL1.IsVisible = false;
            SL2.IsVisible = false;
            SL3.IsVisible = false;
            SL4.IsVisible = true;
        }

        void Handle_ItemSelected(object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
        {
            try
            {
                var Seleccion = e.SelectedItem;

                if (Seleccion != null)
                {
                    var selection = e.SelectedItem as SalaPokerDetalle;

                    Img1provisional.Source = selection.spo_imagen;


                }
            }
            catch (Exception)
            {

            }


        }

        void Handle_ItemSelected2(object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
        {
            try
            {
                var Seleccion = e.SelectedItem;

                if (Seleccion != null)
                {
                    var selection = e.SelectedItem;

                    Img2provisional.Source = selection.ToString();
                }
            }
            catch (Exception)
            {

            }
        }
    
        void CambiaIcono(object sender, System.EventArgs e)
        {
            try
            {
                bool isLoggedIn = Application.Current.Properties.ContainsKey("IsLoggedIn") ?
                                     (bool)Application.Current.Properties["IsLoggedIn"] : false;

                if (isLoggedIn)
                {

                    Image image = sender as Image;

                    if (image.BackgroundColor != Color.Transparent)
                    {
                        image.BackgroundColor = Color.Transparent;
                        image.Source = "FavoritoOK";
                    }
                    else
                    {
                        image.BackgroundColor = Color.White;
                        image.Source = "Favorito";
                    }

                }

            }
            catch (Exception)
            {


            }
        }

        void CambiaIcono2(object sender, System.EventArgs e)
        {
            try
            {
                bool isLoggedIn = Application.Current.Properties.ContainsKey("IsLoggedIn") ?
                                     (bool)Application.Current.Properties["IsLoggedIn"] : false;

                if (isLoggedIn)
                {

                    Image image = sender as Image;

                    if (image.BackgroundColor != Color.Transparent)
                    {
                        image.BackgroundColor = Color.Transparent;
                        image.Source = "Favorito";
                    }
                    else
                    {
                        image.BackgroundColor = Color.White;
                        image.Source = "FavoritoOK";
                    }

                }

            }
            catch (Exception)
            {


            }
        }
    
    }
}
