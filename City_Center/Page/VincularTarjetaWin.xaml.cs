using System;
using System.Collections.Generic;
using Acr.UserDialogs;
using City_Center.Helper;
using Xamarin.Forms;

namespace City_Center.Page
{
    public partial class VincularTarjetaWin : ContentPage
    {
        public VincularTarjetaWin()
        {
            InitializeComponent();
            NavigationPage.SetTitleIcon(this, "logo.png");
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
    }
}
