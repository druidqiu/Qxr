using Qxr.Models.Entities;
using System.Linq;
using Qxr.Models.IRepositories;
using Qxr.EntityFramework;

namespace Qxr.Tests.Repositories.Repositories
{
    public class UserRepository : QxrTestRepositoryBase<User>, IUserRepository
    {
        //TODO:每个类都有构造函数就太丑了
        public UserRepository(IDbContextProvider<QxrTestDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

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
