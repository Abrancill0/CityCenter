using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using static City_Center.Models.EventosResultado;
using GalaSoft.MvvmLight.Helpers;
using City_Center.ViewModels;
using City_Center.Clases;
using System.Linq;

using Xamarin.Forms;

namespace City_Center.Page.SlideMenu
{
    public partial class TerminosCondiciones : ContentPage
    {
        public TerminosCondiciones()
        {
            InitializeComponent();

            NavigationPage.SetTitleIcon(this, "logo@2x.png.png");
        }

        protected override void OnDisappearing()
        {

            base.OnDisappearing();
            ActualizaBarra.Cambio(VariablesGlobales.VentanaActual);
            GC.Collect();

        }
    }
}
