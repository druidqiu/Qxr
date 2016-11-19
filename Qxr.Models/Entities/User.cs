using System.Collections.Generic;
using Qxr.Domain;

namespace Qxr.Models.Entities
{
    public class User : IAggregateRoot //,IValidatableObject
    {
        public User()
        {
            Roles = new List<Role>();
        }

        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        public List<Role> Roles { get; set; }
    }
}
