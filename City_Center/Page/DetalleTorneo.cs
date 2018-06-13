using System;
using System.Collections.Generic;
using Acr.UserDialogs;
using City_Center.Clases;
using City_Center.Helper;
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

			
         
        }

        void Handle_Clicked(object sender, System.EventArgs e)
        {
            if (SLR.IsVisible == false)
            {
                SLR.IsVisible = true;
                SLT.IsVisible = false;
				BotonInscripcion.Text = "REGRESAR";
            }
            else
            {
                SLR.IsVisible = false;
                SLT.IsVisible = true;
				BotonInscripcion.Text = "INSCRIBETE";
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
    
        async void Fecha_Focused(object sender, Xamarin.Forms.FocusEventArgs e)
        {
            var result = await UserDialogs.Instance.DatePromptAsync(new DatePromptConfig
            {
                IsCancellable = true,
                CancelText = "CANCELAR",
                Title = "Fecha Nacimiento"
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
