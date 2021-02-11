using Assignment.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment.Entities
{
    public class User
    {
        public User()
        {
        }

        public User(string userName,string password,string role)
        {
            Id = Guid.NewGuid();
            Username = userName;
            Password = password;
            UserRole = role;
            Modules = Enum.GetNames(typeof(Module)).ToList();
        }

       
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string UserRole { get; set; }
        public string Token { get; set; }

        public List<string> Modules { get; set; }
    }
}
