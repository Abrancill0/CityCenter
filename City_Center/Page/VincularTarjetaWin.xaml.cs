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
            var result = await UserDialogs.Instance.ActionSheetAsync("Numero de socio Win", "Cancelar", null, null, "DNI", "LE", "LC", "CI");

            if (result != "Cancelar")
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
