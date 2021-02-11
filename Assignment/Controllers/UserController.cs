using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Assignment.Models;
using Assignment.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Assignment.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserServices _userServices;

        public UserController(IUserServices userServices)
        {
            _userServices = userServices;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]  
        public IActionResult Authenticate([FromBody] AuthenticateModel model)
        {
            var user = _userServices.Authenticate(model.Username, model.Password);

            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(user);
        }

        [HttpGet("view_modules")]
        public IActionResult GetUserDeatils()
        {
            
            var currentUserId = Guid.Parse(User.Identity.Name);
            var user = _userServices.GetUserById(currentUserId);
            if (user != null)
            {
                return Ok(user.Modules);
            }
            return NotFound();
        }
    }
}
