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
    public class InstructorController : ControllerBase
    {
        private IUserServices _userServices;

        public InstructorController(IUserServices userServices)
        {
            _userServices = userServices;
        }

        [Authorize(Roles = Role.Instructor)]
        [HttpPost("class_create")]
        public IActionResult AddClass([FromBody] Class newClass)
        {

            var randomPassword = _userServices.AddUsers(newClass.StudentNames,newClass.Modules);

            if (randomPassword == null)
                return BadRequest(new { message = "Cannot Add Class Right Now !!" });

            return Ok(randomPassword);
        }
    }
}
