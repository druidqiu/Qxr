using System.Collections.Generic;
using Qxr.Application.Services;
using Qxr.Services.Messaging.UserService;
using Qxr.Services.ServiceModels;

namespace Qxr.Services.Interfaces
{
    public interface IUserService : IApplicationService
    {
        void AddUser(AddUserRequest request);
        IEnumerable<UserModel> GetUsers();
    }
}
