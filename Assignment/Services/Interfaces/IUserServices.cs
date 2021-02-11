using Assignment.Entities;
using Assignment.Entities.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment.Services.Interfaces
{
    public interface IUserServices
    {
        UserDto Authenticate(string username, string password);
        User GetUserById(Guid id);
        string AddUsers(string[] usernames, int[] modules);
        bool IsUserNameExist(string userName);
        User AddUser(string userName, string role);
    }
}
