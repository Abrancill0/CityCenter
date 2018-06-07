using Android.App;
using Android.Util;
using Firebase.Iid;
using Firebase.Messaging;
using Plugin.FirebasePushNotification;
using City_Center.Clases;

namespace City_Center.Droid
{
    [Service]
    [IntentFilter(new[] { "com.google.firebase.INSTANCE_ID_EVENT" })]
    public class FirebaseService : FirebaseInstanceIdService
    {
        const string TAG = "MyFirebaseIIDService";

        public override void OnTokenRefresh()
        {
            try
            {

              

                //if (string.IsNullOrEmpty(App.Current.Properties["Token"].ToString()))
                //{
                    var refreshedToken = FirebaseInstanceId.Instance.Token;
                    Log.Debug(TAG, "Refreshed token: " + refreshedToken);
                    //App.Current.Properties["Token"] = refreshedToken;
                    SendRegistrationToServer(refreshedToken);

                   //App.Current.SavePropertiesAsync();


               
                //}
                //else
                //{
                //    var refreshedToken = FirebaseInstanceId.Instance.Token;
                //    Log.Debug(TAG, "Refreshed token: " + refreshedToken);
                //    App.Current.Properties["Token"] = refreshedToken;
                //    SendRegistrationToServer(refreshedToken);

                //    App.Current.SavePropertiesAsync();
                //}
            }
            catch (System.Exception ex)
            {
                //var refreshedToken = FirebaseInstanceId.Instance.Token;
                //Log.Debug(TAG, "Refreshed token: " + refreshedToken);
                //App.Current.Properties["Token"] = refreshedToken;
                //SendRegistrationToServer(refreshedToken);

                //App.Current.SavePropertiesAsync();
            }

        }

        void SendRegistrationToServer(string token)
        {
           // Mensajes.Alerta(token);
           // Mensajes.success(token);
        }
    }
}
