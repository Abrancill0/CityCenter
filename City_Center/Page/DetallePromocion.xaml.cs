using System;
using System.Collections.Generic;
using Acr.UserDialogs;
using City_Center.Clases;
using City_Center.Helper;
using Xamarin.Forms;

namespace City_Center.Page
{
    public partial class DetallePromocion : ContentPage
    {
        public DetallePromocion()
        {
            InitializeComponent();
            NavigationPage.SetTitleIcon(this, "logo@2x.png");
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

        async void FechaSolicitada_Tapped(object sender, System.EventArgs e)
        {
            #if __IOS__
                        DependencyService.Get<IForceKeyboardDismissalService>().DismissKeyboard();
            #endif

            var result = await UserDialogs.Instance.DatePromptAsync(new DatePromptConfig
            {
                IsCancellable = true,
                CancelText = "CANCELAR",
            });


            if (result.Ok)
            {
                FechaSolicitada.Text = String.Format("{0:dd/MM/yyyy}", result.SelectedDate);
                FechaSolicitada.Unfocus();
                DependencyService.Get<IForceKeyboardDismissalService>().DismissKeyboard();
            }
            else
            {
                FechaSolicitada.Unfocus();
                DependencyService.Get<IForceKeyboardDismissalService>().DismissKeyboard();
            }
        }

        async void Fecha_Tapped(object sender, System.EventArgs e)
        {
            #if __IOS__
            DependencyService.Get<IForceKeyboardDismissalService>().DismissKeyboard();
            #endif

            var result = await UserDialogs.Instance.DatePromptAsync(new DatePromptConfig
            {
                IsCancellable = true,
                CancelText = "CANCELAR",
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

        async void FechaNacimiento_Tapped(object sender, System.EventArgs e)
        {
            #if __IOS__
            DependencyService.Get<IForceKeyboardDismissalService>().DismissKeyboard();
            #endif

            var result = await UserDialogs.Instance.DatePromptAsync(new DatePromptConfig
            {
                IsCancellable = true,
                CancelText = "CANCELAR",
            });


            if (result.Ok)
            {
                FechaNacimiento.Text = String.Format("{0:dd/MM/yyyy}", result.SelectedDate);
                FechaNacimiento.Unfocus();
                DependencyService.Get<IForceKeyboardDismissalService>().DismissKeyboard();
            }
            else
            {
                FechaNacimiento.Unfocus();
                DependencyService.Get<IForceKeyboardDismissalService>().DismissKeyboard();
            }   
        }
    }
}
