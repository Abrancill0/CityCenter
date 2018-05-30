using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Windows.Input;
using City_Center.Services;
using GalaSoft.MvvmLight.Command;
using City_Center.Models;
using Xamarin.Forms;
using City_Center.Clases;

namespace City_Center.ViewModels
{
    public class ChatviewModel:BaseViewModel
    {
		#region Services
        private ApiService apiService;
        #endregion



        #region Commands


      

        public ICommand TerminarChatCommand
        {
            get
            {
                return new RelayCommand(TerminarChat);
            }
        }

        private async void TerminarChat()
        {
            try
            {
                var content = new FormUrlEncodedContent(new[]
                {
                new KeyValuePair<string, string>("", ""),
                });

                string ruta="";

            if (VariablesGlobales.TipoChat == "casino")
                {
                    ruta= Application.Current.Properties["VariableChatCasino"].ToString();

                    Application.Current.Properties["RutaChatCasino"] = "";
                    Application.Current.Properties["VariableChatCasino"] = "";
                    Application.Current.Properties["Casino"] = 1;

                }
                else if (VariablesGlobales.TipoChat == "hotel")
                {
                    ruta = Application.Current.Properties["VariableChatHotel"].ToString();


                    Application.Current.Properties["VariableChatHotel"] = "";
                    Application.Current.Properties["RutaChatHotel"] = "";

                    Application.Current.Properties["Hotel"] = 1;

                }

                Application.Current.SavePropertiesAsync();

                var response = await this.apiService.GetReal<GuardadoGenerico>("/chat/terminar_chat_app/",ruta, content);

                if (!response.IsSuccess)
                {

                    //await Mensajes.Error("Error al cargar Tarjetas");

                    return;
                }  
            }
            catch (Exception)
            {

            }
        }


        #endregion

      
        #region Contructors
        public ChatviewModel()
        {
            this.apiService = new ApiService();
           
           
        }
        #endregion 
    }
}
