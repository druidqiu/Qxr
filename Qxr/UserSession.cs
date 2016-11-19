using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qxr
{
    [Serializable]
    public class UserSession
    {
        public UserSession(string username, string role)
        {
            Username = username;
            Role = role;
        }

        public string Username { get; private set; }
        public string Role { get; private set; }
    }
}
