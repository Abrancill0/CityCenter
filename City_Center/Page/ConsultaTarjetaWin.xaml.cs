using System;
using System.Collections.Generic;
using Acr.UserDialogs;
using City_Center.Helper;
using System.Globalization;
using System.Text.RegularExpressions;

using Xamarin.Forms;

namespace City_Center.Page
{
    public partial class ConsultaTarjetaWin : ContentPage
    {
             
        public ConsultaTarjetaWin()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
   
			TarjetaPrueba.Source = "DummyCard";

        }

	    async void Handle_Focused(object sender, Xamarin.Forms.FocusEventArgs e)
		{
			var result = await UserDialogs.Instance.ActionSheetAsync("Tipo Documento", "Cancel", null, null, "DNI", "LE", "LC", "CI");

            if (result != "Cancel")
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
