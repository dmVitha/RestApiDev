using Assignment.Entities;
using Assignment.Entities.DataTransferObjects;
using Assignment.Enums;
using Assignment.Helper;
using Assignment.Services.Interfaces;
using AutoMapper;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.Services
{
    public class UserService : IUserServices
    {
        private readonly IMapper _mapper;
        private readonly AppSettings _appSettings;

        private List<User> _users = new List<User>
        {
            new User ("admin","1234",Role.Admin)
        };

        public UserService(IMapper mapper, IOptions<AppSettings> appSettings)
        {
            _mapper = mapper;
            _appSettings = appSettings.Value;
        }

        public UserDto Authenticate(string username, string password)
        {
            var user = _users.SingleOrDefault(x => x.Username == username && x.Password == password);

            // return null if user not found
            if (user == null)
                return null;

            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                    new Claim(ClaimTypes.Role, user.UserRole)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);

            return _mapper.Map<UserDto>(user);
        }

        public User AddUser(string userName,string role)
        {
            string generatePassword = CreateRandomPassword(8);
            User newUser = new User(userName, generatePassword, role);
            _users.Add(newUser);
            return newUser;
        }

        public string AddUsers(string[] usernames, int[] modules)
        {
            
            string generatePassword = CreateRandomPassword(8);
            foreach(string name in usernames)
            {
                User user = new User();
                user.Id = Guid.NewGuid();
                user.UserRole = Role.Student;
                user.Password = generatePassword;
                user.Username = name;
                user.Modules = this.ModuleNames(modules);
                _users.Add(user);
          
            }
            return generatePassword;
        }

        public List<string> ModuleNames(int[] modules)
        {
            List<string> moduleNames = new List<string>();
            foreach(int moduleCode in modules)
            {
                moduleNames.Add(Enum.GetName(typeof(Module), moduleCode));
            }

            return moduleNames;
        }
        public User GetUserById(Guid id)
        {
            var user = _users.FirstOrDefault(x => x.Id == id);
            if (user == null)
            {
                return null;
            }
            return user;
        }

        public bool IsUserNameExist(string userName)
        {
            var user = _users.FirstOrDefault(x => x.Username == userName);
            if (user == null)
            {
                return false;
            }
            return true;
        }
        public static string CreateRandomPassword(int length)
        {
            string validChars = "ABCDEFGHJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*?_-";
            Random random = new Random();
            char[] chars = new char[length];
            for (int i = 0; i < length; i++)
            {
                chars[i] = validChars[random.Next(0, validChars.Length)];
            }
            return new string(chars);
        }
    }
}
