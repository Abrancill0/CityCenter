using System;
using System.Collections.Generic;

using Xamarin.Forms;
using City_Center.Clases;
using System.Threading.Tasks;
using System.Net.Http;
using static City_Center.Models.MensajesPendientesResultado;

namespace City_Center.Page
{
    public partial class Chat : ContentPage
    {
        public Chat()
        {
            InitializeComponent();

            NavigationPage.SetTitleIcon(this, "logo_hdpi.png");
            //if (VariablesGlobales.TipoChat == "casino")
            //{
            //    WebViewChat1.Source = "http://wpage.citycenter-rosario.com.ar/chat/terminar_chat_app/" + Application.Current.Properties["VariableChatCasino"].ToString();
            //    //Application.Current.Properties["VariableChatCasino"] = "";
            //}
            //else if (VariablesGlobales.TipoChat == "hotel")
            //{
            //    WebViewChat1.Source = "http://wpage.citycenter-rosario.com.ar/chat/terminar_chat_app/" + Application.Current.Properties["VariableChatHotel"].ToString();
            //    //Application.Current.Properties["VariableChatHotel"] = "";
            //}

            //Application.Current.SavePropertiesAsync();

            Task.Delay(800);

            string Nombre = Application.Current.Properties["NombreCompleto"].ToString();
            string Email = Application.Current.Properties["Email"].ToString();
            string TipoChat = VariablesGlobales.TipoChat;
            string n = Convert.ToString(Int64.Parse(DateTime.Now.ToString("yyyyMMddHHmmss")));
            string VariableChat = "chat_" + TipoChat + "_" + n;

            if (VariablesGlobales.TipoChat == "casino")
            {

                if (Application.Current.Properties["Casino"].ToString() == "1")
                {

                    Application.Current.Properties["VariableChatCasino"] = VariableChat;

                    WebViewChat.Source = "http://wpage.citycenter-rosario.com.ar/chat_app/" + TipoChat + "/" + VariableChat + "/" + Nombre + "/" + Email + "/" + Application.Current.Properties["Casino"].ToString();

                    Application.Current.Properties["RutaChatCasino"] = "http://wpage.citycenter-rosario.com.ar/chat_app/" + TipoChat + "/" + VariableChat + "/" + Nombre + "/" + Email + "/" + "0";

                    Application.Current.Properties["Casino"] = "0";
                }
                else
                {
                    WebViewChat.Source = Application.Current.Properties["RutaChatCasino"].ToString();
                }


            }
            else if (VariablesGlobales.TipoChat == "hotel")
            {

                if (Application.Current.Properties["Hotel"].ToString() == "1")
                {

                    Application.Current.Properties["VariableChatHotel"] = VariableChat;

                    WebViewChat.Source = "http://wpage.citycenter-rosario.com.ar/chat_app/" + TipoChat + "/" + VariableChat + "/" + Nombre + "/" + Email + "/" + Application.Current.Properties["Hotel"].ToString();

                    Application.Current.Properties["RutaChatHotel"] = "http://wpage.citycenter-rosario.com.ar/chat_app/" + TipoChat + "/" + VariableChat + "/" + Nombre + "/" + Email + "/" + "0";


                    Application.Current.Properties["Hotel"] = "0";
                }
                else
                {
                    WebViewChat.Source = Application.Current.Properties["RutaChatHotel"].ToString();
                }


            }

            Application.Current.SavePropertiesAsync();
        }

        void Handle_Clicked(object sender, System.EventArgs e)
        {
            if (VariablesGlobales.TipoChat == "casino")
            {
                WebViewChat1.Source = "http://wpage.citycenter-rosario.com.ar/chat/terminar_chat_app/" + Application.Current.Properties["VariableChatCasino"].ToString();

                Application.Current.Properties["RutaChatCasino"] = "";
                Application.Current.Properties["VariableChatCasino"] = "";
                Application.Current.Properties["Casino"] = 1;

            }
            else if (VariablesGlobales.TipoChat == "hotel")
            {
                WebViewChat1.Source = "http://wpage.citycenter-rosario.com.ar/chat/terminar_chat_app/" + Application.Current.Properties["VariableChatHotel"].ToString();


                Application.Current.Properties["VariableChatHotel"] = "";
                Application.Current.Properties["RutaChatHotel"] = "";

                Application.Current.Properties["Hotel"] = 1;

            }

            Application.Current.SavePropertiesAsync();

            Task.Delay(1000);

            Navigation.PopAsync();

        }

        void Handle_Clicked_1(object sender, System.EventArgs e)
        {
            if (VariablesGlobales.TipoChat == "casino")
            {

                WebViewChat1.Source = Application.Current.Properties["RutaChatCasino"].ToString() + "/" + Mensajito.Text;

                Mensajito.Text = string.Empty;

            }
            else if (VariablesGlobales.TipoChat == "hotel")
            {


                WebViewChat1.Source = Application.Current.Properties["RutaChatHotel"].ToString() + "/" + Mensajito.Text;

                Mensajito.Text = string.Empty;
            }
        }

        protected async override void OnDisappearing()
        {
            base.OnDisappearing();

            if (VariablesGlobales.TipoChat == "casino" == true)
            {

                var content = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("ccn_chat", Application.Current.Properties["VariableChatCasino"].ToString()),
                    new KeyValuePair<string, string>("ccn_email", Application.Current.Properties["Email"].ToString())
                 });


                Restcliente Mensajitos = new Restcliente();

                var response = await Mensajitos.Get<MensajesPendientesReturn>("/chat/marcar_visto_mensaje_web", content);

                if (response != null)
                {
                    GlobalResources.Current.ImagenChat = "chat";

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

                var response = await Mensajitos.GetReal<MensajesPendientesReturn>("/chat/marcar_visto_mensaje_web/", content);
                if (response != null)
                {
                    
                        GlobalResources.Current.ImagenChat = "chat";
                }
            }

        }
    }
}
