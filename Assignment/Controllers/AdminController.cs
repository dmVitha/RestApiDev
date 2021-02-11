using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Assignment.Entities;
using Assignment.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Assignment.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private IUserServices _userServices;
        public AdminController(IUserServices userServices)
        {
            _userServices = userServices;
        }

        [Authorize(Roles = Role.Admin)]
        [HttpPost("instructor_create")]
        public IActionResult AddInstructor([FromBody] string instructorName)
        {
            bool isUserNameExist = _userServices.IsUserNameExist(instructorName);
            if (!isUserNameExist) {
                var user = _userServices.AddUser(instructorName, Role.Instructor);
                return Ok(user.Password);
            }
            return BadRequest("Instructor name is exist !!!");

           
        }
    }
}
