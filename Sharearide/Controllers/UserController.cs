using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using sharearideApplication.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace sharearideApplication.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        /// <summary>
        /// Get all users ordered by firstname
        /// </summary>
        /// <returns>array of users</returns>
        [HttpGet]
        public IEnumerable<User> GetAllUsers()
        {
            return _userRepository.GetAll();
        }

        /// <summary>
        /// Get user with given email
        /// </summary>
        /// <param name="email">the email of the user</param>
        /// <returns>a single object of user</returns>
        [HttpGet("{email}")]
        public ActionResult<User> GetUser(string email)
        {
            var user = _userRepository.GetByEmail(email);
            if (user == null)
                return NotFound();
            return user;
        }

        /// <summary>
        /// Adds a new user to the database
        /// </summary>
        /// <param name="user">the new user</param>
        [HttpPost]
        public ActionResult<User> AddUser(User user)
        {
            if (user == null)
                return NotFound();

            _userRepository.Add(user);
            _userRepository.SaveChanges();
            return CreatedAtAction(nameof(GetUser), new { email = user.Email }, user);
        }
    }
}