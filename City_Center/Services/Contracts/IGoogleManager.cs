using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using City_Center.Models;

namespace City_Center.Services.Contracts
{
	public interface IGoogleManager
	{
        void Login(Action<GoogleUser, string> OnLoginComplete);

		void Logout();
	}
}
