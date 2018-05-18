using System;
using System.Collections.Generic;
using Acr.UserDialogs;
using City_Center.Clases;
using City_Center.Helper;
using Xamarin.Forms;

namespace City_Center.Page
{
    public partial class DetalleRestaurante : ContentPage
    {
        private string[] ListaOpciones;
        private string[] ListaOpciones2;
        private string[] ListaOpciones3;

        public DetalleRestaurante()
        {
            InitializeComponent();
            NavigationPage.SetTitleIcon(this, "logo.png");
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            ListaOpciones = new string[] { VariablesGlobales.Img1, VariablesGlobales.Img2, VariablesGlobales.Img3, VariablesGlobales.Img4 };
            
			listaDetalleRestaurante.ItemsSource = ListaOpciones;

            
        }

        void Handle_ItemSelected(object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
        {
            try
            {
                var Seleccion = e.SelectedItem;


                if (Seleccion != null)
                {
                    var selection = e.SelectedItem;

                    Img1provisional.Source = selection.ToString();
                }

            }
            catch (Exception)
            {

            }


        }

        void Handle_Tapped(object sender, System.EventArgs e)
        {

            if (SLM.IsVisible == false)
            {
                SLM.IsVisible = true;
                SLR.IsVisible = false;
            }
            else
            {
                SLM.IsVisible = false;
            }


        }

        void Handle_Tapped_1(object sender, System.EventArgs e)
        {
            if (SLR.IsVisible == false)
            {
                SLR.IsVisible = true;
                SLM.IsVisible = false;
            }
            else
            {
                SLR.IsVisible = false;
            }
        }
       
		async void FechaR1_Focused(object sender, Xamarin.Forms.FocusEventArgs e)
        {
            var result = await UserDialogs.Instance.DatePromptAsync(new DatePromptConfig
            {
                IsCancellable = true,
                MinimumDate = DateTime.Now.AddDays(0)
            });


            if (result.Ok)
            {
                FechaR1.Text = String.Format("{0:dd/MM/yyyy}", result.SelectedDate);
                FechaR1.Unfocus();
                DependencyService.Get<IForceKeyboardDismissalService>().DismissKeyboard();
            }
            else
            {
                FechaR1.Unfocus();
                DependencyService.Get<IForceKeyboardDismissalService>().DismissKeyboard();
            }

            //String.Format("{0:dd MMMM yyyy}"

        }

        async void HoraR1_Focused(object sender, Xamarin.Forms.FocusEventArgs e)
        {
            var result = await UserDialogs.Instance.TimePromptAsync(new TimePromptConfig
            {
                IsCancellable = true
            });


            if (result.Ok)
            {
                HoraR1.Text = Convert.ToString(result.SelectedTime);
                HoraR1.Unfocus();
                DependencyService.Get<IForceKeyboardDismissalService>().DismissKeyboard();
            }
            else
            {
                HoraR1.Unfocus();
                DependencyService.Get<IForceKeyboardDismissalService>().DismissKeyboard();
            }

        }
        
        async void SillaNinos_Focused(object sender, Xamarin.Forms.FocusEventArgs e)
        {
            var result = await UserDialogs.Instance.ActionSheetAsync("Sillas niños", "Cancel", null, null, "No", "Si");

            if (result != "Cancel")
            {
                SillaNiño.Text = result.ToString();

                SillaNiño.Unfocus();
                DependencyService.Get<IForceKeyboardDismissalService>().DismissKeyboard();
            }
            else
            {
                SillaNiño.Unfocus();
                DependencyService.Get<IForceKeyboardDismissalService>().DismissKeyboard();
            }

        }
            
		void Handle_Tapped_2(object sender, System.EventArgs e)
        {
            SLR.IsVisible = false;
        }

		void Handle_Tapped_3(object sender, System.EventArgs e)
        {
            SLM.IsVisible = false;
        }
        
    }
}
