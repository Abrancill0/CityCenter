using System;
using System.Collections.Generic;
using Acr.UserDialogs;
using City_Center.Helper;
using Xamarin.Forms;
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

			ListTipoEvento = new string[] { "Casamiento", "Congresos, Convenciones, Seminarios", "Eventos Corporativos", "Eventos Sociales", "Otros" };

			TipoEvento.ItemsSource = ListTipoEvento;

			TipoEvento.SelectedIndex = 0;

			TipoEvento1.ItemsSource = ListTipoEvento;

			TipoEvento1.SelectedIndex = 0;
		}


		protected override void OnDisappearing()
		{
			base.OnDisappearing();

			GC.Collect();

		}

		//TipoEvento

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
			if (Frame1.IsVisible == false)
			{
				Stack1.IsVisible = false;
				Frame1.IsVisible = true;
				BtnContacto1.Image = "ContactoUp.png";

			}
			else
			{
				BtnContacto1.Image = "btn_contacto.png";
				Stack1.IsVisible = true;
				Frame1.IsVisible = false;
			}

		}

		void Handle_Clicked2(object sender, System.EventArgs e)
		{
			if (Frame2.IsVisible == false)
			{
				Stack2.IsVisible = false;
				Frame2.IsVisible = true;

			}
			else
			{

				Stack2.IsVisible = true;
				Frame2.IsVisible = false;
			}

		}

		async void Handle_Focused(object sender, Xamarin.Forms.FocusEventArgs e)
		{
			var result = await UserDialogs.Instance.DatePromptAsync(new DatePromptConfig
        {
            IsCancellable = true,
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

		void Handle_Focused_1(object sender, Xamarin.Forms.FocusEventArgs e)
		{
			throw new NotImplementedException();
		}
	}
}