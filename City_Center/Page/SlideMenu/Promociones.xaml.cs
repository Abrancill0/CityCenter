using System;
using System.Collections.Generic;
using City_Center.Clases;
using Xamarin.Forms;

namespace City_Center.Page.SlideMenu
{
    public partial class Promociones : ContentPage
    {
        public Promociones()
        {
            InitializeComponent();

            NavigationPage.SetTitleIcon(this, "logo@2x.png.png");
        }

        protected override void OnDisappearing()
        {

            base.OnDisappearing();

            GC.Collect();

        }

        void Tab1_Tapped(object sender, System.EventArgs e)
        {
            LabelTab1.TextColor = Color.FromHex("#EAE8ED");
            LabelTab2.TextColor = Color.FromHex("#71628A");
            LabelTab3.TextColor = Color.FromHex("#71628A");
            LabelTab4.TextColor = Color.FromHex("#71628A");
            LabelTab5.TextColor = Color.FromHex("#71628A");

            BV1.IsVisible = true;
            BV2.IsVisible = false;
            BV3.IsVisible = false;
            BV4.IsVisible = false;
            BV5.IsVisible = false;

        }

        void Tab2_Tapped(object sender, System.EventArgs e)
        {
            LabelTab1.TextColor = Color.FromHex("#71628A");
            LabelTab2.TextColor = Color.FromHex("#EAE8ED");
            LabelTab3.TextColor = Color.FromHex("#71628A");
            LabelTab4.TextColor = Color.FromHex("#71628A");
            LabelTab5.TextColor = Color.FromHex("#71628A");


            BV1.IsVisible = false;
            BV2.IsVisible = true;
            BV3.IsVisible = false;
            BV4.IsVisible = false;
            BV5.IsVisible = false;

        }

        void Tab3_Tapped(object sender, System.EventArgs e)
        {
            LabelTab1.TextColor = Color.FromHex("#71628A");
            LabelTab2.TextColor = Color.FromHex("#71628A");
            LabelTab3.TextColor = Color.FromHex("#EAE8ED");
            LabelTab4.TextColor = Color.FromHex("#71628A");
            LabelTab5.TextColor = Color.FromHex("#71628A");


            BV1.IsVisible = false;
            BV2.IsVisible = false;
            BV3.IsVisible = true;
            BV4.IsVisible = false;
            BV5.IsVisible = false;

        }

        void Tab4_Tapped(object sender, System.EventArgs e)
        {
            LabelTab1.TextColor = Color.FromHex("#71628A");
            LabelTab2.TextColor = Color.FromHex("#71628A");
            LabelTab3.TextColor = Color.FromHex("#71628A");
            LabelTab4.TextColor = Color.FromHex("#EAE8ED");
            LabelTab5.TextColor = Color.FromHex("#71628A");


            BV1.IsVisible = false;
            BV2.IsVisible = false;
            BV3.IsVisible = false;
            BV4.IsVisible = true;
            BV5.IsVisible = false;

        }

        void Tab5_Tapped(object sender, System.EventArgs e)
        {
            LabelTab1.TextColor = Color.FromHex("#71628A");
            LabelTab2.TextColor = Color.FromHex("#71628A");
            LabelTab3.TextColor = Color.FromHex("#71628A");
            LabelTab4.TextColor = Color.FromHex("#71628A");
            LabelTab5.TextColor = Color.FromHex("#EAE8ED");


            BV1.IsVisible = false;
            BV2.IsVisible = false;
            BV3.IsVisible = false;
            BV4.IsVisible = false;
            BV5.IsVisible = true;

           
        }

        async void Chat_click(object sender, System.EventArgs e)
        {
            bool isLoggedIn = Application.Current.Properties.ContainsKey("IsLoggedIn") ?
                                    (bool)Application.Current.Properties["IsLoggedIn"] : false;

            if (isLoggedIn)
            {
                #if __ANDROID__
                await ((MasterPage)Application.Current.MainPage).Detail.Navigation.PushAsync(new SeleccionTipoChat());
#endif
            }
            else
            {
                await Mensajes.Alerta("Es necesario que te registres para completar esta acción");
            }
        }

    }
}
