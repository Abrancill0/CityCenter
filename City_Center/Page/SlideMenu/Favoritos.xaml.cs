using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace City_Center.Page.SlideMenu
{
    public partial class Favoritos : ContentPage
    {
        public Favoritos()
        {
            InitializeComponent();
        }

        protected override void OnDisappearing()
        {

            base.OnDisappearing();

            GC.Collect();

        }
    }
}
