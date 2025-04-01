using CollegeCardroomAPI.Managers.Interfaces;
using CollegeCardroomAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CollegeCardroomAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserManager userManager;

        public UserController(IUserManager userManager)
        {
            this.userManager = userManager;
        }

        [HttpGet]
        public ActionResult<List<User>> GetUsers()
        {
            var users = userManager.GetAllUsers();
            return Ok(users);
        }

        [HttpGet]
        [Route("{userId}")]
        public ActionResult<User> GetUser(int userId)
        {
            var user = userManager.GetUser(userId);
            return Ok(user);
        }

        [HttpPost]
        public IActionResult AddUser([FromBody] User user)
        {
            userManager.AddUser(user);
            return Ok();
        }

        [HttpDelete("{userId}")]
        public IActionResult RemoveUser(int userId)
        {
            userManager.RemoveUser(userId);
            return Ok();
        }
    }
}