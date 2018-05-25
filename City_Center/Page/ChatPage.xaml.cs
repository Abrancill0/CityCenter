namespace City_Center.Page
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using City_Center.Database;
    using City_Center.Models;
    using Xamarin.Forms;

        public partial class ChatPage : ContentPage
        {
            DBFire dB = new DBFire();
            Room rm = new Room();

        public ChatPage()
            {
                InitializeComponent();

                NavigationPage.SetTitleIcon(this, "logo.png");

                MessagingCenter.Subscribe<DBFire, Room>(this, "RoomProp", (page, data) =>
                {
                    rm = data;
                    _listChat.BindingContext = dB.subChat(data.Key);
                    MessagingCenter.Unsubscribe<DBFire, Room>(this, "RoomProp");

                });
            }

        async void Handle_Clicked(object sender, System.EventArgs e)
            {
            var chatOBJ = new Chat { UserMessage = _etMessage.Text, UserName = Application.Current.Properties["NombreCompleto"].ToString(), UserFecha =Convert.ToString(DateTime.Now) };
                _etMessage.Text = "";
                await dB.saveMessage(chatOBJ, rm.Key);

            //_listChat.ScrollTo(dB.subChat(rm.Key).Last(), ScrollToPosition.End,true);
            }

        void Handle_ItemAppearing(object sender, Xamarin.Forms.ItemVisibilityEventArgs e)
        {
            _listChat.IsRefreshing = false;
        }

        }
    }
