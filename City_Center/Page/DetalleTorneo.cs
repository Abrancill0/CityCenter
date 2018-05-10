using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace City_Center.Page
{
    public partial class DetalleTorneo : ContentPage
    {
		private string[] ListaOpciones;

        public DetalleTorneo()
        {
            InitializeComponent();
            NavigationPage.SetTitleIcon(this, "logo.png");

			ListaOpciones = new string[] { "DNI", "LE", "LC", "CI" };

            TipoDocumento.ItemsSource = ListaOpciones;

            TipoDocumento.SelectedIndex = 0;

        }

        void Handle_Clicked(object sender, System.EventArgs e)
        {
            if (SLR.IsVisible == false)
            {
                SLR.IsVisible = true;
                SLT.IsVisible = false;
            }
            else
            {
                SLR.IsVisible = false;
                SLT.IsVisible = true;
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

                    image.Source = "FavoritoOK";
                }

            }
            catch (Exception)
            {


            }
        }
    
    }
}
