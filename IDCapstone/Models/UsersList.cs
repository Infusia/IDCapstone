using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IDCapstone.Models
{
    public class UsersList
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public IEnumerable<ApplicationUser> Roles { get; set; }
    }
}
