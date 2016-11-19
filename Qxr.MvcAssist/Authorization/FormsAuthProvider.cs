using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;

namespace Qxr.MvcAssist.Authorization
{
    public class FormsAuthProvider: IAuthProvider
    {
        public bool Authenticate(string username, string password)
        {
            bool result = FormsAuthentication.Authenticate(username, password);
            if (result)
            {
                FormsAuthentication.SetAuthCookie(username, false);
            }
            return result;
        }


        public bool Authenticate(string username, Func<bool> action)
        {
            bool result = action();
            if (result)
            {
                FormsAuthentication.SetAuthCookie(username, false);
            }
            return result;
        }

        public void SignOut()
        {
            FormsAuthentication.SignOut();
        }

        public string LoginUrl
        {
            get { return FormsAuthentication.LoginUrl; }
        }
    }
}
