using System;
using System.Collections.Generic;
using Acr.UserDialogs;
using City_Center.Clases;
using City_Center.Helper;
using Xamarin.Forms;

namespace City_Center.Page
{
    public partial class DetalleTorneo2 : ContentPage
    {
        public DetalleTorneo2()
        {
            InitializeComponent();
            NavigationPage.SetTitleIcon(this, "logo@2x.png");
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
