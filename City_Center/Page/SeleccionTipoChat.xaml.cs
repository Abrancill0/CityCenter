using System;
using System.Collections.Generic;

using Xamarin.Forms;
using City_Center.Clases;
using City_Center.ViewModels;
using System.Net.Http;
using static City_Center.Models.MensajesPendientesResultado;

namespace City_Center.Page
{
    public partial class SeleccionTipoChat : ContentPage
    {
        public SeleccionTipoChat()
        {
            InitializeComponent();
            NavigationPage.SetTitleIcon(this, "logo@2x.png.png");
        }

        async void ChatCasino(object sender, System.EventArgs e)
        {
            if (string.IsNullOrEmpty(Application.Current.Properties["RutaChatCasino"].ToString()))
            {
                Application.Current.Properties["Casino"] = 1;
            }
            else
            {
                Application.Current.Properties["Casino"] = 0;
            }

            await Application.Current.SavePropertiesAsync();

            MainViewModel.GetInstance().Chat = new ChatviewModel();

            VariablesGlobales.TipoChat = "casino";

            base.OnDisappearing();

            string ValorHotel = Application.Current.Properties["Hotel"].ToString();

            if (ValorHotel == "0")
            {
                await Mensajes.Alerta("Ya se encuentra un chat abierto en hotel");
                return;
            }


#if __ANDROID__
            await ((MasterPage)Application.Current.MainPage).Detail.Navigation.PushAsync(new Chat());
#endif


#if __IOS__
            await ((MasterPage)Application.Current.MainPage).Detail.Navigation.PushAsync(new ChatIos());
#endif

        }

        async void ChatHotel(object sender, System.EventArgs e)
        {

            if (string.IsNullOrEmpty(Application.Current.Properties["RutaChatHotel"].ToString()))
            {
                Application.Current.Properties["Hotel"] = 1;
            }
            else
            {
                Application.Current.Properties["Hotel"] = 0;
            }

            await Application.Current.SavePropertiesAsync();

            string ValorCasino = Application.Current.Properties["Casino"].ToString();

            if (ValorCasino == "0")
            {
                await Mensajes.Alerta("Ya se encuentra un chat abierto en casino");
                return;
            }


            VariablesGlobales.TipoChat = "hotel";
#if __ANDROID__
            await ((MasterPage)Application.Current.MainPage).Detail.Navigation.PushAsync(new Chat());
#endif


#if __IOS__
            await ((MasterPage)Application.Current.MainPage).Detail.Navigation.PushAsync(new ChatIos());
#endif

        }

        protected async override void OnDisappearing()
        {
            base.OnDisappearing();

            if (VariablesGlobales.TipoChat == "casino")
            {

                var content = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("ccn_chat", Application.Current.Properties["VariableChatCasino"].ToString()),
                    new KeyValuePair<string, string>("ccn_email", Application.Current.Properties["Email"].ToString())
                 });


                Restcliente Mensajitos = new Restcliente();

                var response = await Mensajitos.Get<MensajesPendientesReturn>("/chat/marcar_visto_mensaje_web", content);

                if (VariablesGlobales.MensajeVisto == 1)
                {

                    #if __IOS__
                    GlobalResources.Current.ImagenChat = "chat@2x";
#endif

#if __ANDROID__
                    GlobalResources.Current.ImagenChat = "chat";

#endif

                }
            }
            else if (VariablesGlobales.TipoChat == "hotel")
            {


                var content = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("ccn_chat", Application.Current.Properties["VariableChatHotel"].ToString()),
                    new KeyValuePair<string, string>("ccn_email", Application.Current.Properties["Email"].ToString())
                });


                Restcliente Mensajitos = new Restcliente();

                var response = await Mensajitos.Get<MensajesPendientesReturn>("/chat/marcar_visto_mensaje_web", content);
                if (VariablesGlobales.MensajeVisto == 1)
                {
                    #if __IOS__
                    GlobalResources.Current.ImagenChat = "chat@2x";
#endif

#if __ANDROID__
                    GlobalResources.Current.ImagenChat = "chat";

#endif
                   
                  
                }
            }

        }

    }
}
