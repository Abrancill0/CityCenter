using System;
using System.Collections.Generic;

using Xamarin.Forms;
using City_Center.Clases;

namespace City_Center.Page
{
    public partial class SeleccionTipoChat : ContentPage
    {
        public SeleccionTipoChat()
        {
            InitializeComponent();
        }

      async  void ChatCasino(object sender, System.EventArgs e)
        {
            VariablesGlobales.TipoChat = "casino";
            await((MasterPage)Application.Current.MainPage).Detail.Navigation.PushAsync(new Chat());
  
        }

      async  void ChatHotel(object sender, System.EventArgs e)
        {
            VariablesGlobales.TipoChat = "hotel";
            await((MasterPage)Application.Current.MainPage).Detail.Navigation.PushAsync(new Chat());
 
        }
    }
}
