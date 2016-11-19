using Qxr.Domain;
using Qxr.Models.Entities;

namespace Qxr.Models.IRepositories
{
    public interface IUserRepository : IRepository<User>
    {
        RoleNames GetRoles(int userId);
    }
}
