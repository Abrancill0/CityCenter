﻿using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace City_Center.Page
{
    public partial class DetalleDestacados : ContentPage
    {
        public DetalleDestacados()
        {
            InitializeComponent();

            NavigationPage.SetTitleIcon(this, "logo.png");
        }

        void CambiaIcono(object sender, System.EventArgs e)
        {
            try
            {
                bool isLoggedIn = Application.Current.Properties.ContainsKey("IsLoggedIn") ?
                                     (bool)Application.Current.Properties["IsLoggedIn"] : false;

                if (isLoggedIn)
                {
                    if (Icono1.BackgroundColor != Color.Transparent)
                    {
                        Icono1.BackgroundColor = Color.Transparent;
                        Icono1.Source = "FavoritoOK";
                    }
                    else
                    {
                        Icono1.BackgroundColor = Color.White;
                        Icono1.Source = "Favorito";
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

                    if (Icono2.BackgroundColor != Color.Transparent)
                    {
                        Icono2.BackgroundColor = Color.Transparent;
                        Icono2.Source = "Favorito";
                    }
                    else
                    {
                        Icono2.BackgroundColor = Color.White;
                        Icono2.Source = "FavoritoOK";
                    }

                }

            }
            catch (Exception)
            {


            }
        }
    
    }
}
