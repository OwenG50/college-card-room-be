using CollegeCardroomAPI.Managers.Interfaces;
using CollegeCardroomAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CollegeCardroomAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUsersManager usersManager;

        public UsersController(IUsersManager usersManager)
        {
            this.usersManager = usersManager;
        }

        [HttpGet]
        public ActionResult<List<User>> GetUsers()
        {
            var users = usersManager.GetAllUsers();
            return Ok(users);
        }

        [HttpGet]
        [Route("{userId}")]
        public ActionResult<User> GetUser(int userId)
        {
            var user = usersManager.GetUser(userId);
            return Ok(user);
        }

        [HttpPost]
        public IActionResult AddUser([FromBody] User user)
        {
            usersManager.AddUser(user);
            return Ok();
        }

        [HttpDelete("{userId}")]
        public IActionResult RemoveUser(int userId)
        {
            usersManager.RemoveUser(userId);
            return Ok();
        }

        [HttpPut]
        public IActionResult UpdateUser([FromBody] User user)
        {
            usersManager.UpdateUser(user);
            return Ok();
        }
    }
}