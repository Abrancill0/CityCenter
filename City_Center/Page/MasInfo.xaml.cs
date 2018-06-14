using System;
using System.Collections.Generic;
using Acr.UserDialogs;
using City_Center.Helper;
using Xamarin.Forms;
using City_Center.Clases;
using static City_Center.Models.SalasEventosResultado;

namespace City_Center.Page
{
	public partial class MasInfo : ContentPage
	{
		private string[] ListTipoEvento;

		public MasInfo()
		{
			InitializeComponent();
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();

            ListTipoEvento = new string[] { "Congresos, Convenciones, Seminarios","Casamiento", "Eventos Corporativos", "Eventos Sociales", "Otros" };

			//TipoEvento.ItemsSource = ListTipoEvento;

			//TipoEvento.SelectedIndex = 0;
   
		}
        
		protected override void OnDisappearing()
		{
			base.OnDisappearing();

			GC.Collect();

		}
       
		void Tab1_Tapped(object sender, System.EventArgs e)
		{
			LabelTab1.TextColor = Color.FromHex("#E7E9F6");
			LabelTab2.TextColor = Color.FromHex("#8E99D3");
			LabelTab3.TextColor = Color.FromHex("#8E99D3");

			BV1.IsVisible = true;
			BV2.IsVisible = false;
			BV3.IsVisible = false;

			SL1.IsVisible = true;
			SL2.IsVisible = false;
			SL3.IsVisible = false;
		}

		void Tab2_Tapped(object sender, System.EventArgs e)
		{
			LabelTab1.TextColor = Color.FromHex("#8E99D3");
			LabelTab2.TextColor = Color.FromHex("#E7E9F6");
			LabelTab3.TextColor = Color.FromHex("#8E99D3");

			BV1.IsVisible = false;
			BV2.IsVisible = true;
			BV3.IsVisible = false;

			SL1.IsVisible = false;
			SL2.IsVisible = true;
			SL3.IsVisible = false;
		}

		void Tab3_Tapped(object sender, System.EventArgs e)
		{
			LabelTab1.TextColor = Color.FromHex("#8E99D3");
			LabelTab2.TextColor = Color.FromHex("#8E99D3");
			LabelTab3.TextColor = Color.FromHex("#E7E9F6");

			BV1.IsVisible = false;
			BV2.IsVisible = false;
			BV3.IsVisible = true;

			SL1.IsVisible = false;
			SL2.IsVisible = false;
			SL3.IsVisible = true;

		}

		void Handle_ItemSelected(object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
		{
			try
			{
				var Seleccion = e.SelectedItem;

				if (Seleccion != null)
				{
					var selection = e.SelectedItem as SalasEventosDetalle;

					Img1provisional.Source = selection.gal_imagen;
					Img1provisional2.Source = selection.gal_imagen;

				}
			}
			catch (Exception ex)
			{

			}


		}

		void Handle_Clicked(object sender, System.EventArgs e)
		{
			if (Stack1.IsVisible == true)
			{
				Stack1.IsVisible = false;
				SLContacto1.IsVisible = true;
				BtnContacto1.Image = "ContactoUp.png";

			}
			else
			{
				BtnContacto1.Image = "btn_contacto.png";
				Stack1.IsVisible = true;
				SLContacto1.IsVisible = false;
			}

		}

		void Handle_Clicked2(object sender, System.EventArgs e)
		{
			if (Stack2.IsVisible == true)
			{
				Stack2.IsVisible = false;
				SLContacto2.IsVisible = true;
                BtnContacto2.Image = "ContactoUp.png";
			}
			else
			{

				Stack2.IsVisible = true;
				SLContacto2.IsVisible = false;
                BtnContacto2.Image = "btn_contacto.png";
			}

		}

		async void Handle_Focused(object sender, Xamarin.Forms.FocusEventArgs e)
		{
            #if __IOS__
            DependencyService.Get<IForceKeyboardDismissalService>().DismissKeyboard();
#endif

			var result = await UserDialogs.Instance.DatePromptAsync(new DatePromptConfig
			{
				IsCancellable = true,
				CancelText = "CANCELAR",
				MinimumDate = DateTime.Now.AddDays(0)
			});


			if (result.Ok)
			{
				Fecha1.Text = String.Format("{0:dd/MM/yyyy}", result.SelectedDate);
				Fecha1.Unfocus();
				DependencyService.Get<IForceKeyboardDismissalService>().DismissKeyboard();

			}
			else
			{
				Fecha1.Unfocus();
				DependencyService.Get<IForceKeyboardDismissalService>().DismissKeyboard();
			}

		}

		async void Handle_Focused_1(object sender, Xamarin.Forms.FocusEventArgs e)
		{
            #if __IOS__
            DependencyService.Get<IForceKeyboardDismissalService>().DismissKeyboard();
#endif

			var result = await UserDialogs.Instance.DatePromptAsync(new DatePromptConfig
			{
				IsCancellable = true,
				CancelText = "CANCELAR",
				MinimumDate = DateTime.Now.AddDays(0)
			});


			if (result.Ok)
			{
				Fecha2.Text = String.Format("{0:dd/MM/yyyy}", result.SelectedDate);
				Fecha2.Unfocus();
				DependencyService.Get<IForceKeyboardDismissalService>().DismissKeyboard();

			}
			else
			{
				Fecha2.Unfocus();
				DependencyService.Get<IForceKeyboardDismissalService>().DismissKeyboard();
			}
		}
  
		async void TipoEvento_Focused(object sender, Xamarin.Forms.FocusEventArgs e)
        {
            #if __IOS__
            DependencyService.Get<IForceKeyboardDismissalService>().DismissKeyboard();
#endif

            var result = await UserDialogs.Instance.ActionSheetAsync("Convenciones y salas", "CANCELAR", null, null, "Congresos, Convenciones, Seminarios","Casamiento",  "Eventos Corporativos", "Eventos Sociales", "Otros");

            if (result != "CANCELAR")
            {
				TipoEvento.Text = result.ToString();

				TipoEvento.Unfocus();
                DependencyService.Get<IForceKeyboardDismissalService>().DismissKeyboard();
            }
            else
            {
				TipoEvento.Unfocus();
                DependencyService.Get<IForceKeyboardDismissalService>().DismissKeyboard();
            }

        }
        
		async void TipoEvento1_Focused(object sender, Xamarin.Forms.FocusEventArgs e)
        {
            #if __IOS__
            DependencyService.Get<IForceKeyboardDismissalService>().DismissKeyboard();
#endif

            var result = await UserDialogs.Instance.ActionSheetAsync("Convenciones y salas", "CANCELAR", null, null,"Congresos, Convenciones, Seminarios", "Casamiento",  "Eventos Corporativos", "Eventos Sociales", "Otros");

            if (result != "CANCELAR")
            {
                TipoEvento1.Text = result.ToString();

                TipoEvento1.Unfocus();
                DependencyService.Get<IForceKeyboardDismissalService>().DismissKeyboard();
            }
            else
            {
                TipoEvento1.Unfocus();
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