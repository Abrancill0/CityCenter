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
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();


        }

        private async void loadTarjet()
        {
            var content = new FormUrlEncodedContent(new[]
                {
                new KeyValuePair<string, string>("", ""),
            });
            Restcliente DatosUsuarioRequest = new Restcliente();

            var response = await DatosUsuarioRequest.Get<TarjetasReturn>("/tarjetas/indexApp", content);

            if (response.estatus != 0){
               foreach(TarjetasDetalle it in response.resultado)
                {
                    
                }
            }

        }

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
            catch (Exception ex)
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
            catch (Exception ex)
            {

            }
        }
    }
}
