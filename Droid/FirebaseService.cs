using Android.App;
using Android.Util;
using Firebase.Iid;
using Firebase.Messaging;
using Plugin.FirebasePushNotification;
using City_Center.Clases;
using System.Net.Http;
using System.Collections.Generic;
using City_Center.Models;
using Firebase;
using Plugin.DeviceInfo;

namespace City_Center.Droid
{
    [Service]
    [IntentFilter(new[] { "com.google.firebase.INSTANCE_ID_EVENT" })]
    public class FirebaseService : FirebaseInstanceIdService
    {
        const string TAG = "MyFirebaseIIDService";

        public override void OnTokenRefresh()
        {
         
                var refreshedToken = FirebaseInstanceId.Instance.Token;
         
                Log.Debug(TAG, "Refreshed token: " + refreshedToken);
           
                SendRegistrationToServer(refreshedToken);

        }

       async void SendRegistrationToServer(string token)
        {
            try
            {
                Restcliente Cliente = new Restcliente();

                var content = new FormUrlEncodedContent(new[]
                {
                new KeyValuePair<string, string>("neq_equipo", token),
                new KeyValuePair<string, string>("neq_id_usuario", "0"),
                new KeyValuePair<string, string>("neq_dispositivo", CrossDeviceInfo.Current.Platform.ToString()),
                new KeyValuePair<string, string>("neq_app_id", CrossDeviceInfo.Current.Id)
            });


                var LoginReturn = await Cliente.Get<GuardadoGenerico>("/notificaciones/guardar_equipo", content);

                if (LoginReturn != null)
                {
                    //await Mensajes.success("OK");
                }
                // Mensajes.Alerta(token);
                // Mensajes.success(token);
            }
            catch (System.Exception)
            {

                throw;
            }
            
        }
    }
}
