using System.Collections.Generic;
using System.Linq;
using Qxr.Domain;
using Qxr.Services.Interfaces;
using Qxr.Services.Messaging.UserService;
using Qxr.AutoMapper;
using Qxr.Models.IRepositories;

namespace Qxr.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _uow;
        public UserService(IUserRepository userRepository, IUnitOfWork uow)
        {
            _userRepository = userRepository;
           _uow = uow;
        }

        public void AddUser(AddUserRequest request)
        {
            if (request == null)
            {
                return;
            }
            var user = new Models.Entities.User { Code = request.UserCode, Name = request.UserName };

            _userRepository.Add(user);

            _userRepository.BulkDelete(t => t.Id == 7);

            //_userRepository.Add(new Models.Entities.User());

            _uow.Commit();
        }


        public IEnumerable<ServiceModels.User> GetUsers()
        {
            var users = _userRepository.GetAllInclude(m => m.Roles).ToList();
            return users.MapTo<IEnumerable<ServiceModels.User>>();
        }
    }
}
