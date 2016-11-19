using System.Collections.Generic;
using System;
using Qxr.Domain;

namespace Qxr.Models.Entities
{
    [Flags]
    public enum RoleNames
    {
        None = 0,
        Admin = 1,
        BuTester = 2,
        Tester = 4
    }

    public class Role : IAggregateRoot
    {
        public Role()
        {
            Users = new List<User>();
        }

        public int Id { get; set; }
        public RoleNames RoleName { get; set; }
        public string Name { get; set; }

        public List<User> Users { get; set; }
    }
}
