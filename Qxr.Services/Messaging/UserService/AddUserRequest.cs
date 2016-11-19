using Qxr.Application.Services.Dto;

namespace Qxr.Services.Messaging.UserService
{
    public class AddUserRequest : IRequestDto
    {
        public string UserCode { get; set; }
        public string UserName { get; set; }
    }
}
