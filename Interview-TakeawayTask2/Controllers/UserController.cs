using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using InterviewTakeawayTask2.Models;
using InterviewTakeawayTask2.Repositories;

namespace InterviewTakeawayTask2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserRepository _userRepository;

        public UserController(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        // GET: Gets All Users
        [HttpGet]
        public ActionResult<IEnumerable<User>> GetAllUsers()
        {
            var users = _userRepository.GetAllUsers();
            return Ok(users);
        }

        // GET: Gets Specific Users
        [HttpGet("{id}")]
        public ActionResult<User> GetUserById(int id)
        {
            var user = _userRepository.GetUserById(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        // POST: Creates a User
        [HttpPost]
        public ActionResult<User> CreateUser([FromBody] UserRequest user)
        {
            if (user == null || string.IsNullOrEmpty(user.Name) || string.IsNullOrEmpty(user.Email))
            {
                return BadRequest("Invalid user data.");
            }

            var newUser = _userRepository.CreateUser(user.Name, user.Password, user.Email, user.Age);
            return CreatedAtAction(nameof(GetUserById), new { id = newUser.Id }, newUser);
        }

        // POST: Updates a User
        [HttpPost]
        public ActionResult<User> UpdateUser([FromBody] User user)
        {
            var existingUser = _userRepository.GetUserById(user.Id);

            if (existingUser == null)
            {
                return BadRequest("User not found.");
            }

            if (user == null || string.IsNullOrEmpty(user.Password) || string.IsNullOrEmpty(user.Username))
            {
                return BadRequest("Invalid user data.");
            }

            var updatedUser = _userRepository.UpdateUser(user);
            return CreatedAtAction(nameof(GetUserById), new { id = updatedUser.Id }, updatedUser);
        }
    }
}
