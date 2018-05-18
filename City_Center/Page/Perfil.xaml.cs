using System;
using System.Collections.Generic;
using Acr.UserDialogs;
using City_Center.Helper;
using Xamarin.Forms;

namespace City_Center.Page
{
    public partial class Perfil : ContentPage
    {
        
		private string[] ListaOpciones;

        public Perfil()
        {
            InitializeComponent();
        }


        protected override void OnAppearing()
        {
            base.OnAppearing();

            ListaOpciones = new string[] { "DNI", "LE", "LC", "CI" };


         
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
			var result = await UserDialogs.Instance.DatePromptAsync(new DatePromptConfig
            {
                IsCancellable = true,
                MinimumDate = DateTime.Now.AddDays(0),
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
			var result = await UserDialogs.Instance.ActionSheetAsync("Numero de socio Win", "Cancel", null, null, "DNI", "LE", "LC", "CI");

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
