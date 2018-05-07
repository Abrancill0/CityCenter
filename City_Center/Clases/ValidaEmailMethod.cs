using System;
using System.Text.RegularExpressions;

namespace City_Center.Clases
{
    public class ValidaEmailMethod
    {
        public static bool ValidateEMail(string email)
        {
            if (email == null || email == "") return false;

            Regex oRegExp = new Regex(@"^[A-Za-z0-9_.\-]+@[A-Za-z0-9_\-]+\.([A-Za-z0-9_\-]+\.)*[A-Za-z][A-Za-z]+$", RegexOptions.IgnoreCase);
            return oRegExp.Match(email).Success;
        }
    }
}
