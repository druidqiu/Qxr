using System.Collections.Generic;
using Qxr.Application.Services;
using Qxr.Services.Messaging.UserService;

namespace Qxr.Services.Interfaces
{
    public interface IUserService : IApplicationService
    {
        void AddUser(AddUserRequest request);
        IEnumerable<ServiceModels.User> GetUsers();
    }
}
