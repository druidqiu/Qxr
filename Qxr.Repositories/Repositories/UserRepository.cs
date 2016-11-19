using Qxr.Models.Entities;
using System.Linq;
using Qxr.Models.IRepositories;

namespace Qxr.Repositories.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public RoleNames GetRoles(int userId)
        {
            var user = GetById(userId);
            if (user == null)
            {
                return RoleNames.None;
            }
            var roles = user.Roles.Sum(m => (int)m.RoleName);
            return (RoleNames) roles;
        }
    }
}
