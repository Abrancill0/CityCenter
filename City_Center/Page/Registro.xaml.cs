using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Xamarin.Forms;
using City_Center.Clases;

namespace City_Center.Page
{
    public partial class Registro : ContentPage
    {
        async void Handle_Clicked(object sender, System.EventArgs e)
        {
            await Camara();


        }

        public Registro()
        {
            InitializeComponent();

            FechaInicio.Date = DateTime.Now;
        }


        public async Task Camara()
        {

            try
            {
                await CrossMedia.Current.Initialize();

                if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
                {
                   await DisplayAlert("Error", "Camara no ascesible", "OK");
                }

                //obtenemos fecha actual
                long n = Int64.Parse(DateTime.Now.ToString("yyyyMMddHHmmss"));

                var file = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
                {
                    SaveToAlbum = true,
                    Directory = "CityCenter",
                    PhotoSize = PhotoSize.Custom,
                    CustomPhotoSize = 15,
                    CompressionQuality = 10,
                    Name = Convert.ToString(n)
                });

                VariablesGlobales.RutaImagene = file.Path;

                Image1.Source = ImageSource.FromStream(() =>
                {
                    var stream = file.GetStream();
                    file.Dispose();

                    return stream;
                });



            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.ToString(), "OK");

            }


        }


        async void Handle_Tapped(object sender, System.EventArgs e)
        {
            await Camara();
        }
    }
}
