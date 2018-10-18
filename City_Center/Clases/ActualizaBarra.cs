using System;
using Xamarin.Forms;

namespace City_Center.Clases
{
        public class ActualizaBarra
        {
            public static string Cambio(string ColorBarra)
            {

            switch (ColorBarra)
                {
                    case "Inicio":
                        App.NavPage.BarBackgroundColor = Color.FromHex("#23144B");
                        break;
                    case "Casino":
                        App.NavPage.BarBackgroundColor = Color.FromHex("#E91E63");
                        break;
                    case "Hotel":
                        App.NavPage.BarBackgroundColor = Color.FromHex("#2D97A3");
                        break;
                    case "Gastronomia":
                        App.NavPage.BarBackgroundColor = Color.FromHex("#FF5722");
                        break;
                    case "Salon":
                        App.NavPage.BarBackgroundColor = Color.FromHex("#3F51B5");
                        break;
                   }

                return "";
            }
        }
    }
