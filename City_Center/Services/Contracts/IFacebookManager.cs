using City_Center.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace City_Center.Services.Contracts
{
    public interface IFacebookManager
    {
        void Login(Action<FacebookUser, string> onLoginComplete);

        void Logout();
    }
}
