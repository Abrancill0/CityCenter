using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Acr.UserDialogs;
using City_Center.Clases;
using City_Center.Helper;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using Xamarin.Forms;
using City_Center.ViewModels;

namespace City_Center.Page
{
    public partial class Perfil : ContentPage
    {
        
		private string[] ListaOpciones;

        public Perfil()
        {
            InitializeComponent();

            NavigationPage.SetTitleIcon(this, "logo@2x.png");

            if (VariablesGlobales.Notificaciones == true)
            {
                LabelTab1.TextColor = Color.FromHex("#71628A");
                LabelTab2.TextColor = Color.FromHex("#71628A");
                LabelTab3.TextColor = Color.FromHex("#FDFDFD");

                BV1.IsVisible = false;
                BV2.IsVisible = false;
                BV3.IsVisible = true;

                SL1.IsVisible = false;
                SL2.IsVisible = false;
                SL3.IsVisible = true;
            }
        }
        
        protected override void OnAppearing()
        {
            base.OnAppearing();

        }

        protected override void OnDisappearing()
        {
             //MainViewModel.GetInstance().Detail = new DetailViewModel();
            if  (VariablesGlobales.ActualizaDatos == true)
            {
                VariablesGlobales.ActualizaDatos = false;
                MasterPage fpm = new MasterPage();
         
            Application.Current.MainPage = fpm;
            
            }

            base.OnDisappearing();

        }

        void Tab1_Tapped(object sender, System.EventArgs e)
        {
            LabelTab1.TextColor = Color.FromHex("#FDFDFD");
            LabelTab2.TextColor = Color.FromHex("#71628A");
            LabelTab3.TextColor = Color.FromHex("#71628A");
         
            BV1.IsVisible = true;
            BV2.IsVisible = false;
            BV3.IsVisible = false;
           
            SL1.IsVisible = true;
            SL2.IsVisible = false;
            SL3.IsVisible = false;
           
        }

        void Tab2_Tapped(object sender, System.EventArgs e)
        {
            LabelTab1.TextColor = Color.FromHex("#71628A");
            LabelTab2.TextColor = Color.FromHex("#FDFDFD");
            LabelTab3.TextColor = Color.FromHex("#71628A");

            BV1.IsVisible = false;
            BV2.IsVisible = true;
            BV3.IsVisible = false;
           
            SL1.IsVisible = false;
            SL2.IsVisible = true;
            SL3.IsVisible = false;
           
        }

        void Tab3_Tapped(object sender, System.EventArgs e)
        {
            LabelTab1.TextColor = Color.FromHex("#71628A");
            LabelTab2.TextColor = Color.FromHex("#71628A");
            LabelTab3.TextColor = Color.FromHex("#FDFDFD");
           
            BV1.IsVisible = false;
            BV2.IsVisible = false;
            BV3.IsVisible = true;
           
            SL1.IsVisible = false;
            SL2.IsVisible = false;
            SL3.IsVisible = true;

        }
        
        async void Fecha_Focused(object sender, Xamarin.Forms.FocusEventArgs e)
		{
            #if __IOS__
            DependencyService.Get<IForceKeyboardDismissalService>().DismissKeyboard();
            #endif

			var result = await UserDialogs.Instance.DatePromptAsync(new DatePromptConfig
            {
                IsCancellable = true,
				CancelText = "CANCELAR",
                Title="Fecha Nacimiento"
            });


            if (result.Ok)
            {
                Fecha.Text = String.Format("{0:dd/MM/yyyy}", result.SelectedDate);
                Fecha.Unfocus();
                DependencyService.Get<IForceKeyboardDismissalService>().DismissKeyboard();
            }
            else
            {
                Fecha.Unfocus();
                DependencyService.Get<IForceKeyboardDismissalService>().DismissKeyboard();
            }
		}

		async void TipoDocumento_Focused(object sender, Xamarin.Forms.FocusEventArgs e)
		{
            #if __IOS__
            DependencyService.Get<IForceKeyboardDismissalService>().DismissKeyboard();
            #endif

            var result = await UserDialogs.Instance.ActionSheetAsync("Numero de socio Win", "CANCELAR", null, null, "DNI", "LE", "LC", "CI");

            if (result != "CANCELAR")
            {
				TipoDocumento.Text = result.ToString();

				TipoDocumento.Unfocus();
                DependencyService.Get<IForceKeyboardDismissalService>().DismissKeyboard();
            }
            else
            {
				TipoDocumento.Unfocus();
                DependencyService.Get<IForceKeyboardDismissalService>().DismissKeyboard();
            }
           
		}
	
        public async Task Camara()
        {
            try
            {
                var permissionStatus = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Camera);

                if (permissionStatus == PermissionStatus.Denied)
                {
                    var results = await CrossPermissions.Current.RequestPermissionsAsync(Permission.Camera);

                    if (results.ContainsKey(Permission.Camera))
                    {
                        if (permissionStatus != PermissionStatus.Granted)
                        {
                            await Mensajes.Alerta("Camara no ascesible");

                            return;
                        }
                    }
                }
               
                await CrossMedia.Current.Initialize();


                if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
                {
                    await Mensajes.Alerta("Camara no ascesible");
                }

                //obtenemos fecha actual
                long n = Int64.Parse(DateTime.Now.ToString("yyyyMMddHHmmss"));

                var file = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
                {
                    SaveToAlbum = true,

                    Directory = "CityCenter",
                    PhotoSize = PhotoSize.Custom,
                    CustomPhotoSize = 18,
                    CompressionQuality = 15,
                    Name = Convert.ToString(n)
                });

                VariablesGlobales.RutaImagene = file.Path;

                imagen1.Source = ImageSource.FromStream(() =>
                {
                    var stream = file.GetStream();
                    file.Dispose();

                    return stream;
                });



            }
            catch (Exception ex)
            {
                //await DisplayAlert("Error", ex.ToString(), "OK");

            }


        }

        async void Handle_Tapped(object sender, System.EventArgs e)
        {
            await Camara();
        }
    
        async void Chat_click(object sender, System.EventArgs e)
        {
            bool isLoggedIn = Application.Current.Properties.ContainsKey("IsLoggedIn") ?
                                    (bool)Application.Current.Properties["IsLoggedIn"] : false;

            if (isLoggedIn)
            {
                
                await ((MasterPage)Application.Current.MainPage).Detail.Navigation.PushAsync(new SeleccionTipoChat());
            }
            else
            {
                await Mensajes.Alerta("Es necesario que te registres para completar esta acción");
            }
        }
    }
}
