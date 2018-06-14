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
      

        public DetalleRestaurante()
        {
            InitializeComponent();
            NavigationPage.SetTitleIcon(this, "logo.png");

            FechaR1.Text=String.Format("{0:dd/MM/yyyy}", DateTime.Today);
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
            #if __IOS__
            DependencyService.Get<IForceKeyboardDismissalService>().DismissKeyboard();
            #endif
            
            if (VariablesGlobales.HorarioPIU == true)
            {
                var result = await UserDialogs.Instance.ActionSheetAsync("Horario", "CANCELAR", null, null, "12:30","20:30","21:00","23:00");
             
                if (result != "CANCELAR")
                {
                    HoraR1.Text = result.ToString();

                    HoraR1.Unfocus();
                    DependencyService.Get<IForceKeyboardDismissalService>().DismissKeyboard();
                }
                else
                {
                    HoraR1.Text = "12:30";
                    HoraR1.Unfocus();
                    DependencyService.Get<IForceKeyboardDismissalService>().DismissKeyboard();
                }  
            }
            else if (VariablesGlobales.HorarioLEGULA == true )
            {
                var result = await UserDialogs.Instance.ActionSheetAsync("Horario", "CANCELAR", null, null, "21:00","23:00");
             
                if (result != "CANCELAR")
                {
                    HoraR1.Text = result.ToString();

                    HoraR1.Unfocus();
                    DependencyService.Get<IForceKeyboardDismissalService>().DismissKeyboard();
                }
                else
                {
                    HoraR1.Text = "21:00";
                    HoraR1.Unfocus();
                    DependencyService.Get<IForceKeyboardDismissalService>().DismissKeyboard();
                }   
            }
            else
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

        }
        
        async void SillaNinos_Focused(object sender, Xamarin.Forms.FocusEventArgs e)
        {
            #if __IOS__
            DependencyService.Get<IForceKeyboardDismissalService>().DismissKeyboard();
            #endif

            var result = await UserDialogs.Instance.ActionSheetAsync("Sillas niños", "CANCELAR", null, null, "No", "Si");

            if (result != "CANCELAR")
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
