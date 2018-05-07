using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace City_Center.Page
{
    public partial class DetallePromocion : ContentPage
    {
        public DetallePromocion()
        {
            InitializeComponent();
        }

        void Handle_Clicked(object sender, System.EventArgs e)
        {
            if (SLR.IsVisible==false)
            {
                SLR.IsVisible = true;
                SLP.IsVisible = false;
                FlechaAbajo.IsVisible = false;
                FlechaArriba.IsVisible = true;
            }
            else
            {
                SLR.IsVisible = false;
                SLP.IsVisible = true;
                FlechaAbajo.IsVisible = true;
                FlechaArriba.IsVisible = false;
            }
        }
    }
}
