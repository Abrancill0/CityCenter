using System;
using System.Collections.Generic;

using Xamarin.Forms;
using City_Center.Clases;

namespace City_Center.Page
{
    public partial class Chat : ContentPage
    {
        public Chat()
        {
            InitializeComponent();

            string Nombre = Application.Current.Properties["NombreCompleto"].ToString();
            string Email = Application.Current.Properties["Email"].ToString();
            string TipoChat = VariablesGlobales.TipoChat;
            string n = Convert.ToString(Int64.Parse(DateTime.Now.ToString("yyyyMMddHHmmss")));
            string VariableChat = "chat_"+TipoChat+"_"+n;

            WebViewChat.Source = "http://192.168.100.3:83/chat_app/"+TipoChat+"/" + VariableChat + "/" + Nombre + "/" + Email;
        }
    }
}
