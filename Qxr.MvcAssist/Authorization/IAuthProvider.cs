using System;

namespace Qxr.MvcAssist.Authorization
{
    public interface IAuthProvider
    {
        bool Authenticate(string username, string password);
        bool Authenticate(string username, Func<bool> action);
        void SignOut();
        string LoginUrl { get; }
    }
}
