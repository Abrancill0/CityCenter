using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Plugin.Messaging;

namespace City_Center.Page
{
    public partial class DetalleShow : ContentPage
    {
        public DetalleShow()
        {
            InitializeComponent();
            NavigationPage.SetTitleIcon(this, "logo.png");
        }

        void Handle_Clicked(object sender, System.EventArgs e)
        {
            var phoneCallTask = CrossMessaging.Current.PhoneDialer;

            if (phoneCallTask.CanMakePhoneCall)
            {
                phoneCallTask.MakePhoneCall("4441118438","Nuemero anterior");
                //4446574294
            }
        }
    }
}
