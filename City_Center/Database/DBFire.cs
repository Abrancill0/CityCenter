using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Firebase.Xamarin.Database;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using City_Center.Models;
using System;
using Org.BouncyCastle.Crypto.Tls;

namespace City_Center.Database
{
    public class DBFire
    {
        FirebaseClient fbClient;
        Room _rm = new Room();

        public DBFire()
        {
            fbClient = new FirebaseClient("https://chatcitycenter.firebaseio.com/");
            SelectRoom();
        }

        public async Task<List<Room>> getRoomList()
        {
            return (await fbClient
                    .Child("chat_casino")
                .OnceAsync<Room>())
                .Select((item) =>
                new Room
                {
                    Key = item.Key,
                    Email = item.Object.Email
                }).ToList();
        }

        async void SelectRoom()
        {
            try
            {
                var items = await fbClient.Child("chat_casino").OnceAsync<Room>();

                foreach (var item in items)
                {
                    if (item.Object.Email.Equals(Application.Current.Properties["Email"].ToString()))
                    {
                        _rm.Email = item.Object.Email;
                        _rm.Key = item.Key;
                    }
                }

                MessagingCenter.Send<DBFire, Room>(this, "RoomProp", _rm);
            }
            catch (Exception ex)
            {
                
            }
           
        }

        public async Task saveRoom(Room rm)
        {
            await fbClient.Child("chat_casino")
                    .PostAsync(rm);
        }

        public async Task saveMessage(Chat _ch, string _room)
        {
            await fbClient.Child("chat_casino/" + _room + "/Message").PostAsync(_ch);
        }

        public ObservableCollection<Chat> subChat(string roomKEY)
        {
            return fbClient.Child("chat_casino/" + roomKEY + "/Message")
                           .AsObservable<Chat>()
                           .AsObservableCollection<Chat>();
        }
    }
}
